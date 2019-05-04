using System;
using ExpectedObjects;
using Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao;
using Manutencao.SolicitacaoTestes._Util;
using Xunit;

namespace Manutencao.SolicitacaoTestes.Dominio.SolicitacoesDeManutencao
{
    public class ContratoTeste
    {
        private const string Numero = "224587";
        private const string NomeDaTerceirizada = "Gramas SA";
        private const string CnpjDaTerceirizada = "90994785000158";
        private const string GestorDoContrato = "Hugo Alvez";
        private readonly DateTime DataFinalDaVigencia = DateTime.Now.AddMonths(1);

        [Fact]
        public void Deve_criar_contrato()
        {
            var contratoEsperado = new
            {
                Numero,
                NomeDaTerceirizada,
                CnpjDaTerceirizada,
                GestorDoContrato,
                DataFinalDaVigencia
            };

            var contrato = new Contrato(Numero, NomeDaTerceirizada, CnpjDaTerceirizada, GestorDoContrato, DataFinalDaVigencia);

            contratoEsperado.ToExpectedObject().ShouldMatch(contrato);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Deve_validar_numero(string numeroDoContratoInvalido)
        {
            const string mensagemEsperada = "Número do contrato é inválido";

            AssertExtensions.ThrowsWithMessage(() =>
                new Contrato(numeroDoContratoInvalido, NomeDaTerceirizada, CnpjDaTerceirizada, GestorDoContrato, DataFinalDaVigencia), mensagemEsperada);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Deve_validar_nome_da_terceirizada(string nomeDaTerceirizadaInvalida)
        {
            const string mensagemEsperada = "Nome da terceirizada é inválida";

            AssertExtensions.ThrowsWithMessage(() =>
                new Contrato(Numero, nomeDaTerceirizadaInvalida, CnpjDaTerceirizada, GestorDoContrato, DataFinalDaVigencia), mensagemEsperada);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Deve_validar_cnpj_da_terceirizada(string cnpjDaTerceirizadaInvalido)
        {
            const string mensagemEsperada = "CNPJ da terceirizada é inválida";

            AssertExtensions.ThrowsWithMessage(() =>
                new Contrato(Numero, NomeDaTerceirizada, cnpjDaTerceirizadaInvalido, GestorDoContrato, DataFinalDaVigencia), mensagemEsperada);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Deve_validar_gestor_do_contrato(string gestorDoContratoInvalido)
        {
            const string mensagemEsperada = "Gestor do contrato é inválido";

            AssertExtensions.ThrowsWithMessage(() =>
                new Contrato(Numero, NomeDaTerceirizada, CnpjDaTerceirizada, gestorDoContratoInvalido, DataFinalDaVigencia), mensagemEsperada);
        }

        [Fact]
        public void Deve_validar_data_de_vigencia_do_contrato()
        {
            const string mensagemEsperada = "Vigência do contrato está vencido";
            var dataDaVigenciaDoContratoVencida = DateTime.Now.AddDays(-1);

            AssertExtensions.ThrowsWithMessage(() =>
                new Contrato(Numero, NomeDaTerceirizada, CnpjDaTerceirizada, GestorDoContrato, dataDaVigenciaDoContratoVencida), mensagemEsperada);
        }
    }
}
