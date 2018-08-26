using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Luna.Services
{
    public class LoggingService
    {
        private readonly DiscordSocketClient _discord;
        private readonly CommandService _commands;

        public LoggingService(DiscordSocketClient discord, CommandService commands)
        {
            _discord = discord;
            _commands = commands;

            _discord.Log += LogAsync;
            _commands.Log += LogAsync;
        }

        private Task LogAsync(LogMessage msg)
        {
            Console.Out.WriteLineAsync(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
