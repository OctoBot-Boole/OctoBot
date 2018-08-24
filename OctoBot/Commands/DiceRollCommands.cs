using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using OctoBot.Custom_Library;
using OctoBot.Handeling;
using OctoBot.Helper;

namespace OctoBot.Commands
{
    public class DiceRollCommands : ModuleBase<ShardedCommandContextCustom>
    {
        private readonly SecureRandom _secureRandom;

        public DiceRollCommands(SecureRandom secureRandom)
        {
            _secureRandom = secureRandom;
        }

        [Command("roll")]
        [Alias("Роллл", "Ролл")]
        public async Task Roll(int number, int times)
        {
            try
            {
                var mess = "";
                if (times > 101)
                {
                    await CommandHandeling.ReplyAsync(Context,
                        "Boole! We are not going to roll that many times!");


                    return;
                }

                if (number > 999999999)
                {
                    await CommandHandeling.ReplyAsync(Context,
                        "Boole! This numbers is way too big for us :c");


                    return;
                }

                for (var i = 0; i < times; i++)
                {
                    var randomIndexRoll = _secureRandom.Random(1, number);
                    mess += $"It's a {randomIndexRoll}!\n";
                }

                var embed = new EmbedBuilder();
                embed.WithFooter("lil octo notebook");
                embed.WithTitle($"Roll {times} times:");
                embed.WithDescription($"{mess}");

                await CommandHandeling.ReplyAsync(Context, embed);
            }
            catch
            {
             //   await ReplyAsync(
             //       "boo... An error just appear >_< \nTry to use this command properly: **roll [times] [max_value_of_roll]**\n" +
             //       "Alias: Роллл, Ролл");
            }
        }


        [Command("roll")]
        [Alias("Роллл", "Ролл")]
        public async Task Roll(int number)
        {
            try
            {
                var randomIndexRoll = _secureRandom.Random(1, number);

                await CommandHandeling.ReplyAsync(Context, $"It's a {randomIndexRoll}!");
            }
            catch
            {
              //  await ReplyAsync(
              //      "boo... An error just appear >_< \nTry to use this command properly: **roll [max_value_of_roll]**\n" +
              //      "Alias: Роллл, Ролл");
            }
        }


        [Command("roll")]
        public async Task CalculateStuf([Remainder] string yyyy)
        {
            var low = yyyy.ToLower();
            low = low.Replace(" ", string.Empty);

            var embed = new EmbedBuilder();
            embed.WithColor(Color.Green);

            embed.WithFooter("lil octo notebook");
            var results = "steps:\n";
            var answer = 0;
            var numberString = "";
            var numberString2 = "";
            var sign = 'k';
            var check = 0;
            var doi = 0;
            var count = 0;
            var boole = 0;
            var isSuccess = 0;

            for (var i = 0; i < low.Length; i++)
            {
                isSuccess++;
                if (isSuccess >= 200)
                {
                    await ReplyAsync("буль!!");
                    return;
                }

                string reminder;
                if (low[0] == '-')
                {
                    Console.WriteLine($"hello== {low}");
                    //-79+3d1+7d1+99
                    reminder = "";
                    var kek = "";

                    for (var m = 1; m < low.Length; m++)
                    {
                        if (boole == 0)
                        {
                            if (!int.TryParse($"{low[m]}", out _))
                            {
                                boole = 1;
                                m++;
                            }
                            else
                            {
                                kek += $"{low[m]}";
                            }
                        }

                        if (boole == 1) reminder += $"{low[m]}";
                    }

                    low = reminder + $"-{kek}";
                    // Console.WriteLine($"bye== {low}");
                    i = -1;
                    answer = 0;
                    numberString = "";
                    numberString2 = "";
                    sign = 'k';
                    check = 0;
                    doi = 0;
                    count = 0;
                    boole = 0;
                }
                else
                {
                    if (int.TryParse($"{low[i]}", out _) || i == low.Length - 1 || i == low.Length || low[0] == '-')
                    {
                        switch (check)
                        {
                            case 0:
                                if (low[0] == '-')
                                    numberString += "-";
                                numberString += low[i];
                                break;
                            case 1:
                                numberString2 += low[i];
                                break;
                        }

                        if (check != 2 && i != low.Length - 1) continue;

                        if (doi < 2)
                        {
                            int.TryParse(numberString, out var num1);
                            int.TryParse(numberString2, out var num2);

                            var result = Calculator(num1, num2, sign);
                            answer = result.Item1;

                            if (i == low.Length - 1 && check == 1)
                            {
                                if (result.Item2.Length > 0)
                                    embed.AddField($"It's a {answer}!", $"{results} **{result.Item2}**");
                                else
                                    embed.AddField($"It's a {answer}!", $"{results}");


                                await CommandHandeling.ReplyAsync(Context, embed);

                                return;
                            }


                            reminder = $"{answer}{low[i - 1]}";

                            for (var k = i; k < low.Length; k++) reminder += $"{low[k]}";

                            low = reminder;

                            count++;
                            if (result.Item2.Length > 0)
                                results += $"{count})**|({result.Item2})|** {reminder} \n";
                            else
                                results += $"{count}) {reminder} \n";


                            i = -1;
                            numberString = "";
                            numberString2 = "";
                            check = 0;
                            sign = 'k';
                        }
                        else
                        {
                            reminder = "";


                            for (var k = answer.ToString().Length + 1; k < low.Length; k++)
                                reminder += $"{low[k]}";

                            reminder += $"{low[answer.ToString().Length]}{answer}";

                            low = reminder;

                            i = -1;
                            numberString = "";
                            numberString2 = "";
                            check = 0;
                            sign = 'k';
                            doi = 0;
                        }
                    }
                    else if (low[i] == 'd')
                    {
                        check++;
                        doi++;
                        if (check <= 1)
                            sign = 'd';
                    }
                    else if (low[i] == '+')
                    {
                        check++;
                        if (check <= 1)
                            sign = '+';
                    }
                    else if (low[i] == '-')
                    {
                        check++;
                        if (check <= 1)
                            sign = '-';
                    }
                    else if (low[i] == '*')
                    {
                        check++;
                        if (check <= 1)
                            sign = '*';
                    }
                    else if (low[i] == '/')
                    {
                        check++;
                        if (check <= 1)
                            sign = '/';
                    }
                    else
                    {
                        check++;
                    }
                }
            }

            await CommandHandeling.ReplyAsync(Context, $"It's a {answer}!");
        }

