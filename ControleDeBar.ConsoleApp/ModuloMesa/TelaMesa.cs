using ControleDeBar.ConsoleApp.Compartilhado;

namespace ControleDeBar.ConsoleApp.ModuloMesa
{
    public class TelaMesa : TelaBase<Mesa, RepositorioMesa>
    {
        public TelaMesa(RepositorioMesa repositorio):base(repositorio)
        {
            this.repositorioBase = repositorio;
            this.nomeEntidade = "mesa";
            this.sufixo = "s";
        }

        protected override void MostrarTabela(List<Mesa> registros)
        {
            if (registros.Count > 0)
            {
                string cabecalho = $"{"ID",-3} | {"Capacidade",-12} | {"STATUS",-20}\n----------------------------------";

                MostrarLista(registros, cabecalho);
            }
        }

        protected override Mesa ObterRegistro()
        {
            Console.WriteLine("Informe a capacidade de pessoas da mesa");
            int capacidade = int.Parse(Console.ReadLine()!);

            return new Mesa(capacidade);
        }
    }
}
