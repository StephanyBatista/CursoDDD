using System.Threading.Tasks;
using Manutencao.Solicitacao.Aplicacao;
using Manutencao.Solicitacao.Aplicacao.SolicitacoesDeManutencao;
using Microsoft.AspNetCore.Mvc;

namespace Manutencao.Solicitacao.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitacaoDeManteucaoController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SolicitadorDeManutencao _solicitadorDeManutencao;

        public SolicitacaoDeManteucaoController(IUnitOfWork unitOfWork,
            SolicitadorDeManutencao solicitadorDeManutencao)
        {
            _unitOfWork = unitOfWork;
            _solicitadorDeManutencao = solicitadorDeManutencao;
        }

        [HttpPost]
        public async Task Post([FromBody] SolicitacaoDeManutencaoDto dto)
        {
            _solicitadorDeManutencao.Solicitar(dto);
            await _unitOfWork.Commit();
        }
    }
}
