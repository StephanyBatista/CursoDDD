using Manutencao.Solicitacao.Aplicacao.Subsidiarias;
using Manutencao.Solicitacao.Dominio.Subsidiarias;
using Manutencao.Solicitacao.Infra.BancoDeDados.Contexto;

namespace Manutencao.Solicitacao.Infra.BancoDeDados.Repositorio
{
    public class SubsidiariaRepositorio : RepositorioBase<Subsidiaria>, ISubsidiariaRepositorio
    {
        public SubsidiariaRepositorio(ApplicationDbContext context) : base(context)
        {
        }
    }
}
