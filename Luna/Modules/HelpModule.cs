using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Microsoft.Extensions.Configuration;

namespace Luna.Modules
{
    [Name("Help")]
    public class HelpModule : ModuleBase<SocketCommandContext>
    {
        private readonly CommandService _commands;
        private readonly IConfigurationRoot _config;

        public HelpModule(CommandService commands, IConfigurationRoot config)
        {
            _commands = commands;
            _config = config;
        }

        [Command("Help")]
        public async Task HelpAsync()
        {
            string prefix = _config["prefix"];
            var builder = new EmbedBuilder()
            {
                Color = new Color(114, 137, 218),
                Description = "These are the available commands"
            };

            foreach (var module in _commands.Modules)
            {
                string description = "";
                foreach (var cmd in module.Commands)
                {
                    var result = await cmd.CheckPreconditionsAsync(Context);
                    if (result.IsSuccess)
                    {
                        string parameters = "";
                        foreach (var param in cmd.Parameters)
                        {
                            parameters += param.Summary + " ";
                        }
                        description += $"**{prefix}{cmd.Aliases.FirstOrDefault()}** {parameters}\n" +
                                       $"\t{cmd.Summary}\n";
                    }
                }

                if (!string.IsNullOrWhiteSpace(description))
                {
                    builder.AddField(x =>
                    {
                        x.Name = module.Name;
                        x.Value = description;
                        x.IsInline = false;
                    });
                }
            }

            await ReplyAsync("", false, builder.Build());
        }

    }
}
