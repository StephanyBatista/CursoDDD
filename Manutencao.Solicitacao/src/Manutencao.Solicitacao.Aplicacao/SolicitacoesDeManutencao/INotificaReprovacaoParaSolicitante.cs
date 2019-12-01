using Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao;

namespace Manutencao.Solicitacao.Aplicacao.SolicitacoesDeManutencao
{
    public interface INotificaReprovacaoParaSolicitante
    {
        void Notificar(SolicitacaoDeManutencao solicitacaoDeManutencao);
    }
}
