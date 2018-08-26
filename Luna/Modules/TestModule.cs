using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Luna.Data;
using Luna.Data.DomainClasses;

namespace Luna.Modules
{
    [Name("Example")]
    public class TestModule : ModuleBase<SocketCommandContext>
    {

        [Command("say"), Alias("s")]
        public async Task Say([Remainder] string text)
            => await ReplyAsync(text);

        
    }
}
