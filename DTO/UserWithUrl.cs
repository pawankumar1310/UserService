namespace Dto
{
    public class UserWithUrl
    {
        public string Name { get; set; }
        public string CountryID { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string ZipCodeID { get; set; }

       public List<UpdateUrlModel> UserUrls { get; set; }
    }
}
