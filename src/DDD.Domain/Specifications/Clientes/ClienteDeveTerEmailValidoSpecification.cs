using DomainValidation.Interfaces.Specification;
using DDD.Domain.Entity;
using DDD.Domain.Validations;

namespace DDD.Domain.Specifications.Clientes
{
    public class ClienteDeveTerEmailValidoSpecification : ISpecification<Cliente>
    {
        public bool IsSatisfiedBy(Cliente cliente)
        {
            return EmailValidation.Validar(cliente.Email);
        }
    }
}