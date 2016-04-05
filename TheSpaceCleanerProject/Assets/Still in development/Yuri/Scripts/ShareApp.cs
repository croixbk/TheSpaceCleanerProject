using UnityEngine;
using System.Collections;

public class ShareApp : MonoBehaviour
{

    string subject = "SpaceCleaner";
    string body = "SDDS ARTE";

    public void callShareApp()
    {
        AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
        currentActivity.Call("shareText", subject, body);
    }
}
