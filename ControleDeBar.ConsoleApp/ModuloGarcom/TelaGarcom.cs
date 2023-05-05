using ControleDeBar.ConsoleApp.Compartilhado;
using ControleDeBar.ConsoleApp.ModuloConta;
using System.Collections;

namespace ControleDeBar.ConsoleApp.ModuloGarcom
{
    public class TelaGarcom : TelaBase<Garcom, RepositorioGarcom>
    {
        public TelaGarcom(RepositorioGarcom repositorioGarcom) : base(repositorioGarcom)
        {
            this.repositorioBase = repositorioGarcom;
            this.nomeEntidade = "Garçom";
        }
        protected override void MostrarTabela(List<Garcom> registros)
        {
            if (registros.Count > 0)
            {
                string cabecalho = "Selecionar Garçom\n\nID  | NOME\n-------------------";
                MostrarLista(registros, cabecalho);
            }

        }

        protected override Garcom ObterRegistro()
        {
            Console.WriteLine("Informe o nome:");
            string nome = Console.ReadLine()!;

            return new Garcom(nome);

        }

       


    }
}
