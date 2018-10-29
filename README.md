# SCC402

## Setup

* Clone the project
  * `git clone git@github.com:Shubwub/SCC402.git`
* Open Unity using the SCC402 folder generated above
* Wait for things to setup
* Make sure current scene is `SampleScene` (the name next to the little unity logo in the `Hierarchy` tab)
  * If it isn't:
    * In project tab at bottom of screen, open the `Scenes` folder and open the `SampleScene`

##### Build Configuration
* Go to `File > Build Settings`
* Under `Platform`, Select `Android`
* Click `Switch Platform` button at bottom
* For the `Build System`, select `Internal` in the dropdown

##### Building the App
* Connect your Android device to your computer via USB
* Make sure USB Debugging is enabled
  * If you haven't set up USB Debugging before, follow instructions here:
    * https://www.embarcadero.com/starthere/xe5/mobdevsetup/android/en/enabling_usb_debugging_on_an_android_device.html
* Go to `File > Build And Run`
* Give the .apk file a name and click Save
* Project should build and automatically run on your phone

## Creating Custom Image Targets
* https://www.youtube.com/watch?v=MtiUx_szKbI
