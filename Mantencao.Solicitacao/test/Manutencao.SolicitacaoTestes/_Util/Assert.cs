using System;
using Manutencao.Solicitacao.Dominio;
using Xunit;

namespace Manutencao.SolicitacaoTestes._Util
{
    public static class AssertExtensions
    {
        public static void ThrowsWithMessage(Func<Object> testCode, string messageExpected)
        {
            var mensagem = Assert.Throws<ExcecaoDeDominio>(testCode).Message;
            Assert.Equal(messageExpected, mensagem);
        }
    }
}
