# Quest Template

# 2025-02-03

- [ ] Fixing the PPN being passed on in the URL



# 09-12-2024
- [x] Updating to 6.0.30
- [x] Updating packages
- [x] Modulo on full participantnumber instead of only last digit
- [ ] Match table to desk (height 80, edge of desk on top of lower edge of 'goot')


# 25-10-2024
Testing


# 24-10-2024
During my holiday there were some unforseen complications. For some reason the video didn't play due to it setting itself as an AR app. 
- AR Camera Manager turned off
- Camera Environment Background Type Skybox instead of Solid Color. 
This seems an easy fix, but it was quite some time before I figured the solution. I still don't know the reason how this got so. On my testing at home there was none of this before my holiday. I will need to test this some more to make sure it's stable.



# 02-10-2024
- Moved the keyboard to spawn in front of person, instead of in the middle of the room.

**Update Notification:**
- **New Version:** A new version has been uploaded to Arbor.
- **Check the Glasses:** Ensure your VR glasses have the latest version of the program, which was uploaded tonight. The program should be automatically sent to your glasses.

---

**Steps:**
1. **Delete Old Config:** Manually delete the `config.json` currently on your glasses.
2. **Restart Program:** After restarting the updated program, it will generate a new default `config.json`.

---

**Explanation of Config Fields:**

- **ClipDirectory:** The system will, by default, use the `Arbor/video` directory, which you can modify later if needed.

**New Fields:**
- **PlayWay:** Defines the number of clips to be shown (1, all, or repeat).
- **Order:** Determines the ordering of the videos (e.g., sequential, counterbalanced, etc.).

Enums:
- **PlayWay Values:**
    - One = 0
    - All = 1
    - Repeat = 2
- **Order Values:**
    - Counterbalanced = 3 (and similar for other options).

To show all clips for each participant, for example:
```
"PlayWay": 1
```

---

**Debug Mode:**
- **ShowDebug:** If enabled (`true`), a text box will appear with system info and some debug options (buttons for controlling the 3D environment).

---

**Web Link Construction:**
The URL gets appended with parameters such as:
```
?TaskName=TaskToDo&VideoName=Video_01.mp4&PPN=1111
```

You can ignore the `TaskName` and `VideoName` for now, but the `PPN` (participant number) is significant and could potentially be passed to OSWeb.



## 01-10-2024
I added the Modulus function in a rough version now. The rough part is mostly that it's all embedded in the videoplayermanager, where it really don't belong. Also, I want to be able to check the complete list of possible permutations into a stored list, for instance the config. Then I want to use the modulus to pull from that list the desired order of videos. I want this because I'm not fully trusting the permutation-checker to give us the exact same output each and every time. If indeed that's not too trust-worthy, the entire exercise would be futile. So I'm wanting to store the complete list of options, then pull from there the correct order. So this means that the re-permutating of the full list should be done somehow at the start of the full study.... 

- [ ] Make it so that after pulling the videos from the folder, that it first orders them alphabetically or whatever. This so they always start in the same order. Only then should it go through the sequence of shuffling/modulizing/whatever
- [ ] Pull the entire permutation-madness out of the videoplayermanager, this has nothing to do with that. 

## 27-09-2024

Cleaned up scene.
Cleaned up README.

Build didn't run, stuck at three dots.
Build 1:
Enabled the Meta Quest feature group, no mas

Build two:
Disabled Optimize Buffer Discards.
Disabled Vulkan
Disabled MultiThreaded rendering
Enabled Prebake Collision Meshes
Disabled Strip Engine Code
Enabled Optimize Mesh Data
Enabled Texture Mipmap Stripping

The build after this still didn't open past the dots, but after removing the thing from the HMD and rebuilding it did work?

Working on getting the modulus involved.
I'm not quite sure on how to approach this, will have to ask Elaine for help. 
I get what it does for 2 conditions, but not for 3.
And also need to implement the seeing of the initial lab environment.

## 24-09-2024
- [ ] Graag ook trigger buttons hebben als input. Links en rechts

- [ ] Debug reshuffle video graag veranderen naar een knop die de video opnieuw start, en niet de browser opnieuw doet laden.

Iedere participant ziet eerst een labomgeving (360?), en dan een break van x minuten, om vervolgens counterbalanced de Urban / Nature omgeving te zien, daarna weer een break, en dan de andere conditie.

