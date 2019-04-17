using System;

namespace Manutencao.Solicitacao.Dominio
{
    public class Entidade<TEntidade>
    {
        public string Id { get; set; }

        public Entidade()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}