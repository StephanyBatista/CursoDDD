using ExpectedObjects;
using Manutencao.Solicitacao.Dominio.Subsidiarias;
using Manutencao.SolicitacaoTestes._Util;
using Xunit;

namespace Manutencao.SolicitacaoTestes.Dominio.Subsidiarias
{
    public class SubsidiariaTeste
    {
        [Fact]
        public void Deve_criar_subsidiaria()
        {
            var subsidiariaEsperada = new
            {
                Nome = "Campo Grande",
            };

            var subsidiaria = new Subsidiaria(subsidiariaEsperada.Nome);

            subsidiariaEsperada.ToExpectedObject().ShouldMatch(subsidiaria);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Deve_validar_nome(string nomeInvalido)
        {
            const string mensagemEsperada = "Nome da subsidiária é inválido";

            AssertExtensions.ThrowsWithMessage(() => new Subsidiaria(nomeInvalido), mensagemEsperada);
        }
    }
}
