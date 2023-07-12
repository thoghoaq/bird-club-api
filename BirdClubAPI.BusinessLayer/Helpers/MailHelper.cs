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
                ApiKey = "c66d62178ed4a12eb58db7018c17d289-262b213e-d7a2d2cd",
                DisplayName = "BirdClub Support",
                From = "support@birdclub.com",
                ReplyTo = "noreply@birdclub.com",
                EmailDomain = "sandboxe88483b963bd49cbaa4f051c09a4144e.mailgun.org",
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
