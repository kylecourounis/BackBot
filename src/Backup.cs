namespace BackBot
{
    using System;
    using System.IO;
    using System.Linq;

    using DSharpPlus.Entities;

    internal class Backup
    {
        /// <summary>
        /// Saves the specified message.
        /// </summary>
        /// <param name="Message">The message.</param>
        internal static void Save(DiscordMessage Message)
        {
            Console.WriteLine();

            Console.WriteLine("Saving the latest message...");
            File.AppendAllText("Backups/Messages.txt", $"{Message.Author.Username},{Message.Author.AvatarUrl},{Message.ChannelId},{Message.Content},{Message.Timestamp}" + Environment.NewLine);
            Console.WriteLine("Saved the latest message.");

            Console.WriteLine();
        }

        /// <summary>
        /// Restores the messages.
        /// </summary>
        internal static void RestoreMessages()
        {
            try
            {
                foreach (var Line in File.ReadAllLines("Backups/Messages.txt"))
                {
                    var Message = Line.Split(',');

                    var Channel = DiscordBot.Discord.GetChannelAsync(ulong.Parse(Message[2])).Result;

                    var Author = new DiscordEmbedBuilder.EmbedAuthor
                    {
                        Name = Message[0],
                        IconUrl = Message[1]
                    };

                    var Time = DateTimeOffset.Parse(Message[4]);

                    var Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = Time.Date.ToLongDateString()
                    };

                    var Embed = new DiscordEmbedBuilder
                    {
                        Author = Author,
                        Description = Environment.NewLine + Message[3] + Environment.NewLine,
                        Color = DiscordColor.CornflowerBlue,
                        Footer = Footer
                    };

                    Channel.SendMessageAsync(embed: Embed);
                }
            }
            catch (Exception Exception)
            {
                Console.WriteLine();
                Console.WriteLine($"{Exception.GetType()} : {Exception.Message}");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Removes the specified message.
        /// </summary>
        /// <param name="Message">The message.</param>
        internal static void Remove(DiscordMessage Message)
        {
            Console.WriteLine();
            Console.WriteLine($"Deleting '{Message.Content}' from {Message.Author.Username}...");
            
            var Text = File.ReadAllText("Backups/Messages.txt");
            var Lines = File.ReadAllLines("Backups/Messages.txt").ToList();

            File.WriteAllText("Backups/Messages.tmp", Text);
            var TmpText = File.ReadAllText("Backups/Messages.tmp");

            var Line = Lines.Find(Value => Value.Contains($"{Message.Author.Username},{Message.Author.AvatarUrl},{Message.ChannelId},{Message.Content}"));

            File.WriteAllText("Backups/Messages.txt", TmpText.Replace(Line, string.Empty));
            File.Delete("Backups/Messages.tmp");

            Console.WriteLine("Deleted the message.");
            Console.WriteLine();
        }
    }
}
