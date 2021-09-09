using EverisStore.Domain.DomainObjects;
using System.Collections.Generic;

namespace EverisStore.Domain.Models
{
    public class Perfil : Entity
    {
        public string Descricao { get; set; }
        public string Regra { get; set; }
    }
}
