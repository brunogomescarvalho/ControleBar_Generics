using ControleDeBar.ConsoleApp.ModuloProduto;

namespace ControleDeBar.ConsoleApp.ModuloConta;

public class Pedido
{
    public Produto Produto { get; private set; }
    public int Quantidade { get; private set; }
    public int Id { get; private set; }

    private static int contadorId = 1;

    public Pedido(Produto produto, int quantidade)
    {
        this.Produto = produto;
        this.Quantidade = quantidade;
        this.Id = contadorId++;
    }

    public decimal CalcularValorPedido()
    {
        return Quantidade * Produto.Preco;
    }

    public override string ToString()
    {
        return $"{Id,-3} | {Produto.Nome,-18} | {Produto.Descricao,-18} | {Quantidade,-5} | R$ {CalcularValorPedido(),-10}";
    }

}
