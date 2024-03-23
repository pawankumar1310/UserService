namespace Dto
{
    public class FilterByDateModel
    {
        public string institutionID { get; set; }
        public string? name {  get; set; }
        public string? iInstitutionStatusID {  get; set; }
        public bool? isDeleted {  get; set; }
        public string? address {  get; set; }
        public string? zipcodeID {  get; set; }
        public DateTime? createdDate {  get; set; }

    }
}
