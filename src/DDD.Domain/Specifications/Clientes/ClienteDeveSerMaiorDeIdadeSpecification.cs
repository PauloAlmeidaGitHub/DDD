using System;
using DomainValidation.Interfaces.Specification;
using DDD.Domain.Entity;

namespace DDD.Domain.Specifications.Clientes
{
    public class ClienteDeveSerMaiorDeIdadeSpecification : ISpecification<Cliente>
    {

        public bool IsSatisfiedBy(Cliente cliente)
        {
            return DateTime.Now.Year - cliente.DataNascimento.Year >= 21;
        }
    }
}