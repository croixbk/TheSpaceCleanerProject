package com.Arysi.SpaceCleaner;

import  com.unity3d.player.UnityPlayerActivity;
import  com.unity3d.player.UnityPlayer;

public class Bridge extends UnityPlayerActivity {

    private static int myInt = 2;

    static public int getMyInt(){
        return myInt;
    }

    static public void callbackToUnityMethod(int num, String gameObject, String methodName){
        myInt = num;
        UnityPlayer.UnitySendMessage(gameObject, methodName, "READY");
    }

}
