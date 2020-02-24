using System.Threading.Tasks;
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

        public async Task Analisar(AnaliseDeAprovacaoDto analiseDeAprovacaoDto)
        {
            var solicitacaoDeManutencao =
                _solicitacaoDeManutencaoRepositorio.ObterPorId(analiseDeAprovacaoDto.IdDaSolicitacao);
            ExcecaoDeDominio.LancarQuando(solicitacaoDeManutencao == null, "Solicitação não encontrada");
            ExcecaoDeDominio.LancarQuando(solicitacaoDeManutencao.Reprovada(), "Solicitação já analisada e está reprovada");
            ExcecaoDeDominio.LancarQuando(solicitacaoDeManutencao.Aprovada(), "Solicitação já analisada e está aprovada");

            var aprovador = new Autor(analiseDeAprovacaoDto.AprovadorId, analiseDeAprovacaoDto.NomeDoAprovador);

            if (analiseDeAprovacaoDto.Aprovado)
                await Aprovar(solicitacaoDeManutencao, aprovador);
            else
                Reprovar(solicitacaoDeManutencao, aprovador);
        }

        private async Task Aprovar(SolicitacaoDeManutencao solicitacaoDeManutencao, Autor aprovador)
        {
            solicitacaoDeManutencao.Aprovar(aprovador);
            await _notificaContextoDeServico.Notificar(solicitacaoDeManutencao);
        }

        private void Reprovar(SolicitacaoDeManutencao solicitacaoDeManutencao, Autor aprovador)
        {
            solicitacaoDeManutencao.Reprovar(aprovador);
            _notificaReprovacaoParaSolicitante.Notificar(solicitacaoDeManutencao);
        }
    }
}
