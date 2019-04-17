using System.Collections.Generic;

namespace Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao
{
    public class CanceladorDeSolicitacoesDeManutencaoPendentes : ICanceladorDeSolicitacoesDeManutencaoPendentes
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