using ControleDeBar.ConsoleApp.Compartilhado;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeBar.ConsoleApp.ModuloProduto
{
    public class TelaProduto : TelaBase<Produto,RepositorioProduto>
    {
        public TelaProduto(RepositorioProduto repositorioProduto):base(repositorioProduto)
        {
            this.repositorioBase = repositorioProduto;
            this.nomeEntidade = "Produto";
            this.sufixo = "s";
           
        }

        protected override void MostrarTabela(List<Produto> registros)
        {
            if (registros.Count > 0)
            {
                string cabecalho = $"{"ID",-3} | {"NOME",-18} | {"DESCRIÇAO",-18} | {"PREÇO",-10}";
                MostrarLista(registros, cabecalho);
            }
        }

        protected override Produto ObterRegistro()
        {
            Console.WriteLine($"Informe o nome do {nomeEntidade}:");
            string nome = Console.ReadLine()!;

            Console.WriteLine("Informe a descrição:");
            string descricao = Console.ReadLine()!;

            Console.WriteLine("Informe o preço:");
            decimal preco = decimal.Parse(Console.ReadLine()!);

          return new Produto(nome, descricao, preco);

        }
    }
}
