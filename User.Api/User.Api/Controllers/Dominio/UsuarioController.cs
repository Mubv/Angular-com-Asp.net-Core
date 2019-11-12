using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using User.Api.Controllers.Comum;
using User.Dominio.Commands.Input;
using User.Dominio.Commands.Output;
using User.Dominio.Handler;
using User.Dominio.Queries;
using User.Dominio.Repositorios;
using User.Infra.Comum;

namespace User.Api.Controllers.Dominio
{
    //[RequireHttps]
    [Route("Usuario")]
    [ApiController]
    public class UsuarioController : ApiController
    {
        private readonly IUsuarioRepositorio _repositorio;
        private readonly UsuarioHandler _handler;

        public UsuarioController(IUsuarioRepositorio repositorio, UsuarioHandler handler)
        {
            _repositorio = repositorio;
            _handler = handler;
        }

        /// <summary>
        /// HealthCheck
        /// </summary>        
        /// <remarks><h2><b><i>Afere a resposta deste contexto do serviço.</i></b></h2></remarks>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/HealthCheck")]
        public string UsuarioHealthCheck()
        {
            return "ESTOU VIVO!";
        }

        /// <summary>
        /// Usuários
        /// </summary>                
        /// <remarks><h2><b>Lista todos Usuários.</b></h2></remarks>      
        /// <response code="200">OK Request</response>
        /// <response code="204">Not Content</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/Usuarios")]
        public IEnumerable<UsuarioQueryResult> Usuarios()
        {
            return _repositorio.ListarUsuarios();
        }

        /// <summary>
        /// Usuário
        /// </summary>                
        /// <remarks><h2><b>Detalha Usuário.</b></h2></remarks>      
        /// <param name="Id">Parâmetro requerido Id do Usuário Agenda</param>
        /// <response code="200">OK Request</response>
        /// <response code="204">Not Content</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("v1/Usuario/{Id:int}")]
        public UsuarioQueryResult Usuario(int Id)
        {
            return _repositorio.ObterUsuario(Id);
        }

        /// <summary>
        /// Incluir Usuário 
        /// </summary>                
        /// <remarks><h2><b>Inclui novo Usuário na base de dados.</b></h2></remarks>
        /// <param name="command" >Parâmetro requerido command de Insert</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("v1/UsuarioNovo")]
        public ICommandResult UsuarioNovo([FromBody] AdicionarUsuarioCommand command)
        {
            return (UsuarioCommandResult)_handler.Handle(command);
        }

        /// <summary>
        /// Alterar Usuário
        /// </summary>        
        /// <remarks><h2><b>Altera Usuário na base de dados.</b></h2></remarks>        
        /// <param name="command" >Parâmetro requerido command de Update</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("v1/UsuarioAlterar")]
        public ICommandResult UsuarioAlterar([FromBody]AtualizarUsuarioCommand command)
        {
            return (UsuarioCommandResult)_handler.Handle(command);
        }
        /// <summary>
        /// Excluir Usuário
        /// </summary>                
        /// <remarks><h2><b>Exclui Usuário na base de dados.</b></h2></remarks>
        /// <param name="command" >Parâmetro requerido command de Delete</param>
        /// <response code="200">OK Request</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete]
        [Route("v1/UsuarioExcluir")]
        public ICommandResult UsuarioExcluir([FromBody]ApagarUsuarioCommand command)
        {
            return (UsuarioCommandResult)_handler.Handle(command);
        }
    }
}