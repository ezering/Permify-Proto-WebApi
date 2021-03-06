using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthChecks.MongoDb;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Permify_Proto_WebApi.interfaces;
using Permify_Proto_WebApi.Repositories;
using Permify_Proto_WebApi.settings;

namespace Permify.Proto.WebApi
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
            services.AddSingleton<IConfiguration>(Configuration);
            // BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String)); // every time there is a GUID it should be a string in the database
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String)); // every time there is a DateTime it should be a string in the database
            // var mongoDbSettings = Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();

            var mongoDBUsername = Configuration.GetValue<String>("MongoDBUsername");
            var mongoDBPassword = Configuration.GetValue<String>("MongoDBPassword");

            var mongoDbSettings = new MongoDbSettings(mongoDBUsername, mongoDBPassword);

            services.AddSingleton<IMongoClient>(
                serviceProvider =>
                {
                    return new MongoClient(mongoDbSettings.ConnectionString);
                }
            );
            services.AddSingleton<IProtoRepository, MongoDbProtoRepository>();
            services.AddControllers(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Permify.Proto.WebApi", Version = "v1" });
            });

            services.AddHealthChecks()
                .AddMongoDb(mongoDbSettings.ConnectionString, name: "MongoDb", timeout: TimeSpan.FromSeconds(5));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Permify.Proto.WebApi v1"));
            }

            if (env.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
