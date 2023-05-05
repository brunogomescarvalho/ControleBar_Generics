
namespace ControleDeBar.ConsoleApp.Compartilhado
{
    public abstract class RepositorioBase<TEntidade> where TEntidade : EntidadeBase<TEntidade>
    {
        protected List<TEntidade> listaRegistros;

        protected int contadorRegistros = 0;

        public RepositorioBase(List<TEntidade> listaRegistros)
        {
            this.listaRegistros = listaRegistros;
        }

        public virtual void Inserir(TEntidade registro)
        {
            contadorRegistros++;

            registro.id = contadorRegistros;

            listaRegistros.Add(registro);
        }

        public virtual void Editar(int id, TEntidade registroAtualizado)
        {
            TEntidade registroSelecionado = SelecionarPorId(id);

            registroSelecionado.AtualizarInformacoes(registroAtualizado);
        }

        public virtual void Editar(TEntidade registroSelecionado, TEntidade registroAtualizado)
        {
            registroSelecionado.AtualizarInformacoes(registroAtualizado);
        }

        public virtual void Excluir(int id)
        {
            TEntidade registroSelecionado = SelecionarPorId(id);

            listaRegistros.Remove(registroSelecionado);
        }

        public virtual void Excluir(TEntidade registroSelecionado)
        {
            listaRegistros.Remove(registroSelecionado);
        }

        public virtual TEntidade SelecionarPorId(int id)
        {
            int index = listaRegistros.FindIndex(i => i.id == id);
            
            return listaRegistros[index];
        }

        public virtual List<TEntidade> SelecionarTodos()
        {
            return listaRegistros;
        }

        public bool TemRegistros()
        {
            return listaRegistros.Count > 0;
        }

      
    }
}
