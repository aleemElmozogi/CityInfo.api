
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInfo.api.Entities
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(200)]
        public string? Description { get; set; }
        public IList<PointOfInrest> pointOfInrests { get; set; } = new List<PointOfInrest>();
        public City(string name) { Name = name; }
    }
}
