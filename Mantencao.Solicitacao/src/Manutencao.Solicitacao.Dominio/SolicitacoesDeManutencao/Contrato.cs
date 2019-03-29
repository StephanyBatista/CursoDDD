using System;

namespace Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao
{
    public class Contrato
    {
        public string Numero { get; }
        public string NomeDaEmpresa { get; }
        public string CnpjDaEmpresa { get; }
        public DateTime DataFinalDaVigencia { get; }

        public Contrato(string numero, string nomeDaEmpresa, string cnpjDaEmpresa, DateTime dataFinalDaVigencia)
        {
            ExcecaoDeDominio.LancarQuando(string.IsNullOrEmpty(numero), "Número do contrato é inválido");
            ExcecaoDeDominio.LancarQuando(string.IsNullOrEmpty(nomeDaEmpresa), "Nome da empresa contrato é inválido");
            ExcecaoDeDominio.LancarQuando(
                string.IsNullOrEmpty(cnpjDaEmpresa) || 
                cnpjDaEmpresa.Length != 14, "CNPJ da empresa do contrato é inválido");
            ExcecaoDeDominio.LancarQuando(dataFinalDaVigencia.Date < DateTime.Now.Date, "Vigência do contrato está vencido");

            Numero = numero;
            NomeDaEmpresa = nomeDaEmpresa;
            CnpjDaEmpresa = cnpjDaEmpresa;
            DataFinalDaVigencia = dataFinalDaVigencia;
        }
    }
}