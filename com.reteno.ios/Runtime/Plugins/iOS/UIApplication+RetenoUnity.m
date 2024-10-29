#import "UIApplication+RetenoUnity.h"
#import <objc/runtime.h>

static Class retenoGetClassWithProtocolInHierarchy(Class searchClass, Protocol *protocolToFind) {
    if (!class_conformsToProtocol(searchClass, protocolToFind)) {
        if ([searchClass superclass] == [NSObject class])
            return nil;
        Class foundClass = retenoGetClassWithProtocolInHierarchy([searchClass superclass], protocolToFind);
        if (foundClass)
            return foundClass;
        return searchClass;
    }
    return searchClass;
}

BOOL retenoInjectSelector(Class newClass, SEL newSel, Class addToClass, SEL makeLikeSel) {
    Method newMeth = class_getInstanceMethod(newClass, newSel);
    IMP imp = method_getImplementation(newMeth);

    const char* methodTypeEncoding = method_getTypeEncoding(newMeth);
    BOOL existing = class_getInstanceMethod(addToClass, makeLikeSel) != NULL;

    if (existing) {
        class_addMethod(addToClass, newSel, imp, methodTypeEncoding);
        newMeth = class_getInstanceMethod(addToClass, newSel);
        Method orgMeth = class_getInstanceMethod(addToClass, makeLikeSel);
        method_exchangeImplementations(orgMeth, newMeth);
    }
    else {
        class_addMethod(addToClass, makeLikeSel, imp, methodTypeEncoding);
    }

    return existing;
}

static bool swizzled = false;

@implementation UIApplication (RetenoUnity)

+ (void)load {
    method_exchangeImplementations(
        class_getInstanceMethod(self, @selector(setDelegate:)),
        class_getInstanceMethod(self, @selector(retenoUnityDelegate:))
    );
}

- (void)retenoUnityDelegate:(id <UIApplicationDelegate>)delegate {
    if (swizzled) {
        [self retenoUnityDelegate:delegate];
        return;
    }

    Class delegateClass = retenoGetClassWithProtocolInHierarchy([delegate class], @protocol(UIApplicationDelegate));

    retenoInjectSelector(
        self.class, @selector(retenoApplication:didFinishLaunchingWithOptions:),
        delegateClass, @selector(application:didFinishLaunchingWithOptions:)
    );

    swizzled = true;

    [self retenoUnityDelegate:delegate];
}

- (BOOL)retenoApplication:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions {
    
    
   
    [InitializationManager delayStartReteno];

    if ([self respondsToSelector:@selector(retenoApplication:didFinishLaunchingWithOptions:)])
        return [self retenoApplication:application didFinishLaunchingWithOptions:launchOptions];

    return YES;
}

@end
