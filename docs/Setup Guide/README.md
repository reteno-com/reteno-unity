## RetenoSDK for Unity
### Overview
RetenoSDK provides functionalities for Unity developers to manage mobile notifications, track user interactions, and communicate seamlessly with an API across both Android and iOS platforms.

### Getting Started
Before diving into the specific methods and functionalities, ensure the SDK is properly integrated into your Unity project. Follow the installation instructions provided with the SDK package.

#### RetenoSDK's main components include:

- RetenoSdk: SDK's main entry point.
- AndroidRetenoNotificationsPlatform & IOSRetenoNotificationsPlatform: Handle Android and iOS notifications.
- ApiContract: Contains API links.
- DictionaryHelper: Stores dictionaries.
- RetenoData: Serializes messages and user data.
- RetenoHttpClient: Manages API communication.
- RetenoJsonDeserializer & RetenoJsonSerializer: JSON utilities.

### Initialization
- RetenoSdk.Initialize(string accessKey, string externalUserId)

Initialize the SDK by setting the access key and external user ID. The SDK determines the platform (Android or iOS) and sets up the appropriate notification handling.

Usage:
```csharp
RetenoSdk.Initialize("your_access_key", "external_user_id");
```

### Setting User Attributes
- RetenoSdk.SetUserAttributes(User user)

Define attributes for a recognized user. The method first checks for the presence of an FCM token or an external user ID, then it updates the user data through the HTTP client.

Usage:
```csharp
User user = new User();
// Set user attributes here...
RetenoSdk.SetUserAttributes(user);
```
- #### RetenoSdk.SetAnonymousUserAttributes(User user)
Set attributes for an anonymous user, similar to the recognized user but without some identification attributes.

Usage:
```csharp
User anonymousUser = new User();
// Set user attributes here...
RetenoSdk.SetAnonymousUserAttributes(anonymousUser);
```
### Sending Custom Events
- RetenoSdk.SendCustomEvent(List<CustomEvent> customEvents)

Communicate a list of custom events to the server-side API. The device's ID and external user ID are bundled with the events before transmission.

Usage:
```csharp
List<CustomEvent> events = new List<CustomEvent>();
// Add events to the list...
RetenoSdk.SendCustomEvent(events);
```

### Processing Incoming Messages
- RetenoSdk.ProcessMessage(IDictionary<string, string> data)

Handles the incoming messages, extracting image links and other relevant data from them. Following this, it sends a notification to the user and updates the notification status as 'DELIVERED' on the server.

Usage:
```csharp
IDictionary<string, string> messageData = new Dictionary<string, string>();
// Populate the message data...
RetenoSdk.ProcessMessage(messageData);
```

### Handling Token Receipt
- RetenoSdk.SetToken(string token)

Used to assign the Firebase Cloud Messaging (FCM) token when received. Following this, the device is set up with this token.

Usage:
```csharp
RetenoSdk.SetToken("your_fcm_token");
```

### Events
#### Notification Click
- RetenoSdk.OnNotificationClicked()

Triggered when a user interacts with a notification. It retrieves the last notification's details, updates its status as 'CLICKED', and optionally, redirects the user using an associated link.

Usage:
```csharp
RetenoSdk.OnNotificationClicked();
```

### Tips
- Initialization is Key: Always initialize the SDK before invoking any other methods.
- Error Handling: It's crucial to handle potential errors based on API responses to ensure smooth user experiences.
- Stay Updated: Regularly check for SDK updates to incorporate new features or fixes.

### Setup Guides
- [Unity SDK Setup with Firebase Cloud Messaging](UnitySdkSetupWithFirebaseCloudMessaging.md)
- [Unity SDK Setup with Apple Push Notification service](UnitySdkSetupWithApplePushNotificationService.md)