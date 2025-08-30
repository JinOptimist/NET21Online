namespace WebPortal.DbStuff.Models
{
    public class CommentEntity: BaseModel
    {
        public string Message { get; set; } = string.Empty;
        public decimal Mark { get; set; } = decimal.Zero;
        public DateTime CreatedAt { get; set; }

        public int GuitarId { get; set; }
        public virtual GuitarEntity Guitar { get; set; }

        public int AuthorId { get; set; }
        public virtual User Author { get; set; }

    }
}
