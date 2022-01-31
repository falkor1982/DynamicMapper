using ModelExample;
using ModelExample.Managers;
using System.Collections.Generic;
using DynamicMapper;
using System.Threading;

namespace DynamicMapperExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            ListExamples();
            ObjectExamples();
        }

        private static void ListExamples()
        {
            string cultureNameEs = "es-AR";
            string cultureNameEn = "en";

            List<Person> allPersons = PersonManager.GetAll();

            //Example 1
            List<DTOPerson> allPersonsV1 = Mapper.MapListToDTO<DTOPerson, Person>(allPersons, cultureNameEs);


            //Example 2
            List<DTOPerson> allPersonsV2 = Mapper.MapListToDTO<DTOPerson, Person>(allPersons, Mapper.MappingOptionFlags.MapCurrentEntityAndChildFlatTypes, cultureNameEn);


            List<string> fillPropertyList = new List<string>() { "Residence", "FavouriteSport", "FavouriteSport.PlayedBy" };

            //Example 3
            List<DTOPerson> allPersonsV3 = Mapper.MapListToDTO<DTOPerson, Person>(allPersons, fillPropertyList, cultureNameEs);


            //Example 4
            //For this example to work,  in DTOCustomer class rename property "FavouriteSport" to "LovedSport", and "PlayedBy" to "LovedBy" in DTOSport class
            List<string> fillPropertyListRenamed = new List<string>() { "Residence", "LovedSport", "LovedSport.LovedBy" };
            List<DTOPerson> allPersonsV4 = Mapper.MapListToDTO<DTOPerson, Person>(allPersons, GetDtoTypePropertyToEntityPropertyMatching(), fillPropertyListRenamed, cultureNameEn);
        }

        private static void ObjectExamples()
        {
            //Mapping one object has identical parameters options than mapping lists
            string cultureName = "es";
            Country country = CountryManager.Get(2);
            List<string> fillPropertyList = new List<string>() { "Residents", "Residents.FavouriteSport" , "Residents.FavouriteSport.PlayedBy" };
            DTOCountry dTOCountry = Mapper.MapToDTO<DTOCountry, Country>(country, cultureName, fillPropertyList);
        }


        private static Dictionary<string, string> GetDtoTypePropertyToEntityPropertyMatching()
        {
            Dictionary<string, string> dtoTypePropertyToEntityPropertyMatching = new Dictionary<string, string>();
            dtoTypePropertyToEntityPropertyMatching.Add("LovedSport", "FavouriteSport");
            dtoTypePropertyToEntityPropertyMatching.Add("LovedSport.LovedBy", "FavouriteSport.PlayedBy");
            return dtoTypePropertyToEntityPropertyMatching;
        }
    }
}





