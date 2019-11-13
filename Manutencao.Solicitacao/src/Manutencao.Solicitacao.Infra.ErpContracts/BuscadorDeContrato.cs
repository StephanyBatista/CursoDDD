using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Manutencao.Solicitacao.Aplicacao.SolicitacoesDeManutencao;
using Newtonsoft.Json;

namespace Manutencao.Solicitacao.Infra.ErpContracts
{
    public class BuscadorDeContrato : IBuscadorDeContrato
    {
        private readonly string _endereco;

        public BuscadorDeContrato(string endereco)
        {
            _endereco = endereco;
        }

        public async Task<ContratoDto> Buscar(string numero)
        {
            using (var cliente = new HttpClient())
            {
                var respostaDoServidor = await cliente.GetAsync($"{_endereco}/{numero}");

                if (!respostaDoServidor.IsSuccessStatusCode)
                {
                    if (respostaDoServidor.StatusCode == HttpStatusCode.NotFound)
                        return null;
                    throw new Exception("Comunicação com ERP Contrato não disponível");
                }

                var conteudo = await respostaDoServidor.Content.ReadAsStringAsync();
                var contratoDoErp = JsonConvert.DeserializeObject<ContratoDoErp>(conteudo);
                return new ContratoDto
                {
                    Numero = contratoDoErp.Id,
                    NomeDaTerceirizada = contratoDoErp.Company,
                    CnpjDaTerceirizada = contratoDoErp.CompanyId,
                    GestorDoContrato = contratoDoErp.Manager,
                    DataFinalDaVigencia = DateTime.Parse(contratoDoErp.FinalDate)
                };
            }
        }
    }
}
