// <copyright file="Startup.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Sample
{
    using System;
    using Filters;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Models;

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            this.Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>();
            services.AddSingleton(ConfigureInMemoryDatabase);

            services.AddMemoryCache();

            // Add framework services.
            services.AddMvc(options =>
            {
                options.Filters.Add(new HttpContextProviderAttribute());
            });

            services.AddFluentRestBuilder()
                .AddEntityFrameworkCoreIntegration<ApplicationDbContext>()
                .ConfigureFiltersAndOrderByExpressionsForDbContextEntities<ApplicationDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(this.Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.MockUser();
            app.UseMvc();
        }

        private static DbContextOptions<ApplicationDbContext> ConfigureInMemoryDatabase(IServiceProvider provider)
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

            // Create a fresh service provider, and therefore a fresh
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var options = builder.UseInMemoryDatabase("sample")
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            MockData.CreateMockUser(options);
            return options;
        }
    }
}
