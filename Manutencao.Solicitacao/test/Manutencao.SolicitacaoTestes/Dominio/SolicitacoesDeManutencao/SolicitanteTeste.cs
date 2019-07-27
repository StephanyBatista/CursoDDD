using ExpectedObjects;
using Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao;
using Manutencao.SolicitacaoTestes._Util;
using Xunit;

namespace Manutencao.SolicitacaoTestes.Dominio.SolicitacoesDeManutencao
{
    public class SolicitanteTeste
    {
        private const int Identificador = 55;
        private const string Nome = "Henrique Almeida";

        [Fact]
        public void Deve_criar_solicitante()
        {
            var solicitanteEsperado = new
            {
                Identificador,
                Nome
            };

            var solicitante = new Solicitante(solicitanteEsperado.Identificador, solicitanteEsperado.Nome);

            solicitanteEsperado.ToExpectedObject().ShouldMatch(solicitante);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Deve_validar_nome_do_solicitante(string nomeInvalido)
        {
            const string mensagemEsperada = "Nome do solicitante é inválido";

            AssertExtensions.ThrowsWithMessage(() => new Solicitante(Identificador, nomeInvalido), mensagemEsperada);
        }
    }
}
