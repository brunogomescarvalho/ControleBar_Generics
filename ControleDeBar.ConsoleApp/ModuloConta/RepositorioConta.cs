using ControleDeBar.ConsoleApp.Compartilhado;
using System.Collections;

namespace ControleDeBar.ConsoleApp.ModuloConta;

public class RepositorioConta : RepositorioBase<Conta>
{
    public RepositorioConta(List<Conta> listaRegistros) : base(listaRegistros)
    {
    }


    public decimal CalcularFaturamentoDoDia(DateTime data)
    {
        decimal valor = 0;

        listaRegistros.ForEach(item =>
        {
            item.DataConta.Date.Equals(data); item.Aberta.Equals(false);
            item.Pedidos.ForEach(pedido => valor += pedido.CalcularValorPedido());
        });

        return valor;
    }

    public List<Conta> MostrarContasEmAberto()
    {
        List<Conta> contasEmAberto = new();

        listaRegistros.ForEach(i => { i.Aberta.Equals(true); contasEmAberto.Add(i); });

        return contasEmAberto;
    }
}
