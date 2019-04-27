using AutoMapper;
using DDD.Application.ViewModels;
using DDD.Domain.Entity;

namespace DDD.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<ClienteViewModel, Cliente>();
            CreateMap<ClienteEnderecoViewModel, Cliente>();

            CreateMap<ClienteEnderecoViewModel, Endereco>();
            CreateMap<EnderecoViewModel, Endereco>();
        }
    }
}