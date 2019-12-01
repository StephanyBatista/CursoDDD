using Manutencao.Solicitacao.Dominio;
using Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao;

namespace Manutencao.Solicitacao.Aplicacao.SolicitacoesDeManutencao
{
    public class AnaliseDeAprovacaoDaSolicitacaoDeManutencao
    {
        private readonly ISolicitacaoDeManutencaoRepositorio _solicitacaoDeManutencaoRepositorio;
        private readonly INotificaReprovacaoParaSolicitante _notificaReprovacaoParaSolicitante;
        private readonly INotificaContextoDeServico _notificaContextoDeServico;

        public AnaliseDeAprovacaoDaSolicitacaoDeManutencao(
            ISolicitacaoDeManutencaoRepositorio solicitacaoDeManutencaoRepositorio,
            INotificaReprovacaoParaSolicitante notificaReprovacaoParaSolicitante,
            INotificaContextoDeServico notificaContextoDeServico)
        {
            _solicitacaoDeManutencaoRepositorio = solicitacaoDeManutencaoRepositorio;
            _notificaReprovacaoParaSolicitante = notificaReprovacaoParaSolicitante;
            _notificaContextoDeServico = notificaContextoDeServico;
        }

        public void Analisar(AnaliseDeAprovacaoDto analiseDeAprovacaoDto)
        {
            var solicitacaoDeManutencao =
                _solicitacaoDeManutencaoRepositorio.ObterPorId(analiseDeAprovacaoDto.IdDaSolicitacao);
            ExcecaoDeDominio.LancarQuando(solicitacaoDeManutencao == null, "Solicitação não encontrada");

            var aprovador = new Autor(analiseDeAprovacaoDto.IdentificadorDoAprovador, analiseDeAprovacaoDto.NomeDoAprovador);

            if (analiseDeAprovacaoDto.Aprovado)
                Aprovar(solicitacaoDeManutencao, aprovador);
            else
                Reprovar(solicitacaoDeManutencao, aprovador);
        }

        private void Aprovar(SolicitacaoDeManutencao solicitacaoDeManutencao, Autor aprovador)
        {
            solicitacaoDeManutencao.Aprovar(aprovador);
            _notificaContextoDeServico.Notificar(solicitacaoDeManutencao);
        }

        private void Reprovar(SolicitacaoDeManutencao solicitacaoDeManutencao, Autor aprovador)
        {
            solicitacaoDeManutencao.Reprovar(aprovador);
            _notificaReprovacaoParaSolicitante.Notificar(solicitacaoDeManutencao);
        }
    }
}
