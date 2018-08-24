using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using OctoBot.Configs;
using OctoBot.Custom_Library;
using OctoBot.Handeling;

namespace OctoBot.CustomForServers
{
    public class UpdateShadowMess : ModuleBase<ShardedCommandContextCustom>
    {
        [Command("буль228")]
        public async Task Boole()
        {
            //  await Context.Message.DeleteAsync();

            var peaceKeepo = Emote.Parse("<:PeaceKeepo:438257037667729408>");
            var praise = Emote.Parse("<:praise:445274481917952001>");
            var rem = Emote.Parse("<:rem:445275743719522304>");
            var steampunk = Emote.Parse("<:Steampunk:445276776676196353>");
            var mumu = Emote.Parse("<:mumu:445277916872310785>");
            var monkaS = Emote.Parse("<:monkaS:398183436613058570>");


            var embed = new EmbedBuilder();
            embed.WithAuthor(Global.Client.GetUser(326736083847086081));
            embed.WithColor(Color.Green);
            embed.AddField("Роль для цвета",
                $"Чтобы **получить** или **снять** роль нажмите определенную эмоцию, или пропишите соответствующую команду в <#374914059679694848>\n" +
                $"**_______**\n" +
                $"{new Emoji("<:rem:445275743719522304>")} <@&374900834946908160> - последователи анимэ (!weeb)\n{new Emoji("🦊")} <@&375079829642412034>- могущественные лисы и лисицы! (!fox)\n" +
                $"{new Emoji("<:PeaceKeepo:438257037667729408>")} <@&374900824880447489> - Авэ Мария! (!deus)\n" +
                $"{new Emoji("<:Steampunk:445276776676196353>")} <@&374900827632041986> - нет того, чего нельзя изобрести на паровом движке (!steampunk)\n" +
                $"{new Emoji("<:praise:445274481917952001>")} <@&440420047005941761> - Praise The Sun (!praise)\n{new Emoji("<:monkaS:398183436613058570>")} <@&374900838096961546> - последователи великого Пепе (!meme)\n" +
                $"{new Emoji("🐲")} <@&374900821382529025> - лазурные драконы! (!dragon)\n{new Emoji("🐼")} <@&374980985281970206> - злобные  существа - Панды  (!panda)\n" +
                $"{new Emoji("🦎")} <@&425736884870840322> - ящеры хвостатые (!lizard)\n{new Emoji("🌑")} <@&375197931793285130> - тени сервера (!shadow)\n" +
                $"{new Emoji("<:mumu:445277916872310785>")} <@&374981082707394569> - добрые, в отличие от Панд, Мыфы (!nazrin)\n{new Emoji("🐱")} <@&375376861594910720> - кисики сервера (!cat)\n");


            var socketMsg = await Context.Channel.SendMessageAsync("", false, embed.Build());


            await socketMsg.AddReactionAsync(rem);
            await socketMsg.AddReactionAsync(new Emoji("🦊"));
            await socketMsg.AddReactionAsync(peaceKeepo);
            await socketMsg.AddReactionAsync(steampunk);
            await socketMsg.AddReactionAsync(praise);
            await socketMsg.AddReactionAsync(monkaS);
            await socketMsg.AddReactionAsync(new Emoji("🐲"));
            await socketMsg.AddReactionAsync(new Emoji("🐼"));
            await socketMsg.AddReactionAsync(new Emoji("🦎"));
            await socketMsg.AddReactionAsync(new Emoji("🌑"));
            await socketMsg.AddReactionAsync(mumu);
            await socketMsg.AddReactionAsync(new Emoji("🐱"));
        }


        [Command("бууль228")]
        public async Task Boooole()
        {
            var roles = Context.Guild.Roles.ToArray();
            for (var i = 0; i < roles.Length; i++) Console.WriteLine($"({i}){roles[i].Name} {roles[i].Id}");

            await Task.CompletedTask;
        }

        [Command("буууль228")]
        public async Task Booooole()
        {
            var chanels = Context.Guild.TextChannels.ToArray();
            for (var i = 0; i < chanels.Length; i++) Console.WriteLine($"({i}){chanels[i].Name} {chanels[i].Id}");

            await Task.CompletedTask;
        }

