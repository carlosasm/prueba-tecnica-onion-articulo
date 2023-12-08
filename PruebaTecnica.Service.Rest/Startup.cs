using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using PruebaTecnica.Model.Configuration;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication;

namespace PruebaTecnica.Service.Rest
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Model.Configuration.SwaggerConfiguration swaggerConfiguration = new Model.Configuration.SwaggerConfiguration();
            Configuration.Bind("SwaggerConfiguration", swaggerConfiguration);
            services.AddSingleton(swaggerConfiguration);

            AzureADOptions azureAdOptions = new AzureADOptions();
            Configuration.Bind("AzureAd", azureAdOptions);
            services.AddSingleton(azureAdOptions);

            services.AddAuthentication(AzureADDefaults.BearerAuthenticationScheme)
                .AddAzureADBearer(options => Configuration.Bind("AzureAd", options));

            services.AddScoped<DataAccess.Interface.IConnectionManager, DataAccess.ConnectionManager>();
            services.AddScoped<DataAccess.Interface.IArticulo, DataAccess.Articulo>();
            services.AddScoped<BusinessLogic.Interface.IArticulo, BusinessLogic.Articulo>();

            services.AddControllers();
            services.AddSwaggerGen(options =>
            {
                options.IgnoreObsoleteActions();
                options.IgnoreObsoleteProperties();
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = $"PruebaTI.Service - Environment: {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}",
                    Description = "Componentes Prueba",
                    TermsOfService = new Uri("http://www.grupomonge.com"),
                    Contact = new OpenApiContact()
                    {
                        Name = "Tecnologías de la información",
                        Email = "arquitecturati@grupomonge.com",
                        Url = new Uri("http://www.grupomonge.com")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "GrupoMonge©",
                        Url = new Uri("http://www.grupomonge.com")
                    },
                });
                options.AddSecurityDefinition("Gmg Azure Active Directory", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"{this.Configuration["AzureAd:Instance"]}{this.Configuration["AzureAd:TenantId"]}/oauth2/authorize", UriKind.Absolute),
                            Scopes = new Dictionary<string, string> { { "user_impersonation", "Access API" } }
                        }
                    }
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Gmg Azure Active Directory"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                         new List<string>()
                    }
                });
                options.IncludeXmlComments(System.IO.Path.Combine(AppContext.BaseDirectory, $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Model.Configuration.SwaggerConfiguration swaggerConfiguration, AzureADOptions azureAdOptions)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints => endpoints.MapControllers());

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.OAuthClientId(swaggerConfiguration.ClientId);
                options.OAuthClientSecret(swaggerConfiguration.ClientSecret);
                options.OAuthRealm(azureAdOptions.ClientId);
                options.OAuthAppName("Gmg.Monedero.Catalogo.Service");
                options.OAuthScopeSeparator(" ");
                options.OAuthAdditionalQueryStringParams(new Dictionary<string, string> { { "resource", azureAdOptions.ClientId } });
                foreach (var endpoint in swaggerConfiguration.Endpoints)
                {
                    options.SwaggerEndpoint(endpoint.Url, endpoint.Name);
                }
            });

            //app.MapApiKey();
        }
    }
}
