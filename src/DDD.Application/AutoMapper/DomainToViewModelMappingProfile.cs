using AutoMapper;
using DDD.Application.ViewModels;
using DDD.Domain.Entity;

namespace DDD.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Cliente, ClienteViewModel>();
            CreateMap<Cliente, ClienteEnderecoViewModel>();

            CreateMap<Endereco, ClienteEnderecoViewModel>();
            CreateMap<Endereco, EnderecoViewModel>();
        }
    }
}