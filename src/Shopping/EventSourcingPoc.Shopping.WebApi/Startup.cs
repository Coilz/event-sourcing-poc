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
using EventSourcingPoc.Shopping.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using static EventSourcingPoc.Shopping.Application.Bootstrapper;

namespace EventSourcingPoc.Shopping.WebApi
{
    public class Startup
    {
        private readonly ILoggerFactory _loggerFactory;
        public Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSingleton(CreateApplication());
        }

        private PretendApplication CreateApplication()
        {
            var logger = _loggerFactory.CreateLogger("Shopping.WebApi");
            return Bootstrap(logger);
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
