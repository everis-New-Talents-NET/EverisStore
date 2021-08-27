using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace EverisStore.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();

            /* //Multiple Autenthication
            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = "cookie1";
                })
                .AddCookie("cookie1", "cookie1", options =>
                {
                    options.Cookie.Name = "cookie1";
                    options.LoginPath = "/loginc1";
                })
                .AddCookie("cookie2", "cookie2", options =>
                {
                    options.Cookie.Name = "cookie2";
                    options.LoginPath = "/loginc2";
                });
                */
            //             default:


            /*// Code omitted for brevity

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Audience = "https://localhost:5000/";
                    options.Authority = "https://localhost:5000/identity/";
                })
                .AddJwtBearer("AzureAD", options =>
                {
                    options.Audience = "https://localhost:5000/";
                    options.Authority = "https://login.microsoftonline.com/eb971100-6f99-4bdc-8611-1bc8edd7f436/";
                });*/


            //Especifica o esquema usadao para autenticação do tipo Bearer
            //https://developer.okta.com/blog/2018/03/23/token-authentication-aspnetcore-complete-guide
            //https://github.com/dotnet/AspNetCore.Docs/blob/main/aspnetcore/security/authorization/limitingidentitybyscheme.md
            //https://docs.microsoft.com/pt-br/aspnet/core/security/authorization/limitingidentitybyscheme?view=aspnetcore-5.0
            //https://docs.microsoft.com/pt-br/aspnet/core/security/authentication/?view=aspnetcore-5.0
            //https://sandrino.dev/blog/aspnet-core-5-jwt-authorization

            var key = Encoding.UTF8.GetBytes(Configuration["SecurityKey"]);
            
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        // ValidateLifetime = false,
                        ValidateIssuerSigningKey = true,
                        // ValidIssuer = "everisstore.com.br",
                        // ValidAudience = "everisstore",
                        IssuerSigningKey =
                            new SymmetricSecurityKey(key)
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine($"Token inválido...:{context.Exception.Message}");
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            Console.WriteLine($"Token válido...:{context.SecurityToken}");
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddSwaggerGen(c =>
            {
                // add JWT Authentication
                /*var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter JWT Bearer token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer", // must be lower case
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };*/
                /*c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, new string[] { }}
                });*/
                // c.SwaggerDoc("v1", new OpenApiInfo {Title = "EverisStore.API", Version = "v1"});
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "EverisStore.API",
                    Version = "v1",
                    Description = "An API",
                    Contact = new OpenApiContact
                    {
                        Name = "EverisStore",
                        Email = string.Empty,
                        Url = new Uri("https://EverisStore.com/"),
                    },
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EverisStore.API v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
            );
            app.UseAuthentication();
            app.UseAuthorization();

//registrar o modelo de autenticação no middleware
            // app.Use(next =>
            // {
            //     return async ctx =>
            //     {
            //         switch(ctx.Request.Path)
            //         {
            //             case "/loginc1":
            //                 var identity1 = new ClaimsIdentity("cookie1");
            //                 identity1.AddClaim(new Claim("name", "Alice-c1"));
            //                 await ctx.SignInAsync("cookie1", new ClaimsPrincipal(identity1));
            //                 break;
            //             case "/loginc2":
            //                 var identity2 = new ClaimsIdentity("cookie2");
            //                 identity2.AddClaim(new Claim("name", "Alice-c2"));
            //                 await ctx.SignInAsync("cookie2", new ClaimsPrincipal(identity2));
            //                 break;
            //             case "/logoutc1":
            //                 await ctx.SignOutAsync("cookie1");
            //                 break;
            //             case "/logoutc2":
            //                 await ctx.SignOutAsync("cookie2");
            //                 break;
            //             default:
            //                 await next(ctx);
            //                 break;
            //         }
            //     };
            // });

            
           

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}