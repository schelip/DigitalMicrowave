using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalMicrowave.Business.Entities
{
    public class HeatingProcedure
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string FoodName { get; set; }
        [Required]
        public int Time { get; set; }
        [Required]
        public int PowerLevel { get; set; }
        [Required]
        [MaxLength(1)]
        [Index("HeatingStringIndex", IsUnique = true)]
        public string HeatingString { get; set; }
        public string Instructions { get; set; }
    }
}