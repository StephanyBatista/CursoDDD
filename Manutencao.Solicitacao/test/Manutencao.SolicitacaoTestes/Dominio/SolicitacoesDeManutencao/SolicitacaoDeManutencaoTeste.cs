using System;
using ExpectedObjects;
using Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao;
using Manutencao.SolicitacaoTestes._Util;
using Nosbor.FluentBuilder.Lib;
using Xunit;

namespace Manutencao.SolicitacaoTestes.Dominio.SolicitacoesDeManutencao
{
    public class SolicitacaoDeManutencaoTeste
    {
        private const string Justificativa = "Grama muito alta";
        private const int SolicitanteId = 5;
        private const string NomeDoSoliciante = "Ricardo Almeida";
        private const string NumeroDoContrato = "234617";
        private const string NomeDaEmpresaDoContrato = "Grama SA";
        private const string CnpjDaEmpresaDoContrato = "59773744000191";
        private readonly DateTime _dataFinalDaVigenciaDoContrato = DateTime.Now.AddMonths(2);
        private readonly DateTime _inicioDesejadoParaManutencao = DateTime.Now.AddDays(20);
        private readonly TipoDeSolicitacaoDeManutencao _tipoDeSolicitacaoDeManutencao = TipoDeSolicitacaoDeManutencao.ApararGrama;

        [Fact]
        public void Deve_criar_solicitacao_de_manutencao()
        {
            var solicitacao = new
            {
                Solicitante = new Solicitante(SolicitanteId, NomeDoSoliciante),
                TipoDeSolicitacaoDeManutencao = _tipoDeSolicitacaoDeManutencao,
                Justificativa,
                Contrato = new Contrato(NumeroDoContrato, NomeDaEmpresaDoContrato, CnpjDaEmpresaDoContrato, _dataFinalDaVigenciaDoContrato),
                InicioDesejadoParaManutencao = _inicioDesejadoParaManutencao
            };

            var solicitacaoDeManutencao = new SolicitacaoDeManutencao(
                SolicitanteId,
                NomeDoSoliciante,
                _tipoDeSolicitacaoDeManutencao,
                Justificativa,
                NumeroDoContrato,
                NomeDaEmpresaDoContrato,
                CnpjDaEmpresaDoContrato,
                _dataFinalDaVigenciaDoContrato,
                _inicioDesejadoParaManutencao);

            solicitacao.ToExpectedObject().ShouldMatch(solicitacaoDeManutencao);
        }

        [Fact]
        public void Deve_solicitacao_de_manutencao_ter_data_da_solicitacao_de_hoje()
        {
            var dataDaSolicitacaoEsperada = DateTime.Now;

            var solicitacaoDeManutencao = new SolicitacaoDeManutencao(
                SolicitanteId,
                NomeDoSoliciante,
                _tipoDeSolicitacaoDeManutencao,
                Justificativa,
                NumeroDoContrato,
                NomeDaEmpresaDoContrato,
                CnpjDaEmpresaDoContrato,
                _dataFinalDaVigenciaDoContrato,
                _inicioDesejadoParaManutencao);

            Assert.Equal(dataDaSolicitacaoEsperada.Date, solicitacaoDeManutencao.DataDaSolicitacao.Date);
        }

        [Fact]
        public void Deve_solicitacao_de_manutencao_iniciar_com_status_pendente()
        {
            var statusDaSolicitacao = StatusDaSolicitacao.Pendente;

            var solicitacaoDeManutencao = new SolicitacaoDeManutencao(
                SolicitanteId,
                NomeDoSoliciante,
                _tipoDeSolicitacaoDeManutencao,
                Justificativa,
                NumeroDoContrato,
                NomeDaEmpresaDoContrato,
                CnpjDaEmpresaDoContrato,
                _dataFinalDaVigenciaDoContrato,
                _inicioDesejadoParaManutencao);

            Assert.Equal(statusDaSolicitacao, solicitacaoDeManutencao.StatusDaSolicitacao);
        }

        [Fact]
        public void Deve_cancelar_solicitacao_de_manutencao()
        {
            var solicitacao = FluentBuilder<SolicitacaoDeManutencao>.New().Build();

            solicitacao.Cancelar();

            Assert.Equal(StatusDaSolicitacao.Cancelada, solicitacao.StatusDaSolicitacao);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Deve_validar_justificativa(string justificativaInvalida)
        {
            const string mensagemEsperada = "Justificativa inválida";

            AssertExtensions.ThrowsWithMessage(() => new SolicitacaoDeManutencao(
                SolicitanteId,
                NomeDoSoliciante,
                _tipoDeSolicitacaoDeManutencao,
                justificativaInvalida,
                NumeroDoContrato,
                NomeDaEmpresaDoContrato,
                CnpjDaEmpresaDoContrato,
                _dataFinalDaVigenciaDoContrato,
                _inicioDesejadoParaManutencao), mensagemEsperada);
        }

        [Fact]
        public void Deve_validar_data_de_inicio_desejado_para_manutencao()
        {
            const string mensagemEsperada = "Data do inicio desejado não pode ser inferior a data de hoje";
            var dataInvalida = DateTime.Now.AddDays(-1);

            AssertExtensions.ThrowsWithMessage(() => new SolicitacaoDeManutencao(
                SolicitanteId,
                NomeDoSoliciante,
                _tipoDeSolicitacaoDeManutencao,
                Justificativa,
                NumeroDoContrato,
                NomeDaEmpresaDoContrato,
                CnpjDaEmpresaDoContrato,
                _dataFinalDaVigenciaDoContrato,
                dataInvalida), mensagemEsperada);
        }
    }
}
