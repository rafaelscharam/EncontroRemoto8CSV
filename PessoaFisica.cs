using System.Collections.Generic;
using System;
using System.IO;

namespace cadastroPessoa
{
    public class PessoaFisica : Pessoa
    {
        public string cpf { get; set; }

        public DateTime dataNascimento { get; set; }

        public string caminho { get; private set; } = "Database/PessoaFisica.csv";

        public override double PagarImposto(float rendimento)
        {
            if (rendimento <= 1500)
            {
                return 0;

            }
            else if (rendimento > 1500 && rendimento <= 5000)
            {
                return rendimento * .03;

            }
            else
            {
                return (rendimento / 100) * 5;
            }
        }

        public bool validarDataNascimento(DateTime dataNasc)
        {

            DateTime dataAtual = DateTime.Today;

            double anos = (dataAtual - dataNasc).TotalDays / 365;

            if (anos >= 18)
            {

                return true;
            }

            return false;

        }

        public string PrepararLinhasCsv(PessoaFisica pf)
        {
            return $"{pf.cpf};{pf.nome};{pf.rendimento}";
        }

        public void Inserir(PessoaFisica pf)
        {
            string[] linhas = { PrepararLinhasCsv(pf) };

            File.AppendAllLines(caminho, linhas);
        }

        public List<PessoaFisica> Ler()
        {
            List<PessoaFisica> listaPf = new List<PessoaFisica>();

            string[] linhas = File.ReadAllLines(caminho);

            foreach (var cadaLinha in linhas)
            {
                string[] atributos = cadaLinha.Split(";");

                PessoaFisica cadaPf = new PessoaFisica();

                cadaPf.cpf = atributos[0];
                cadaPf.nome = atributos[1];
                cadaPf.rendimento = float.Parse(atributos[2]);

                listaPf.Add(cadaPf);
            }

            return listaPf;

        }
    }
}