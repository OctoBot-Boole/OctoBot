# OctoBot-Core
[![Build Status](https://travis-ci.org/petrspelos/Community-Discord-BOT.svg?branch=master)](https://travis-ci.org/mylorik/OctoBot-Core)
[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/mylorik/OctoBot-Core/blob/master/LICENSE)


**THE FILE IN PROGRESS EDITING**

**You can add this bot to your server using this webSite: https://discordbots.org/bot/423593006436712458**

**Also, there will be _better explanation_ of what the bot does for the server, and how to use it (bot do have a welcome message so you will understand what it does)**



-------------------------------------------------------------------------------------------------------------------


This is a C# project using 3rd party API, for to interact with Discord users. 

Interesting commands, from programmer perspective:

*P.s. there is A LOT of commands and interactions, and even **a game** in progress!*

1)If you edit your previous command, Octobot will edit the previous answer as well

2)[**Remind** AnyText **IN** 1d13h33m33s] This command will remind you anything you want in the time you ask as a DM message. (https://i.imgur.com/ARFxYVl.png)

How it works: It creates a struct with calculated time WhenToPost, and the text you wanted and save it to accounts file. Every 5 seconds app is checking all users have a reminder in account file, and if date now => WhenToPost, it will try to send the message, if success - delete it from the file. So this will work even if the application was restarted. [**mute @user**] works in the same way. You also can make this reminder for other people.

3)In *OctoBot-Core/OctoBot/Commands/ShadowCItyCOmmand/* I have two messages with multiple emojis under it. Both of them made for users to **get or delete a server role(tag) by pressing on particular emoji**, which gives access to closed rooms on the server or just a colour on your nickname. ( one mess for colours, another for rooms) (https://i.imgur.com/MD7LaAs.png)

How it works: every time a user placing an emoji under any message, the application is triggered by an event - placed emoji, which takes 3 parameters user, message, emoji. if messageID == to a particular ID, it will check if you have the role assigned to this emoji, if yes - delete, no - add it, and then remove your emoji from that mess. 

4)**Blog system** You can sub or unsub to any user on the server, and everyone can use command **Blog AnyText** and it will send a DM message to every user subscribed to you with that AnyText **AND** it will add grade emojis (1 to 5) everything **SIMULTANEOUSLY**. Then, every subscriber can GRADE your blog(only one grade per blog, but you can remove your previous grade and place another one( all emojis are already under the message placed by the application bot, so you don't have to look for them) By the way, even if you are subscribed to yourself, you can not grade yourself. You can check top users by average grade for blogs using **topb** command. (https://i.imgur.com/QPFa4wq.png)
