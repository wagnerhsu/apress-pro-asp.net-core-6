// Copyright (c) xxx, 2022. All rights reserved.

using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebApp.Models;

namespace WebApp.Infrastructure;

public static class ServiceExtensions
{
    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "WebApp Swagger"
            });
            options.EnableAnnotations();
        });
    }

    public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(opts =>
        {
            opts.UseSqlServer(configuration[
                "ConnectionStrings:ProductConnection"]);
            opts.EnableSensitiveDataLogging(true);
        });
    }

    public static void ConfigureJsonOptions(this IServiceCollection services)
    {
        services.Configure<JsonOptions>(opts =>
        {
            opts.JsonSerializerOptions.DefaultIgnoreCondition
                = JsonIgnoreCondition.WhenWritingNull;
        });
    }

    public static void ConfigureNewtonsoftJsonOptions(this IServiceCollection services)
    {
        services.Configure<MvcNewtonsoftJsonOptions>(opts =>
        {
            opts.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
        });
    }
}
