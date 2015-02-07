# KloutSharp
The Klout API for C# 

##Introduction
Like it or hate it, there's not a lot of tool to measure your impact on social networks. Klout is one of those, check it on http://klout.com.
The Klout APIs are pretty simple be a C# implementation will ease your work. I created a portable library so it should be usable for .NET 4.5, Window, Windows Phone, iOS and Android.
It relies on Json.NET https://www.nuget.org/packages/Newtonsoft.Json and Microsoft HTTP Client Libraries https://www.nuget.org/packages/Microsoft.Net.Http

##How to use it?
First, get a Klout API key http://developer.klout.com/apps/mykeys
Then, add the KloutSharp NuGet package to your project https://www.nuget.org/packages/danvy.kloutsharp.lib/  
The entry point is the Klout object so create a Klout object
```
var k = new Klout(key);
```
Then you might need to get the Klout identifier of your user. You can use any Klout.Identity*Async method depending on the information your currently have (Twitter Id, Google+ Id, etc.).
```
var identity = await k.IdentityAsync("danvy");
Console.WriteLine(string.Format("Your Klout Id is {0}", identity.Id));
```
Once you have the Klout Id, you can keep it forever.
Now you can call the other APIs such as UserAsync to get user informations
```
var user = await k.UserAsync(kloutId);
Console.WriteLine(string.Format("Klout user nick={0} score={1}", user.Nick, user.Score.Score));
```
A sample app with full source code is provided.

##What's new?
1.0 Initial release. Supports all Klout API available on http://api.klout.com/v2/ on 2015-02-01

##Q&A
#### Q: What are the validated platforms?
Console app on Windows 8.1 by @danvy on 2015-02-01

If you have any problem with the scripts, use GitHub or contact me on Twitter http://twitter.com/danvy
