using ControleDeBar.ConsoleApp.Compartilhado;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeBar.ConsoleApp.ModuloProduto
{
    public class Produto : EntidadeBase<Produto>
    {

        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public decimal Preco { get; private set; }

        public Produto(string nome, string descricao, decimal preco)
        {
            this.Nome = nome;
            this.Descricao = descricao;
            this.Preco = preco;
        }

        public override void AtualizarInformacoes(Produto produto)
        {
          
            Nome = produto.Nome;
            Descricao = produto.Descricao;
            Preco = produto.Preco;
        }

        public override List<string> Validar()
        {
            List<string> lista = new();
            if (Preco <= 0)
            {
                lista.Add("Preço informado é inválido.");
            }
            if (Descricao.Trim() == string.Empty || Descricao.Trim().Length == 0)
            {
                lista.Add("Descrião campo é obrigatório");
            }
            if (Nome.Trim() == string.Empty || Nome.Trim().Length == 0)
            {
                lista.Add("Nome campo é obrigatório");
            }
            return lista;
        }

        public override string ToString()
        {
            return $"{id,-3} | {Nome,-18} | {Descricao,-18} | {Preco,-10}";
        }
    }
}

