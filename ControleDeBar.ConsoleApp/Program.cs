using ControleDeBar.ConsoleApp.Compartilhado;
using ControleDeBar.ConsoleApp.ModuloConta;
using ControleDeBar.ConsoleApp.ModuloPrincipal;

namespace ControleDeBar.ConsoleApp;

public class Program
{
    static void Main(string[] args)
    {
        TelaPrincipal telaPrincipal = new TelaPrincipal();

        ITelaBase tela;

        telaPrincipal.AdicionarAlgunsItens();

        bool continuar = true;

        while (continuar)
        {
            try
            {
                tela = telaPrincipal.Menu();

                string subMenu = "";

                if (tela is TelaConta telaConta)
                {
                    while (subMenu != "s")
                    {
                        subMenu = tela.ApresentarMenu();

                        switch (subMenu)
                        {
                            case "1": telaConta.InserirNovoRegistro(); break;
                            case "2": telaConta.ExibirContasCadastradas(); break;
                            case "3": telaConta.ExibirContasEmAberto(); break;
                            case "4": telaConta.ExibirDetalhesConta(); break;
                            case "5": telaConta.IncluirPedido(); break;
                            case "6": telaConta.ExcluirPedido(); break;
                            case "7": telaConta.Finalizar(); break;
                            case "8": telaConta.ExibirFaturamentoDiario(); break;

                        }
                    }
                }

                while (subMenu != "s")
                {
                    subMenu = tela.ApresentarMenu();

                    switch (subMenu)
                    {
                        case "1": tela.InserirNovoRegistro(); break;
                        case "2": tela.VisualizarRegistros(true); Console.ReadKey(); break;
                        case "3": tela.EditarRegistro(); break;
                        case "4": tela.ExcluirRegistro(); break;

                        default: continue;
                    }
                }

            }
            catch (FormatException)
            {
                Console.Clear();
                Console.WriteLine("Opção Inválida");
                Console.ReadLine();
            }
            catch (NullReferenceException)
            {
                Console.Clear();
                Console.WriteLine("Opção Null");
                Console.ReadLine();
            }

        }

    }

}