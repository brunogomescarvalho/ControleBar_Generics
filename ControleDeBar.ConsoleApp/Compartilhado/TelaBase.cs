using System.Collections;

namespace ControleDeBar.ConsoleApp.Compartilhado;

public abstract class TelaBase<TEntidade,TRepositorio> : ITelaBase where TEntidade : EntidadeBase<TEntidade> where TRepositorio : RepositorioBase<TEntidade>
{
    public string nomeEntidade = string.Empty;
    public string sufixo = string.Empty;

    protected RepositorioBase<TEntidade> repositorioBase;

    public TelaBase(RepositorioBase<TEntidade> repositorio)
    {
        this.repositorioBase = repositorio;
    }

    public void MostrarCabecalho(string titulo, string subtitulo)
    {
        Console.Clear();

        Console.WriteLine(titulo + "\n");

        Console.WriteLine(subtitulo + "\n");
    }

    public void MostrarTexto(string texto)
    {
        Console.Clear();

        Console.WriteLine(texto);

    }

    private void MostrarMensagem(string mensagem, TipoMenssagem tipo)
    {
        Console.WriteLine();
        ConsoleColor cor;

        switch (tipo)
        {
            case TipoMenssagem.Sucesso: cor = ConsoleColor.DarkGreen;break;
            case TipoMenssagem.Erro: cor = ConsoleColor.Red; break;
            case TipoMenssagem.Atencao: cor = ConsoleColor.DarkYellow;break;
            default: cor = ConsoleColor.White; break;
        }

        Console.ForegroundColor = cor;

        Console.WriteLine(mensagem);

        Console.ResetColor();

        Console.ReadLine();
    }

    public virtual string ApresentarMenu()
    {
        Console.Clear();

        Console.WriteLine($"Cadastro de {nomeEntidade}{sufixo} \n");

        Console.WriteLine($"Digite 1 para Inserir {nomeEntidade}");
        Console.WriteLine($"Digite 2 para Visualizar {nomeEntidade}{sufixo}");
        Console.WriteLine($"Digite 3 para Editar {nomeEntidade}{sufixo}");
        Console.WriteLine($"Digite 4 para Excluir {nomeEntidade}{sufixo}\n");

        Console.WriteLine("Digite s para Sair");

        string opcao = Console.ReadLine()!;

        return opcao;
    }

    public virtual void InserirNovoRegistro()
    {
        MostrarCabecalho($"Cadastro de {nomeEntidade}{sufixo}", "Inserindo um novo registro...");

        TEntidade registro = ObterRegistro();

        if (TemErrosDeValidacao(registro))
        {
            InserirNovoRegistro(); //chamada recursiva

            return;
        }

        repositorioBase.Inserir(registro);

        MostrarMensagemDeSucesso("Registro inserido com sucesso!");
    }
    
    public virtual void VisualizarRegistros(bool mostrarCabecalho)
    {
        if (mostrarCabecalho)
            MostrarCabecalho($"Cadastro de {nomeEntidade}{sufixo}", "Visualizando registros já cadastrados...");

        List<TEntidade> registros = repositorioBase.SelecionarTodos();

        if (registros.Count == 0)
        {
            MostrarMensagemDeAlerta("Nenhum registro cadastrado");
            return;
        }

        MostrarTabela(registros);
    }

    public virtual void EditarRegistro()
    {
        MostrarCabecalho($"Cadastro de {nomeEntidade}{sufixo}", "Editando um registro já cadastrado...");

        VisualizarRegistros(false);

        Console.WriteLine();

        TEntidade registro = EncontrarRegistro("Digite o id do registro: ");

        TEntidade registroAtualizado = ObterRegistro();

        if (TemErrosDeValidacao(registroAtualizado))
        {
            EditarRegistro();

            return;
        }

        repositorioBase.Editar(registro, registroAtualizado);

        MostrarMensagemDeSucesso("Registro editado com sucesso!");
    }

    public virtual void ExcluirRegistro()
    {
        MostrarCabecalho($"Cadastro de {nomeEntidade}{sufixo}", "Excluindo um registro já cadastrado...");

        VisualizarRegistros(false);

        Console.WriteLine();

        TEntidade registro = EncontrarRegistro("Digite o id do registro: ");

        repositorioBase.Excluir(registro);

        MostrarMensagemDeSucesso("Registro excluído com sucesso!");
    }  
    

    public virtual TEntidade EncontrarRegistro(string textoCampo)
    {            
        bool idInvalido;
        TEntidade registroSelecionado = null!;

        do
        {
            idInvalido = false;
            Console.Write("\n" + textoCampo);
            try
            {
                int id = Convert.ToInt32(Console.ReadLine());

                registroSelecionado = repositorioBase.SelecionarPorId(id);

                if (registroSelecionado == null)
                    idInvalido = true;
            }
            catch (FormatException)
            {
                idInvalido = true;
            }

            if (idInvalido)
                MostrarMensagemDeErro("Id inválido, tente novamente");

        } while (idInvalido);

        return registroSelecionado!;
    }

    protected bool TemErrosDeValidacao(TEntidade registro)
    {
        bool temErros = false;

        List<string> erros = registro.Validar();

        if (erros.Count > 0)
        {
            temErros = true;
            Console.ForegroundColor = ConsoleColor.Red;

            erros.ForEach(i => Console.WriteLine(i));

            Console.ResetColor();

            Console.ReadLine();
        }

        return temErros;
    }

    protected void MostrarLista(List<TEntidade> lista, string cabecalho)
    {
        Console.WriteLine(cabecalho);
        lista.ForEach(i => Console.WriteLine(i));
    }

    protected abstract TEntidade ObterRegistro();

    protected abstract void MostrarTabela(List<TEntidade> registros);

  

    public void MostrarMensagemDeSucesso(string mensagem)
    {
        MostrarMensagem(mensagem, TipoMenssagem.Sucesso);
    }

    public void MostrarMensagemDeErro(string mensagem)
    {
        MostrarMensagem(mensagem,TipoMenssagem.Erro);
    }

    public void MostrarMensagemDeAlerta(string mensagem)
    {
        MostrarMensagem(mensagem, TipoMenssagem.Atencao);
    }



    public enum TipoMenssagem
    {
        Sucesso,
        Erro,
        Atencao
    }

}
