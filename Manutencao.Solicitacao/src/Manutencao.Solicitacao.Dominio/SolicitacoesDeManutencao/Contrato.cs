using System;

namespace Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao
{
    public class Contrato
    {
        public string Numero { get; private set; }
        public string NomeDaTerceirizada { get; private set; }
        public string CnpjDaTerceirizada { get; private set; }
        public string GestorDoContrato { get; private set; }
        public DateTime DataFinalDaVigencia { get; private set; }

        private Contrato() { }

        public Contrato(string numero, string nomeDaTerceirizada, string cnpjDaTerceirizada, string gestorDoContrato, DateTime dataFinalDaVigencia)
        {
            ExcecaoDeDominio.LancarQuando(string.IsNullOrEmpty(numero), "Número do contrato é inválido");
            ExcecaoDeDominio.LancarQuando(string.IsNullOrEmpty(nomeDaTerceirizada), "Nome da terceirizada é inválida");
            ExcecaoDeDominio.LancarQuando(
                string.IsNullOrEmpty(cnpjDaTerceirizada) || 
                cnpjDaTerceirizada.Length != 14, "CNPJ da terceirizada é inválida");
            ExcecaoDeDominio.LancarQuando(string.IsNullOrEmpty(gestorDoContrato), "Gestor do contrato é inválido");
            ExcecaoDeDominio.LancarQuando(dataFinalDaVigencia.Date < DateTime.Now.Date, "Vigência do contrato está vencido");

            Numero = numero;
            NomeDaTerceirizada = nomeDaTerceirizada;
            CnpjDaTerceirizada = cnpjDaTerceirizada;
            GestorDoContrato = gestorDoContrato;
            DataFinalDaVigencia = dataFinalDaVigencia;
        }
    }
}