namespace BirdClubAPI.BusinessLayer.Helpers
{
    internal class EmailSettings
    {
        public string ApiKey { get; set; }
        public string DisplayName { get; set; }
        public string From { get; set; }
        public string ReplyTo { get; set; }
        public string EmailDomain { get; set; }
    }
}
