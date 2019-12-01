using System.Threading.Tasks;
using Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao;

namespace Manutencao.Solicitacao.Aplicacao.SolicitacoesDeManutencao
{
    public interface INotificaContextoDeServico
    {
        Task Notificar(SolicitacaoDeManutencao solicitacaoDeManutencao);
    }
}
