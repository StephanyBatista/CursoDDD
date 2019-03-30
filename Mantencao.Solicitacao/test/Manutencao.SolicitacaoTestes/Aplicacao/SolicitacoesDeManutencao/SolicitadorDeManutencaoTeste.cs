using System;
using Manutencao.Solicitacao.Aplicacao.SolicitacoesDeManutencao;
using Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao;
using NSubstitute;
using Xunit;

namespace Manutencao.SolicitacaoTestes.Aplicacao.SolicitacoesDeManutencao
{
    public class SolicitadorDeManutencaoTeste
    {
        [Fact]
        public void Deve_salvar_solicitacao_de_manutencao()
        {
            var repositorio = Substitute.For<ISolicitacaoDeManutencaoRepositorio>();
            var solicitador = new SolicitadorDeManutencao(repositorio);
            var dto = new SolicitacaoDeManutencaoDto
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

            solicitador.Solicitar(dto);

            repositorio.Received(1)
                .Adicionar(Arg.Is<SolicitacaoDeManutencao>(solicitacao =>
                    solicitacao.Solicitante.Id == dto.SolicitanteId));
        }
    }
}
