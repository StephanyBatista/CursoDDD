using Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao;

namespace Manutencao.Solicitacao.Aplicacao.SolicitacoesDeManutencao
{
    public interface IEmailDeReprovacaoParaSolicitante
    {
        void Enviar(SolicitacaoDeManutencao solicitadorDeManutencao);
    }
}
