using System.Collections.Generic;
using System.IO;

namespace cadastroPessoa
{
    public class PessoaJuridica : Pessoa
    {
        public string cnpj { get; set; }

        public string razaoSocial { get; set; }

        public string caminho { get; private set; } = "Database/PessoaJuridica.csv";

        public override double PagarImposto(float rendimento)
        {
            if (rendimento <= 5000)
            {
                return rendimento * .06;

            }
            else if (rendimento > 5000 && rendimento <= 10000)
            {
                return rendimento * .08;

            }
            else
            {
                return (rendimento / 100) * 10;
            }
        }

        public bool validarCNPJ(string cnpj)
        {
            if (cnpj.Length == 14 && cnpj.Substring(cnpj.Length - 6, 4) == "0001")
            {
                return true;
            }
            return false;
        }

        public string PrepararLinhasCsv(PessoaJuridica pj)
        {
            return $"{pj.cnpj};{pj.razaoSocial};{pj.rendimento}";
        }

        public void Inserir(PessoaJuridica pj)
        {
            string[] linhas = { PrepararLinhasCsv(pj) };

            File.AppendAllLines(caminho, linhas);
        }

        public List<PessoaJuridica> Ler()
        {
            List<PessoaJuridica> listaPj = new List<PessoaJuridica>();

            string[] linhas = File.ReadAllLines(caminho);

            foreach (var cadaLinha in linhas)
            {
                string[] atributos = cadaLinha.Split(";");

                PessoaJuridica cadaPj = new PessoaJuridica();

                cadaPj.cnpj = atributos[0];
                cadaPj.razaoSocial = atributos[1];
                cadaPj.rendimento = float.Parse(atributos[2]);

                listaPj.Add(cadaPj);
            }

            return listaPj;

        }

    }
}