namespace BackBot
{
    using System.Threading.Tasks;

    using DSharpPlus;
    using DSharpPlus.Entities;
    using DSharpPlus.EventArgs;

    internal static class DiscordBot
    {
        private static DiscordClient Discord;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="DiscordBot"/> has been initialized.
        /// </summary>
        internal static bool Initialized
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscordBot"/> class.
        /// </summary>
        internal static async void Initialize()
        {
            if (DiscordBot.Initialized)
            {
                return;
            }
            
            var Configuration = new DiscordConfiguration
            {
                Token = "NDUzNTcxODc5OTE2MDExNTQw.Dfg1Pw.V3MuemM0HEAsWhyG866o8VeA0KM"
            };

            DiscordBot.Discord = new DiscordClient(Configuration);
            
            DiscordBot.Discord.MessageCreated += DiscordBot.OnMessageSent;
            DiscordBot.Discord.MessageDeleted += DiscordBot.OnMessageDeleted;

            await DiscordBot.Discord.ConnectAsync();
            await DiscordBot.Discord.UpdateStatusAsync(user_status: UserStatus.Online);

            DiscordBot.Initialized = true;
        }

        /// <summary>
        /// Raised when a message has been sent.
        /// </summary>
        /// <param name="Args">The <see cref="MessageCreateEventArgs"/> instance containing the event data.</param>
        private static Task OnMessageSent(MessageCreateEventArgs Args)
        {
            if (Args.Author.Username != "BackBot")
            {
                Backup.Save(Args.Message);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Raised when a message has been deleted.
        /// </summary>
        /// <param name="Args">The <see cref="MessageDeleteEventArgs"/> instance containing the event data.</param>
        private static Task OnMessageDeleted(MessageDeleteEventArgs Args)
        {
            // TODO: Fix removal system.
            // Backup.Remove(Args.Message);

            return Task.CompletedTask;
        }
    }
}

