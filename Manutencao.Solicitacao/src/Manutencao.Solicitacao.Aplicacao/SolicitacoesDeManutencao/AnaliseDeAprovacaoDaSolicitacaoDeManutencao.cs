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

        public void Analisar(AnaliseDeAprovacaoDto dto)
        {
            var solicitacao = _solicitacaoDeManutencaoRepositorio.ObterPorId(dto.IdDaSolicitacao);
            ExcecaoDeDominio.LancarQuando(solicitacao == null, "Solicitação não encontrada");

            if (dto.Aprovado)
            {
                solicitacao.Aprovar(new Autor(dto.AprovadorId, dto.NomeDoAprovador));
                _notificaContextoDeServico.Notificar(solicitacao);
            }
            else
            {
                solicitacao.Reprovar(new Autor(dto.AprovadorId, dto.NomeDoAprovador));
                _notificaReprovacaoParaSolicitante.Notificar(solicitacao);
            }
        }
    }
}