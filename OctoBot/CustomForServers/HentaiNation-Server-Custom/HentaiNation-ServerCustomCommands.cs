using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using OctoBot.Configs;
using OctoBot.Custom_Library;

namespace OctoBot.CustomForServers
{
    public class ServerCustomCommands : ModuleBase<ShardedCommandContextCustom>
    {
        [Command("HentaiWelcomeMess")]
        [RequireOwner]
        public async Task Boole()
        {
            //  await Context.Message.DeleteAsync();

            var pedoFedora = Emote.Parse("<:PedoFedora:396676199738507265>");
            var gasm = Emote.Parse("<:Gasm:396672726783361034>");
            var kappaPride = Emote.Parse("<:KappaPride:396676035715792896>");
            var yuiStop = Emote.Parse("<:YuiStop:398278458783825920>");
            var realy = Emote.Parse("<:realy:397035718913818624>");
            var henlo = Emote.Parse("<:henlo:396773624909922316>");
            var aworry = Emote.Parse("<:aworry:396773971959218185>");
            var pekaPled = Emote.Parse("<:PekaPled:398276512119390208>");
            var woah = Emote.Parse("<:Woah:396773773644267532>");
            var angery = Emote.Parse("<:Angery:398280630116548623>");
            var feelsBadMan = Emote.Parse("<:FeelsBadMan:397035718901366786>");
            var mumu = Emote.Parse("<:mumu:396672675977756674>");
            var padoru = Emote.Parse("<:PADORU:399269451448713238>");
            var oSsloth = Emote.Parse("<:OSsloth:398276510831869952>");
            var takai = Emote.Parse("<:Takai:406938709058125825>");
            var pekaApple = Emote.Parse("<:pekaApple:402174155954585601>");


            var embed = new EmbedBuilder();
            embed.WithAuthor(Global.Client.GetUser(161246859786977280));
            embed.WithColor(Color.Green);
            embed.AddField("Добро пожаловать, в святилище картинок!",
                $"У нас нет особых правил, но **НА БУТЫЛКУ СЕСТЬ ВСЁ ЖЕ МОЖНО.**\n" +
                $"\n" +
                $"1)Не нарушайте ламповую атмосферу\n" +
                $"2)Кидайте пикчи в **СООТВЕТСТВУЮЩИЕ ИМ КАНАЛЫ**\n" +
                $"А так, разрешено всё,что не запрещено.\n" +
                $"" +
                $"Чтобы **получить** или **убрать** доступ к **NSFW** комнате, просто нажмите определенную эмоцию. (или пропишите соответствующую команду в <#396684355814162442>)\n" +
                $"**_______**\n" +
                $"1)!loli = Да возлюбит всевышний,тех кто слаб телом своим.( {new Emoji("<:PedoFedora:396676199738507265>")} )\n" +
                $"2)!yuri = Женская дружба ещё не была настолько сладкой. ( {new Emoji("<:Gasm:396672726783361034>")} )\n" +
                $"3)!yaoi = Всё что нужно настоящему мужчине,это настоящий мужик ( {new Emoji("<:KappaPride:396676035715792896>")} )\n" +
                $"4)!futa = У каждой уважающей себя дамы должен быть свой секрет ( {new Emoji("<:YuiStop:398278458783825920>")} )\n" +
                $"5)!bdsm = Нет ничего лучше,чем самобичевания во имя господа.... не только господа...( {new Emoji("<:realy:397035718913818624>")} )\n" +
                $"6)!neko = Уши! Ну вы видели! У них УШИ! ( {new Emoji("<:henlo:396773624909922316>")} )\n");
            embed.AddField("**______**",
                $"7)!trash = Для любителей пожестче ( {new Emoji("<:aworry:396773971959218185>")} )\n" +
                $"8)!figure = Свою вайфу, можно и погладить ( {new Emoji("<:PekaPled:398276512119390208>")} )\n" +
                $"9)!exotic = Для любителей редких видов суккубов и монстров ( {new Emoji("<:Woah:396773773644267532>")}  )\n" +
                $"10)!furry = Для тех, кто любит помягче ( {new Emoji("<:Angery:398280630116548623>")}  )\n" +
                $"11)!trap = Однажды я пошел на свидания с девушкой, а она пошла в мужской туалет ( {new Emoji("<:FeelsBadMan:397035718901366786>")} )\n" +
                $"12)!touhou = Богини, одни Богини! Но истиная всего одна ( {new Emoji("<:mumu:396672675977756674>")} )\n" +
                $"13)!fate = хммм, сейбер, сейбер, сейбер, семирамида, сейбер ,сейбер  ( {new Emoji("<:PADORU:399269451448713238>")} )\n" +
                $"14)!lovelive = Запомните девочки, только продюсеру можно вас трогать  ( {new Emoji("<:OSsloth:398276510831869952>")} )\n" +
                $"15)!iwantall = Для тех, кто слаб духом своим(получить все роли) ( {new Emoji("<:Takai:406938709058125825>")} )");


            var socketMsg = await Context.Channel.SendMessageAsync("", false, embed.Build());


            await socketMsg.AddReactionAsync(pedoFedora);
            await socketMsg.AddReactionAsync(gasm);
            await socketMsg.AddReactionAsync(kappaPride);
            await socketMsg.AddReactionAsync(yuiStop);
            await socketMsg.AddReactionAsync(realy);
            await socketMsg.AddReactionAsync(henlo);
            await socketMsg.AddReactionAsync(aworry);
            await socketMsg.AddReactionAsync(pekaPled);
            await socketMsg.AddReactionAsync(woah);
            await socketMsg.AddReactionAsync(angery);
            await socketMsg.AddReactionAsync(feelsBadMan);
            await socketMsg.AddReactionAsync(mumu);
            await socketMsg.AddReactionAsync(padoru);
            await socketMsg.AddReactionAsync(oSsloth);
            await socketMsg.AddReactionAsync(takai);
            await socketMsg.AddReactionAsync(pekaApple);
        }
    }
}