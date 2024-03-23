namespace Dto
{
    public class UserWithUrlsModel
    {
        public string? UserID { get; set; }
        public string? Name { get; set; }
        public string? AdditionalAddress { get; set; }
        public string? UTLzipcodeID { get; set; }
        public long? PhoneNumber { get; set; }
        public string? UTLcountryID { get; set; }

        public List<GetUrl> Urls { get; set; }
    }
}
