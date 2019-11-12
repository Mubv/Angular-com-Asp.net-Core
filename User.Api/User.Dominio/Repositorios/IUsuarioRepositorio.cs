using System.Collections.Generic;
using User.Dominio.Entidades;
using User.Dominio.Queries;

namespace User.Dominio.Repositorios
{
    public interface IUsuarioRepositorio
    {
        string Salvar(Usuario usuario);
        string Atualizar(Usuario usuario, int id);
        string Deletar(int id);
        UsuarioQueryResult ObterUsuario(int id);
        IEnumerable<UsuarioQueryResult> ListarUsuarios();
        bool CheckId(int id);
        int LocalizarMaxId();
    }
}