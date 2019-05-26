namespace Manutencao.Solicitacao.Dominio.Subsidiarias
{
    public class Subsidiaria : Entidade
    {
        public string Nome { get; set; }

        public Subsidiaria(string nome)
        {
            ExcecaoDeDominio.LancarQuando(string.IsNullOrEmpty(nome), "Nome da subsidiária é inválido");

            Nome = nome;
        }
    }
}