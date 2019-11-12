using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using User.Dominio.Entidades;
using User.Dominio.Queries;
using User.Dominio.Repositorios;
using User.Infra.Data.DataContexts;

namespace User.Infra.Data.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly DataContext _ctx;
        StringBuilder Sql = new StringBuilder();
        DynamicParameters parametros = new DynamicParameters();

        public UsuarioRepositorio(DataContext ctx)
        {
            _ctx = ctx;
        }

        public string Salvar(Usuario usuario)
        {
            try
            {
                parametros.Add("Nome", usuario.Nome.ToString(), DbType.String);
                parametros.Add("Email", usuario.Email.ToString(), DbType.String);
                parametros.Add("CPF", usuario.CPF.ToString(), DbType.String);
                parametros.Add("DataNascimento", usuario.DataNascimento, DbType.Date);
                parametros.Add("CEP", usuario.CEP.ToString(), DbType.String);
                parametros.Add("Estado", usuario.Estado.ToString(), DbType.String);
                parametros.Add("Cidade", usuario.Cidade, DbType.String);

                Sql.Clear();
                Sql.Append("INSERT INTO Usuario ( ");
                Sql.Append("Nome,");
                Sql.Append("Email,");
                Sql.Append("CPF,");
                Sql.Append("DataNascimento,");
                Sql.Append("CEP,");
                Sql.Append("Estado,");
                Sql.Append("Cidade) ");

                Sql.Append("VALUES (");
                Sql.Append("@Nome,");
                Sql.Append("@Email,");
                Sql.Append("@CPF,");
                Sql.Append("@DataNascimento,");
                Sql.Append("@CEP,");
                Sql.Append("@Estado,");
                Sql.Append("@Cidade )");

                _ctx.Connection.Execute(Sql.ToString(), parametros);

                return "Sucesso";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string Atualizar(Usuario usuario, int id)
        {
            try
            {
                parametros.Add("Id", usuario.Id, DbType.Int32);
                parametros.Add("Nome", usuario.Nome.ToString(), DbType.String);
                parametros.Add("Email", usuario.Email.ToString(), DbType.String);
                parametros.Add("CPF", usuario.CPF.ToString(), DbType.String);
                parametros.Add("DataNascimento", usuario.DataNascimento, DbType.Date);
                parametros.Add("CEP", usuario.CEP.ToString(), DbType.String);
                parametros.Add("Estado", usuario.Estado.ToString(), DbType.String);
                parametros.Add("Cidade", usuario.Cidade, DbType.String);

                Sql.Clear();
                Sql.Append("UPDATE Usuario SET ");
                Sql.Append("Nome = @Nome, ");
                Sql.Append("Email = @Email, ");
                Sql.Append("CPF = @CPF, ");
                Sql.Append("DataNascimento = @DataNascimento, ");
                Sql.Append("CEP = @CEP, ");
                Sql.Append("Estado = @Estado, ");
                Sql.Append("Cidade = @Cidade ");
                Sql.Append("WHERE Id = @Id");

                _ctx.Connection.Execute(Sql.ToString(), parametros);

                return "Sucesso";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string Deletar(int id)
        {
            try
            {
                parametros.Add("Id", id, DbType.Int32);

                Sql.Clear();
                Sql.Append("DELETE FROM Usuario ");
                Sql.Append("WHERE Id = @Id");

                _ctx.Connection.Execute(Sql.ToString(), parametros);

                return "Sucesso";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public UsuarioQueryResult ObterUsuario(int id)
        {
            Sql.Clear();
            Sql.Append("SELECT ");
            Sql.Append("Id,");
            Sql.Append("Nome, ");
            Sql.Append("Email, ");
            Sql.Append("CPF, ");
            Sql.Append("DataNascimento, ");
            Sql.Append("CEP, ");
            Sql.Append("Estado, ");
            Sql.Append("Cidade ");
            Sql.Append("FROM Usuario ");
            Sql.Append("WHERE Id = @id ");

            return _ctx.Connection.Query<UsuarioQueryResult>(Sql.ToString(), new { id = id }).FirstOrDefault();
        }

        public IEnumerable<UsuarioQueryResult> ListarUsuarios()
        {
            Sql.Clear();
            Sql.Append("SELECT ");
            Sql.Append("Id,");
            Sql.Append("Nome, ");
            Sql.Append("Email, ");
            Sql.Append("CPF, ");
            Sql.Append("DataNascimento, ");
            Sql.Append("CEP, ");
            Sql.Append("Estado, ");
            Sql.Append("Cidade ");
            Sql.Append("FROM Usuario ");
            Sql.Append("ORDER BY Id ASC");

            return _ctx.Connection.Query<UsuarioQueryResult>(Sql.ToString()).Distinct().ToList();
        }

        public bool CheckId(int id)
        {
            parametros.Add("Id", id, DbType.Int32);

            Sql.Clear();
            Sql.Append("SELECT Id FROM Usuario ");
            Sql.Append("WHERE Id = @Id");

            return _ctx.Connection.Query<bool>(Sql.ToString(), parametros).FirstOrDefault();
        }

        public int LocalizarMaxId()
        {
            Sql.Clear();
            Sql.Append("SELECT MAX(Id) ");
            Sql.Append("FROM Usuario");

            return _ctx.Connection.Query<int>(Sql.ToString()).FirstOrDefault();
        }
    }
}