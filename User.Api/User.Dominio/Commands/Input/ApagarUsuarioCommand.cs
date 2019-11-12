using Nucleus.Validacoes;
using System;
using User.Infra.Comum;

namespace User.Dominio.Commands.Input
{
    public class ApagarUsuarioCommand : Notifiable, CommandPadrao
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
            );
            return Valid;
        }
    }
}