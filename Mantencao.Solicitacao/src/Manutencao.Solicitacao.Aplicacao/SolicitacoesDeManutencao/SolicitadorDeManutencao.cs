using System;
using Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao;

namespace Manutencao.Solicitacao.Aplicacao.SolicitacoesDeManutencao
{
    public class SolicitadorDeManutencao
    {
        private readonly ISolicitacaoDeManutencaoRepositorio _solicitacaoDeManutencaoRepositorio;
        private readonly ICanceladorDeSolicitacoesDeManutencaoPendentes _canceladorDeSolicitacoesDeManutencaoPendentes;

        public SolicitadorDeManutencao(ISolicitacaoDeManutencaoRepositorio solicitacaoDeManutencaoRepositorio,
            ICanceladorDeSolicitacoesDeManutencaoPendentes canceladorDeSolicitacoesDeManutencaoPendentes)
        {
            _solicitacaoDeManutencaoRepositorio = solicitacaoDeManutencaoRepositorio;
            _canceladorDeSolicitacoesDeManutencaoPendentes = canceladorDeSolicitacoesDeManutencaoPendentes;
        }

        public void Solicitar(SolicitacaoDeManutencaoDto dto)
        {
            var tipoDeSolicitacaoDeManutencao =
                Enum.Parse<TipoDeSolicitacaoDeManutencao>(dto.TipoDeSolicitacaoDeManutencao.ToString());
            var solicitacaoDeManutencao = 
                new SolicitacaoDeManutencao(
                    dto.SolicitanteId, 
                    dto.NomeDoSolicitante,
                    tipoDeSolicitacaoDeManutencao, 
                    dto.Justificativa, 
                    dto.NumeroDoContrato, 
                    dto.NomeDaEmpresa, 
                    dto.CnpjDaEmpresa, 
                    dto.DataFinalDaVigencia,
                    dto.InicioDesejadoParaManutencao);

            var solicitacoesDeManutencaoPendentes =
                _solicitacaoDeManutencaoRepositorio.ObterPendentesDoTipo(tipoDeSolicitacaoDeManutencao);
            _canceladorDeSolicitacoesDeManutencaoPendentes.Cancelar(solicitacoesDeManutencaoPendentes);

            _solicitacaoDeManutencaoRepositorio.Adicionar(solicitacaoDeManutencao);
        }
    }
}