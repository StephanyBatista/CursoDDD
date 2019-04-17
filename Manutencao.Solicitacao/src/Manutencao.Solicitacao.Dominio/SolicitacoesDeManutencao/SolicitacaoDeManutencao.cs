using System;

namespace Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao
{
    public enum StatusDaSolicitacao
    {
        Pendente,
        Cancelada
    }

    public class SolicitacaoDeManutencao : Entidade<SolicitacaoDeManutencao>
    {
        public Solicitante Solicitante { get; private set; }
        public TipoDeSolicitacaoDeManutencao TipoDeSolicitacaoDeManutencao { get; private set; }
        public string Justificativa { get; private set; }
        public Contrato Contrato { get; private set; }
        public DateTime InicioDesejadoParaManutencao { get; private set; }
        public DateTime DataDaSolicitacao { get; private set; }
        public StatusDaSolicitacao StatusDaSolicitacao { get; private set; }

        private SolicitacaoDeManutencao() { }

        public SolicitacaoDeManutencao(int solicitanteId, string nomeDoSolicitante, 
            TipoDeSolicitacaoDeManutencao tipoDeSolicitacaoDeManutencao, 
            string justificativa, 
            string numeroDoContrato, string nomeDaEmpresa, string cnpjDaEmpresa, DateTime dataFinalDaVigência,
            DateTime inicioDesejadoParaManutencao)
        {
            
            ExcecaoDeDominio.LancarQuando(string.IsNullOrEmpty(justificativa), "Justificativa inválida");
            ExcecaoDeDominio.LancarQuando(inicioDesejadoParaManutencao.Date < DateTime.Now.Date, "Data do inicio desejado não pode ser inferior a data de hoje");

            Solicitante = new Solicitante(solicitanteId, nomeDoSolicitante);
            TipoDeSolicitacaoDeManutencao = tipoDeSolicitacaoDeManutencao;
            Justificativa = justificativa;
            Contrato = new Contrato(numeroDoContrato, nomeDaEmpresa, cnpjDaEmpresa, dataFinalDaVigência);
            InicioDesejadoParaManutencao = inicioDesejadoParaManutencao;
            DataDaSolicitacao = DateTime.Now;
            StatusDaSolicitacao = StatusDaSolicitacao.Pendente;
        }

        public void Cancelar()
        {
            StatusDaSolicitacao = StatusDaSolicitacao.Cancelada;
        }
    }
}