using System.Collections.Generic;
using Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao;
using Nosbor.FluentBuilder.Lib;
using Xunit;

namespace Manutencao.SolicitacaoTestes.Dominio.SolicitacoesDeManutencao
{
    public class CanceladorDeSolicitacoesDeManutencaoPendentesTeste
    {
        [Fact]
        public void Deve_cancelar_todas_solicitacoes_de_manutencao()
        {
            var solicitacoesPendentes = new[]
            {
                FluentBuilder<SolicitacaoDeManutencao>.New().With(solicitacao => solicitacao.StatusDaSolicitacao,
                    StatusDaSolicitacao.Pendente).Build(),
                FluentBuilder<SolicitacaoDeManutencao>.New().With(solicitacao => solicitacao.StatusDaSolicitacao,
                    StatusDaSolicitacao.Pendente).Build(),
                FluentBuilder<SolicitacaoDeManutencao>.New().With(solicitacao => solicitacao.StatusDaSolicitacao,
                    StatusDaSolicitacao.Pendente).Build(),
            };
            var cancelador = new CanceladorDeSolicitacoesDeManutencaoPendentes();

            cancelador.Cancelar(solicitacoesPendentes);

            Assert.DoesNotContain(solicitacoesPendentes, solitacao => solitacao.StatusDaSolicitacao == StatusDaSolicitacao.Pendente);
        }

        [Fact]
        public void Nao_deve_lancar_execao_quando_solicitacoes_de_manutencao_for_nula()
        {
            var cancelador = new CanceladorDeSolicitacoesDeManutencaoPendentes();

            cancelador.Cancelar(null);
        }
    }

    public class CanceladorDeSolicitacoesDeManutencaoPendentes
    {
        public void Cancelar(IEnumerable<SolicitacaoDeManutencao> solicitacoesDeManutencaoPendentes)
        {
            if (solicitacoesDeManutencaoPendentes == null)
                return;

            foreach (var solicitacaoDeManutencao in solicitacoesDeManutencaoPendentes)
            {
                solicitacaoDeManutencao.Cancelar();
            }
        }
    }
}
