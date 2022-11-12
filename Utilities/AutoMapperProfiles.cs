using AutoMapper;
using BE_LoansApp.DTOs;
using BE_LoansApp.Entities;
using BE_LoansApp.Models;

namespace BE_LoansApp.Utilities
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ThingCreationDTO, Thing>();
            CreateMap<Thing, ThingDTO>(); // desde BD Thing hacia ThingDTO

            CreateMap<PersonCreationDTO, Person>();
            CreateMap<Person, PersonDTO>();

            CreateMap<LoanCreationDTO, Loan>();
            CreateMap<Loan, LoanDTO>();

            CreateMap<CategoryCreationDTO, Category>();
            CreateMap<Category, CategoryDTO>();

            CreateMap<ThingViewModelDTO, Thing>();
            CreateMap<Thing, ThingViewModelDTO>();

            CreateMap<CategoryViewModel, Category>();
            CreateMap<Category, CategoryViewModel>();

            CreateMap<ThingViewModel, Thing>();
            CreateMap<Thing, ThingViewModel>();

            CreateMap<ThingViewModelDTO, Category>();
            CreateMap<Category, ThingViewModelDTO>();
        }

    }
}
