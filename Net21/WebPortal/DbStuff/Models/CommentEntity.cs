using WebPortal.DbStuff.Models;

namespace UnderTheBridge.Data.Models
{
    public class CommentEntity: BaseModel
    {
        public int GuitarId { get; set; }
        public GuitarEntity? Guitar { get; set; }

        public string Message { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public decimal Mark { get; set; } = decimal.Zero;
        public DateTime CreatedAt { get; set; }

    }
}
