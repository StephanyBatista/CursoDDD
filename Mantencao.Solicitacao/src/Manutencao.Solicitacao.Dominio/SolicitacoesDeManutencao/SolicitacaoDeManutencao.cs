using System;

namespace Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao
{
    public class SolicitacaoDeManutencao
    {
        public int SolicitanteId { get; }
        public TipoDeSolicitacaoDeManutencao TipoDeSolicitacaoDeManutencao { get; }
        public string Justificativa { get; }
        public string NumeroDoContrato { get; }
        public string EmpresaDoContrato { get; }
        public string CnpjDaEmpresaDoContrato { get; }
        public DateTime InicioDesejadoParaManutencao { get; }
        public DateTime DataDaSolicitacao { get; set; }

        public SolicitacaoDeManutencao(int solicitanteId, 
            TipoDeSolicitacaoDeManutencao tipoDeSolicitacaoDeManutencao, 
            string justificativa, 
            string numeroDoContrato, 
            string empresaDoContrato, 
            string cnpjDaEmpresaDoContrato, 
            DateTime inicioDesejadoParaManutencao)
        {
            SolicitanteId = solicitanteId;
            TipoDeSolicitacaoDeManutencao = tipoDeSolicitacaoDeManutencao;
            Justificativa = justificativa;
            NumeroDoContrato = numeroDoContrato;
            EmpresaDoContrato = empresaDoContrato;
            CnpjDaEmpresaDoContrato = cnpjDaEmpresaDoContrato;
            InicioDesejadoParaManutencao = inicioDesejadoParaManutencao;
            DataDaSolicitacao = DateTime.Now;
        }
    }
}