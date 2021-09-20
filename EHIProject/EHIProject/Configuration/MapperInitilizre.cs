using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EHIProject.Data;
using EHIProject.Model;

namespace EHIProject.Configuration
{
    public class MapperInitilizre : Profile
    {
        public MapperInitilizre()
        {
            CreateMap<Contact, ContactDTO>().ReverseMap();
            CreateMap<Contact, CreateContactDTO>().ReverseMap();
        }
    }
}
