using Application.Services.Implementations;
using Application.Services.Interfaces;
using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Application.Services.UserServices;
using System.Text;

namespace Infrastructure.Configurations
{
    public static class AppConfiguration
    {
        public static void AddDependenceInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            // ...
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            AddSwagger(services);
            ConfigureAuthentication(services, configuration);
            
        }
        public static void AddSwagger(this IServiceCollection services)
        {
                services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
/*                c.DescribeAllParametersInCamelCase();
*/                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ASP.Net 8.0 - SuaMe88", Description = "APIs Service", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                        },
                        new string[] {}
                      }
                 });
            });
        }
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["JWT_Configuration:Issuer"],
                        ValidAudience = configuration["JWT_Configuration:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT_Configuration:SecretKey"]!))
                    };
                });
        }




        //public static void UseJwt(this IApplicationBuilder app)
        //{
        //    app.UseMiddleware<JwtMiddleware>();
        //}
    }
}