        [Command("бульк228")]
        public async Task Boolek()
        {
            //  await Context.Message.DeleteAsync();

            var rkn = Emote.Parse("<:RKN:445325930022436874>");
            var realy = Emote.Parse("<:realy:374655750657540106>");
            var feelsBadMan = Emote.Parse("<:FeelsBadMan:374655964843868162>");
            var woahMorfin = Emote.Parse("<:WoahMorfin:436787514813186050>");
            var thonk = Emote.Parse("<:thonk:445324435403309087>");
            var akaShrug = Emote.Parse("<:AkaShrug:374802737596071936>");
            var such = Emote.Parse("<:such:445322074781908993>");
            var kannNom = Emote.Parse("<:GWnanamiKannaNom:445321264169746434>");
            var pogChamp = Emote.Parse("<:PogChamp:374656108117098517>");
            var hanzo = Emote.Parse("<:hanzo:445324859690582018>");
            var mine = Emote.Parse("<:Minecrafticonfilegzpvzfll:445325226427940874>");
            var yasuo = Emote.Parse("<:yasuo:445323301137547264>");
            var gacHiPride = Emote.Parse("<:gacHIPride:394782921749430273>");
            var pekaohmy = Emote.Parse("<:pekaohmy:374656330742497280>");
            var warframe = Emote.Parse("<:warframe:445467639242948618>");
            // var nintendoswitch = Emote.Parse("<:nintendoswitch:447209827501080596>");


            var embed = new EmbedBuilder();
            embed.WithAuthor(Global.Client.GetUser(326736083847086081));
            embed.WithColor(Color.Blue);
            embed.AddField("Роль для рум",
                "Чтобы **получить** или **снять** роль просто нажмите определенную эмоцию, или пропишите соответствующую команду в <#374914059679694848>\n" +
                "**________**\n");
            embed.AddField("Блокирующие каналы роли:",
                $"!блок - всю категорию **stream**, больше никаких бесполезных оповещений не будет {new Emoji("<:RKN:445325930022436874>")}\n" +
                $"!антисрач - блокирует <#375600819011190787>, не хочешь видеть о чем срутся люди? Ну так и не надо {new Emoji("<:realy:374655750657540106>")}\n" +
                $"!eventblock - блокирует категорию events, эх, а ведь ты мог получить халявную репку {new Emoji("<:FeelsBadMan:374655964843868162>")}\n" +
                $"**________**");

            embed.AddField($"Добавляющие каналы роли:",
                $"!voice - <#421951522700787713> когда-нибудь тут проведут мафию, свояк, или еще что-нибудь {new Emoji("<:WoahMorfin:436787514813186050>")}\n" +
                $"!riddler - <#421972081232838677> любишь загадки? Они тут есть, правда, ну честно, ну есть же... {new Emoji("<:thonk:445324435403309087>")}\n" +
                $"!настолки - <#422674753552384001>, <#436706101027799061> <#436550169215893524> описание будет от Лемура, позже, ага {new Emoji("<:AkaShrug:374802737596071936>")}\n" +
                $"!tech - <#374635063976919051>, <#421637061787779072> ты просто должен понимать, что там сидят шкальники и диванные аналитики, которые все знают лучше тебя {new Emoji("<:such:445322074781908993>")}\n" +
                $"!anime - ну тут без коммента... SONO CHI NO SADAME! {new Emoji("<:GWnanamiKannaNom:445321264169746434>")}\n" +
                $"!cards - ты тоже любишь поиграть ручками с... рандомом? <#438955526450315265> <#438955581965860864> {new Emoji("<:PogChamp:374656108117098517>")}\n" +
                $"**________**\n");

            embed.AddField($"Игровые Комнаты:",
                $"!hots - <#421637740137021450> и <#425354467022602258> Нет времени объяснять ─ фокуси танка {new Emoji("<:hanzo:445324859690582018>")}\n" +
                $"!minecraft - <#425654423642177536> это то место, где ты сможешь построить **САМУЮ КРАСИВУЮ КОРОБКУ**, лучше из грязи {new Emoji("<:Minecrafticonfilegzpvzfll:445325226427940874>")}\n" +
                $"!lol - <#429345059486564352>, <#436522034231640064> пикай Ясуо вместе с Ривен и получай удовольствие {new Emoji("<:yasuo:445323301137547264>")}\n" +
                $"!r6 - стань радужным воином в <#436938171692089344> {new Emoji("<:gacHIPride:394782921749430273>")}\n" +
                $"!warframe - симулятор фермера в космосе {new Emoji("<:warframe:445467639242948618>")}\n" +
                $"**________**\n" +
                $"Сделай сервер максимально удобным для себя! {new Emoji("<:pekaohmy:374656330742497280>")}");


            var mess = await Context.Channel.SendMessageAsync("", false, embed.Build());

            await mess.AddReactionAsync(rkn);
            await mess.AddReactionAsync(realy);
            await mess.AddReactionAsync(feelsBadMan);
            await mess.AddReactionAsync(woahMorfin);
            await mess.AddReactionAsync(thonk);
            await mess.AddReactionAsync(akaShrug);
            await mess.AddReactionAsync(such);
            await mess.AddReactionAsync(kannNom);
            await mess.AddReactionAsync(pogChamp);
            await mess.AddReactionAsync(hanzo);
            await mess.AddReactionAsync(mine);
            await mess.AddReactionAsync(yasuo);
            await mess.AddReactionAsync(gacHiPride);
            await mess.AddReactionAsync(warframe);
            await mess.AddReactionAsync(pekaohmy);
        }

