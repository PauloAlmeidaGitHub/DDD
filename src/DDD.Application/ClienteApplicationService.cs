using System;
using System.Collections.Generic;
using AutoMapper;
using DDD.Application.Interfaces;
using DDD.Application.ViewModels;
using DDD.Domain.Entity;

using DDD.Infrastructure.Data.UoW;  //(Quando tem UoW)

//using DDD.Domain.Interfaces.Repository; ANTES
using DDD.Domain.Interfaces.Services;

//using DDD.Infrastructure.Data.Repository; ANTES

namespace DDD.Application
{
    public class ClienteApplicationService : IClienteApplicationService
    {
        //Usamos aqui a Interface IClienteRepository porque a classe ClienteRepository usada implementa este IClienteRepository
        //=========================================================================================================================================
        //ANTIGAMENTE SEM A DI
        //private readonly IClienteRepository _clienteRepository; 
        //public ClienteApplicationService()
        //{
        //    _clienteRepository = new ClienteRepository();
        //}


        //=========================================================================================================================================
        //ATUALMENTE COM A DI
        private readonly IClienteService _clienteService;

        //SEM UoW
        //===================================================================
        //public ClienteApplicationService(IClienteService clienteRepository) 
        //{
        // _clienteService = clienteRepository;
        //}
        //===================================================================


        //COM UoW
        //===================================================================
        private readonly IUnitOfWork _uow; // Vai precisar do using DDD.Infrastructure.Data.UoW;
        public ClienteApplicationService(IClienteService clienteRepository, IUnitOfWork uow) 
        {
            _clienteService = clienteRepository;
            _uow = uow;
        }
        //=========================================================================================================================================



        public ClienteEnderecoViewModel Adicionar(ClienteEnderecoViewModel clienteEnderecoViewModel)
        {
            var cliente = Mapper.Map<Cliente>(clienteEnderecoViewModel);
            var endereco = Mapper.Map<Endereco>(clienteEnderecoViewModel);
            cliente.Enderecos.Add(endereco);

            //Agora é _clienteService (que é um Serviço de Domínio) quem vai acessar o Repository pra todos os métodos (antes era _clienteRepository)
            _clienteService.Adicionar(cliente);

            //Add Log
            //Addregistroadicional

            //Se está tudo ok
            _uow.Commit();  //SE ESTIVER USANDO UOW
            
            return clienteEnderecoViewModel;
        }

        public ClienteViewModel ObterPorId(Guid id)
        {
            return Mapper.Map<ClienteViewModel>(_clienteService.ObterPorId(id));
        }

        public IEnumerable<ClienteViewModel> ObterTodos()
        {
            return Mapper.Map<IEnumerable<ClienteViewModel>>(_clienteService.ObterTodos());
            //O EF traz um objeto complexo e isso pro AutoMapper pode ser significativo no momento de realizar o mapeamento
            //Então, para ler usaremos o Dapper.
        }

        public ClienteViewModel ObterPorCPF(string cpf)
        {
            return Mapper.Map<ClienteViewModel>(_clienteService.ObterPorCpf(cpf));
        }

        public ClienteViewModel ObterPorEmail(string email)
        {
            return Mapper.Map<ClienteViewModel>(_clienteService.ObterPorEmail(email));
        }

        public ClienteViewModel Atualizar(ClienteViewModel clienteViewModel)
        {
            var cliente = Mapper.Map<Cliente>(clienteViewModel);
            _clienteService.Atualizar(cliente);
            return clienteViewModel;
        }

        public void Remover(Guid id)
        {
            _clienteService.Remover(id);
        }

        public void Dispose()
        {
            _clienteService.Dispose();
        }
    }
}