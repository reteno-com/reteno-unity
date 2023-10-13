# Unity SDK Setup with Apple Push Notification Service

### The Reteno Unity SDK for Mobile Customer Engagement and Analytics solutions

## Overview

`Reteno` is a lightweight SDK for Unity that helps mobile teams integrate Reteno into their Unity apps. The server-side library makes it easy to call the `Reteno API`.

##### The SDK supports:

- Unity 2021.2 or later
- C# 9 or higher

## Getting started with Reteno SDK / Setup guide

### Step 1: Add the Reteno SDK to your project
In your Unity project go to `Assets` -> `Import Package` -> `Custom Package...` and select the [`RetenoSdk.unitypackage` file](../RetenoSDK.unitypackage).

### Step 2: Install the Push Notifications SDK
- Open your `Project Settings` from `Edit` -> `Project Settings`
- Enable pre-release packages from `Package Manager` -> `Advanced Settings` -> `Enable Pre-release Packages`.
- Open the package manager from `Component` -> `Package Manager`
- Under the Packages dropdown, select `Unity Registry`
- Search for `Push Notifications` in the search bar in the top right to view the `Push Notifications` package and `click Install`
- More info on [Package Manager documentation](https://docs.unity3d.com/Packages/com.unity.package-manager-ui@latest/index.html)

### Step 3: Register for Push Notifications
To receive Push Notifications, your app needs to register for Push Notifications. Follow Registering for [Push Notifications](https://docs.unity.com/ugs/en-us/manual/push-notifications/manual/PushNotificationsSDK#Registering_for_Push_Notifications) for details.

### Step 4: Upload your service keys
You need to add an Apple key, project, and account details to your Unity project settings before sending notifications to Apple devices. You can reuse the same Apple key between games, environments, and development and production builds in the Unity Dashboard (go to step 8 if you already have one). This needs to be done for every UGS environment of the game that you expect to test or use notifications with.
- Log in to your [Apple Developer](https://developer.apple.com/) console
- Go to the Certificates, Identifiers & Profiles page and select Keys
- Select + to create a new key
- Name your key and enable the “Apple Push Notifications service (APNs)” option to enable notifications, then select Continue. Note, you can only enable this capability on two keys per account
- Select Register on the next page to confirm
- Download the generated key and make note of the Key ID provided as you’ll need it later. You can only download the key file once; it needs to be revoked and regenerated if lost.
- Go to `Player Engagement` -> `Notifications` -> `Settings` for your project in the Unity Dashboard, click the Set Up Keys link in the Setup banner at the top, then select Add Key or the Edit Icon in the "Apple Key" row
- There are five fields that need to be populated:
  * `Key`: Upload the key created in the previous steps
  * `Key ID`: This is provided when you registered the key and can also be retrieved by selecting the key in the Apple Developer console.
  * `Team ID`: This is your team ID displayed under your account name on the Apple Developer console.
  * `Topic ID`: Populate with the bundle identifier for your game. It needs to exactly match the one in “Identifiers” in the “Certificates, Identifiers & Profiles” page in your Apple Developer console.
  * `Sandbox`: If you want to message players from a development/debug build of your game, set the sandbox value to True. If you want to message users on a production build, set to False.
- Select Finish. For security reasons this file won’t be visible if you re-enter the edit settings page.
- When building the application within XCODE, ensure that the app is given the “Remote Notification” capabilities so it can receive notifications.

### Step 4: Set up Mobile Notification package
- Go to `Window` -> `Package Manager` -> `Mobile Notification` -> `Install`. More info on [Package Manager documentation](https://docs.unity3d.com/Packages/com.unity.package-manager-ui@latest/index.html)
- Add reference to you *.asmdef file. More info on [Assembly Definition Files documentation](https://docs.unity3d.com/Manual/ScriptCompilationAssemblyDefinitionFiles.html)
    - For iOS — `Unity.Notifications.iOS`
    - For Android — `Unity.Notifications.Android`

##### Licence
Reteno Unity SDK is released under the MIT license.