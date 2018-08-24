/*using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Newtonsoft.Json;
*/

namespace OctoBot.Commands
{
    public class RandomCommandsKek //: ModuleBase<ShardedCommandContext>
    {


        
        /*
        internal static readonly string CaptchaCss = "<style>@import url('https://fonts.googleapis.com/css?family=Roboto');body{margin: 0px;font-family: 'Roboto', sans-serif;}</style> \n <meta charset=\"utf-8\"> \n";
        internal static readonly string CaptchaHtml = "<div style=\"background: url('https://i.imgur.com/j5Bo9iF.png'); width: 323px; height: 90px;\"><p style=\"color: white; font-size: 14px; font-weight: 400; position: fixed; top: 21px; left: 62px; text-overflow: ellipsis;width: 184px;white-space: nowrap; overflow: hidden;\">{0}</p></div>";
        internal static readonly string CaretakerHtmLp1 = "<style>body{ background-color: #36393e; }</style><div style=\"background-image: url('";
        internal static readonly string CaretakerHtmLp2 = "'); width: 1000px; height: 500px; background-size: cover; background-position:center;\"><img src='https://i.imgur.com/LO4cgyW.png'></div>";

        [Command("капча")]
        public async Task CaptchaImg([Remainder]string message)
        {
            var fullHtml = CaptchaCss + String.Format(CaptchaHtml, message);

            var htmlToImageConv = new HtmlToImageConverter
            {
                Width = 323,
                Height = 90
            };

            var jpegBytes = htmlToImageConv.GenerateImage(fullHtml, ImageFormat.Jpeg);

            await Context.Channel.SendFileAsync(new MemoryStream(jpegBytes), "captcha.jpg");
        }


        internal static readonly string Captcha1Css = "<style> @font-face {src: url('C:/Users/baker/Desktop/OctoBot/font/fot.ttf'); font-family: 'Whitney-Medium';}body{margin: 0px; font-family: 'Whitney-Medium', sans-serif;}</style> \n <meta charset=\"utf-8\"> \n";
        internal static readonly string Captcha1Html = "<div style=\"background: url('https://i.imgur.com/aoCLyxT.jpg'); width: 280px; height: 70px;\"><p style=\"color: rgba(255,255,255,.7); font-style: normal ; font-size: 15px; font-weight: 100; position: fixed; top: 22px; left: 75px; text-overflow: ellipsis;width: 500px;white-space: nowrap; overflow: hidden;\">{0}</p></div>";



        [Command("цитата")]
        public async Task Captcha1Img(IGuildUser user, [Remainder]string message)
        {


            var red = 0;
            var green = 0;
            var blue = 0;

            var kek = user.RoleIds.ToArray();
            var size = user.RoleIds.Count;


            for (var i = 0; size > i; size--)
            {
                var userRole = user.Guild.GetRole(kek[size-1]);
                red = userRole.Color.R;
                green = userRole.Color.G;
                blue = userRole.Color.B;
                if (red != 0 && green != 0 && blue != 0 && red != 255 && green != 255 && blue != 255)
                {
                    red = userRole.Color.R;
                    green = userRole.Color.G;
                    blue = userRole.Color.B;
                    break;
                }

                red = 255;
                green = 255;
                blue = 255;
            }

            var namecolor = $"({red},{green},{blue})";


            string dname;
            double namesize;
            switch (user.Nickname)
            {
                case null when user.Username.Length < 6:
                    dname = user.Username;
                    namesize = user.Username.Length * 8.5 + 75;
                    break;
                case null when user.Username.Length < 9:
                    dname = user.Username;
                    namesize = user.Username.Length * 8.7 + 75;
                    break;
                case null when user.Username.Length < 13:
                    dname = user.Username;
                    namesize = user.Username.Length * 7.75 + 75;
                    break;
                case null when user.Username.Length < 20:
                    dname = user.Username;
                    namesize = user.Username.Length * 6.8 + 75;
                    break;
                case null:
                    dname = user.Username;
                    namesize = user.Username.Length * 5.5 + 75;
                    break;
                default:
                    if (user.Nickname != null && user.Nickname.Length < 6)
                    {
                        dname = user.Nickname;
                        namesize = user.Nickname.Length * 8.7 + 75;
                    }
                    else if (user.Nickname != null && user.Nickname.Length < 9)
                    {
                        dname = user.Nickname;
                        namesize = user.Nickname.Length * 9 + 75;
                    }


                    else if (user.Nickname != null && user.Nickname.Length < 13)
                    {
                        dname = user.Nickname;
                        namesize = user.Nickname.Length * 7.75 + 75;
                    }
                    else if (user.Nickname != null && user.Nickname.Length < 20)
                    {
                        dname = user.Nickname;
                        namesize = user.Nickname.Length * 6.8 + 75;

                    }
                    else
                    {
                        dname = user.Nickname;
                        namesize = user.Nickname.Length * 8 + 75;
                    }

                    break;
            }
        
            namesize = (int)namesize;

             var avatar = ("https://cdn.discordapp.com/avatars/" + user.Id + "/" + user.AvatarId + ".png");
    
           const string kek1 = "{";
            const string kek2 = "}";

            //  "<div style=\"   \">   <p style=\"color: rgba(255,255,255,.7); font-style: normal ; font-size: 15px; font-weight: 500; position: fixed; top: 30px; left: 75px;\"> </p></div>";

            var name = ($"{kek1} position: absolute; font-size: 15px; font-weight: 500;  top: 0px; left: 75px; color: rgb{namecolor}; {kek2}");
            var time = ($"{kek1} position: absolute; font-size: 12px; font-weight: 200;  top: 5px; left: {namesize}px; color: rgba(255,255,255,.2); {kek2}");
            const string background = "{  background-image: url(\"https://i.imgur.com/4X2kAqg.jpg  \"); }";
            const string img = "{  border-radius: 50%; position: absolute;  top: 10px;  left: 10px;  }";


            var avatarHtml = ($"<html> <style>  @font-face {kek1}font-family: 'uni_sansthin_caps'; src: url('C:\\Users\\durae\\Desktop\\OctoBot\\fontuni_sans_thin-webfont.woff2') format('woff2'), url('C:\\Users\\durae\\Desktop\\OctoBot\\fontuni_sans_thin-webfont.woff') format('woff'); font-weight: normal; font-style: normal;  {kek2} .name {name} .time {time} img {img} body {background} </style> <body> <div class=\"name\"> <p>{dname}</p> </div> <div class =\"time\"> <p>Вчера в 22:28</p> </div> <img src= \"{avatar} \" style=\"height:45px; width:45px;\"> </body> <meta charset=\"utf-8\"> </html>");

            //@font-face {kek1} font-family: SjqtpEd; src: url(C:\\Users\\durae\\Desktop\\OctoBot\\SjqtpEd.otf);


            var fullHtml = avatarHtml +Captcha1Css + String.Format(Captcha1Html, message);

            var htmlToImageConv = new HtmlToImageConverter
            {
                Width = 400,
                Height = 80
            };

            var jpegBytes = htmlToImageConv.GenerateImage(fullHtml, ImageFormat.Jpeg);

            await Context.Channel.SendFileAsync(new MemoryStream(jpegBytes), "quote.png");
        }


        public static bool UrlIsValidImg(string url)
        {
            var req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "HEAD";
            using (var resp = req.GetResponse())
            {
                return resp.ContentType.ToLower(CultureInfo.InvariantCulture)
                           .StartsWith("image/");
            }
        }

        [Command("care")]
        public async Task CaretakerImg(string imageUrl)
        {
            if (imageUrl[0] == '<' && imageUrl[imageUrl.Length - 1] == '>')
            {
                imageUrl = imageUrl.Substring(1, (imageUrl.Length - 2));
            }
            else
            {
                var dm = await Context.User.GetOrCreateDMChannelAsync();
                if (Context.Channel != dm)
                    await Context.Message.DeleteAsync();
            }

            if (UrlIsValidImg(imageUrl))
            {
                var html = CaretakerHtmLp1 + imageUrl + CaretakerHtmLp2;
                var htmlToImageConv = new HtmlToImageConverter();
                var jpegBytes = htmlToImageConv.GenerateImage(html, ImageFormat.Jpeg);

                await Context.Channel.SendFileAsync(new MemoryStream(jpegBytes), "caretaker.jpg");
            }


        }

        [Command("quote")]
        public async Task Quote(IGuildUser user, [Remainder]string message)
        {


            var red = 0;
            var green = 0;
            var blue = 0;

            var kek = user.RoleIds.ToArray();
            var size = user.RoleIds.Count;


            for (var i = 0; size > i; size--)
            {
                var userRole = user.Guild.GetRole(kek[size - 1]);
                red = userRole.Color.R;
                green = userRole.Color.G;
                blue = userRole.Color.B;
                if (red != 0 && green != 0 && blue != 0 && red != 255 && green != 255 && blue != 255)
                {
                    red = userRole.Color.R;
                    green = userRole.Color.G;
                    blue = userRole.Color.B;
                    break;
                }

                red = 255;
                green = 255;
                blue = 255;
            }


            var embed = new EmbedBuilder();
            embed.WithColor(red,green,blue);
            embed.WithAuthor(user);
            embed.WithUrl("https://www.google.com");
            embed.WithDescription(message);
            embed.WithTimestamp(DateTime.UtcNow);
                            if (Context.MessageContentForEdit != "edit")

        }

        [Command("рандом")]
        public async Task GetRandomPerson()
        {
            string json;
            using (var client = new WebClient())
            {
                json = client.DownloadString("https://randomuser.me/api/");
            }

            var dataObject = JsonConvert.DeserializeObject<dynamic>(json);

            string firstName = dataObject.results[0].name.first.ToString();
            string lastName = dataObject.results[0].name.last.ToString();
            string avatarUrl = dataObject.results[0].picture.large.ToString();

            var embed = new EmbedBuilder();
            embed.WithThumbnailUrl(avatarUrl);
            embed.WithTitle("Странный API");
            embed.AddField("First name", firstName);
            embed.AddField("Last name", lastName);

                      
        }*/
    }
}
