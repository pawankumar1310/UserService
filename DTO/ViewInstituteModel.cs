namespace Dto
{

 public class ViewInstitutionModel
    {
        public string InstitutionID { get; set; }
        public string Name { get; set; }
        public string statusReferenceID { get; set; }
        public bool IsDeleted { get; set; }
        public string additionalAddress { get; set; }
        public bool IsDeletable { get; set; }
        public string UTLzipcodeID { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }

}