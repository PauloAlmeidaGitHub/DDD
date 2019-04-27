using DomainValidation.Validation;
using DDD.Domain.Entity;
using DDD.Domain.Interfaces.Repository;
using DDD.Domain.Specifications.Clientes;

namespace DDD.Domain.Validations.Clientes
{
    public class ClienteAptoParaCadastroValidation : Validator<Cliente>
    {
        public ClienteAptoParaCadastroValidation(IClienteRepository clienteRepository)
        {
            var cpfDuplicado = new ClienteDevePossuirCPFUnicoSpecification(clienteRepository);
            var emailDuplicado = new ClienteDevePossuirEmailUnicoSpecification(clienteRepository);

            base.Add("cpfDuplicado", new Rule<Cliente>(cpfDuplicado, "CPF já cadastrado! Esqueceu sua senha?"));
            base.Add("emailDuplicado", new Rule<Cliente>(emailDuplicado, "E-mail já cadastrado! Esqueceu sua senha?"));
        }
    }
}