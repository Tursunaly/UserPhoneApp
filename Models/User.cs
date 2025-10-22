namespace UserPhoneApp.Models
{
    public class User
    {
        public Guid id { get; set; }

        public string name { get; set; }

        public string email { get; set; }

        public DateTime DateBirth { get; set; }
    }
}
