using System;
using System.Threading.Tasks;
using Manutencao.Solicitacao.Dominio;
using Xunit;

namespace Manutencao.SolicitacaoTestes._Util
{
    public static class AssertExtensions
    {
        public static void ThrowsWithMessage(Action testCode, string messageExpected)
        {
            var message = Assert.Throws<ExcecaoDeDominio>(testCode).Message;
            Assert.Equal(messageExpected, message);
        }

        public static void ThrowsWithMessageAsync(Func<Task> testCode, string messageExpected)
        {
            var result = Assert.ThrowsAsync<ExcecaoDeDominio>(testCode).Result;
            Assert.Equal(messageExpected, result.Message);
        }
    }
}
