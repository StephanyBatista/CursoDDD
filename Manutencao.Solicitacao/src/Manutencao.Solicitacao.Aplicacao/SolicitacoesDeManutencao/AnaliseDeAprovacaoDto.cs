namespace Manutencao.Solicitacao.Aplicacao.SolicitacoesDeManutencao
{
    public class AnaliseDeAprovacaoDto
    {
        public int AprovadorId { get; set; }
        public string NomeDoAprovador { get; set; }
        public string Justificativa { get; set; }
        public bool Aprovado { get; set; }
        public string IdDaSolicitacao { get; set; }
    }
}