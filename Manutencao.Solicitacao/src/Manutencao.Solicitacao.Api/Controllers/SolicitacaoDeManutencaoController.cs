using System.Threading.Tasks;
using Manutencao.Solicitacao.Aplicacao;
using Manutencao.Solicitacao.Aplicacao.SolicitacoesDeManutencao;
using Microsoft.AspNetCore.Mvc;

namespace Manutencao.Solicitacao.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitacaoDeManutencaoController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SolicitadorDeManutencao _solicitadorDeManutencao;
        private readonly AnaliseDeAprovacaoDaSolicitacaoDeManutencao _analiseDeAprovacaoDaSolicitacaoDeManutencao;

        public SolicitacaoDeManutencaoController(IUnitOfWork unitOfWork,
            SolicitadorDeManutencao solicitadorDeManutencao, 
            AnaliseDeAprovacaoDaSolicitacaoDeManutencao analiseDeAprovacaoDaSolicitacaoDeManutencao)
        {
            _unitOfWork = unitOfWork;
            _solicitadorDeManutencao = solicitadorDeManutencao;
            _analiseDeAprovacaoDaSolicitacaoDeManutencao = analiseDeAprovacaoDaSolicitacaoDeManutencao;
        }

        [HttpPost]
        public async Task Post([FromBody] SolicitacaoDeManutencaoDto dto)
        {
            _solicitadorDeManutencao.Solicitar(dto);
            await _unitOfWork.Commit();
        }

        [HttpPut("analise")]
        public async Task<IActionResult> Put([FromBody] AnaliseDeAprovacaoDto dto)
        {
            await _analiseDeAprovacaoDaSolicitacaoDeManutencao.Analisar(dto);
            await _unitOfWork.Commit();
            return Ok();
        }
    }
}
