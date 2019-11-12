using Nucleus.Validacoes;

namespace User.Infra.ValueObjects
{
    public class UF : Notifiable
    {
        public string Valor { get; private set; }

        public UF(string valor)
        {
            this.Valor = valor;

            AddNotifications(new ValidationContract().IsTrue(Validar(Valor), "UF", "UF inválido"));
        }

        public bool Validar(string uf)
        {
            uf = uf.ToUpper();

            if (uf == "AC" || uf == "AL" || uf == "AP" || uf == "AM" || uf == "BA" || uf == "CE" || uf == "DF" || uf == "ES" || uf == "GO" || uf == "MA" ||
                uf == "MT" || uf == "MS" || uf == "MG" || uf == "PA" || uf == "PB" || uf == "PR" || uf == "PE" || uf == "PI" || uf == "RJ" || uf == "RN" ||
                uf == "RS" || uf == "RO" || uf == "RR" || uf == "SC" || uf == "SP" || uf == "SE" || uf == "TO")
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