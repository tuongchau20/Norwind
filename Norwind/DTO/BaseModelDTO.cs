using Norwind.Models;

namespace Norwind.DTO
{
    public class BaseModelDTO 
    {
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
