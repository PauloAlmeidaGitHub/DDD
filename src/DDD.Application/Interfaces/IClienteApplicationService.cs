using System;
using System.Collections.Generic;
using DDD.Application.ViewModels;

namespace DDD.Application.Interfaces
{
    public interface IClienteApplicationService : IDisposable
    {
        //Para adicionar, recebe a entidade combinada
        ClienteEnderecoViewModel Adicionar(ClienteEnderecoViewModel clienteEnderecoViewModel);
        //Nas demais será específica da entidade em questão
        ClienteViewModel ObterPorId(Guid id);
        IEnumerable<ClienteViewModel> ObterTodos();
        ClienteViewModel ObterPorCPF(string cpf);
        ClienteViewModel ObterPorEmail(string email);
        ClienteViewModel Atualizar(ClienteViewModel clienteViewModel);
        void Remover(Guid id);
    }

}