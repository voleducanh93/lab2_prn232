using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTO
{
    public class CosmeticCategoryDTO
    {
        [Key]
        public string CategoryId { get; set; }

        public string CategoryName { get; set; }
        public string UsagePurpose { get; set; }
        public string FormulationType { get; set; }
    }
}
