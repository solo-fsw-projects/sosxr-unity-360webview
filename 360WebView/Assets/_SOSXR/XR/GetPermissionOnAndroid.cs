using SOSXR.EnhancedLogger;
using UnityEngine;
using UnityEngine.Android;


public class GetPermissionOnAndroid : MonoBehaviour
{
    private void Awake()
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
            GetExternalReadPermission();
            GetExternalWritePermission();
        #endif
    }


    private void GetExternalReadPermission()
    {
        GetPermission(Permission.ExternalStorageRead);
    }


    private static void GetExternalWritePermission()
    {
        GetPermission(Permission.ExternalStorageWrite);
    }


    private static void GetPermission(string permission)
    {
        if (Permission.HasUserAuthorizedPermission(permission))
        {
            Log.Success("GetPermissionOnAndroid", "Permission is already granted.");

            return;
        }

        Log.Info("GetPermissionOnAndroid", "Requesting permission to", permission);

        Permission.RequestUserPermission(permission);
    }
}


// AndroidManifest.xml
/*
<?xml version="1.0" encoding="utf-8"?>
   <manifest xmlns:android="http://schemas.android.com/apk/res/android"
             package="com.SOSXR.Video360"
             xmlns:tools="http://schemas.android.com/tools">

       <application>
           <activity android:name="com.unity3d.player.UnityPlayerActivity">
               <intent-filter>
                   <action android:name="android.intent.action.MAIN" />
                   <category android:name="android.intent.category.LAUNCHER" />
               </intent-filter>
               <meta-data android:name="unityplayer.UnityActivity" android:value="true" />
           </activity>
       </application>

       <uses-permission android:name="android.permission.READ_EXTERNAL_STORAGE" />
       <uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE" />

   </manifest>
*/