using System;
using System.Net;
using System.Web.Mvc;
using DDD.Application;
using DDD.Application.Interfaces;
using DDD.Application.ViewModels;
using DDD.Infrastructure.CrossCutting.MvcFilters;

namespace DDD.MVC.Controllers
{
    //Permissoescliente - CL, CI, CE, CD, CX (Listar, Incluir, Editar, Detalhes, eXcluir)  (CLAIMS)
    [Authorize]
    [RoutePrefix("gestao/cadastros")]  //ROTEAMENTO DO CONTROLLER
    public class ClientesController : Controller
    {
        //ANTES
        //private readonly IClienteApplicationService  _ClienteApplicationService;
        //public ClientesController()
        //{
        //    _ClienteApplicationService = new ClienteApplicationService();  
        //}


        //ClienteApplicationService espera um DI de um serviço de domínio
        private readonly IClienteApplicationService _ClienteApplicationService;
        public ClientesController(IClienteApplicationService ClienteApplicationService)
        {
            _ClienteApplicationService = ClienteApplicationService;
            // Observar que em ClienteApplicationService existe uma classe de ClienteService que reside em Domain/Services via IClienteService e usa a Infrastructure
            // ClienteService está injetado em ClienteApplicationService
            // O ClienteRepository está injetado no ClienteService
            // Cliente Repository faz o restante
            // Por isso temos as interfaces de Repository em DDD.Domain
        }

        //[AllowAnonymous] // Todos podem listar os clientes
        [ClaimsAuthorize("PermissoesCliente","CL")]
        [Route("listar-clientes")]  //ROTEAMENTO DA ACTION
        public ActionResult Index()
        {
            return View(_ClienteApplicationService.ObterTodos());
        }


        [ClaimsAuthorize("PermissoesCliente","CD")]
        [Route("{id:guid}/detalhe-cliente")]  //ROTEAMENTO DA ACTION
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //(id.Value) - Value é a property do id, quando id não é nulo
            var clienteViewModel = _ClienteApplicationService.ObterPorId(id.Value);
            if (clienteViewModel == null)
            {
                return HttpNotFound();
            }
            return View(clienteViewModel);
        }


        [ClaimsAuthorize("PermissoesCliente","CI")]
        [Route("novo_cliente")]
        public ActionResult Create()
        {
            return View();
        }

        [ClaimsAuthorize("PermissoesCliente","CI")]
        [Route("novo_cliente")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClienteEnderecoViewModel clienteEnderecoViewModel)
        {
            if (ModelState.IsValid)
            {
                _ClienteApplicationService.Adicionar(clienteEnderecoViewModel);
                return RedirectToAction("Index");
            }
            return View(clienteEnderecoViewModel);
        }


        [ClaimsAuthorize("PermissoesCliente","CE")]
        [Route("{id:guid}/editar-cliente")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var clienteViewModel = _ClienteApplicationService.ObterPorId(id.Value);
            if (clienteViewModel == null)
            {
                return HttpNotFound();
            }
            return View(clienteViewModel);
        }


        [ClaimsAuthorize("PermissoesCliente","CE")]
        [Route("{id:guid}/editar-cliente")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ClienteId,Nome,Email,CPF,DataNascimento,DataCadastro,Ativo")] ClienteViewModel clienteViewModel)
        public ActionResult Edit(ClienteViewModel clienteViewModel)
        {
            if (ModelState.IsValid)
            {
                _ClienteApplicationService.Atualizar(clienteViewModel);
                return RedirectToAction("Index");
            }
            return View(clienteViewModel);
        }

        [ClaimsAuthorize("PermissoesCliente","CX")]
        [Authorize(Roles = "admin")]
        [Route("{id:guid}/excluir-cliente")]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var clienteViewModel = _ClienteApplicationService.ObterPorId(id.Value);
            if (clienteViewModel == null)
            {
                return HttpNotFound();
            }
            return View(clienteViewModel);
        }


        [ClaimsAuthorize("PermissoesCliente","CX")]
        [Route("{id:guid}/excluir-cliente")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _ClienteApplicationService.Remover(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _ClienteApplicationService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
