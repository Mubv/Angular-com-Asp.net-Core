using Nucleus.Validacoes;
using System;
using User.Dominio.Commands.Input;
using User.Dominio.Commands.Output;
using User.Dominio.Entidades;
using User.Dominio.Repositorios;
using User.Infra.Comum;
using User.Infra.ValueObjects;

namespace User.Dominio.Handler
{
    public class UsuarioHandler : Notifiable, ICommandHandler<AdicionarUsuarioCommand>,
                                              ICommandHandler<AtualizarUsuarioCommand>,
                                              ICommandHandler<ApagarUsuarioCommand>
    {
        private readonly IUsuarioRepositorio _repository;

        public UsuarioHandler(IUsuarioRepositorio repository)
        {
            _repository = repository;
        }

        public ICommandResult Handle(AdicionarUsuarioCommand command)
        {
            NomenclaturaPadrao nome = new NomenclaturaPadrao(command.Nome);
            CPF cpf = new CPF(command.CPF);
            Email email = new Email(command.Email);
            DateTime dataNascimento = command.DataNascimento;
            UF estado = new UF(command.Estado);
            CEP cep = new CEP(command.CEP);
            string cidade = command.Cidade;

            Usuario usuario = new Usuario(0, nome, cpf, email, dataNascimento, cep, estado, cidade);

            //Valida Entidade e Vo's            
            AddNotifications(usuario.Notifications);
            

            if (Invalid)
                return new UsuarioCommandResult(false, "Por favor, corrija as inconsistências abaixo", Notifications);

            //Persiste os dados
            string ret = _repository.Salvar(usuario);

            usuario.Id = _repository.LocalizarMaxId();

            //Notifica
            if (ret == "Sucesso")
            {
                // Retornar o resultado para tela
                return new UsuarioCommandResult(true, "Usuário gravado com sucesso!", new
                {
                    Id = usuario.Id,
                    CPF = usuario.CPF.ToString(),
                    Nome = usuario.Nome.ToString()
                });
            }
            else
            {
                // Retornar o resultado para tela
                return new UsuarioCommandResult(false, "Por favor, corrija as inconsistências abaixo", ret);
            }
        }

        public ICommandResult Handle(AtualizarUsuarioCommand command)
        {
            int id = command.Id;
            NomenclaturaPadrao nome = new NomenclaturaPadrao(command.Nome);
            CPF cpf = new CPF(command.CPF);
            Email email = new Email(command.Email);
            DateTime dataNascimento = command.DataNascimento;
            UF estado = new UF(command.Estado);
            CEP cep = new CEP(command.CEP);
            string cidade = command.Cidade;

            Usuario usuario = new Usuario(id, nome, cpf, email, dataNascimento, cep, estado, cidade);

            //Valida Entidade e Vo's            
            AddNotifications(usuario.Notifications);

            //Validar Dependências
            if (usuario.Id == 0)
            {
                AddNotification("Id", "Id não está vinculada à operação solicitada");
            }

            if (Invalid)
                return new UsuarioCommandResult(false, "Por favor, corrija as inconsistências abaixo", Notifications);

            //Persiste os dados
            string ret = _repository.Atualizar(usuario, usuario.Id);

            //Notifica
            if (ret == "Sucesso")
            {
                // Retornar o resultado para tela
                return new UsuarioCommandResult(true, "Usuário atualizado com sucesso!", new
                {
                    Id = usuario.Id,
                    CPF = usuario.CPF.ToString(),
                    Nome = usuario.Nome.ToString()
                });
            }
            else
            {
                // Retornar o resultado para tela
                return new UsuarioCommandResult(false, "Por favor, corrija as inconsistências abaixo", ret);
            }
        }

        public ICommandResult Handle(ApagarUsuarioCommand command)
        {
            if (!_repository.CheckId(command.Id))
                AddNotification("Id", "Este Id não está cadastrado! Impossível prosseguir sem um Id válido.");

            if (Invalid)
                return new UsuarioCommandResult(false, "Por favor, corrija as inconsistências abaixo", Notifications);

            //Persiste os dados
            string retorno = _repository.Deletar(command.Id);

            //Notifica
            if (retorno == "Sucesso")
            {
                // Retornar o resultado para tela
                return new UsuarioCommandResult(true, "Usuário excluído com sucesso!", new { Id = command.Id });
            }
            else
            {
                // Retornar o resultado para tela
                return new UsuarioCommandResult(false, "Por favor, corrija as inconsistências abaixo", retorno);
            }
        }
    }
}