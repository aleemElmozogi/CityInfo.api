namespace CityInfo.api.Models
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public IList<PoinOfIntrestDTO> pointOfInrests { get; set; } = new List<PoinOfIntrestDTO>();
        public int NumberOfPOintOfIntrest { get { return pointOfInrests.Count; } }
    }
}
