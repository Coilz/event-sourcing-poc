using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventSourcingPoc.Data;
using EventSourcingPoc.EventProcessing;
using EventSourcingPoc.EventSourcing;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Readmodels;
using EventSourcingPoc.Readmodels.Orders;
using EventSourcingPoc.Readmodels.Shop;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EventSourcingPoc.Shopping.WebApi
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
            services.AddMvc();

            services.AddSingleton<IEventStore, InMemoryEventStore>();
            services.AddSingleton<IReadModelStore<ShoppingCartReadModel>, InMemoryReadModelStore<ShoppingCartReadModel>>();
            services.AddSingleton<IReadModelStore<OrderReadModel>, InMemoryReadModelStore<OrderReadModel>>();

            services.AddScoped<IShoppingCartReadModelRepository, ShoppingCartReadModelRepository>();
            services.AddScoped<IOrderReadModelRepository, OrderReadModelRepository>();

            services.AddSingleton<IEventBus, EventBus>();
            services.AddScoped<IRepository, AggregateRepository>();
            services.RegisterCommandHandlers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
