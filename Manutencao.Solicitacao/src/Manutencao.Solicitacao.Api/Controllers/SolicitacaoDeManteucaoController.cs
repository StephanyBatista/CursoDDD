using Manutencao.Solicitacao.Aplicacao.SolicitacoesDeManutencao;
using Microsoft.AspNetCore.Mvc;

namespace Manutencao.Solicitacao.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitacaoDeManteucaoController : ControllerBase
    {
        private readonly SolicitadorDeManutencao _solicitadorDeManutencao;

        public SolicitacaoDeManteucaoController(SolicitadorDeManutencao solicitadorDeManutencao)
        {
            _solicitadorDeManutencao = solicitadorDeManutencao;
        }

        [HttpPost]
        public void Post([FromBody] SolicitacaoDeManutencaoDto dto)
        {
            _solicitadorDeManutencao.Solicitar(dto);
        }
    }
}
