using UnityEngine;


namespace SOSXR.XR
{
    #if UNITY_ANDROID
    using static UnityEngine.Android.Permission;
    using UnityEngine.Android;
    #endif


    public class GetPermissionOnAndroid : MonoBehaviour
    {
        private void Awake()
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
            GetExternalReadPermission();
            GetExternalWritePermission();
            
            if (GetAndroidAPILevel() >= 30) // For Android 11+
            {
                // RequestManageExternalStoragePermission(); // This will open up a window to manage all files access permission.
            }
            
            #else
            Debug.Log("GetPermissionOnAndroid: This script is only for Android platform.");
            #endif
        }


        #if UNITY_ANDROID && !UNITY_EDITOR
        private void GetExternalReadPermission() => GetPermission(ExternalStorageRead);
        private static void GetExternalWritePermission() => GetPermission(ExternalStorageWrite);
        
        private static void GetPermission(string permission)
        {
            if (Permission.HasUserAuthorizedPermission(permission))
            {
                Debug.Log($"GetPermissionOnAndroid: Permission {permission} is already granted.");
                return;
            }

            Debug.Log($"GetPermissionOnAndroid: Requesting permission for {permission}");
            Permission.RequestUserPermission(permission);
        }

        private int GetAndroidAPILevel()
        {
            using (var version = new AndroidJavaClass("android.os.Build$VERSION"))
            {
                return version.GetStatic<int>("SDK_INT");
            }
        }

        private void RequestManageExternalStoragePermission()
        {
            using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            using (var currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                var uri = new AndroidJavaClass("android.net.Uri")
                    .CallStatic<AndroidJavaObject>("parse", "package:" + currentActivity.Call<string>("getPackageName"));

                var intent = new AndroidJavaObject("android.content.Intent", "android.settings.MANAGE_APP_ALL_FILES_ACCESS_PERMISSION", uri);
                currentActivity.Call("startActivity", intent);
            }
        }
        #endif
    }
}


// AndroidManifest.xml
/*
<?xml version="1.0" encoding="utf-8"?>
   <manifest xmlns:android="http://schemas.android.com/apk/res/android"
             package="com.SOSXR.Video360"
             xmlns:tools="http://schemas.android.com/tools">

       <application
           android:requestLegacyExternalStorage="true">
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
       <uses-permission android:name="android.permission.MANAGE_EXTERNAL_STORAGE" tools:ignore="ScopedStorage" />

   </manifest>
*/