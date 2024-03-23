namespace Dto
{
    public class AddInstituteModel
    {
       
        public string Name { get; set; }

        public string Address { get; set; }

        public string ZipcodeID { get; set; }
        public string CreatedBy { get; set; }

        public string url {  get; set; }    
        public List<string> institutionFacilities { get; set; }

        public List<string> institutionGovernance { get; set; }

    }
    

}