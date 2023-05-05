namespace ControleDeBar.ConsoleApp.Compartilhado;

public interface ITelaBase
{
    string ApresentarMenu();
    void InserirNovoRegistro();
    void VisualizarRegistros(bool mostrarCabecalho);
    void EditarRegistro();
    void ExcluirRegistro();

}
