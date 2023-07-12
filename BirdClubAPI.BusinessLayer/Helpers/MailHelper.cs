using System.Net.Http.Headers;

using System.Text;
namespace BirdClubAPI.BusinessLayer.Helpers
{
    public class MailHelper
    {
        public static async Task SendEmail(string email, string subject, string txtMessage, string verificationLink)
        {
            // Setup
            var _emailSettings = new EmailSettings
            {
                ApiKey = "38a60536f7da5078f214dac1a47ce08b-262b213e-fe5ef3b7",
                DisplayName = "BirdClub Support",
                From = "support@birdclub.com",
                ReplyTo = "noreply@birdclub.com",
                EmailDomain = "sandbox6d786bd2bad641d48e67d7b67a9c8ad5.mailgun.org",
            };

            string templateFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Helpers/template.html");
            string templateContent = File.ReadAllText(templateFilePath);
            string htmlMessage = templateContent.Replace("[VERIFICATION_URL]", verificationLink);

            // Send
            using (var httpClient = new HttpClient())
            {
                var authToken = Encoding.ASCII.GetBytes($"api:{_emailSettings.ApiKey}");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));

                var formContent = new FormUrlEncodedContent(new Dictionary<string, string> {
                    { "from", $"{_emailSettings.DisplayName} <{_emailSettings.From}>" },
                    { "h:Reply-To", $"{_emailSettings.DisplayName} <{_emailSettings.ReplyTo}>" },
                    { "to", email },
                    { "subject", subject },
                    { "text", txtMessage },
                    { "html", htmlMessage }
                });

                var result = await httpClient.PostAsync($"https://api.mailgun.net/v3/{_emailSettings.EmailDomain}/messages", formContent);
                result.EnsureSuccessStatusCode();
            }
        }
    }
}
