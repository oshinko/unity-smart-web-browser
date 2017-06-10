using SmartWebBrowser;
using UnityEngine;

public class OpenInBrowser : MonoBehaviour
{
    public void Open()
    {
        WebBrowser.Open(url: "https://httpbin.org/headers", token: "mine");
    }
}