         [Command("testRandom", RunMode = RunMode.Async)]
         [RequireOwner]
        public async Task TestRandom(int times, int max, int heimerdonger)
        {
            var repeat = 0;
            var consecutiveNumbers = 0;
            var repeatSecure = 0;
            var consecutiveSecure = 0;
            var repeatNormal = 0;
            var consecutiveNormal = 0;
            var distinct1 = 0;
            var distinctSecure1 = 0;
            var distinctNormal1 = 0;

            for (var k = 0; k < heimerdonger; k++)
            {
                int[] numbers = new int[times];
                int[] numbersSecure = new int[times];
                int[] numbersNormal = new int[times];

                var randomS2 = new Random(Guid.NewGuid().GetHashCode());
                var _random = new Random();
                for (var i = 0; i < times; i++)
                {
                    var randomNumber = randomS2.Next(max); //Random(Guid.NewGuid().GetHashCode())

                    var randomSecure = _secureRandom.Random(1, max);

                    var randomNormal = _random.Next(max); //Random();


                    if (numbers.Any(n => n == randomNumber))
                    {
                        repeat++;
                    }

                    if (numbersSecure.Any(n => n == randomSecure))
                    {
                        repeatSecure++;
                    }

                    if (numbersNormal.Any(n => n == randomNormal))
                    {
                        repeatNormal++;
                    }


                    numbers[i] = randomNumber;
                    numbersSecure[i] = randomSecure;
                    numbersNormal[i] = randomNormal;

                    if (i <= 0) continue;
                    if (numbers[i - 1] == numbers[i])
                    {
                        consecutiveNumbers++;
                    }

                    if (numbersSecure[i - 1] == numbersSecure[i])
                    {
                        consecutiveSecure++;
                    }

                    if (numbersNormal[i - 1] == numbersNormal[i])
                    {
                        consecutiveNormal++;
                    }
                }


                var distinct = numbers.Distinct().Count();
                distinct1 += distinct;
                var distinctNormal = numbersNormal.Distinct().Count();
                distinctNormal1 += distinctNormal;
                var distinctSecure = numbersSecure.Distinct().Count();
                distinctSecure1 += distinctSecure;
                //var message = String.Join(" ", ordered);
            }


            var winner = "";
            if (repeat < repeatSecure && repeat < repeatNormal)
                winner = "Random(Guid.NewGuid().GetHashCode())";
            if ( repeatSecure < repeat && repeatSecure < repeatNormal)
                winner = "SecureRandom";
            if (repeatNormal < repeatSecure && repeatNormal < repeat)
                winner = "Random()";


            var embed = new EmbedBuilder()
                .WithTitle($"heimerdonger = {heimerdonger}")
                .AddField("Random(Guid.NewGuid().GetHashCode())", $"**Repeated numbers**: {repeat}\n" +
                                                                  $"**Distinct numbers**:{distinct1}\n" +
                                                                  $"**Consecutive numbers**: {consecutiveNumbers}")
                .AddField("SecureRandom", $"**Repeated numbers**: {repeatSecure}\n" +
                                          $"**Distinct numbers**:{distinctSecure1}\n" +
                                          $"**Consecutive numbers**: {consecutiveSecure}")
                .AddField("Random()", $"**Repeated numbers**: {repeatNormal}\n" +
                                      $"**Distinct numbers**:{distinctNormal1}\n" +
                                      $"**Consecutive numbers**: {consecutiveNormal}")
                .AddField("THE WINNER:", $"{winner}!!!!! ( by lowest Repeated numbers)");
           
            await Context.Channel.SendMessageAsync(embed: embed.Build());
        }
        
        public (int, string ) Calculator(int times, int num, char sign)
        {
            switch (sign)
            {
                case 'm':
                    break;
                case 'l':
                    break;
                case '+':
                    return (times + num, "");
                case '-':
                    return (times - num, "");
                case '*':
                    return (times * num, "");
                case '/':
                    return (times / num, "");
                case 'd':
                    var result = new List<int>();

                    var answer = 0;

                    if (times <= 0 || num <= 0)
                        return (0, "error");
                    if (times == 1)
                        return (_secureRandom.Random(1, num), "");

                    var resultString = "";
                    if (times <= 1000 && num <= 120)
                    {
                        for (var i = 0; i < times; i++)
                        {
                            var lol = _secureRandom.Random(1, num);
                            result.Add(lol);
                        }

                        for (var i = 0; i < result.Count; i++)
                        {
                            answer += result[i];
                            resultString += $"{result[i]}";
                            if (i < result.Count - 1)
                                resultString += $" + ";
                        }
                    }

                    return (answer, resultString);
            }

            return (0, "error");
        }
    }
}