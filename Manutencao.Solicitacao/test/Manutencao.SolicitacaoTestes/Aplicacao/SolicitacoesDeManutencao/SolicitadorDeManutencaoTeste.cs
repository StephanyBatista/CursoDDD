using System;
using Manutencao.Solicitacao.Aplicacao.SolicitacoesDeManutencao;
using Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao;
using Manutencao.SolicitacaoTestes._Util;
using Nosbor.FluentBuilder.Lib;
using NSubstitute;
using Xunit;

namespace Manutencao.SolicitacaoTestes.Aplicacao.SolicitacoesDeManutencao
{
    public class SolicitadorDeManutencaoTeste
    {
        private readonly ContratoDto _contratoDto;
        private readonly SolicitacaoDeManutencaoDto _dto;
        private readonly SolicitadorDeManutencao _solicitador;
        private readonly IBuscadorDeContrato _buscadorDeContrato;
        private readonly ISolicitacaoDeManutencaoRepositorio _repositorio;
        private readonly ICanceladorDeSolicitacoesDeManutencaoPendentes _canceladorDeSolicitacoesDeManutencaoPendentes;

        public SolicitadorDeManutencaoTeste()
        {
            _dto = new SolicitacaoDeManutencaoDto
            {
                SolicitanteId = 1,
                NomeDoSolicitante = "Ricardo José",
                TipoDeSolicitacaoDeManutencao = TipoDeSolicitacaoDeManutencao.ApararGrama.GetHashCode(),
                Justificativa = "Grama Alta",
                NumeroDoContrato = "2135",
                InicioDesejadoParaManutencao = DateTime.Now.AddMonths(2)
            };
            _contratoDto = new ContratoDto
            {
                Numero = _dto.NumeroDoContrato,
                NomeDaEmpresa = "Grama SA",
                CnpjDaEmpresa = "00000000000000",
                DataFinalDaVigencia = DateTime.Now.AddMonths(1)
            };

            _repositorio = Substitute.For<ISolicitacaoDeManutencaoRepositorio>();
            _canceladorDeSolicitacoesDeManutencaoPendentes = Substitute.For<ICanceladorDeSolicitacoesDeManutencaoPendentes>();
            _buscadorDeContrato = Substitute.For<IBuscadorDeContrato>();
            _buscadorDeContrato.Buscar(_dto.NumeroDoContrato).Returns(_contratoDto);
            _solicitador = new SolicitadorDeManutencao(_repositorio, _buscadorDeContrato, _canceladorDeSolicitacoesDeManutencaoPendentes);
            
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
        public void Deve_solicitacao_criada_ter_informacoes_do_contrato_buscado()
        {
            _solicitador.Solicitar(_dto);

            _repositorio.Received(1)
                .Adicionar(Arg.Is<SolicitacaoDeManutencao>(solicitacao =>
                    solicitacao.Contrato.Numero == _contratoDto.Numero &&
                    solicitacao.Contrato.NomeDaEmpresa == _contratoDto.NomeDaEmpresa &&
                    solicitacao.Contrato.CnpjDaEmpresa == _contratoDto.CnpjDaEmpresa &&
                    solicitacao.Contrato.DataFinalDaVigencia == _contratoDto.DataFinalDaVigencia));
        }

        [Fact]
        public void Deve_validar_contrato_quando_nao_encontrado_no_erp()
        {
            const string mensagemEsperada = "Contrato não encontrado no ERP";
            _buscadorDeContrato.Buscar(_dto.NumeroDoContrato).Returns((ContratoDto) null);

            AssertExtensions.ThrowsWithMessage(() => _solicitador.Solicitar(_dto), mensagemEsperada);
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
