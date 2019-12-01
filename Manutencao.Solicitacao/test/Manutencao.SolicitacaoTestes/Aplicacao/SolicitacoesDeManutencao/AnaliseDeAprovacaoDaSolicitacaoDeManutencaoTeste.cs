using Manutencao.Solicitacao.Aplicacao.SolicitacoesDeManutencao;
using Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao;
using Manutencao.SolicitacaoTestes._Util;
using Nosbor.FluentBuilder.Lib;
using NSubstitute;
using Xunit;

namespace Manutencao.SolicitacaoTestes.Aplicacao.SolicitacoesDeManutencao
{
    public class AnaliseDeAprovacaoDaSolicitacaoDeManutencaoTeste
    {
        private readonly AnaliseDeAprovacaoDto _dto;
        private readonly AnaliseDeAprovacaoDaSolicitacaoDeManutencao _analiseDeAprovacaoDaSolicitacao;
        private readonly SolicitacaoDeManutencao _solicitacaoDeManutencao;
        private readonly INotificaReprovacaoParaSolicitante _notificaReprovacaoParaSolicitante;
        private readonly INotificaContextoDeServico _notificaContextoDeServico;

        public AnaliseDeAprovacaoDaSolicitacaoDeManutencaoTeste()
        {
            _dto = new AnaliseDeAprovacaoDto { IdDaSolicitacao = "XPTO", IdentificadorDoAprovador = 1, NomeDoAprovador = "Mario"};
            _solicitacaoDeManutencao = FluentBuilder<SolicitacaoDeManutencao>.New().Build();
            var solicitacaoDeManutencaoRepositorio = Substitute.For<ISolicitacaoDeManutencaoRepositorio>();
            _notificaReprovacaoParaSolicitante = Substitute.For<INotificaReprovacaoParaSolicitante>();
            _notificaContextoDeServico = Substitute.For<INotificaContextoDeServico>();
            solicitacaoDeManutencaoRepositorio.ObterPorId(_dto.IdDaSolicitacao).Returns(_solicitacaoDeManutencao);
            _analiseDeAprovacaoDaSolicitacao =
                new AnaliseDeAprovacaoDaSolicitacaoDeManutencao(
                    solicitacaoDeManutencaoRepositorio, _notificaReprovacaoParaSolicitante, _notificaContextoDeServico);
        }

        [Fact]
        public void Deve_reprovar_solicitacao_de_manutencao()
        {
            _analiseDeAprovacaoDaSolicitacao.Analisar(_dto);

            Assert.Equal(StatusDaSolicitacao.Reprovada, _solicitacaoDeManutencao.StatusDaSolicitacao);
        }

        [Fact]
        public void Deve_validar_solicitacao_de_manutencao_a_ser_analisada()
        {
            const string mensagemEsperada = "Solicitação não encontrada";
            const string idDaSolicitacaoInvalido = "WERT";
            _dto.IdDaSolicitacao = idDaSolicitacaoInvalido;

            AssertExtensions.ThrowsWithMessage(() => _analiseDeAprovacaoDaSolicitacao.Analisar(_dto), mensagemEsperada);
        }

        [Fact]
        public void Deve_notificar_solicitante_sobre_reprovacao()
        {
            _analiseDeAprovacaoDaSolicitacao.Analisar(_dto);

            _notificaReprovacaoParaSolicitante.Received(1).Notificar(_solicitacaoDeManutencao);
        }

        [Fact]
        public void Deve_aprovar_solicitacao_de_manutencao()
        {
            _dto.Aprovado = true;

            _analiseDeAprovacaoDaSolicitacao.Analisar(_dto);

            Assert.Equal(StatusDaSolicitacao.Aprovada, _solicitacaoDeManutencao.StatusDaSolicitacao);
        }

        [Fact]
        public void Nao_deve_notificar_solicitante_quando_aprovado()
        {
            _dto.Aprovado = true;

            _analiseDeAprovacaoDaSolicitacao.Analisar(_dto);

            _notificaReprovacaoParaSolicitante.DidNotReceive().Notificar(_solicitacaoDeManutencao);
        }

        [Fact]
        public void Deve_notificar_contexto_de_servico_quando_aprovar()
        {
            _dto.Aprovado = true;

            _analiseDeAprovacaoDaSolicitacao.Analisar(_dto);

            _notificaContextoDeServico.Received(1).Notificar(_solicitacaoDeManutencao);
        }

        [Fact]
        public void Nao_deve_notificar_contexto_de_servico_quando_reprovar()
        {
            _dto.Aprovado = false;

            _analiseDeAprovacaoDaSolicitacao.Analisar(_dto);

            _notificaContextoDeServico.DidNotReceive().Notificar(_solicitacaoDeManutencao);
        }
    }
}