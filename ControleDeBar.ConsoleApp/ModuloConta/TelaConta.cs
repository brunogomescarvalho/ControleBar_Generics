using ControleDeBar.ConsoleApp.Compartilhado;
using ControleDeBar.ConsoleApp.ModuloGarcom;
using ControleDeBar.ConsoleApp.ModuloMesa;
using ControleDeBar.ConsoleApp.ModuloProduto;
using System.Collections;


namespace ControleDeBar.ConsoleApp.ModuloConta
{
    public class TelaConta : TelaBase<Conta, RepositorioConta>
    {
        private readonly TelaGarcom telaGarcom;
        private readonly TelaMesa telaMesa;
        private readonly TelaProduto telaProduto;
        private readonly RepositorioConta repositorioConta;
     
        public TelaConta(RepositorioConta repositorioConta, TelaGarcom telaGarcom, TelaMesa telaMesa, TelaProduto telaProduto):base(repositorioConta)
        {
            this.repositorioConta = repositorioConta;
            this.repositorioBase = repositorioConta;
            this.telaProduto = telaProduto;
            this.telaMesa = telaMesa;
            this.telaGarcom = telaGarcom;
            this.nomeEntidade = "conta";
            this.sufixo = "s";
        }
        public override string ApresentarMenu()
        {
            Console.Clear();

            Console.WriteLine($"Cadastro de {nomeEntidade}{sufixo} \n");
            Console.WriteLine($"Digite 1 para Abrir {nomeEntidade}");
            Console.WriteLine($"Digite 2 para Visualizar Todas as {nomeEntidade}{sufixo}");
            Console.WriteLine($"Digite 3 para Visualizar {nomeEntidade}{sufixo}  Em Aberto");
            Console.WriteLine($"Digite 4 para Visualizar Detalhes da Conta");
            Console.WriteLine($"Digite 5 para Adicionar pedido");
            Console.WriteLine($"Digite 6 para Remover pedido");
            Console.WriteLine($"Digite 7 para Finalizar conta");
            Console.WriteLine($"Digite 8 para Visualizar Total Faturado no Dia");

            Console.WriteLine("Digite s para Sair");

            string opcao = Console.ReadLine()!;

            return opcao;
        }

        protected override void MostrarTabela(List<Conta> registros)
        {
            if (registros.Count > 0)
            {
                MostrarCabecalhoConta();
                foreach (var item in registros)
                {
                    Console.WriteLine(item);
                }
            }
        }

        public void ExibirContasEmAberto()
        {
            MostrarTexto("=== Exibindo contas em aberto ===");
            MostrarContasEmAberto();

            if (repositorioBase.SelecionarTodos().Count == 0)
                return;

            Console.ReadKey();
        }

        public void ExibirContasCadastradas()
        {
            Console.Clear();
            VisualizarRegistros(false);

            if (repositorioBase.SelecionarTodos().Count > 0)
                Console.ReadKey();
        }

        public void Finalizar()
        {
            MostrarTexto("== Finalizar Conta == \n");
            MostrarContasEmAberto();

            if (repositorioBase.SelecionarTodos().Count == 0)
                return;

            Conta conta = EncontrarRegistro("Informe o id da conta para finalizar:\n=>");

            if (conta.Aberta == false)
            {
                MostrarMensagemDeAlerta("Conta já finalizada");
                return;
            }

            conta.FinalizarConta();

            MostrarMensagemDeSucesso("Conta finalizada com sucesso");
        }

        protected override Conta ObterRegistro()
        {
          telaMesa.VisualizarRegistros(false);

            Mesa mesa = telaMesa.EncontrarRegistro("\nInforme o id da mesa:\n=> ");

            if (mesa.Disponivel == false)
            {
                MostrarMensagemDeAlerta("Mesa já ocupada, por favor escolha outra");
                Console.Clear();
                ObterRegistro();
            }

            Console.Clear();
            telaGarcom.VisualizarRegistros(false);

            Garcom garcom = telaGarcom.EncontrarRegistro("\nDigite o id do garçom:\n=> ");
            return new Conta(mesa, garcom);

        }

