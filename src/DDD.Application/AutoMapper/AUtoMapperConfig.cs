using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Application.AutoMapper
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
           Mapper.Initialize(x =>
           {
               x.AddProfile<DomainToViewModelMappingProfile>();
               x.AddProfile<ViewModelToDomainMappingProfile>();
           });   
        }
    }
}
//Globalasax => AutoMapperConfig.RegisterMappings();
//Não esquecer de, em DDD.MVC, fazer referência a DDD.Application