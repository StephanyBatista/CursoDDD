using Manutencao.Solicitacao.Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Manutencao.Solicitacao.Api.Filter
{
    public class TratamentoDeExcecaoFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var status = context.Exception is ExcecaoDeDominio ? 400 : 500;
            var excecaoInternaDoServidor = context.Exception.InnerException != null ? context.Exception.InnerException.Message : "";
            var execaoDoServidor = $"{context.Exception.Message} {excecaoInternaDoServidor}";
            var mensagem = status == 400 ? context.Exception.Message : execaoDoServidor;
            var response = context.HttpContext.Response;

            response.StatusCode = (int)status;
            response.ContentType = "application/json";
            context.Result = new JsonResult(mensagem);
        }
    }
}
