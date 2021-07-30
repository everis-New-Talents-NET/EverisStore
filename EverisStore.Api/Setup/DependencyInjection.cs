using EverisStore.Application.Automapper;
using EverisStore.Application.Services;
using EverisStore.Data;
using EverisStore.Data.Repositories;
using EverisStore.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EverisStore.Api.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DomainToViewModelMappingProfile), typeof(ViewModelToDomainMappingProfile));

            services.AddScoped<EverisStoreContext>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IAutenticacaoService, AutenticacaoService>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<IEstoqueService, EstoqueService>();
        }
    }
}
