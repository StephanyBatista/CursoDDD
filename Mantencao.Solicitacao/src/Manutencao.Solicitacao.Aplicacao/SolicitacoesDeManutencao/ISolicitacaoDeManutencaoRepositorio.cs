using System.Collections.Generic;
using Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao;

namespace Manutencao.Solicitacao.Aplicacao.SolicitacoesDeManutencao
{
    public interface ISolicitacaoDeManutencaoRepositorio
    {
        void Adicionar(SolicitacaoDeManutencao solicitacao);
        IEnumerable<SolicitacaoDeManutencao> ObterPendentesDoTipo(TipoDeSolicitacaoDeManutencao apararGrama);
    }
}