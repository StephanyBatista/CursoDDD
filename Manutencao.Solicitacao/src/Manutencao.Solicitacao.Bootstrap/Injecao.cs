using Manutencao.Solicitacao.Aplicacao;
using Manutencao.Solicitacao.Aplicacao.SolicitacoesDeManutencao;
using Manutencao.Solicitacao.Aplicacao.Subsidiarias;
using Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao;
using Manutencao.Solicitacao.Infra.BancoDeDados.Contexto;
using Manutencao.Solicitacao.Infra.BancoDeDados.Repositorio;
using Manutencao.Solicitacao.Infra.ContextoDeServico;
using Manutencao.Solicitacao.Infra.Email;
using Manutencao.Solicitacao.Infra.ErpContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Manutencao.Solicitacao.Bootstrap
{
    public static class Injecao 
    {
        public static void Inicializar(IServiceCollection services, string conexaoDoBancoDeDados)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(conexaoDoBancoDeDados));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ISolicitacaoDeManutencaoRepositorio, 
                SolicitacaoDeManutencaoRepositorio>();
            services.AddScoped<ISubsidiariaRepositorio, SubsidiariaRepositorio>();
            services.AddScoped<IBuscadorDeContrato>(buscador => 
                new BuscadorDeContrato("http://localhost:3000/contracts"));
            services.AddScoped<INotificaContextoDeServico>(buscador =>
                new NotificaContextoDeServico("http://localhost:3000/servico"));
            services.AddScoped<INotificaReprovacaoParaSolicitante,
                NotificaReprovacaoParaSolicitante>();
            services.AddScoped<ICanceladorDeSolicitacoesDeManutencaoPendentes, 
                CanceladorDeSolicitacoesDeManutencaoPendentes>();
            services.AddScoped<SolicitadorDeManutencao, SolicitadorDeManutencao>();
            services.AddScoped<FabricaDeSolicitacaoDeManutencao, FabricaDeSolicitacaoDeManutencao>();
            services.AddScoped<AnaliseDeAprovacaoDaSolicitacaoDeManutencao, AnaliseDeAprovacaoDaSolicitacaoDeManutencao>();
        }
    }
}
