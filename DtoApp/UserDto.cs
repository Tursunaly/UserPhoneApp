namespace UserPhoneApp.DtoApp
{
    public class UserDto
    {
        public Guid id { get; set; }
        public string name { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public DateTime DateBirth { get; set; }
    }
    public class CreateUserDto
    {
        public string name { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public DateTime DateBirth { get; set; }
    }
    public class UpdateUserDto
    {
        public string name { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public DateTime DateBirth { get; set; }
    }
}
