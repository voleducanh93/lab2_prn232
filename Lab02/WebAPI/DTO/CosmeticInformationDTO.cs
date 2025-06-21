using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTO
{
    public class CosmeticInformationDTO
    {
        [Key]
        public string CosmeticId { get; set; }

        public string CosmeticName { get; set; }
        public string SkinType { get; set; }
        public string ExpirationDate { get; set; }
        public string CosmeticSize { get; set; }
        public decimal DollarPrice { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; } 

    }
}
