using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
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
    class Program
    {
        public CommandService commands;
        public DiscordSocketClient client;
        private IServiceProvider services;
        static void Main(string[] args)
        {
            new Program().Start().GetAwaiter().GetResult();
        }
        public async Task Start()
        {
            client = new DiscordSocketClient();
            commands = new CommandService();
            services = new ServiceCollection().BuildServiceProvider();
            client.Log += Log;
            await CommandRegiester();
            await client.LoginAsync(TokenType.Bot, "MjM1ODUyMDQ2NDQ2NzU1ODQw.DTABfg.0CsKUEZG8_68Ll0I1cusHLAxXrA");
            await client.StartAsync();


            await Task.Delay(-1);
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
