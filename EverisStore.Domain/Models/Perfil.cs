using EverisStore.Domain.DomainObjects;
using System.Collections.Generic;

namespace EverisStore.Domain.Models
{
    public class Perfil : Entity, IAggregateRoot
    {
        public string Descricao { get; set; }
        public string Regra { get; set; }
        public ICollection<Usuario> Usuarios { get; set; }
    }
}
