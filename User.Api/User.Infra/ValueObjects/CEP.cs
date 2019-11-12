using Nucleus.Validacoes;
using System.Text.RegularExpressions;

namespace User.Infra.ValueObjects
{
    public class CEP : Notifiable
    {
        public string Valor { get; private set; }

        public CEP(string valor)
        {
            this.Valor = valor;

            AddNotifications(new ValidationContract().IsTrue(Validar(Valor), "Cep", "Cep inválido"));
        }

        public bool Validar(string cep)
        {
            return Regex.IsMatch(cep, @"^\d{5}\-?\d{3}$");
        }

        public override string ToString()
        {
            return Valor;
        }
    }
}