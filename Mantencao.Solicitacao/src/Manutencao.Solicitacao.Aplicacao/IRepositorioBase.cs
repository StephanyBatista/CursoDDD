using System.Collections.Generic;

namespace Manutencao.Solicitacao.Aplicacao
{
    public interface IRepositorio<TEntidade>
    {
        List<TEntidade> Consultar();
        TEntidade ObterPorId(string id);
        void Adicionar(TEntidade entity);
    }
}
