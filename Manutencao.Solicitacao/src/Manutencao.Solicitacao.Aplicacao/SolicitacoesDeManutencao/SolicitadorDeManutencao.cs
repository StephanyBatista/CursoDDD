using Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao;

namespace Manutencao.Solicitacao.Aplicacao.SolicitacoesDeManutencao
{
    public class SolicitadorDeManutencao
    {
        private readonly ISolicitacaoDeManutencaoRepositorio _solicitacaoDeManutencaoRepositorio;
        private readonly FabricaDeSolicitacaoDeManutencao _fabricaDeSolicitacaoDeManutencao;
        private readonly ICanceladorDeSolicitacoesDeManutencaoPendentes _canceladorDeSolicitacoesDeManutencaoPendentes;

        public SolicitadorDeManutencao(ISolicitacaoDeManutencaoRepositorio solicitacaoDeManutencaoRepositorio,
            FabricaDeSolicitacaoDeManutencao fabricaDeSolicitacaoDeManutencao,
            ICanceladorDeSolicitacoesDeManutencaoPendentes canceladorDeSolicitacoesDeManutencaoPendentes)
        {
            _solicitacaoDeManutencaoRepositorio = solicitacaoDeManutencaoRepositorio;
            _fabricaDeSolicitacaoDeManutencao = fabricaDeSolicitacaoDeManutencao;
            _canceladorDeSolicitacoesDeManutencaoPendentes = canceladorDeSolicitacoesDeManutencaoPendentes;
        }

        public void Solicitar(SolicitacaoDeManutencaoDto dto)
        {
            var solicitacaoDeManutencao = _fabricaDeSolicitacaoDeManutencao.Fabricar(dto);

            var solicitacoesDeManutencaoPendentes =
                _solicitacaoDeManutencaoRepositorio
                    .ObterPendentesDoTipo(solicitacaoDeManutencao.TipoDeSolicitacaoDeManutencao, solicitacaoDeManutencao.IdentificadorDaSubsidiaria);
            _canceladorDeSolicitacoesDeManutencaoPendentes.Cancelar(solicitacoesDeManutencaoPendentes);
            _solicitacaoDeManutencaoRepositorio.Adicionar(solicitacaoDeManutencao);
        }
    }
}