using Nucleus.Validacoes;
using System;
using User.Infra.ValueObjects;

namespace User.Dominio.Entidades
{
    public class Usuario : Notifiable
    {
        public int Id { get; set; }
        public NomenclaturaPadrao Nome { get; set; }
        public CPF CPF { get; set; }
        public Email Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public CEP CEP { get; set; }
        public UF Estado { get; set; }
        public string Cidade { get; set; }


        public Usuario(int id,
                       NomenclaturaPadrao nome,
                       CPF cpf,
                       Email email,
                       DateTime dataNascimento,
                       CEP cep,
                       UF estado,
                       string cidade)
        {
            this.Id = id;
            this.Nome = nome;
            this.CPF = cpf;
            this.Email = email;
            this.DataNascimento = dataNascimento;
            this.CEP = cep;
            this.Estado = estado;
            this.Cidade = cidade;
        }
    }
}