        public void IncluirPedido()
        {
            MostrarTexto("== Incluir Pedido ==\n");
            MostrarContasEmAberto();

            if (!repositorioBase.TemRegistros())
                return;

            Conta conta = EncontrarRegistro("\nInforme o id da conta:\n=> ");

            if (conta.Aberta == false)
            {
                MostrarMensagemDeAlerta("Conta já finalizada");
                return;
            }

            Console.Clear();
            telaProduto.VisualizarRegistros(false);

            Produto produto = telaProduto.EncontrarRegistro("\nDigite o id do produto:\n=> ");

            MostrarTexto("Informe a quantidade");
            int quantidade = int.Parse(Console.ReadLine()!);

            conta.AdicionarPedido(quantidade, produto);

            MostrarMensagemDeSucesso("Pedido incluido com sucesso");

        }

        public void ExcluirPedido()
        {
            MostrarTexto("== Excluir pedido ==\n");
            MostrarContasEmAberto();

            if (!repositorioBase.TemRegistros())
                return;

            Conta conta = EncontrarRegistro("\nInforme o id da conta\n=> ");

            if(conta.Aberta == false)
            {
                MostrarMensagemDeAlerta("Conta já finalizada");
                return;
            }

            Console.Clear();
            MostrarItensConta(conta, false);

            Console.Write("\nInforme o id do pedido para excluir\n=> ");
            int id = int.Parse(Console.ReadLine()!);

            foreach (Pedido item in conta.Pedidos)
            {
                if (item.Id == id)
                {
                    conta.Pedidos.Remove(item);
                    MostrarMensagemDeSucesso("Item removido");
                    return;
                }
            }

            MostrarMensagemDeErro("Pedido não localizado");
           
        }


        public void ExibirDetalhesConta()
        {
            MostrarTexto("== Detalhes da Conta ==\n");
            VisualizarRegistros(false);

            if (!repositorioBase.TemRegistros())
                return;

            Conta conta = EncontrarRegistro("Informe o id da conta para ver detalhes:\n=> ");

            Console.Clear();

            MostrarItensConta(conta, true);
        }

        private void MostrarItensConta(Conta conta, bool esperarTecla)
        {
            if (conta.Pedidos.Count == 0)
            {
                MostrarMensagemDeAlerta("Nenhum pedido até o momento!");
                return;
            }

            Console.WriteLine(MostrarCabecalhoPedidos());

            foreach (var item in conta.Pedidos)
            {
                Console.WriteLine(item);
            }

            if(esperarTecla)
                Console.ReadKey();

        }



        public void ExibirFaturamentoDiario()
        {
            MostrarCabecalho("Faturamento diario", "Digite a data que deseja faturar (dd/MM/yyyy)...");

            try
            {
                DateTime data = Convert.ToDateTime(Console.ReadLine()!);

                decimal valor = repositorioConta.CalcularFaturamentoDoDia(data);

                if (valor == 0)
                {
                    MostrarMensagemDeAlerta($"Nenhum valor faturado no dia {data:d}");
                    return;
                }
                MostrarMensagemDeSucesso($"O faturamento no dia {data:d} foi de R$ {valor}");
            }
            catch (FormatException)
            {
                MostrarMensagemDeErro("Data informada em um formato inválido");
            }
        }

        private void MostrarContasEmAberto()
        {
            List<Conta> contasEmAberto = repositorioConta.MostrarContasEmAberto();

            if (contasEmAberto.Count == 0)
            {
                MostrarMensagemDeAlerta("Nenhum registro em aberto até o momento!");
            }

            MostrarTabela(contasEmAberto);
        }



        private void MostrarCabecalhoConta()
        {
            Console.WriteLine($"{"ID",-3} | {"MESA",-5} | {"GARÇOM",-10} | {"STATUS",-10} | {"VALOR",-18} | {"DATA-HORA"}");
            Console.WriteLine("---------------------------------------------------------------------------");
        }

        private string MostrarCabecalhoPedidos()
        {
            return $"{"ID",-3} | {"Produto",-18} | {"Descricao",-18} | {"Qtd",-5} | {"Total item",-10}";
        }
    }
}
