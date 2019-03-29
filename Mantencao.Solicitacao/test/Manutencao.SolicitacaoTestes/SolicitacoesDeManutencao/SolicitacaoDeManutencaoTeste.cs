using System;
using ExpectedObjects;
using Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao;
using Xunit;

namespace Manutencao.SolicitacaoTestes.SolicitacoesDeManutencao
{
    public class SolicitacaoDeManutencaoTeste
    {
        [Fact]
        public void Deve_criar_solicitacao_de_manutencao()
        {
            var solicitacao = new
            {
                SolicitanteId = 1,
                TipoDeSolicitacaoDeManutencao = TipoDeSolicitacaoDeManutencao.ApararGrama,
                Justificativa = "Grama muito alta",
                NumeroDoContrato = "221345",
                EmpresaDoContrato = "Cortadores de Grama SA",
                CnpjDaEmpresaDoContrato = "1234123654",
                InicioDesejadoParaManutencao = DateTime.Now.AddDays(20)
            };

            var solicitacaoDeManutencao = new SolicitacaoDeManutencao(
                solicitacao.SolicitanteId, 
                solicitacao.TipoDeSolicitacaoDeManutencao, 
                solicitacao.Justificativa, 
                solicitacao.NumeroDoContrato, 
                solicitacao.EmpresaDoContrato, 
                solicitacao.CnpjDaEmpresaDoContrato, 
                solicitacao.InicioDesejadoParaManutencao);

            solicitacao.ToExpectedObject().ShouldMatch(solicitacaoDeManutencao);
        }

        [Fact]
        public void Deve_solicitacao_de_manutencao_ter_data_da_solicitacao_de_hoje()
        {
            var dataDaSolicitacaoEsperada = DateTime.Now;
            var solicitacao = new
            {
                SolicitanteId = 1,
                TipoDeSolicitacaoDeManutencao = TipoDeSolicitacaoDeManutencao.ApararGrama,
                Justificativa = "Grama muito alta",
                NumeroDoContrato = "221345",
                EmpresaDoContrato = "Cortadores de Grama SA",
                CnpjDaEmpresaDoContrato = "1234123654",
                InicioDesejadoParaManutencao = DateTime.Now.AddDays(20)
            };

            var solicitacaoDeManutencao = new SolicitacaoDeManutencao(
                solicitacao.SolicitanteId,
                solicitacao.TipoDeSolicitacaoDeManutencao,
                solicitacao.Justificativa,
                solicitacao.NumeroDoContrato,
                solicitacao.EmpresaDoContrato,
                solicitacao.CnpjDaEmpresaDoContrato,
                solicitacao.InicioDesejadoParaManutencao);

            Assert.Equal(dataDaSolicitacaoEsperada.Date, solicitacaoDeManutencao.DataDaSolicitacao.Date);
        }
    }
}
