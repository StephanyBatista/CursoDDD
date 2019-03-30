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
        public Solicitante Solicitante { get; }
        public TipoDeSolicitacaoDeManutencao TipoDeSolicitacaoDeManutencao { get; }
        public string Justificativa { get; }
        public Contrato Contrato { get; }
        public DateTime InicioDesejadoParaManutencao { get; }
        public DateTime DataDaSolicitacao { get; }
        public StatusDaSolicitacao StatusDaSolicitacao { get; private set; }

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