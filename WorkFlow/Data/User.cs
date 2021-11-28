namespace WorkFlow.Data
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }

    public enum UserRole
    {
        USER,
        ADMIN
    }
}