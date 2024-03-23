namespace Dto
{

    public class GetInstitution
    {
        public string institutionID {get; set; }
        public string institutionName {get; set; }
        public string zipCodeID{get; set ;}
        public string pinCode {get; set;}
        public string additionalAddress {get ;set ;}
        public string url {get; set ;}
        public List<string> governance {get; set; }
        public List<string> facilities {get; set; }

    }

}