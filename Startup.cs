using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Http;
using GraphQL.Server;
using GraphQL.Server.Transports.AspNetCore;
using GraphQL.Server.Transports.WebSockets;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using intevent_web.GraphQL;
using intevent_web.Models;
using intevent_web.Services;

namespace intevent_web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();

            services.AddSingleton<IPartyService, PartyService>();
            services.AddSingleton<SongGraphType>();
            services.AddSingleton<SongVoteGraphType>();
            services.AddSingleton<VoteTotalGraphType>();
            services.AddSingleton<SongVoteInputGraphType>();
            services.AddSingleton<PartyMutations>();
            services.AddSingleton<PartyQueries>();
            services.AddSingleton<PartySubscriptions>();
            services.AddSingleton<PartySchema>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            /*
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            */

            services.AddGraphQL(options =>
            {
                options.EnableMetrics = true;
                options.ExposeExceptions = true;
            })
            .AddWebSockets();
            //https://github.com/graphql-dotnet/server/issues/162
            //.AddDataLoader();
            
            //.AddUserContextBuilder(httpContext => new GraphQLUserContext { User = httpContext.User });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
/*
            app.Use(async (context, next) => 
            { 
                await next(); 
                string path = context.Request.Path.Value;

                if (!path.StartsWith("/api") && !path.StartsWith("/graphql") && !Path.HasExtension(path)) 
                { 
                    context.Request.Path = "/"; 
                    await next(); 
                } 
            });
*/
            // app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseWebSockets();
            app.UseGraphQLWebSockets<PartySchema>("/graphql");
            app.UseGraphQL<PartySchema>("/graphql");
            // app.UseCookiePolicy();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions
            {
                Path = "/ui/playground"
            });
            app.UseMvc();
        }
    }
}
