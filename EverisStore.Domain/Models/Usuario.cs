using EverisStore.Domain.DomainObjects;
using System;

namespace EverisStore.Domain.Models
{
    public class Usuario : Entity
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public Guid PerfilId { get; private set; }
        public Perfil Perfil { get; private set; }

        protected Usuario()
        {
        }

        public Usuario(string nome, string email, string senha, Perfil perfil)
        {
            Nome = nome;
            Senha = senha;
            Perfil = perfil;
        }
    }
}
