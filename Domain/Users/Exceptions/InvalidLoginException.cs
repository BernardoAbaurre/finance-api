namespace finance_api.Domain.Users.Exceptions
{
    public class InvalidLoginException : ArgumentException
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public InvalidLoginException(string email, string password) : base("Invalid e-mail or password")
        {
            Email = email;
            Password = password;
        }

        public InvalidLoginException(Exception innerException, string email, string password) : base("Invalid e-mail or password", innerException)
        {
            Email = email;
            Password = password;
        }
    }
}
