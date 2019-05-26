using System;

namespace Manutencao.Solicitacao.Dominio
{
    public abstract class Entidade
    {
        public string Id { get; set; }

        protected Entidade()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}