Randomization op basis van Modulus. Ppn 1 heeft Urban eerst, Ppn 2 heeft Nature nature, Ppn 3 heeft Urban eerst, etc. Dit werkt ook goed als je dan 3 condities hebt. Zelfde logica: Ppn 1 heeft Urban, Ppn 2 heeft Nature, Ppn 3 heeft Control, Ppn 4 heeft Urban, etc.

- [ ] Zet alleen de Nature & Urban in de brillen



## 02-07-2024
Made keyboard sound shorter at start, so it's more in line with actually clicking, instead of 20ms too late. Also made the sound localised to the specific key, instead of from the middle of the keyboard. 

Arbor installeert de Videos direct in de video map, dus daar kan niet nog een map onder, tenminste niet vanuit onze vrinden van Arbor. 



## 01-07-2024
config.json simply cannot be somewhere else. There is no folder that is available for Arbor, where the app can also access, and vice versa. So it works as follows:

App installation on device(s) can be done via Arbor.
Videos can be placed on device(s) via Arbor.
config.json is automatically generated, which is placed in the /storage/emulated/0/Android/data/com.SOSXR.Video360/files/config.json.
Changes to the config.json must be done locally, via SideQuest or something similar.
These changes will then be saved: a new config.json will not simply overwrite it.


## 28-06-2024
What is still not working, strangely enough, is the `config.json` on the Quest. It is in the same `Movies` folder, works on MacOS when loading (now still only) the website URL. However, on the Quest I get:
> `Autoconnected Player "Autoconnected Player" UnauthorizedAccessException: Access to the path "/storage/emulated/0/Movies/config.json" is denied.`

Which is not the same as what you get if the file is not in that folder:
> `AndroidPlayer "Oculus_Quest 2@ADB:1WMHH841M31061:0" [LoadConfig][!ERROR!] : Config file not found at : /storage/emulated/0/Movies/DOES_NOT_EXIST.json`

I have now adjusted the Manifest. Hopefully, this does something.

That did nothing. I have given up trying to put it in one of the standard folders of the Quest. I have now placed it in the Application.persistentDataPath, which:
>     On Android is at:  
>     - /storage/emulated/0/Android/data/com.SOSXR.Video360/files/config.json  
>     On macOS is at:  
>     - /Users/[USERNAME]/Library/Application Support/SOSXR/Video360/config.json

This is perfectly fine. So the films are in the Movies folder, and the config file in the persistent data folder.

I also ensured that a default .json is created if none was available. I did this by making a ScriptableObject of `DefaultValues`, which then has the actual `ConfigFile` as a child. Quite clever, if I say so myself.

This works really well on MacOS.


