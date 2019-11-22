using Manutencao.Solicitacao.Aplicacao.SolicitacoesDeManutencao;
using Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao;
using Nosbor.FluentBuilder.Lib;
using NSubstitute;
using Xunit;

namespace Manutencao.SolicitacaoTestes.Aplicacao.SolicitacoesDeManutencao
{
    public class AnaliseDeAprovacaoDaSolicitacaoDeManutencaoTeste
    {
        private readonly AnaliseDeAprovacaoDto _dto;
        private readonly AnaliseDeAprovacaoDaSolicitacaoDeManutencao _analiseDeAprovacaoDaSoliicitacao;
        private readonly SolicitacaoDeManutencao _solicitacaoDeManutencao;
        private readonly ISolicitacaoDeManutencaoRepositorio _solicitacaoDeManutencaoRepositorio;

        public AnaliseDeAprovacaoDaSolicitacaoDeManutencaoTeste()
        {
            _dto = new AnaliseDeAprovacaoDto { IdDaSolicitacao = "XPTO", AprovadorId = 1, NomeDoAprovador = "Mario", Justificativa = "Grama Alta"};
            _solicitacaoDeManutencao = FluentBuilder<SolicitacaoDeManutencao>.New().Build();
            _solicitacaoDeManutencaoRepositorio = Substitute.For<ISolicitacaoDeManutencaoRepositorio>();
            _solicitacaoDeManutencaoRepositorio.ObterPorId(_dto.IdDaSolicitacao).Returns(_solicitacaoDeManutencao);
            _analiseDeAprovacaoDaSoliicitacao = new AnaliseDeAprovacaoDaSolicitacaoDeManutencao(_solicitacaoDeManutencaoRepositorio);
        }

        [Fact]
        public void Deve_reprovar_solicitacao_de_manutencao()
        {
            _dto.Aprovado = false;

            _analiseDeAprovacaoDaSoliicitacao.Reprovar(_dto);

            Assert.Equal(StatusDaSolicitacao.Reprovada, _solicitacaoDeManutencao.StatusDaSolicitacao);
        }

        [Fact]
        public void Deve_aprovar_solicitacao_de_manutencao()
        {
            _dto.Aprovado = false;

            //_analiseDeAprovacaoDaSoliicitacao.Aprovar()

            Assert.Equal(StatusDaSolicitacao.Reprovada, _solicitacaoDeManutencao.StatusDaSolicitacao);
        }
    }

    public class AnaliseDeAprovacaoDaSolicitacaoDeManutencao
    {
        private readonly ISolicitacaoDeManutencaoRepositorio _solicitacaoDeManutencaoRepositorio;

        public AnaliseDeAprovacaoDaSolicitacaoDeManutencao(
            ISolicitacaoDeManutencaoRepositorio solicitacaoDeManutencaoRepositorio)
        {
            _solicitacaoDeManutencaoRepositorio = solicitacaoDeManutencaoRepositorio;
        }

        public void Reprovar(AnaliseDeAprovacaoDto dto)
        {
            var solicitacao = _solicitacaoDeManutencaoRepositorio.ObterPorId(dto.IdDaSolicitacao);
            solicitacao.Reprovar(new Autor(dto.AprovadorId, dto.NomeDoAprovador));
        }
    }

    public class AnaliseDeAprovacaoDto
    {
        public int AprovadorId { get; set; }
        public string NomeDoAprovador { get; set; }
        public string Justificativa { get; set; }
        public bool Aprovado { get; set; }
        public string IdDaSolicitacao { get; set; }
    }
}