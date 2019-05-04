using System;

namespace Manutencao.Solicitacao.Dominio
{
    public class Entidade
    {
        public string Id { get; set; }

        public Entidade()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}