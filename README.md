Amplitude for Xamarin
=====================

A cross-platform  [Xamarin](https://www.xamarin.com) SDK for tracking events to [Amplitude](https://amplitude.com).


Installation
------------

Install Uniforms.Amplitude as component from Xamarin Components store:

https://components.xamarin.com/view/uniforms-amplitude

**Trial version doesn't have any limitations at the moment. But please consider purchasing to support further development of component and other libraries!**



Basic usage
-----------

1. Initialize platform specific classes:

    For **iOS** application in `UIApplicationDelegate.FinishedLaunching()`:

    ```csharp
    Uniforms.Amplitude.iOS.Amplitude.Register();
    ```

    For **Android** application in `OnCreate()` of main activity:

    ```csharp
    Uniforms.Amplitude.Droid.Amplitude.Register(this);
    ```

2. Initialize Amplitude with API key:

    ```csharp
    Amplitude.Instance.Initialize("--- Amplitude key here ---");
    ```

3. Send events to Amplitude:

    ```csharp
    Amplitude.Instance.LogEvent("Start");
    ```


API reference
-------------

Cross-platform interface is available via ``Uniforms.Amplitude`` static class:

```csharp
namespace Uniforms.Amplitude
{
    public static class Amplitude
    {
        /// <summary>
        /// Register class implementing IAmplitude interface for specific platform.
        /// </summary>
        public static void Register(Type implementationClass);

        /// <summary>
        /// Get default Amplitude instance.
        /// </summary>
        public static IAmplitude Instance { get; }

        /// <summary>
        /// Get Amplitude instance with identifier.
        /// </summary>
        public static IAmplitude GetInstanceWithName(string name);
    }
}
```

And here's the `IAmplitude` quick reference:


```csharp
    public interface IAmplitude
    {
        /// <summary>
        /// Gets ot sets the userId.
        /// </summary>
        /// <description>
        /// If your app has its own login system that you want to track users with,
        /// you can set the userId.
        /// </description>
        string UserId { get; set; }

        /// <summary>
        /// Gets or sets the deviceId.
        /// </summary>
        /// <description>
        /// If your app has its own system for tracking devices, you can set the deviceId.
        /// </description>
        string DeviceId { get; set; }

        /// <summary>
        /// Gets or sets tracking opt out.
        /// </summary>
        /// <description>
        /// If the user wants to opt out of all tracking, use this method to enable opt
        /// out for them. Once opt out is enabled, no events will be saved locally or
        /// sent to the server. Calling this method again with enabled set to NO will
        /// turn tracking back on for the user.
        /// </description>
        bool OptOut { get; set; }

        /// <summary>
        /// Disables sending logged events to Amplitude servers.
        /// </summary>
        bool Offline { set; }

        /// <summary>
        /// Initializes the Amplitude with your Amplitude api key and optional user ID.
        /// </summary>
        void Initialize(string apiKey, string userId = null);

        /// <summary>
        /// Tracks an event.
        /// </summary>
        /// <description>
        /// Events are saved locally. Uploads are batched to occur every 30 events
        /// and every 30 seconds, as well as on app close.
        /// After calling logEvent in your app, you will immediately see data appear
        /// on the Amplitude Website.
        /// It's important to think about what types of events you care about as a developer.
        /// You should aim to track between 50 and 200 types of events within your app.
        /// Common event types are different screens within the app, actions the user
        /// initiates (such as pressing a button), and events you want the user to complete
        /// (such as filling out a form, completing a level, or making a payment).
        /// </description>
        void LogEvent(string eventType, object properties = null, bool outOfSession = false);

        /// <summary>
        /// Tracks revenue.
        /// </summary>
        /// <description>
        /// To track revenue from a user, call LogRevenue() each time the user generates revenue.
        /// Method takes in a double with the dollar amount of the sale as the only argument.
        /// </description>
        void LogRevenue(double amount);

        /// <summary>
        /// Tracks revenue with product identifier and optional transaction receipt.
        /// </summary>
        void LogRevenue(string productIdentifier, int quantity, double price, byte[] receipt = null);

        /// <summary>
        /// Adds or replaces properties that are tracked on the user level.
        /// </summary>
        void SetUserProperties(object userProperties, bool replace = false);

        /// <summary>
        /// Clears all properties that are tracked on the user level.
        /// </summary>
        void ClearUserProperties();

        /// <summary>
        /// Manually forces the class to immediately upload all queued events.
        /// </summary>
        void UploadEvents();

        /// <summary>
        /// Enables location tracking.
        /// </summary>
        /// <description>
        /// If the user has granted your app location permissions, the SDK
        /// will also grab the location of the user. Amplitude will never prompt
        /// the user for location permissions itself, this must be done by your app.
        /// </description>
        void EnableLocationListening();

        /// <summary>
        /// Disables location tracking.
        /// </summary>
        /// <description>
        /// If you want location tracking disabled on startup of the app, call
        /// DisableLocationListening() before you call InitializeApiKey().
        /// </description>
        void DisableLocationListening();

        /// <summary>
        /// Forces the SDK to update with the user's last known location if possible.
        /// </summary>
        /// <description>
        /// If you want to manually force the SDK to update with the user's last known
        /// location, call updateLocation.
        /// </description>
        void UpdateLocation();

        /// <summary>
        /// Uses advertisingIdentifier instead of identifierForVendor as the device ID.
        /// </summary>
        /// <description>
        /// Apple prohibits the use of advertisingIdentifier if your app does not have
        /// advertising. Useful for tying together data from advertising campaigns to
        /// anlaytics data. Must be called before InitializeApiKey() is called.
        /// </description>
        void UseAdvertisingIdForDeviceId();

        /// <summary>
        /// Debugging method to find out how many events are being stored locally on the device.
        /// </summary>
        void PrintEventsCount();
    }
```


Troubleshooting
---------------

### System.NullReferenceException

Having such exception on `Amplitude.Instance.Initialize()` means that implementation class was not registered. Make user you call `Register()` static method before trying to get Amplitude instance.

License
-------

The **Uniforms.Amplitude** project is licensed under **Apache License Version 2.0**. See the LICENSE file for details.