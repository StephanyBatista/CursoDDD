using System.Threading.Tasks;

namespace Manutencao.Solicitacao.Aplicacao
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
