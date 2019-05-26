namespace Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao
{
    public class Solicitante
    {
        public int Identificador { get; private set; }
        public string Nome { get; private set; }

        private Solicitante() { }

        public Solicitante(int identificador, string nome)
        {
            ExcecaoDeDominio.LancarQuando(string.IsNullOrEmpty(nome), "Nome do solicitante é inválido");

            Identificador = identificador;
            Nome = nome;
        }
    }
}