        [Command("roomApd")]
        public async Task RoomApd()
        {
            try
            {
                /*
                                var rkn = Emote.Parse("<:RKN:445325930022436874>");
                                var realy = Emote.Parse("<:realy:374655750657540106>");
                                var feelsBadMan = Emote.Parse("<:FeelsBadMan:374655964843868162>");
                                var woahMorfin = Emote.Parse("<:WoahMorfin:436787514813186050>");
                                var thonk = Emote.Parse("<:thonk:445324435403309087>");
                                var akaShrug = Emote.Parse("<:AkaShrug:374802737596071936>");
                                var such = Emote.Parse("<:such:445322074781908993>");
                                var kannNom = Emote.Parse("<:GWnanamiKannaNom:445321264169746434>");
                                var pogChamp = Emote.Parse("<:PogChamp:374656108117098517>");
                                var hanzo = Emote.Parse("<:hanzo:445324859690582018>");
                                var mine = Emote.Parse("<:Minecrafticonfilegzpvzfll:445325226427940874>");
                                var yasuo = Emote.Parse("<:yasuo:445323301137547264>");
                                var gacHiPride = Emote.Parse("<:gacHIPride:394782921749430273>");
                                var pekaohmy = Emote.Parse("<:pekaohmy:374656330742497280>");
                                var warframe = Emote.Parse("<:warframe:445467639242948618>");
                                */


                var embed = new EmbedBuilder();
                embed.WithAuthor(Global.Client.GetUser(326736083847086081));
                embed.WithColor(Color.Blue);
                // embed.WithFooter("lil octo notebook");
                embed.WithFooter("буль.");
                embed.AddField("Роль для рум",
                    "Чтобы **получить** или **снять** роль просто нажмите определенную эмоцию, или пропишите соответствующую команду в <#374914059679694848>\n" +
                    "**________**\n");
                embed.AddField("Блокирующие каналы роли:",
                    $"{new Emoji("<:RKN:445325930022436874>")} - !блок - всю категорию **stream**, больше никаких бесполезных оповещений не будет\n" +
                    $"{new Emoji("<:realy:374655750657540106>")} - !антисрач - блокирует <#375600819011190787>, не хочешь видеть о чем срутся люди? Ну так и не надо\n" +
                    $"{new Emoji("<:FeelsBadMan:374655964843868162>")} - !eventblock - блокирует категорию events, эх, а ведь ты мог получить халявную репку\n" +
                    $"**________**");
                embed.AddField($"Добавляющие каналы роли:",
                    $"{new Emoji("<:thonk:445324435403309087>")} - !riddler - <#421972081232838677> любишь загадки? Они тут есть, правда, ну честно, ну есть же...\n" +
                    $"{new Emoji("<:such:445322074781908993>")} - !tech - <#374635063976919051>, <#421637061787779072> ты просто должен понимать, что там сидят шкальники и диванные аналитики, которые все знают лучше тебя\n" +
                    $"{new Emoji("<:GWnanamiKannaNom:445321264169746434>")} - !anime - ну тут без коммента... SONO CHI NO SADAME!\n" +
                    $"{new Emoji("<:nintendoswitch:447209808064413707>")} - !nintendo - Рума которую никто не просил\n" +
                    $"{new Emoji("<:AkaShrug:374802737596071936>")} - !настолки - <#422674753552384001>, <#436706101027799061> <#436550169215893524> описание будет от Лемура, позже, ага\n" +
                    $"**________**\n");
                embed.AddField($"Игровые Комнаты:",
                    $"{new Emoji("<:PogChamp:374656108117098517>")} - !cards - ты тоже любишь поиграть ручками с... рандомом? <#438955581965860864>\n" +
                    $"{new Emoji("<:hanzo:445324859690582018>")} - !hots - <#421637740137021450> и <#425354467022602258> Нет времени объяснять ─ фокуси танка\n" +
                    $"{new Emoji("<:yasuo:445323301137547264>")} - !lol - <#429345059486564352>, <#436522034231640064> пикай Ясуо вместе с Ривен и получай удовольствие\n" +
                    $"{new Emoji("<:gacHIPride:394782921749430273>")} -!r6 - стань радужным воином в <#436938171692089344>\n" +
                    $"{new Emoji("<:sir:430853466110427137>")} - !strategy - Любишь по вечерам управлять империей? Узнай как ее потерять <#464089685463924747>\n" +
                    $"{new Emoji("<:fuckyeah:430853466408353792>")} - !fps - косплееры штурмовиков в <#469779105387249665> играх\n" +
                    $"{new Emoji("<:warframe:445467639242948618>")} - !warframe - симулятор фермера в космосе <#437699858384551966>\n" +
                    $"**________**\n" +
                    $"{new Emoji("<:pekaohmy:374656330742497280>")} - Сделай сервер максимально удобным для себя!");


                if (await Global.Client.GetGuild(338355570669256705)
                    .GetTextChannel(374627268162617344)
                    .GetMessageAsync(445502492088860672) is IUserMessage message)
                    await message.ModifyAsync(mess =>
                    {
                        mess.Embed = embed.Build();
                        // This somehow can't be empty or it won't update the 
                        // embed propperly sometimes... I don't know why
                        // message.Content =  Constants.InvisibleString;
                    });


                await CommandHandeling.ReplyAsync(Context,
                    "Бульк. Мы заапдейтили сообщение для рум!");
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: '{0}'", e);
            }
        }


