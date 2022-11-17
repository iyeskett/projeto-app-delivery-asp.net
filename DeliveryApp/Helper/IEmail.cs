namespace DeliveryApp.Helper
{
    public interface IEmail
    {
        public bool SendEmail(string email, string subject, string message);
    }
}
