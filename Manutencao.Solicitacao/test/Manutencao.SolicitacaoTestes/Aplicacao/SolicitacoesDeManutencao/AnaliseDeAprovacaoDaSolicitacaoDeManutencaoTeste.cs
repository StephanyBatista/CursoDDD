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
        private SolicitacaoDeManutencao _solicitacaoDeManutencao;
        private readonly INotificaReprovacaoParaSolicitante _notificaReprovacaoParaSolicitante;
        private readonly INotificaContextoDeServico _notificaContextoDeServico;
        private readonly ISolicitacaoDeManutencaoRepositorio _solicitacaoDeManutencaoRepositorio;

        public AnaliseDeAprovacaoDaSolicitacaoDeManutencaoTeste()
        {
            _dto = new AnaliseDeAprovacaoDto { IdDaSolicitacao = "XPTO", AprovadorId = 1, NomeDoAprovador = "Mario"};
            _solicitacaoDeManutencao = FluentBuilder<SolicitacaoDeManutencao>.New().Build();
            _solicitacaoDeManutencaoRepositorio = Substitute.For<ISolicitacaoDeManutencaoRepositorio>();
            _notificaReprovacaoParaSolicitante = Substitute.For<INotificaReprovacaoParaSolicitante>();
            _notificaContextoDeServico = Substitute.For<INotificaContextoDeServico>();
            _solicitacaoDeManutencaoRepositorio.ObterPorId(_dto.IdDaSolicitacao).Returns(_solicitacaoDeManutencao);
            _analiseDeAprovacaoDaSolicitacao =
                new AnaliseDeAprovacaoDaSolicitacaoDeManutencao(
                    _solicitacaoDeManutencaoRepositorio, _notificaReprovacaoParaSolicitante, _notificaContextoDeServico);

        }

        [Fact]
        public void Deve_reprovar_solicitacao_de_manutencao()
        {
            _analiseDeAprovacaoDaSolicitacao.Analisar(_dto).Wait();

            Assert.Equal(StatusDaSolicitacao.Reprovada, _solicitacaoDeManutencao.StatusDaSolicitacao);
        }

        [Fact]
        public void Deve_validar_solicitacao_de_manutencao_a_ser_analisada()
        {
            const string mensagemEsperada = "Solicitação não encontrada";
            const string idDaSolicitacaoInvalido = "WERT";
            _dto.IdDaSolicitacao = idDaSolicitacaoInvalido;

            AssertExtensions.ThrowsWithMessageAsync(() => _analiseDeAprovacaoDaSolicitacao.Analisar(_dto), mensagemEsperada);
        }

        [Fact]
        public void Deve_validar_solicitacao_de_manutencao_quando_ja_reprovada()
        {
            const string mensagemEsperada = "Solicitação já analisada e está reprovada";
            _solicitacaoDeManutencao = FluentBuilder<SolicitacaoDeManutencao>.New().With(solicitacao => solicitacao.StatusDaSolicitacao, StatusDaSolicitacao.Reprovada).Build();
            _solicitacaoDeManutencaoRepositorio.ObterPorId(_dto.IdDaSolicitacao).Returns(_solicitacaoDeManutencao);

            AssertExtensions.ThrowsWithMessageAsync(() => _analiseDeAprovacaoDaSolicitacao.Analisar(_dto), mensagemEsperada);
        }

        [Fact]
        public void Deve_validar_solicitacao_de_manutencao_quando_ja_aprovada()
        {
            const string mensagemEsperada = "Solicitação já analisada e está aprovada";
            _solicitacaoDeManutencao = FluentBuilder<SolicitacaoDeManutencao>.New().With(solicitacao => solicitacao.StatusDaSolicitacao, StatusDaSolicitacao.Aprovada).Build();
            _solicitacaoDeManutencaoRepositorio.ObterPorId(_dto.IdDaSolicitacao).Returns(_solicitacaoDeManutencao);

            AssertExtensions.ThrowsWithMessageAsync(() => _analiseDeAprovacaoDaSolicitacao.Analisar(_dto), mensagemEsperada);
        }

        [Fact]
        public void Deve_notificar_solicitante_sobre_reprovacao()
        {
            _analiseDeAprovacaoDaSolicitacao.Analisar(_dto).Wait();

            _notificaReprovacaoParaSolicitante.Received(1).Notificar(_solicitacaoDeManutencao);
        }

        [Fact]
        public void Deve_aprovar_solicitacao_de_manutencao()
        {
            _dto.Aprovado = true;

            _analiseDeAprovacaoDaSolicitacao.Analisar(_dto).Wait();

            Assert.Equal(StatusDaSolicitacao.Aprovada, _solicitacaoDeManutencao.StatusDaSolicitacao);
        }

        [Fact]
        public void Nao_deve_notificar_solicitante_quando_aprovado()
        {
            _dto.Aprovado = true;

            _analiseDeAprovacaoDaSolicitacao.Analisar(_dto).Wait();

            _notificaReprovacaoParaSolicitante.DidNotReceive().Notificar(_solicitacaoDeManutencao);
        }

        [Fact]
        public void Deve_notificar_contexto_de_servico_quando_aprovar()
        {
            _dto.Aprovado = true;

            _analiseDeAprovacaoDaSolicitacao.Analisar(_dto).Wait();

            _notificaContextoDeServico.Received(1).Notificar(_solicitacaoDeManutencao);
        }

        [Fact]
        public void Nao_deve_notificar_contexto_de_servico_quando_reprovar()
        {
            _dto.Aprovado = false;

            _analiseDeAprovacaoDaSolicitacao.Analisar(_dto).Wait();

            _notificaContextoDeServico.DidNotReceive().Notificar(_solicitacaoDeManutencao);
        }
    }
}