using System.Collections.Generic;
using System.Linq;
using Manutencao.Solicitacao.Aplicacao.SolicitacoesDeManutencao;
using Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao;
using Manutencao.Solicitacao.Dominio.Subsidiarias;
using Manutencao.Solicitacao.Infra.BancoDeDados.Contexto;

namespace Manutencao.Solicitacao.Infra.BancoDeDados.Repositorio
{
    public class SolicitacaoDeManutencaoRepositorio : RepositorioBase<SolicitacaoDeManutencao>, ISolicitacaoDeManutencaoRepositorio
    {
        public SolicitacaoDeManutencaoRepositorio(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<SolicitacaoDeManutencao> ObterPendentesDoTipo(
            TipoDeSolicitacaoDeManutencao tipo, Subsidiaria subsidiaria)
        {
            return Context.Set<SolicitacaoDeManutencao>()
                .Where(entidade => 
                    entidade.TipoDeSolicitacaoDeManutencao == tipo &&
                    entidade.Subsidiaria == subsidiaria);
        }
    }
}
