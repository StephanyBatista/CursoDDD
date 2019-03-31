using System.Threading.Tasks;
using Manutencao.Solicitacao.Aplicacao;

namespace Manutencao.Solicitacao.Infra.BancoDeDados.Contexto
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
