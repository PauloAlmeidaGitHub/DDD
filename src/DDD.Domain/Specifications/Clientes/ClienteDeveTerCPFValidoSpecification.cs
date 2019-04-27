using DomainValidation.Interfaces.Specification;
using DDD.Domain.Entity;
using DDD.Domain.Validations;

namespace DDD.Domain.Specifications.Clientes
{
    public class ClienteDeveTerCPFValidoSpecification : ISpecification<Cliente>
    {
        public bool IsSatisfiedBy(Entity.Cliente cliente)
        {
            return CPFValidation.Validar(cliente.CPF);
        }
    }
}
