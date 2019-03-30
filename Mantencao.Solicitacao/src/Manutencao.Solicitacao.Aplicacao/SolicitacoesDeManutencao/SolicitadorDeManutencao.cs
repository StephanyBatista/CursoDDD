using System;
using Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao;

namespace Manutencao.Solicitacao.Aplicacao.SolicitacoesDeManutencao
{
    public class SolicitadorDeManutencao
    {
        private readonly ISolicitacaoDeManutencaoRepositorio _solicitacaoDeManutencaoRepositorio;

        public SolicitadorDeManutencao(ISolicitacaoDeManutencaoRepositorio solicitacaoDeManutencaoRepositorio)
        {
            _solicitacaoDeManutencaoRepositorio = solicitacaoDeManutencaoRepositorio;
        }

        public void Solicitar(SolicitacaoDeManutencaoDto dto)
        {
            var solicitacaoDeManutencao = 
                new SolicitacaoDeManutencao(
                    dto.SolicitanteId, 
                    dto.NomeDoSolicitante, 
                    Enum.Parse<TipoDeSolicitacaoDeManutencao>(dto.TipoDeSolicitacaoDeManutencao.ToString()), 
                    dto.Justificativa, 
                    dto.NumeroDoContrato, 
                    dto.NomeDaEmpresa, 
                    dto.CnpjDaEmpresa, 
                    dto.DataFinalDaVigencia,
                    dto.InicioDesejadoParaManutencao);

            _solicitacaoDeManutencaoRepositorio.Adicionar(solicitacaoDeManutencao);
        }
    }
}