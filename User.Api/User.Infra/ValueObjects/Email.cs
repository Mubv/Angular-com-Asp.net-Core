using Nucleus.Validacoes;
using System.Text.RegularExpressions;

namespace User.Infra.ValueObjects
{
    public class Email : Notifiable
    {
        public string Valor { get; private set; }

        public Email(string valor)
        {
            this.Valor = valor;

            AddNotifications(new ValidationContract().IsTrue(Validar(Valor), "Email", "Email inválido"));
        }

        public bool Validar(string email)
        {
            return Regex.IsMatch(email, @"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");
        }

        public override string ToString()
        {
            return Valor;
        }
    }
}