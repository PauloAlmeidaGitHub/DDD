using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using Dapper;
using DDD.Domain.Entity;
using DDD.Domain.Interfaces.Repository;
using System.Configuration;
using DDD.Infrastructure.Data.Context;

namespace DDD.Infrastructure.Data.Repository
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        private SqlConnection dapperConnection;
        private string connectionString;
        private string strSQL = "";

        //SEM UoW
        //==========================================================================================
        //public ClienteRepository()
        //{
        //    connectionString = ConfigurationManager.ConnectionStrings["CONNECTION"].ConnectionString;
        //}
        //==========================================================================================


        //COM UoW
        //==========================================================================================
        public ClienteRepository(EFContext context) : base(context)
        {
            connectionString = ConfigurationManager.ConnectionStrings["CONNECTION"].ConnectionString;
        }
        //==========================================================================================


        public override void Remover(Guid id)
        {
            var cliente = ObterPorId(id);
            cliente.Ativo = false;
            Atualizar(cliente);
        }

        public IEnumerable<Cliente> ObterAtivos()
        {
            return Buscar(c => c.Ativo);
        }

        // Dapper
        public override IEnumerable<Cliente> ObterTodos()
        {
            strSQL = @"SELECT * FROM Clientes";

            ////Com o Dapper exemplo EP
            ////========================================================================
            //var cn = Db.Database.Connection
            //return cn.Query<Cliente>(sql);


            //Com o Dapper (Sérgio Mendes)
            //========================================================================
            using (dapperConnection = new SqlConnection(connectionString)) { return dapperConnection.Query<Cliente>(strSQL).ToList(); }
        }

        // Dapper
        public override Cliente ObterPorId(Guid id)
        {
            strSQL = @"SELECT * FROM Clientes c LEFT JOIN Enderecos e ON c.ClienteId = e.ClienteId WHERE c.ClienteId = @pid";

            ////Com o Dapper exemplo EP
            ////========================================================================
            //var cn = Db.Database.Connection;
            //var cliente = cn.Query<Cliente, Endereco, Cliente>(strSQL,
            //    (c, e) =>
            //    {
            //        c.Enderecos.Add(e);
            //        return c;
            //    }, new { pid = id }, splitOn: "ClienteId, EnderecoId");
            //return cliente.FirstOrDefault();



            //Com o Dapper (Sérgio Mendes)
            //========================================================================
            using (dapperConnection = new SqlConnection(connectionString))
            {
                //O último parâmetro do Dapper é a sempre o tipo de dado que queremos retornar => (Cliente)
                var cliente = dapperConnection.Query<Cliente, Endereco, Cliente>(strSQL,
                    (c, e) =>  // Expressão para o Left Join Um Cliente possui 1 ou vários endereços
                    {
                        c.Enderecos.Add(e);  // Adiciona os endereços em Cliente
                        return c;  //Retorna a Entidade
                    }
                    , new { pid = id } //É um objeto anônimo
                    , splitOn: "ClienteId, EnderecoId"   //O SplitOn define o corte do objeto (cliente possui ClienteId e Endereco possui EnderecoId)
                    );


                //CONTROLE DE EXCEPTIONS
                //Essencial Parte II - 02:28:25 
                //=======================================================================================================
                ////Esta linha quando ativada é capturada em: DDD.Infrastructure.CrossCutting.MvcFilters
                
                //throw new Exception("MENSAGEM DE ERRO!");  //Consultar Views/Shared/ConfigurationErrorsException.cshtml

                ////Mostra como se pode fazer um log para Exceptions
                //=======================================================================================================


                return cliente.FirstOrDefault();  // Retorna apenas o primeiro da Lista
            }





        }

        public Cliente ObterPorCpf(string cpf)
        {
            return Buscar(c => c.CPF == cpf && c.Ativo).FirstOrDefault();
        }

        public Cliente ObterPorEmail(string email)
        {
            return Buscar(c => c.Email == email).FirstOrDefault();
        }
    }
}