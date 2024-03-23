namespace Dto
{
    public class UpdateInstitutionModel
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? ZipcodeID { get; set; }
        public string? UpdatedBy { get; set; }
        public string? url {  get; set; }
        public List<string>? institutionFacility {  get; set; }
        public List<string>? institutionGovernance {  get; set; }



    }
}
