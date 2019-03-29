using System;
using ExpectedObjects;
using Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao;
using Manutencao.SolicitacaoTestes._Util;
using Nosbor.FluentBuilder;
using Xunit;

namespace Manutencao.SolicitacaoTestes.SolicitacoesDeManutencao
{
    public class SolicitacaoDeManutencaoTeste
    {
        private readonly Solicitante _solicitante = FluentBuilder<Solicitante>.New().Build();
        private readonly Contrato _contrato = FluentBuilder<Contrato>.New().Build();
        private readonly TipoDeSolicitacaoDeManutencao _tipoDeSolicitacaoDeManutencao = TipoDeSolicitacaoDeManutencao.ApararGrama;
        private readonly DateTime _inicioDesejadoParaManutencao = DateTime.Now.AddDays(20);
        private const string Justificativa = "Grama muito alta";

        [Fact]
        public void Deve_criar_solicitacao_de_manutencao()
        {
            var solicitacao = new
            {
                Solicitante = _solicitante,
                TipoDeSolicitacaoDeManutencao = _tipoDeSolicitacaoDeManutencao,
                Justificativa,
                Contrato = _contrato,
                InicioDesejadoParaManutencao = _inicioDesejadoParaManutencao
            };

            var solicitacaoDeManutencao = new SolicitacaoDeManutencao(
                _solicitante,
                _tipoDeSolicitacaoDeManutencao,
                Justificativa,
                _contrato,
                _inicioDesejadoParaManutencao);

            solicitacao.ToExpectedObject().ShouldMatch(solicitacaoDeManutencao);
        }

        [Fact]
        public void Deve_solicitacao_de_manutencao_ter_data_da_solicitacao_de_hoje()
        {
            var dataDaSolicitacaoEsperada = DateTime.Now;

            var solicitacaoDeManutencao = new SolicitacaoDeManutencao(
                _solicitante,
                _tipoDeSolicitacaoDeManutencao,
                Justificativa,
                _contrato,
                _inicioDesejadoParaManutencao);

            Assert.Equal(dataDaSolicitacaoEsperada.Date, solicitacaoDeManutencao.DataDaSolicitacao.Date);
        }

        [Fact]
        public void Deve_validar_solicitante()
        {
            const string mensagemEsperada = "Solicitante inválido";
            const Solicitante solicitanteInvalido = null;

            AssertExtensions.ThrowsWithMessage(() => new SolicitacaoDeManutencao(
                solicitanteInvalido,
                _tipoDeSolicitacaoDeManutencao,
                Justificativa,
                _contrato,
                _inicioDesejadoParaManutencao), mensagemEsperada);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Deve_validar_justificativa(string justificativaInvalida)
        {
            const string mensagemEsperada = "Justificativa inválida";

            AssertExtensions.ThrowsWithMessage(() => new SolicitacaoDeManutencao(
                _solicitante,
                _tipoDeSolicitacaoDeManutencao,
                justificativaInvalida,
                _contrato,
                _inicioDesejadoParaManutencao), mensagemEsperada);
        }

        [Fact]
        public void Deve_validar_contrato()
        {
            const string mensagemEsperada = "Contrato inválido";
            const Contrato contratoInvalido = null;

            AssertExtensions.ThrowsWithMessage(() => new SolicitacaoDeManutencao(
                _solicitante,
                _tipoDeSolicitacaoDeManutencao,
                Justificativa,
                contratoInvalido,
                _inicioDesejadoParaManutencao), mensagemEsperada);
        }
    }
}
