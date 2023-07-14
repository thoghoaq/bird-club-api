using Firebase.Database;
using Firebase.Database.Query;

namespace BirdClubAPI.BusinessLayer.Helpers
{
    public class Notification
    {
        public string Title { get; set; } = null!;
        public string Message { get; set; } = null!;
        public DateTime DateTime { get; set; }
        public bool IsRead { get; set; }
    }

    public class FirebaseHelper
    {
        private const string firebaseDatabaseUrl = "https://birdclub-dbd3d-default-rtdb.asia-southeast1.firebasedatabase.app";

        public static async Task Write(int userId, Notification notification)
        {
            try
            {
                notification.IsRead = false;
                notification.DateTime = DateTime.Now;
                var firebaseClient = new FirebaseClient(firebaseDatabaseUrl);
                await firebaseClient.Child("users").Child(userId.ToString()).Child("notifications").PostAsync(notification);
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
