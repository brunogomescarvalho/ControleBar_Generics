using ControleDeBar.ConsoleApp.Compartilhado;
using System.Collections;

namespace ControleDeBar.ConsoleApp.ModuloGarcom;

public class RepositorioGarcom : RepositorioBase<Garcom>
{
    public RepositorioGarcom(List<Garcom> listaRegistros) : base(listaRegistros)
    {
    }
}