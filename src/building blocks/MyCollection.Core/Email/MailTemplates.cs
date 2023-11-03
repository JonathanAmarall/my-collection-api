namespace MyCollection.Core.Email
{
    public static class MailTemplates
    {
        public static (string subject, string body) CreateRentedMessageBorrowerEmail(string rentedItemTitle, DateTime dueDate, string fullName, string emailTo)
            => ($"Welcome to My Collection {fullName}! 🎉",
                $"Congratulations! You have rented item {rentedItemTitle}," +
                $"Don't forget to make the return by {dueDate.ToShortDateString()}" +
                Environment.NewLine +
                Environment.NewLine +
                $"You have registered with the email {emailTo}.");

        public static (string subject, string body) CreateWelcomeBorrowerMail(string emailTo, string fullName)
           => ("Welcome to My Collection! 🎉",
               $"\"Welcome to My Collection {fullName}" +
               Environment.NewLine +
               Environment.NewLine +
               $"You have registered with the email {emailTo}.");
    }
}
