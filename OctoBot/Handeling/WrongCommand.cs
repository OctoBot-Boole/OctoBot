
using System.Threading.Tasks;
using Discord.WebSocket;


namespace OctoBot.Handeling
{
    public class WrongCommand
    {
    
        public static async Task ErrorCommandReply(SocketMessage msg)
        {
           var message = msg.ToString().ToLower();
           var msgChannel = msg.Channel;

           
            if (message.Contains("66666666666666666666"))
            {
                await msgChannel.SendMessageAsync("ббуууль");
            }
            else if (message.Contains("giftpoints"))
            {
                await msgChannel.SendMessageAsync("boo... An error just appear >_< \nTry to use this command properly: **GiftPoints [ping_user(or user ID)] [number_of_points]**\nAlias: GiftPoint ");
            }
            else if (message.Contains("pass"))
            {
                await msgChannel.SendMessageAsync("boo... An error just appear >_< \nTry to use this command properly: **pass**\n Alias: Пасс, Пропуск, Доступ, КупитьПропуск, Купить Пропуск");
            }
            else if (message.Contains("stats"))
            {
                await msgChannel.SendMessageAsync("boo... An error just appear >_< \nTry to use this command properly: **Stats [user_ping(or user ID)]**");
            }
            else if (message.Contains("octopoint"))
            {
                await msgChannel.SendMessageAsync("boo... An error just appear >_< \nTry to use this command properly: **OctoPoint [ping_user(or user ID)] [number_of_points]**\n" +
                                               "Alias: OctoPoints, ОктоПоинты, Поинты, points, point");
            }
            else if (message.Contains("octorep"))
            {
                await msgChannel.SendMessageAsync("boo... An error just appear >_< \nTry to use this command properly: **OctoRep [ping_user(or user ID)] [number_of_points]**\n" +
                                               "Alias: Rep, октоРепа, Репа, Окто Репа");
            }
            else if (message.Contains("lol"))
            {
                await msgChannel.SendMessageAsync("KEK");
            }
            else if (message.Contains("lol"))
            {
                await msgChannel.SendMessageAsync("KEK");
            }
            else if (message.Contains("lol"))
            {
                await msgChannel.SendMessageAsync("KEK");
            }
            else if (message.Contains("lol"))
            {
                await msgChannel.SendMessageAsync("KEK");
            }
            else if (message.Contains("lol"))
            {
                await msgChannel.SendMessageAsync("KEK");
            }
            else if (message.Contains("lol"))
            {
                await msgChannel.SendMessageAsync("KEK");
            }
            else if (message.Contains("lol"))
            {
                await msgChannel.SendMessageAsync("KEK");
            }
            else if (message.Contains("lol"))
            {
                await msgChannel.SendMessageAsync("KEK");
            }
            else if (message.Contains("lol"))
            {
                await msgChannel.SendMessageAsync("KEK");
            }
            else if (message.Contains("lol"))
            {
                await msgChannel.SendMessageAsync("KEK");
            }
            else if (message.Contains("lol"))
            {
                await msgChannel.SendMessageAsync("KEK");
            }
            else if (message.Contains("lol"))
            {
                await msgChannel.SendMessageAsync("KEK");
            }
            else if (message.Contains("lol"))
            {
                await msgChannel.SendMessageAsync("KEK");
            }
            else if (message.Contains("lol"))
            {
                await msgChannel.SendMessageAsync("KEK");
            }
            else if (message.Contains("lol"))
            {
                await msgChannel.SendMessageAsync("KEK");
            }
            else if (message.Contains("lol"))
            {
                await msgChannel.SendMessageAsync("KEK");
            }
            else if (message.Contains("lol"))
            {
                await msgChannel.SendMessageAsync("KEK");
            }
            else if (message.Contains("lol"))
            {
                await msgChannel.SendMessageAsync("KEK");
            }
            else if (message.Contains("lol"))
            {
                await msgChannel.SendMessageAsync("KEK");
            }
            else if (message.Contains("lol"))
            {
                await msgChannel.SendMessageAsync("KEK");
            }
            else if (message.Contains("lol"))
            {
                await msgChannel.SendMessageAsync("KEK");
            }
            else if (message.Contains("lol"))
            {
                await msgChannel.SendMessageAsync("KEK");
            }
            else if (message.Contains("lol"))
            {
                await msgChannel.SendMessageAsync("KEK");
            }
            else if (message.Contains("lol"))
            {
                await msgChannel.SendMessageAsync("KEK");
            }
            else if (message.Contains("lol"))
            {
                await msgChannel.SendMessageAsync("KEK");
            }
            else if (message.Contains("lol"))
            {
                await msgChannel.SendMessageAsync("KEK");
            }
            else if (message.Contains("lol"))
            {
                await msgChannel.SendMessageAsync("KEK");
            }
            else if (message.Contains("lol"))
            {
                await msgChannel.SendMessageAsync("KEK");
            }
            else if (message.Contains("lol"))
            {
                await msgChannel.SendMessageAsync("KEK");
            }
            else if (message.Contains("lol"))
            {
                await msgChannel.SendMessageAsync("KEK");
            }
            else if (message.Contains("lol"))
            {
                await msgChannel.SendMessageAsync("KEK");
            }
            else if (message.Contains("lol"))
            {
                await msgChannel.SendMessageAsync("KEK");
            }
            else if (message.Contains("lol"))
            {
                await msgChannel.SendMessageAsync("KEK");
            }
            else if (message.Contains("lol"))
            {
                await msgChannel.SendMessageAsync("KEK");
            }


           

            await Task.CompletedTask;
        }

    }
}
