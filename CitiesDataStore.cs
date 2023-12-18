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
                poinOfIntrest = new List<PoinOfIntrestDTO>()
                {
                    new PoinOfIntrestDTO(){
                    Id= 2,
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
                   poinOfIntrest = new List<PoinOfIntrestDTO>()
                {
                    new PoinOfIntrestDTO(){
                    Id= 5,
                    Name= "Bengazy",
                    Description = "The Cpital The second"
                    },
                    new PoinOfIntrestDTO(){
                    Id= 6,
                    Name= "Troopl",
                    Description = "The Cpital The second"
                    }

                },
                }, new CityDto() {
                Id= 3,
                Name= "Tarhuna",
                Description = "My home land",
                   poinOfIntrest = new List<PoinOfIntrestDTO>()
                {
                    new PoinOfIntrestDTO(){
                    Id= 7,
                    Name= "Bengazy",
                    Description = "The Cpital The second"
                    },
                    new PoinOfIntrestDTO(){
                    Id= 8,
                    Name= "Benssgazy",
                    Description = "The Cpital The second"
                    }

                },
                }
            };
        }
    }
}
