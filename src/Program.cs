namespace BackBot
{
    using System;
    
    internal class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        private static void Main()
        {
            DiscordBot.Initialize();

            Entry:
            var Key = Console.ReadKey(true).Key;

            switch (Key)
            {
                case ConsoleKey.R:
                {
                    if (DiscordBot.Initialized)
                    {
                        Backup.RestoreMessages();
                    }

                    goto Entry;
                }
                default:
                {
                    goto Entry;
                }
            }
        }
    }
}
