using System;
using Manutencao.Solicitacao.Aplicacao.SolicitacoesDeManutencao;
using Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao;
using Nosbor.FluentBuilder.Lib;
using NSubstitute;
using Xunit;

namespace Manutencao.SolicitacaoTestes.Aplicacao.SolicitacoesDeManutencao
{
    public class SolicitadorDeManutencaoTeste
    {
        private readonly SolicitacaoDeManutencaoDto _dto;
        private readonly SolicitadorDeManutencao _solicitador;
        private readonly SolicitacaoDeManutencao _solicitacaoDeManutencao;
        private readonly ISolicitacaoDeManutencaoRepositorio _solicitacaoDeManutencaoRepositorio;
        private readonly ICanceladorDeSolicitacoesDeManutencaoPendentes _canceladorDeSolicitacoesDeManutencaoPendentes;

        public SolicitadorDeManutencaoTeste()
        {
            _dto = new SolicitacaoDeManutencaoDto
            {
                SubsidiariaId = "XPTO-ABC",
                SolicitanteId = 1,
                NomeDoSolicitante = "Ricardo José",
                TipoDeSolicitacaoDeManutencao = TipoDeSolicitacaoDeManutencao.Jardinagem.GetHashCode(),
                Justificativa = "Grama Alta",
                NumeroDoContrato = "2135",
                InicioDesejadoParaManutencao = DateTime.Now.AddMonths(2)
            };

            _solicitacaoDeManutencaoRepositorio = Substitute.For<ISolicitacaoDeManutencaoRepositorio>();
            _canceladorDeSolicitacoesDeManutencaoPendentes = Substitute.For<ICanceladorDeSolicitacoesDeManutencaoPendentes>();
            _solicitacaoDeManutencao = FluentBuilder<SolicitacaoDeManutencao>.New().With(s => s.IdentificadorDaSubsidiaria, _dto.SubsidiariaId).Build();
            var fabricaDeSolicitacaoDeManutencao = Substitute.For<FabricaDeSolicitacaoDeManutencao>();
            fabricaDeSolicitacaoDeManutencao.Fabricar(_dto).Returns(_solicitacaoDeManutencao);
            _solicitador = new SolicitadorDeManutencao(
                _solicitacaoDeManutencaoRepositorio, fabricaDeSolicitacaoDeManutencao, _canceladorDeSolicitacoesDeManutencaoPendentes);
        }

        [Fact]
        public void Deve_salvar_solicitacao_de_manutencao()
        {
            _solicitador.Solicitar(_dto);

            _solicitacaoDeManutencaoRepositorio.Received(1)
                .Adicionar(Arg.Is<SolicitacaoDeManutencao>(solicitacao =>
                    solicitacao == _solicitacaoDeManutencao));
        }
        
        [Fact]
        public void Deve_cancelar_solicitacoes_de_manutencao_pendentes_para_o_tipo_solicitado()
        {
            var solicitacoesPendentes = new[]
            {
                FluentBuilder<SolicitacaoDeManutencao>.New().With(solicitacao => solicitacao.StatusDaSolicitacao,
                    StatusDaSolicitacao.Pendente).Build()
            };
            _solicitacaoDeManutencaoRepositorio.ObterPendentesDoTipo(
                TipoDeSolicitacaoDeManutencao.Jardinagem, _dto.SubsidiariaId).Returns(solicitacoesPendentes);

            _solicitador.Solicitar(_dto);

            _canceladorDeSolicitacoesDeManutencaoPendentes.Received(1).Cancelar(solicitacoesPendentes);
        }
    }
}
