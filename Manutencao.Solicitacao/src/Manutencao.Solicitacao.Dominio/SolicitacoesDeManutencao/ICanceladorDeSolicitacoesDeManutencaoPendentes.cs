using System.Collections.Generic;

namespace Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao
{
    public interface ICanceladorDeSolicitacoesDeManutencaoPendentes
    {
        void Cancelar(IEnumerable<SolicitacaoDeManutencao> solicitacoesDeManutencaoPendentes);
    }
}