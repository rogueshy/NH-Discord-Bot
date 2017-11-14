using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace NH_bot
{
    class Program
    {
        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();
        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _client.Log += Log;
            _client.MessageReceived += MessageReceived;
            string token = "<enter your bot token here>"; // Bot access token
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
        private DiscordSocketClient _client;
        private async Task MessageReceived(SocketMessage message)
        {
            if ((message.Embeds.Count > 0) & (message.Author.Username.Equals("<username without #>")))
            {
                ulong msgId = message.Id;
                Console.WriteLine("Got something!");
                Console.WriteLine(message.Content);
                foreach (Embed element in message.Embeds)
                {
                    Console.WriteLine("We have a "+element.Type+" here");
                    if (element.Video.HasValue)
                    {
                        await message.DeleteAsync();
                        Console.WriteLine("Video detected!");
                        await message.Channel.SendMessageAsync("В очко себе свой видос забей. :rage:");
                    }
                    else Console.WriteLine("All is good.");
                }
            }
            if (message.Content.Equals("!Ping"))
            {
                await message.Channel.SendMessageAsync("Pong!");
            }
        }
    }
}
