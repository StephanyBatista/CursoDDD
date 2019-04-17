namespace Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao
{
    public class Solicitante
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }

        private Solicitante() { }

        public Solicitante(int id, string nome)
        {
            ExcecaoDeDominio.LancarQuando(string.IsNullOrEmpty(nome), "Nome do solicitante é inválido");

            Id = id;
            Nome = nome;
        }
    }
}