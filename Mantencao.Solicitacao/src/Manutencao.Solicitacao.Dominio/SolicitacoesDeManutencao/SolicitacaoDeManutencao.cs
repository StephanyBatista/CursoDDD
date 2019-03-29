using System;

namespace Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao
{
    public class SolicitacaoDeManutencao : Entidade<SolicitacaoDeManutencao>
    {
        public Solicitante Solicitante { get; }
        public TipoDeSolicitacaoDeManutencao TipoDeSolicitacaoDeManutencao { get; }
        public string Justificativa { get; }
        public Contrato Contrato { get; }
        public DateTime InicioDesejadoParaManutencao { get; }
        public DateTime DataDaSolicitacao { get; set; }

        public SolicitacaoDeManutencao(Solicitante solicitante, 
            TipoDeSolicitacaoDeManutencao tipoDeSolicitacaoDeManutencao, 
            string justificativa, 
            Contrato contrato,
            DateTime inicioDesejadoParaManutencao)
        {
            ExcecaoDeDominio.LancarQuando(solicitante == null, "Solicitante inválido");
            ExcecaoDeDominio.LancarQuando(string.IsNullOrEmpty(justificativa), "Justificativa inválida");
            ExcecaoDeDominio.LancarQuando(contrato == null, "Contrato inválido");

            Solicitante = solicitante;
            TipoDeSolicitacaoDeManutencao = tipoDeSolicitacaoDeManutencao;
            Justificativa = justificativa;
            Contrato = contrato;
            InicioDesejadoParaManutencao = inicioDesejadoParaManutencao;
            DataDaSolicitacao = DateTime.Now;
        }
    }
}