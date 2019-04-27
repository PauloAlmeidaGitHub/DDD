using System;
using System.Collections.Generic;
using DDD.Domain.Entity;
using DDD.Domain.Interfaces.Repository;
using DDD.Domain.Interfaces.Services;

// Quando DomainValidation estiver implementada (Referenciada)
using DDD.Domain.Validations.Clientes;

namespace DDD.Domain.Services
{
    public class ClienteService : IClienteService  // Usa a Classe de repositório
    {
        private readonly IClienteRepository _clienteRepository;
        public ClienteService(IClienteRepository clienteRepository) //Injeta a dependência no construtor da classe que implementa Cliente Repository
        {
            _clienteRepository = clienteRepository;
        }


        public Cliente Adicionar(Cliente cliente)
        {
            /*
            //Quando DomainValidation estiver implementada (Referenciada)
            if (!cliente.Valido()) return cliente;

            //Obtém o Validation Result
            cliente.ValidationResult = new ClienteAptoParaCadastroValidation(_clienteRepository).Validate(cliente);
            if (!cliente.ValidationResult.IsValid) return cliente;
            */

            return _clienteRepository.Adicionar(cliente);
        }

        public Cliente ObterPorId(Guid id)
        {
            return _clienteRepository.ObterPorId(id);
        }

        public IEnumerable<Cliente> ObterTodos()
        {
            return _clienteRepository.ObterTodos();
        }

        public Cliente ObterPorCpf(string cpf)
        {
            return _clienteRepository.ObterPorCpf(cpf);
        }

        public Cliente ObterPorEmail(string email)
        {
            return _clienteRepository.ObterPorEmail(email);
        }

        public Cliente Atualizar(Cliente cliente)
        {
            return _clienteRepository.Atualizar(cliente);
        }

        public void Remover(Guid id)
        {
            _clienteRepository.Remover(id);
        }

        public void Dispose()
        {
            _clienteRepository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
