# BigBangRacing Revival
A WIP custom server for a game called Big Bang Racing, released by TraplightGames in 2016. As of late 2022, the game servers were shut down.

# Project state
Using a modified apk (with a few aditional checks patched out (uploading apk soon)), you should be able to get into the main menu and click around. I will work on it from time to time, any help is wanted. At the moment, I am stuck at replicating the ServerInitialData file (that file contains nodes and lanes with the game data like assets, vehicles, etc...).
Also, please ignore the Server project (it was only meant for testing, before i rewrote it in Services).

# Patching the apk (change url, remove encryption and hash checking)
- Decompile the original apk
- go to assets\bin\Data\Managed
- open Assembly-CSharp.dll in dnspy
- go to psstate.cs at line 122 and replace the url with your one
- go to woeurl.cs at line 12 and replace the url with your one
- go to clienttools.cs at line 412, replace the function content with just " return s; "
- go to clienttools.cs at line 421, replace the function content with just " return _string; "
- go to clienttools.cs at line 428, replace the function return with just " return Encoding.Default.GetString(array); "
- go to clienttools.cs at line 439, replace the function return with just " return Encoding.Default.GetString(array); "
- go to errorhandler.cs at line 114, replace the function content with just "return true";
- file -> save module -> ok
- open AndroidManifest.xml
- add "android:usesCleartextTraffic="true"" into the application [like this](https://imgur.com/a/n8gdD0I)
- recompile the apk
- sign with apktool on android

# Contact
@JurijG on Discord
jurij.grom07@gmail.com