## 27-06-2024
Wrote down a little guide on the totality of Android - Video loading from Movies folder on 
[this forum](https://forum.unity.com/threads/video-not-playing-on-android-using-url-as-source.526015/#post-9911184), and 
[the discussion](https://discussions.unity.com/t/load-video-from-folder-in-videoplayer-on-android/192341/3).

Loading from all files in a folder now works! Je kan nu door de map `Movies` zoeken naar alle `.mp4` en die allemaal laden. At some point moet dit een andere map dan `Movies` worden. 

Ben bezig dat je een config in Movies kan zetten die dan wordt overgecopieerd, dat werkt nog niet helemaal. Hij blijft precedence houden aan degene die al in application.persist.. zit, en niet die in Movies. Ik wil dit namelijk want Movies is toegankelijk vnaaf Arbor, de persistentdatapath niet. 



## 26-06-2024
Turns out you have to enable the custom main manifest:
> Project Settings - Player - Publishing Settings - Build - Custom Main Manifest

This then creates a new AndroidManifest in the Plugins folder which we can edit. 
I'll have to check in a minute to see if we can build to Android from this too. Not sure yet. 

That worked! Also, 
```csharp
   #if UNITY_ANDROID && !UNITY_EDITOR
    using var envClass = new UnityEngine.AndroidJavaClass("android.os.Environment");
    // using var moviesDir = envClass.CallStatic<UnityEngine.AndroidJavaObject>("getExternalStoragePublicDirectory", "DIRECTORY_MOVIES");
    using var moviesDir = envClass.CallStatic<UnityEngine.AndroidJavaObject>("getExternalStoragePublicDirectory", "Movies");
    moviesPath = moviesDir.Call<string>("getAbsolutePath");
```
It needs to search for the `Movies` folder, and not the `DIRECTORY_MOVIES`. 

We don't have a way to get the correct length of the video, therefore we set it to 100 seconds. I'll need to find a 
way to get the correct length of the loaded video. No idea how to do this though yet. 



## 25-06-2024
Had to make sure the rendertextures were of the same size as the videos. This can easily be done by setting the 
height and the width of the rendertexture. However, this cannot be reset once a texture is created. Therefore this 
requires creating new rendertextures every time you want to set a new resolution. I've made it so that on every new 
video being played, a newly correctly sized rendertexture is being created. 

The environment can now be toggled by pressing left joystick down.

The current video details are being displayed above the monitor. This is only for debug of course, but it's useful 
to know if and when a video is not rendering as well expected, that you can know which video is being referred to. 
It lists the name, current number of seconds played, total video duration (seconds), the dimensions, and whether the 
video-list is set as random or not. 

Another debug button: reshuffle videos. For if you don't wanna watch the current vid but rather another. This is 
again just for debugging, and not for the participants to see. 

I'm now trying to get the vids to be loaded from the Movies directory. This is much more fun, since then we can have a much easier way to 'upload' new videos. Fun right?

The thing that's not yet working is the permissions. I'm needing to do something with the AndroidManifest, but I'm 
only getting errors. 



## 20-06-2024
Transcode down to [H265](https://www.obsbot.com/blog/webcam/h.265-vs-h.264)
> H.265 is a video compression standard designed to substantially improve coding efficiency when compared to its predecessor, H.264. It offers better image quality at the same bit rate, and it supports resolutions up to 8192×4320.

That looks hopeful. It also seems to be a little faster than anticipated yesterday. No I was wrong about the speed... that was a lie. That was the percentage per video instead of the total percentage. Also: Didn't solve anything. The videos are still too large. Still getting the same error.

Now I'm going to recode them using Handbrake. I'm going to use the H265 codec and see if that works, with no scaling of the video. The quality setting is going to be at 28, with same as source for the framerate. File size is around 550mb, that's about 1/8th of the original size. Eyeballing quality, it looks similar enough (dropping both in the Meta TV and playing them 'side by side').

In Handbrake, be sure to put all files in the queue and then start the queue. It will take a while to transcode. 
Otherwise if you just hit 'start' it will only do the first file.



## 19-06-2024
Put in Henk's videos. Want to see how they pan out in VR. 

However, I kept getting build errors after putting in Henk's vids.
> Starting a Gradle Daemon, 3 stopped Daemons could not be reused, use --status for details
WARNING:We recommend using a newer Android Gradle plugin to use compileSdk = 34 
> This Android Gradle plugin (7.1.2) was tested up to compileSdk = 32 
> This warning can be suppressed by adding android.suppressUnsupportedCompileSdk=34 to this project's gradle.properties

That's no good. 

Turns out this is due to the [video files being too large](https://forum.unity.com/threads/gradle-error-when-compiling-with-big-files.1455160/). 

Later I'm going to compress them (In the Clip's Import settings) and try again, for now that would take too long to transcode the vid (~33minutes).



## 18-06-2024

Added XR interaction for VUPlex
Added monitor

VUPlex input is very interesting. It doesn't simply take an input from Unity, you have to hard-push an input using
```csharp
   m_webView = FindObjectOfType<CanvasWebViewPrefab>();
     
   m_webView.WebView.SendKey("A");
```
That latter line can be called using the 'new' Unity Input.

Vuplex now can either be static in front of the user or follow (Lerp) the user's head movement, which is useful for 
testing. The button to make it move is the Menu button. Toggle it to make the VUplex follow the user's head 
movement, and toggle it again to make it static.

I managed to make 3D audio! The VideoPlayer has an option to use an external AudioSource for audio playback. I created a new AudioSource and attached as a child GameObject it to the VideoPlayer.
The audio is spatialized, so it sounds like it's coming from a specific location. 
I'm now going to make the videos in custom struct with the position of the audioSource alongside it.
Then you can figure out where the audio should be playing from in each video, and then it will automatically place the AudioSource in that location.

Somehow that serialized class thing is an issue. 
> Unsupported type VideoSettingsCustom
> UnityEngine.GUIUtility:ProcessEvent (int,intptr,bool&)

Not sure what that means. It builds though. I think it's an Editor bug. 

Regardless, I'm bumping this up to an Alpha 0_1_0.

I asked Kerwin to put the task by Manon unto the Wiki so we can play it in VR.



## 17-06-2024

Adding VUPlex

<iframe width="1280" height="720" src="https://www.youtube.com/embed/Dz4NwKzEHYE" title="Installing NixOS on the Raspberry Pi4" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" referrerpolicy="strict-origin-when-cross-origin" allowfullscreen></iframe>