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
        private readonly ISolicitacaoDeManutencaoRepositorio _repositorio;
        private readonly SolicitadorDeManutencao _solicitador;
        private readonly SolicitacaoDeManutencaoDto _dto;
        private readonly ICanceladorDeSolicitacoesDeManutencaoPendentes _canceladorDeSolicitacoesDeManutencaoPendentes;

        public SolicitadorDeManutencaoTeste()
        {
            _repositorio = Substitute.For<ISolicitacaoDeManutencaoRepositorio>();
            _canceladorDeSolicitacoesDeManutencaoPendentes =
                Substitute.For<ICanceladorDeSolicitacoesDeManutencaoPendentes>();
            _solicitador = new SolicitadorDeManutencao(_repositorio, _canceladorDeSolicitacoesDeManutencaoPendentes);
            _dto = new SolicitacaoDeManutencaoDto
            {
                SolicitanteId = 1,
                NomeDoSolicitante = "Ricardo José",
                TipoDeSolicitacaoDeManutencao = TipoDeSolicitacaoDeManutencao.ApararGrama.GetHashCode(),
                Justificativa = "Grama Alta",
                NumeroDoContrato = "2135",
                NomeDaEmpresa = "Grama SA",
                CnpjDaEmpresa = "59773744000191",
                DataFinalDaVigencia = DateTime.Now.AddMonths(2),
                InicioDesejadoParaManutencao = DateTime.Now.AddMonths(2)
            };
        }

        [Fact]
        public void Deve_salvar_solicitacao_de_manutencao()
        {
            _solicitador.Solicitar(_dto);

            _repositorio.Received(1)
                .Adicionar(Arg.Is<SolicitacaoDeManutencao>(solicitacao =>
                    solicitacao.Solicitante.Id == _dto.SolicitanteId));
        }

        [Fact]
        public void Deve_cancelar_solicitacoes_de_manutencao_pendentes_para_o_tipo_solicitado()
        {
            var solicitacoesPendentes = new[]
            {
                FluentBuilder<SolicitacaoDeManutencao>.New().With(solicitacao => solicitacao.StatusDaSolicitacao,
                    StatusDaSolicitacao.Pendente).Build()
            };
            _repositorio.ObterPendentesDoTipo(TipoDeSolicitacaoDeManutencao.ApararGrama).Returns(solicitacoesPendentes);

            _solicitador.Solicitar(_dto);

            _canceladorDeSolicitacoesDeManutencaoPendentes.Received(1).Cancelar(solicitacoesPendentes);
        }
    }
}
