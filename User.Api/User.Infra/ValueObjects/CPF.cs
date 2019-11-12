using Nucleus.Validacoes;
using System.Text.RegularExpressions;

namespace User.Infra.ValueObjects
{
    public class CPF : Notifiable
    {
        public string Valor { get; private set; }

        public CPF(string valor)
        {
            this.Valor = valor;

            AddNotifications(new ValidationContract().IsTrue(Validar(Valor), "Cpf", "Cpf inválido"));
        }

        public bool Validar(string cpf)
        {
            if (Regex.IsMatch(cpf, @"^\d{3}\.\d{3}\.\d{3}-\d{2}$"))
            {
                return true;
            }
            else if (Regex.IsMatch(cpf, @"^\d{3}\d{3}\d{3}\d{2}$"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return Valor;
        }
    }
}