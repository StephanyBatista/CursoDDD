using System;

namespace Manutencao.Solicitacao.Aplicacao.SolicitacoesDeManutencao
{
    public class SolicitacaoDeManutencaoDto
    {
        public int SolicitanteId { get; set; }
        public string NomeDoSolicitante { get; set; }
        public int TipoDeSolicitacaoDeManutencao { get; set; }
        public string Justificativa { get; set; }
        public string NumeroDoContrato { get; set; }
        public DateTime InicioDesejadoParaManutencao { get; set; }
    }
}