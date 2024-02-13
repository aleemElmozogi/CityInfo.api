using CityInfo.api.Models;

namespace CityInfo.api
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get;set; }

        public static CitiesDataStore Current { get; } = new CitiesDataStore();

        public CitiesDataStore() {
            Cities = new List<CityDto>() { 
                new CityDto() { 
                Id= 1,
                Name= "Tripoli",
                Description = "The Cpital",
                pointOfInrests = new List<PoinOfIntrestDTO>()
                {
                    new PoinOfIntrestDTO(){
                    Id= 1,
                    Name= "Bengazy",
                    Description = "The Cpital The second"
                    },
                    new PoinOfIntrestDTO(){
                    Id= 2,
                    Name= "Bengazy",
                    Description = "The Cpital The second"
                    }

                },
                },
                 new CityDto() {
                Id= 2,
                Name= "Bengazy",
                Description = "The Cpital The second",
                   pointOfInrests = new List<PoinOfIntrestDTO>()
                {
                    new PoinOfIntrestDTO(){
                    Id= 1,
                    Name= "Bengazy",
                    Description = "The Cpital The second"
                    },
                    new PoinOfIntrestDTO(){
                    Id= 2,
                    Name= "Troopl",
                    Description = "The Cpital The second"
                    }

                },
                }, new CityDto() {
                Id= 3,
                Name= "Tarhuna",
                Description = "My home land",
                   pointOfInrests = new List<PoinOfIntrestDTO>()
                {
                    new PoinOfIntrestDTO(){
                    Id= 1,
                    Name= "Bengazy",
                    Description = "The Cpital The second"
                    },
                    new PoinOfIntrestDTO(){
                    Id= 2,
                    Name= "Benssgazy",
                    Description = "The Cpital The second"
                    }

                },
                }
            };
        }
    }
}
