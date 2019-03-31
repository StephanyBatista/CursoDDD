using System.Threading.Tasks;

namespace Manutencao.Solicitacao.Aplicacao.SolicitacoesDeManutencao
{
    public interface IBuscadorDeContrato
    {
        Task<ContratoDto> Buscar(string numero);
    }
}
