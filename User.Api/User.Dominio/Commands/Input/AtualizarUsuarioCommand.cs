using Nucleus.Validacoes;
using System;
using User.Infra.Comum;

namespace User.Dominio.Commands.Input
{
    public class AtualizarUsuarioCommand : Notifiable, CommandPadrao
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CEP { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }

        public bool EhValido()
        {
            AddNotifications(new ValidationContract()
                .IsGreaterThan(Id, 0, "Id", "Identificador do Usuário inválido")

                .HasMaxLen(Nome.ToString(), 14, "Nome", "Nome do Usuário maior que o esperado")

                .HasMaxLen(CPF.ToString(), 14, "CPF", "CPF do Usuário maior que o esperado")

                .HasMaxLen(CEP.ToString(), 9, "CEP", "CEP do Usuário maior que o esperado")

                .HasMaxLen(Estado.ToString(), 2, "Estado", "Estado do Usuário maior que o esperado")
            );
            return Valid;
        }
    }
}