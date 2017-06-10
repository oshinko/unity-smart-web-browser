# Unity Smart Web Browser
Platform standard web browser launch for Unity.

## Downloads
Download SmartWebBrowser.unitypackage from link below:

- [Releases](https://github.com/maesin/unity-smart-web-browser/releases)

## Example

### Android
Use [Chrome Custom Tabs](https://developer.chrome.com/multidevice/android/customtabs).

![Android](https://github.com/maesin/unity-smart-web-browsing/raw/master/Example-Android.gif)

### iOS
Use [SFSafariViewController](https://developer.apple.com/reference/safariservices/sfsafariviewcontroller).

![iOS](https://github.com/maesin/unity-smart-web-browsing/raw/master/Example-iOS.gif)

## Usage
```csharp
using SmartWebBrowser;
using UnityEngine;

public class OpenInBrowser : MonoBehaviour
{
    public void Open()
    {
        WebBrowser.Open(url: "https://httpbin.org/headers", token: "mine");
    }
}
```
