using System;

namespace Manutencao.Solicitacao.Dominio
{
    public class ExcecaoDeDominio : Exception
    {
        public ExcecaoDeDominio(string mensagem) : base(mensagem) { }

        public static void LancarQuando(bool regraInvalida, string mensagem)
        {
            if(regraInvalida)
                throw new ExcecaoDeDominio(mensagem);
        }
    }
}
