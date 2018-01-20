using Discord;
using Discord.Commands;
using System.Linq;
using System.Threading.Tasks;

namespace StreetDragon.Modules
{
    public class Help : ModuleBase<SocketCommandContext>
    {
        private readonly CommandService _service;

        public Help(CommandService service)
        {
            _service = service;
        }

        [Command("help"),Alias("Help")]
        [Summary("The command you just called : displays help.")]
        public async Task HelpAsync()
        {
            var builder = new EmbedBuilder()
            {
                Color = new Color(114, 137, 218),
                Description = "These are the commands you can use:",
            };

            foreach (var module in _service.Modules)
            {
                string description = null;
                string name = null;
                foreach (var cmd in module.Commands)
                {
                    var result = await cmd.CheckPreconditionsAsync(Context);
                    if (result.IsSuccess)
                    {
                        name += $"!{module.Name}/!{cmd.Aliases.ElementAt(1)}";
                        description += $"{cmd.Summary}\n";
                    }   
                }

                if (!string.IsNullOrWhiteSpace(description))
                {
                    builder.AddField(x =>
                    {
                        x.Name = name;
                        x.Value = description;
                        x.IsInline = true;
                    });
                }
            }
            await ReplyAsync("", false, builder.Build());
        }
    }
}
