namespace CityInfo.api.Models
{
    public class CityOnlyDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
