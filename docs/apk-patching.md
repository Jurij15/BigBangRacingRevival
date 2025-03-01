# Patching the apk (change url, remove encryption and hash checking)
# Requirements
- a PC
- Visual Studio 2022 (to build the server)
- [ApkTool](https://apktool.org/docs/install)
- [dnSpy](https://github.com/dnSpyEx/dnSpy)

- Some coding skills

# Steps
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
