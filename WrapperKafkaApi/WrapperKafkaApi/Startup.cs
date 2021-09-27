using Confluent.Kafka;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WrapperKafkaApi
{
    public class TopicConfig
    {
        public string Names { set; get; }
    }
    public static class WrapperProducerExtensions
    {
        public static void AddWrapperProducer(this IServiceCollection services, Action<ProducerConfig> producerOptions,
            Action<TopicConfig> topicConfig = null)
        {
            services.AddScoped<IWrapperProducer, WrapperProducer>();
            services.Configure(producerOptions);

            
        }

        public static void AddWrapperProducer(this IServiceCollection services)
        {
            services.AddScoped<IWrapperProducer, WrapperProducer>();


        }
    }
    public class Startup
    {
        public IConfiguration AppConfiguration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddWrapperProducer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
