namespace LipheBot.Infra.Twitch
{
    public class TwitchClientSettings
    {
        public string TwitchUsername { get; set; }
        //public string TwitchChannelId { get; set; }
        //public string TwitchBotUserId { get; set; }
        public string TwitchBotOAuth { get; set; }
       // public string TwitchChannelOAuth { get; set; }
        public string TwitchChannel { get; set; }
        //public string TwitchClientId { get; set; }

        public TwitchClientSettings(string twitchusername, string twitchbotOauth,string twitchChannel)
        {
            this.TwitchUsername = twitchusername;
            this.TwitchBotOAuth = twitchbotOauth;
            this.TwitchChannel = twitchChannel;
        }
    }
}
