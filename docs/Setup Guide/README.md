# Unity SDK Setup

### The Reteno Unity SDK for Mobile Customer Engagement and Analytics solutions

## Overview

`Reteno` is a lightweight SDK for Unity that helps mobile teams integrate Reteno into their Unity apps. The server-side library makes it easy to call the `Reteno API`.

##### The SDK supports:

- Unity 2021.2 or later
- C# 9 or higher

## Getting started with Reteno SDK / Setup guide

### Step 1: Add the Reteno SDK to your project
In your Unity project go to `Assets` -> `Import Package` -> `Custom Package...` and select the [`RetenoSdk.unitypackage` file](../RetenoSDK.unitypackage).

### Step 2: Set up a Firebase Cloud Messaging client app with Unity
- Create a Firebase project in the [Firebase console](https://console.firebase.google.com/).
- Add your Unity app to your Firebase project in the [Firebase console](https://console.firebase.google.com/).
- Add Firebase configuration files to your Unity app.
  - For iOS — GoogleService-Info.plist.
  - For Android — google-services.json.
- Download SDK from [FirebaseUnity](https://firebase.google.com/download/unity) and import it to your project.
- Install firebase messaging package.
    - `FirebaseMessaging.unitypackage`.
    - `FirebaseAuth.unitypackage`.
- On iOS 
  - Podfile should contain `pod 'Firebase/Core'`, `pod 'Firebase/Messaging'` and `pod 'Firebase/Auth'`. Version are not required.
    ```
    ***
    target ‘UnityFramework’ do
       pod ‘Firebase/Core’
       pod ‘Firebase/Messaging’
       pod ‘Firebase/Auth’
    end
    ***
    ```
    Open Terminal at your project folder. If you have an M1 chip type "sudo arch -x86_64 gem install ffi" then "arch -x86_64 pod install" Other wise for Intel "pod install".
  - Disable bitcode on `Your iOS Unity project` and `Pods` -> `Build Settings` -> `Enable Bitcode` -> `No`.

More info on [Firebase documentation](https://firebase.google.com/docs/cloud-messaging/unity/client).

### Step 3: Set up Mobile Notification package
- Go to `Window` -> `Package Manager` -> `Reteno` -> `Mobile Notification` -> `Install`. More info on [Package Manager documentation](https://docs.unity3d.com/Packages/com.unity.package-manager-ui@latest/index.html)
- Add reference to you *.asmdef file. More info on [Assembly Definition Files documentation](https://docs.unity3d.com/Manual/ScriptCompilationAssemblyDefinitionFiles.html)
  - For iOS — `Unity.Notifications.iOS`
  - For Android — `Unity.Notifications.Android`

##### Licence

Reteno Unity SDK is released under the MIT license.