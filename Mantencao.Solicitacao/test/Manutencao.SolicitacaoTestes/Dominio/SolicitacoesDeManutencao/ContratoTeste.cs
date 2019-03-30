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
        private const string NomeDaEmpresa = "Gramas SA";
        private const string CnpjDaEmpresa = "90994785000158";
        private readonly DateTime _dataFinalDaVigencia = DateTime.Now.AddMonths(1);

        [Fact]
        public void Deve_criar_contrato()
        {
            var contratoEsperado = new
            {
                Numero,
                NomeDaEmpresa,
                CnpjDaEmpresa,
                DataFinalDaVigencia = _dataFinalDaVigencia
            };

            var contrato = new Contrato(Numero, NomeDaEmpresa, CnpjDaEmpresa, _dataFinalDaVigencia);

            contratoEsperado.ToExpectedObject().ShouldMatch(contrato);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Deve_validar_numero(string numeroDoContratoInvalido)
        {
            const string mensagemEsperada = "Número do contrato é inválido";

            AssertExtensions.ThrowsWithMessage(() =>
                new Contrato(numeroDoContratoInvalido, NomeDaEmpresa, CnpjDaEmpresa, _dataFinalDaVigencia), mensagemEsperada);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Deve_validar_nome_da_empresa(string nomeDaEmpresaInvalido)
        {
            const string mensagemEsperada = "Nome da empresa contrato é inválido";

            AssertExtensions.ThrowsWithMessage(() =>
                new Contrato(Numero, nomeDaEmpresaInvalido, CnpjDaEmpresa, _dataFinalDaVigencia), mensagemEsperada);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Deve_validar_cnpj_da_empresa(string cnpjDaEmpresaInvalido)
        {
            const string mensagemEsperada = "CNPJ da empresa do contrato é inválido";

            AssertExtensions.ThrowsWithMessage(() =>
                new Contrato(Numero, NomeDaEmpresa, cnpjDaEmpresaInvalido, _dataFinalDaVigencia), mensagemEsperada);
        }

        [Fact]
        public void Deve_validar_data_de_vigencia_do_contrato()
        {
            const string mensagemEsperada = "Vigência do contrato está vencido";
            var dataDaVigenciaDoContratoVencida = DateTime.Now.AddDays(-1);

            AssertExtensions.ThrowsWithMessage(() =>
                new Contrato(Numero, NomeDaEmpresa, CnpjDaEmpresa, dataDaVigenciaDoContratoVencida), mensagemEsperada);
        }
    }
}
