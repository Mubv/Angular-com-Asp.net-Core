using Nucleus.Validacoes;

namespace User.Infra.ValueObjects
{
    public class NomenclaturaPadrao : Notifiable
    {
        public string Nome { get; private set; }

        public override string ToString()
        {
            return Nome;
        }

        public NomenclaturaPadrao(string nome)
        {
            Nome = nome;

            AddNotifications(new ValidationContract()
              .HasMinLen(nome, 3, "Nomenclatura Padrão", "Campo informado inferior à 3 caracteres")
              .HasMaxLen(nome, 40, "Nomenclatura Padrão", "Campo informado superior à 50 caracteres")
            );
        }
    }
}