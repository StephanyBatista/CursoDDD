namespace Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao
{
    public class Autor
    {
        public int Identificador { get; }
        public string Nome { get; }

        private Autor() { }

        public Autor(int identificador, string nome)
        {
            ExcecaoDeDominio.LancarQuando(string.IsNullOrEmpty(nome), "Nome do solicitante é inválido");

            Identificador = identificador;
            Nome = nome;
        }
    }
}