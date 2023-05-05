using ControleDeBar.ConsoleApp.Compartilhado;
using ControleDeBar.ConsoleApp.ModuloConta;
using ControleDeBar.ConsoleApp.ModuloGarcom;
using ControleDeBar.ConsoleApp.ModuloMesa;
using ControleDeBar.ConsoleApp.ModuloProduto;

namespace ControleDeBar.ConsoleApp.ModuloPrincipal;


public class TelaPrincipal
{

    RepositorioGarcom repositorioGarcom = new RepositorioGarcom(new List<Garcom>());
    RepositorioMesa repositorioMesa = new RepositorioMesa(new List<Mesa>());
    RepositorioProduto repositorioProduto = new RepositorioProduto(new List<Produto>());
    RepositorioConta repositorioConta = new RepositorioConta(new List<Conta>());

    TelaGarcom telaGarcom = null!;
    TelaMesa telaMesa = null!;
    TelaProduto telaProduto = null!;

 
    public int Menu()
    {
       
        Console.Clear();
        Console.WriteLine("====== Bar Da Galera ======\n");

        string opcao1 = "1 - Módulo Garçom";
        string opcao2 = "2 - Módulo Mesa";
        string opcao3 = "3 - Módulo Produto";
        string opcao4 = "4 - Módulo Conta";
        string voltar = "9 - Voltar";

        Console.Write($"{opcao1}\n{opcao2}\n{opcao3}\n{opcao4}\n{voltar}\n=> ");
        return int.Parse(Console.ReadLine()!);
    }

    public ITelaBase AtribuirTela(int opcao)
    {
        return opcao switch
        {
            1 => CriarTela(typeof(TelaGarcom)),
            2 => CriarTela(typeof(TelaMesa)),
            3 => CriarTela(typeof(TelaProduto)),
            4 => CriarTela(typeof(TelaConta)),
            _ => null!,
        };
    }

    private ITelaBase CriarTela(Type tipo)
    {
        if (tipo.Equals(typeof(TelaGarcom)))
        {
            telaGarcom = new TelaGarcom(repositorioGarcom);
            return telaGarcom;
        }
        else if (tipo.Equals(typeof(TelaMesa)))
        {
            telaMesa = new TelaMesa(repositorioMesa);
            return telaMesa;
        }
        else if (tipo.Equals(typeof(TelaProduto)))
        {
            telaProduto = new TelaProduto(repositorioProduto);
            return telaProduto;
        }
        else if (tipo.Equals(typeof(TelaConta)))
        {
            return new TelaConta(repositorioConta, telaGarcom, telaMesa, telaProduto);
        }
        else
        {
            return null!;
        }
    }

    private static void AdicionarItens(RepositorioProduto repositorioProduto, RepositorioMesa repositorioMesa, RepositorioGarcom repositorioGarcom)
    {
        repositorioGarcom.Inserir(new Garcom("Alfredo"));
        repositorioGarcom.Inserir(new Garcom("Antenor"));

        repositorioProduto.Inserir(new Produto("Coca-Cola", "Lata 350ml", 4.50M));
        repositorioProduto.Inserir(new Produto("Aipim Frito", "Porção 500g", 14.00M));
        repositorioProduto.Inserir(new Produto("Torresminho", "Porção 500g", 12.00M));
        repositorioProduto.Inserir(new Produto("Cerveja Skol", "Garrafa 600ml", 8.0M));

        repositorioMesa.Inserir(new Mesa(4));
        repositorioMesa.Inserir(new Mesa(6));
        repositorioMesa.Inserir(new Mesa(8));
    }

    public void AdicionarAlgunsItens()
    {
        AdicionarItens(repositorioProduto, repositorioMesa, repositorioGarcom);
    }


}
