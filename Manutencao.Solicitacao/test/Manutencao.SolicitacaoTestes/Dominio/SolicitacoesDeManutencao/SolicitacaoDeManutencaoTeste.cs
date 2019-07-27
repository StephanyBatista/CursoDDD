using System;
using ExpectedObjects;
using Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao;
using Manutencao.Solicitacao.Dominio.Subsidiarias;
using Manutencao.SolicitacaoTestes._Util;
using Nosbor.FluentBuilder.Lib;
using Xunit;

namespace Manutencao.SolicitacaoTestes.Dominio.SolicitacoesDeManutencao
{
    public class SolicitacaoDeManutencaoTeste
    {
        private const int IdentificadorDoSolicitante = 5;
        private const string NomeDoSoliciante = "Ricardo Almeida";
        private const string NumeroDoContrato = "234617";
        private const string NomeDaTerceirizadaDoContrato = "Grama SA";
        private const string CnpjDaTerceirizadaDoContrato = "59773744000191";
        private const string GestorDoContrato = "Hugo Alvez";
        private readonly DateTime _dataFinalDaVigenciaDoContrato = DateTime.Now.AddMonths(2);
        private readonly TipoDeSolicitacaoDeManutencao _tipoDeSolicitacaoDeManutencao = TipoDeSolicitacaoDeManutencao.Jardinagem;
        private string _identificadorDaSubsidiaria = "XPTO=33";
        private string _justificativa = "Grama muito alta";
        private DateTime _inicioDesejadoParaManutencao = DateTime.Now.AddDays(20);

        private SolicitacaoDeManutencao CriarNovaSolicitacao()
        {
            return new SolicitacaoDeManutencao(
                _identificadorDaSubsidiaria,
                IdentificadorDoSolicitante,
                NomeDoSoliciante,
                _tipoDeSolicitacaoDeManutencao,
                _justificativa,
                NumeroDoContrato,
                NomeDaTerceirizadaDoContrato,
                CnpjDaTerceirizadaDoContrato,
                GestorDoContrato,
                _dataFinalDaVigenciaDoContrato,
                _inicioDesejadoParaManutencao);
        }

        [Fact]
        public void Deve_criar_solicitacao_de_manutencao()
        {
            var solicitacao = new
            {
                IdentificadorDaSubsidiaria = _identificadorDaSubsidiaria,
                Solicitante = new Solicitante(IdentificadorDoSolicitante, NomeDoSoliciante),
                TipoDeSolicitacaoDeManutencao = _tipoDeSolicitacaoDeManutencao,
                Justificativa = _justificativa,
                Contrato = new Contrato(NumeroDoContrato, NomeDaTerceirizadaDoContrato, CnpjDaTerceirizadaDoContrato, GestorDoContrato, _dataFinalDaVigenciaDoContrato),
                InicioDesejadoParaManutencao = _inicioDesejadoParaManutencao
            };

            var solicitacaoDeManutencao = CriarNovaSolicitacao();

            solicitacao.ToExpectedObject().ShouldMatch(solicitacaoDeManutencao);
        }

        [Fact]
        public void Deve_solicitacao_de_manutencao_ter_data_da_solicitacao_de_hoje()
        {
            var dataDaSolicitacaoEsperada = DateTime.Now;

            var solicitacaoDeManutencao = CriarNovaSolicitacao();

            Assert.Equal(dataDaSolicitacaoEsperada.Date, solicitacaoDeManutencao.DataDaSolicitacao.Date);
        }

        [Fact]
        public void Deve_solicitacao_de_manutencao_iniciar_com_status_pendente()
        {
            var statusDaSolicitacao = StatusDaSolicitacao.Pendente;

            var solicitacaoDeManutencao = CriarNovaSolicitacao();

            Assert.Equal(statusDaSolicitacao, solicitacaoDeManutencao.StatusDaSolicitacao);
        }

        [Fact]
        public void Deve_validar_subsidiaria()
        {
            const string mensagemEsperada = "Subsidiária é inválida";
            _identificadorDaSubsidiaria = null;

            AssertExtensions.ThrowsWithMessage(() => CriarNovaSolicitacao(), mensagemEsperada);
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
            _justificativa = justificativaInvalida;

            AssertExtensions.ThrowsWithMessage(() => CriarNovaSolicitacao(), mensagemEsperada);
        }

        [Fact]
        public void Deve_validar_data_de_inicio_desejado_para_manutencao()
        {
            const string mensagemEsperada = "Data do inicio desejado não pode ser inferior a data de hoje";
            var dataInvalida = DateTime.Now.AddDays(-1);
            _inicioDesejadoParaManutencao = dataInvalida;

            AssertExtensions.ThrowsWithMessage(() => CriarNovaSolicitacao(), mensagemEsperada);
        }
    }
}
