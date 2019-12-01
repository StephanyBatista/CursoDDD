using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Manutencao.Solicitacao.Aplicacao.SolicitacoesDeManutencao;
using Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao;
using Newtonsoft.Json;

namespace Manutencao.Solicitacao.Infra.ContextoDeServico
{
    public class NotificaContextoDeServico : INotificaContextoDeServico
    {
        private readonly string _endereco;

        public NotificaContextoDeServico(string endereco)
        {
            _endereco = endereco;
        }

        public async Task Notificar(SolicitacaoDeManutencao solicitacaoDeManutencao)
        {
            using (var cliente = new HttpClient())
            {
                var conteudoDoJson = JsonConvert.SerializeObject(new
                {
                    solicitacaoDeManutencao.Id,
                    solicitacaoDeManutencao.Justificativa,
                    solicitacaoDeManutencao.DataDaSolicitacao,
                    solicitacaoDeManutencao.IdentificadorDaSubsidiaria,
                    NumeroDoContrato = solicitacaoDeManutencao.Contrato.Numero
                });
                var conteudo = new StringContent(conteudoDoJson, Encoding.UTF8, "application/json");
                var respostaDoServidor = await cliente.PostAsync($"{_endereco}/servico", conteudo);

                if (!respostaDoServidor.IsSuccessStatusCode)
                    throw new Exception("Comunicação com contexto de serviço não disponível");
            }
        }
    }
}
