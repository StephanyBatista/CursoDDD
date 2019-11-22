using ExpectedObjects;
using Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao;
using Manutencao.SolicitacaoTestes._Util;
using Xunit;

namespace Manutencao.SolicitacaoTestes.Dominio.SolicitacoesDeManutencao
{
    public class AutorTeste
    {
        private const int Identificador = 55;
        private const string Nome = "Henrique Almeida";

        [Fact]
        public void Deve_criar()
        {
            var autorEsperado = new
            {
                Identificador,
                Nome
            };

            var autor = new Autor(autorEsperado.Identificador, autorEsperado.Nome);

            autorEsperado.ToExpectedObject().ShouldMatch(autor);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Deve_validar_nome(string nomeInvalido)
        {
            const string mensagemEsperada = "Nome do solicitante é inválido";

            AssertExtensions.ThrowsWithMessage(() => new Autor(Identificador, nomeInvalido), mensagemEsperada);
        }
    }
}
