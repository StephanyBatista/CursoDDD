using System;

namespace Manutencao.Solicitacao.Aplicacao.SolicitacoesDeManutencao
{
    public class ContratoDto
    {
        public string Numero { get; set; }
        public string NomeDaEmpresa { get; set; }
        public string CnpjDaEmpresa { get; set; }
        public DateTime DataFinalDaVigencia { get; set; }
    }
}
