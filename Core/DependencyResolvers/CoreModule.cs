using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.CrossCuttingConcerns.Caching.Redis;
using Core.Utilities.IoC;
using Core.Utilities.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddMemoryCache();
            serviceDescriptors.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceDescriptors.AddSingleton<ICacheManager, MemoryCacheManager>();
            //serviceDescriptors.AddSingleton<ICacheManager, RedisCacheManager>();
            serviceDescriptors.AddSingleton<Stopwatch>();

            serviceDescriptors.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(SwaggerMessages.Version, new OpenApiInfo
                {
                    Version = SwaggerMessages.Version,
                    Title = SwaggerMessages.Title,
                    Description = SwaggerMessages.Description,
                    //TermsOfService = new Uri(SwaggerMessages.TermsOfService),
                    //Contact = new OpenApiContact
                    //{
                    //    Name = SwaggerMessages.ContactName,
                    //},
                    //License = new OpenApiLicense
                    //{
                    //    Name = SwaggerMessages.LicenceName,
                    //},
                });

                //c.OperationFilter<AddAuthHeaderOperationFilter>();
                c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Description = "`Token only!!!` - without `Bearer_` prefix",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer"
                });
            });
        }
    }
}
