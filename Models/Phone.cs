namespace UserPhoneApp.Models
{
    public class Phone
    {
        public Guid id { get; set; }
        public int number { get; set; }

        public Guid userId { get; set; }
    }
}
