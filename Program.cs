using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using System.Threading;
using System.Net.Sockets;
using System.Reflection;
using System.Net;
using Discord.WebSocket;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Discord.Commands;
using System.Collections.Specialized;
namespace SCPFBot
{
    public class Timer
    {
        public static List<TimeObject> Time = new List<TimeObject>();
    }
    public class TimeObject
    {
        public int Time { get; set; }
        public IGuildUser user { get; set; }
        public TimeObject(int time, IGuildUser user)
        {
            this.Time = time;
            this.user = user;
        }
    }
    class Program
    {
        public Timer time;
        public CommandService commands;
        public DiscordSocketClient client;
        private IServiceProvider services;
        static string key = "";
        static void Main(string[] args)
        {
            //test
            //suck test
            Console.WriteLine("Insert Key.");
            key = Console.ReadLine();
            new Program().Start().GetAwaiter().GetResult();
        }
        public async Task Start()
        {
            Thread thread = new Thread(TimerThread);
            thread.Start();
            client = new DiscordSocketClient();
            commands = new CommandService();
            services = new ServiceCollection().BuildServiceProvider();
            client.Log += Log;
            await CommandRegiester();
            await client.LoginAsync(TokenType.Bot, key);
            await client.StartAsync();


            await Task.Delay(-1);
        }
        public static void TimerThread()
        {
            while (true)
            {
                for (int i = 0; i < Timer.Time.ToArray().Length; i++)
                {
                    int time = Timer.Time[i].Time;
                    var user = Timer.Time[i].user;
                    if (time >= 1200000)
                    {
                        var role = user.Guild.Roles.FirstOrDefault(x => x.Name.ToString() == "Looking For Game");
                        user.RemoveRoleAsync(role);
                        Timer.Time.RemoveAt(i);
                    }
                    else
                    {
                        Timer.Time[i].Time += 1000;
                    }
                }
                Thread.Sleep(1000);
            }
        }
        private Task Log(LogMessage Arg)
        {
            Console.WriteLine(Arg);

            return Task.CompletedTask;
        }
        public async Task CommandRegiester()
        {
            client.MessageReceived += Commands;

            await commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }
        public async Task Commands(SocketMessage e)
        {
            var message = e as SocketUserMessage;
            if (message == null)
                return;

            var context = new SocketCommandContext(client, message);
            int argPos = 0;
            
            if (message.HasCharPrefix('&', ref argPos))
            {
                var result = await commands.ExecuteAsync(context, argPos, services);

                if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                {
                    await context.Channel.SendMessageAsync(result.ErrorReason);
                }
            }
        }
    }
}
