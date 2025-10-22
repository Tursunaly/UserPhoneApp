namespace UserPhoneApp.DtoApp
{
    public class PhoneDto
    {
        public Guid id { get; set; }
        public int number { get; set; }
        public Guid userId { get; set; }
    }
    public class CreatePhoneDto
    {
        public int number { get; set; }
        public Guid userId { get; set; }
    }
    public class UpdatePhoneDto
    {
        public int number { get; set; }
        public Guid userId { get; set; }
    }

}
