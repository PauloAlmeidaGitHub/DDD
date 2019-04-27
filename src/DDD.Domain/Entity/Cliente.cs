using System;
using System.Collections.Generic;
using DomainValidation.Validation;
using DDD.Domain.Validations.Clientes;

namespace DDD.Domain.Entity
{
    public class Cliente
    {
        public Cliente()
        {
            ClienteId = Guid.NewGuid();
            Enderecos = new List<Endereco>();
        }

        public Guid ClienteId { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
        public virtual ICollection<Endereco> Enderecos { get; set; }

        /*
        //Usado pra ver se a entidade está ou não
        public ValidationResult ValidationResult { get; set; }

        public bool Valido()
        {
           ValidationResult = new ClienteEstaConsistenteValidation().Validate(this);
            return ValidationResult.IsValid;
        }
        */
    }
}