﻿using ControleDeBar.ConsoleApp.Compartilhado;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeBar.ConsoleApp.ModuloProduto
{
    public class RepositorioProduto : RepositorioBase<Produto>
    {
        public RepositorioProduto(List<Produto> listaRegistros) : base(listaRegistros)
        {
        }
    }
}