        [Command("colorApd")]
        public async Task ColorApd()
        {
            try
            {
                /*
          var peaceKeepo = Emote.Parse("<:PeaceKeepo:438257037667729408>");
          var praise = Emote.Parse("<:praise:445274481917952001>");
          var rem = Emote.Parse("<:rem:445275743719522304>");
          var steampunk = Emote.Parse("<:Steampunk:445276776676196353>");
          var mumu = Emote.Parse("<:mumu:445277916872310785>");
          var monkaS = Emote.Parse("<:monkaS:398183436613058570>");
              */

                var embed = new EmbedBuilder();
                embed.WithAuthor(Global.Client.GetUser(326736083847086081));
                embed.WithFooter("lil octo notebook");
                embed.WithColor(Color.Green);
                embed.AddField("Роль для цвета",
                    $"Чтобы **получить** или **снять** роль нажмите определенную эмоцию, или пропишите соответствующую команду в <#374914059679694848>\n" +
                    $"**_______**\n");
                embed.AddField("Тыки на эмоцию",
                    $"{new Emoji("<:rem:445275743719522304>")} <@&374900834946908160> - последователи анимэ (!weeb)\n{new Emoji("🦊")} <@&375079829642412034>- могущественные лисы и лисицы! (!fox)\n" +
                    $"{new Emoji("<:PeaceKeepo:438257037667729408>")} <@&374900824880447489> - Авэ Мария! (!deus)\n" +
                    $"{new Emoji("<:Steampunk:445276776676196353>")} <@&374900827632041986> - нет того, чего нельзя изобрести на паровом движке (!steampunk)\n" +
                    $"{new Emoji("<:praise:445274481917952001>")} <@&440420047005941761> - Praise The Sun (!praise)\n{new Emoji("<:monkaS:398183436613058570>")} <@&374900838096961546> - последователи великого Пепе (!meme)\n" +
                    $"{new Emoji("🐲")} <@&374900821382529025> - лазурные драконы! (!dragon)\n{new Emoji("🐼")} <@&374980985281970206> - злобные  существа - Панды  (!panda)\n" +
                    $"{new Emoji("🦎")} <@&425736884870840322> - ящеры хвостатые (!lizard)\n{new Emoji("🌑")} <@&375197931793285130> - тени сервера (!shadow)\n" +
                    $"{new Emoji("<:mumu:445277916872310785>")} <@&374981082707394569> - добрые, в отличие от Панд, Мыфы (!nazrin)\n{new Emoji("🐱")} <@&375376861594910720> - кисики сервера (!cat)\n");

                if (await Global.Client.GetGuild(338355570669256705)
                    .GetTextChannel(374627268162617344)
                    .GetMessageAsync(445501974608216064) is IUserMessage message)
                    await message.ModifyAsync(mess =>
                    {
                        mess.Embed = embed.Build();
                        // This somehow can't be empty or it won't update the 
                        // embed propperly sometimes... I don't know why
                        // message.Content =  Constants.InvisibleString;
                    });


                await CommandHandeling.ReplyAsync(Context,
                    "Бульк. Мы заапдейтили сообщение для цветов!");
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: '{0}'", e);
            }
        }
    }
}