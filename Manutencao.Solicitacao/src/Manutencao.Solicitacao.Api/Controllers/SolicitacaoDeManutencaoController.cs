using System.Collections.Generic;
using System.Linq;
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
        private readonly ISolicitacaoDeManutencaoRepositorio _solicitacaoDeManutencaoRepositorio;

        public SolicitacaoDeManutencaoController(IUnitOfWork unitOfWork,
            SolicitadorDeManutencao solicitadorDeManutencao, 
            AnaliseDeAprovacaoDaSolicitacaoDeManutencao analiseDeAprovacaoDaSolicitacaoDeManutencao, 
            ISolicitacaoDeManutencaoRepositorio solicitacaoDeManutencaoRepositorio)
        {
            _unitOfWork = unitOfWork;
            _solicitadorDeManutencao = solicitadorDeManutencao;
            _analiseDeAprovacaoDaSolicitacaoDeManutencao = analiseDeAprovacaoDaSolicitacaoDeManutencao;
            _solicitacaoDeManutencaoRepositorio = solicitacaoDeManutencaoRepositorio;
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

        [HttpGet("pendentes/{identificadorDaSubsidiaria}")]
        public IEnumerable<dynamic> Get(string identificadorDaSubsidiaria)
        {
            var solicitacoesPendentes = _solicitacaoDeManutencaoRepositorio.ObterPendentesDa(identificadorDaSubsidiaria);

            return solicitacoesPendentes.Select(solicitacao => new
            {
                solicitacao.Id,
                solicitacao.DataDaSolicitacao,
                solicitacao.Justificativa,
                NomeDoSolicitante = solicitacao.Solicitante.Nome,
                Contrato = solicitacao.Contrato.Numero,
                solicitacao.InicioDesejadoParaManutencao,
                TipoDeSolicitacaoDeManutencao = solicitacao.TipoDeSolicitacaoDeManutencao.ToString()
            }).ToList();
        }
    }
}
