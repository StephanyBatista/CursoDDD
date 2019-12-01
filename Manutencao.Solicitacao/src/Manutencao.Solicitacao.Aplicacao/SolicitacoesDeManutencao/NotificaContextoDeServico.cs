using Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao;

namespace Manutencao.Solicitacao.Aplicacao.SolicitacoesDeManutencao
{
    public interface INotificaContextoDeServico
    {
        void Notificar(SolicitacaoDeManutencao solicitacaoDeManutencao);
    }
}
