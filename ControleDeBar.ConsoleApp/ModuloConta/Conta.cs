using ControleDeBar.ConsoleApp.Compartilhado;
using ControleDeBar.ConsoleApp.ModuloGarcom;
using ControleDeBar.ConsoleApp.ModuloMesa;
using ControleDeBar.ConsoleApp.ModuloProduto;
using System.Collections;

namespace ControleDeBar.ConsoleApp.ModuloConta;

public class Conta : EntidadeBase<Conta>
{

    public List<Pedido> Pedidos { get; private set; }
    public Mesa Mesa { get; private set; }
    public Garcom Garcom { get; private set; }
    public bool Aberta { get; private set; }
    public DateTime DataConta { get; private set; }
    public decimal ValorTotal { get; private set; }
  
    public Conta(Mesa mesa, Garcom garcom)
    {
        this.Pedidos = new List<Pedido>();
        this.Mesa = mesa;
        this.Garcom = garcom;
        this.Aberta = true;
        this.DataConta = DateTime.Now;
        this.Mesa.AlterarStatusMesa();
    }

 

    public override void AtualizarInformacoes(Conta conta)
    {
      
        this.Garcom = conta.Garcom;
        this.Mesa = conta.Mesa;
    }

    public override List<string> Validar()
    {
        List<string> list = new();

        if (Mesa == null)
            list.Add("Mesa não encotrada!");

        if (Garcom == null)
            list.Add("Garçom não encontrado!");

        return list;
    }

    public void AdicionarPedido(int quantidade, Produto produto)
    {
        this.Pedidos.Add(new Pedido(produto, quantidade));
    }

    public void FinalizarConta()
    {
        this.ValorTotal = ObterValorParcial();
        this.Aberta = false;
        this.Mesa.AlterarStatusMesa();
    }

    public override string ToString()
    {
        return String.Format($"{id,-3} | {Mesa.id,-5} | {Garcom.Nome,-10} | {(Aberta ? "Aberta" : "Finalizada"),-10} | R$ {(Aberta ? ObterValorParcial(): ValorTotal),-15} | {DataConta:d}");
    }

    private decimal ObterValorParcial()
    {
        decimal valor = 0;
        foreach (Pedido item in Pedidos)
        {
            valor += item.CalcularValorPedido();
        }
        return valor;
    }

}
