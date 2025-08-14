namespace WebPortal.DbStuff.Models.Notes;

public class CdekProject : BaseModel
    {
        public string Name { get; set; }
        public string Question { get; set; }
        public int PhoneNumber { get; set; }
        public DateTime CreationTime { get; set; }
    }
