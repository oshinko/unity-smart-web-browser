using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace SmartWebBrowser
{
    public class WebBrowser
    {
        #if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        static extern void _open(string url);
        #endif

        #if UNITY_ANDROID && !UNITY_EDITOR
        static void OpenAndroid(string url, params string[] headers)
        {
            var extraHeaders = new AndroidJavaObject("android.os.Bundle");

            if (headers.Length % 2 == 0)
            {
                for (var i = 0; i < headers.Length; i++)
                {
                    extraHeaders.Call("putString", headers[i], headers[++i]);
                }
            }

            var customTabsIntent = new AndroidJavaObject("android.support.customtabs.CustomTabsIntent$Builder").
                Call<AndroidJavaObject>("setShowTitle", true).
                Call<AndroidJavaObject>("addDefaultShareMenuItem").
                Call<AndroidJavaObject>("build");

            var intent = customTabsIntent.Get<AndroidJavaObject>("intent");
            var extraHeadersKey = new AndroidJavaClass("android.provider.Browser").GetStatic<string>("EXTRA_HEADERS");
            intent.Call<AndroidJavaObject>("putExtra", extraHeadersKey, extraHeaders);

            var unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer"); 
            var context = unity.GetStatic<AndroidJavaObject>("currentActivity");

            var uri = new AndroidJavaClass("android.net.Uri").CallStatic<AndroidJavaObject>("parse", url);

            context.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                customTabsIntent.Call("launchUrl", context, uri);
            }));
        }
        #endif

        public static void Open(string url)
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
            OpenAndroid(url);
            #elif UNITY_IOS && !UNITY_EDITOR
            _open(url);
            #else
            Application.OpenURL(url);
            #endif
        }

        public static void Open(string url, string token)
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
            OpenAndroid(url, "Authorization", "Bearer " + token);
            #else
            var noQuery = string.IsNullOrEmpty(new UriBuilder(url).Query);
            Open(url + (noQuery ? "?" : "&") + "auth=" + token);
            #endif
        }
    }
}
