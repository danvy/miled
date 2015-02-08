# MiLED

[![Join the chat at https://gitter.im/danvy/miled](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/danvy/miled?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)
Control your MiLight LimitlessLED EasyBulb lights with C#

##Introduction
I a big Internet of Things fan so I started to install some connected lights and bulbs in my house. I wanted more. I wanted to control those lights myself and give that power to the community.
Then came the Microsoft France TechDays 2014. During the keynote, I made the demonstration that it is very easy to embrace the IoT wave using Windows and Windows Phone. The MiLight is the extension of this demo. Most possible scenarii are implemented.

##How to use it?
Add a reference to the MiLight.SharedLib shared project, and start to play.
Here are some examples :
First connect to the bridge
```
var bridge = new LEDBridge("10.10.100.254", "8899");
```
Then switch on all the connected lights
```
await bridge.RGBWOnAsync(LEDGroups.All);
```
Change some colors
```
await bridge.RGBWColorAsync(LEDColor.Violet);
```
Sample apps with full source code are provided.
It's now your turn to play.
Happy light show.

##What's new?
1.0 Initial release.

##Q&A
#### Q: What are the validated platforms?
Console app on Windows 8.1 by @danvy on 2015-02-07
Windows Universal app on Windows 8.1 by @danvy on 2015-02-07

If you have any problem with the scripts, use GitHub or contact me on Twitter http://twitter.com/danvy
