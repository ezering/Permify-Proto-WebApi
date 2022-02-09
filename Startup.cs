using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            // BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String)); // every time there is a GUID it should be a string in the database
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String)); // every time there is a DateTime it should be a string in the database

            services.AddSingleton<IMongoClient>(
                serviceProvider =>
                {
                    var settings = Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
                    Console.WriteLine(settings.ConnectionString);
                    return new MongoClient(settings.ConnectionString);
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Permify.Proto.WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
