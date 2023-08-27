using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text;

namespace Diplom_Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration().WriteTo.Console()
                                                  .WriteTo.File(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "logs", "diagnostics.txt"),
                                                                rollingInterval: RollingInterval.Day,
                                                                fileSizeLimitBytes: 10 * 1024 * 1024,
                                                                retainedFileCountLimit: 2,
                                                                rollOnFileSizeLimit: true,
                                                                shared: true,
                                                                flushToDiskInterval: TimeSpan.FromSeconds(1))
                                                  .CreateLogger();

            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog();

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationClientsideAdapters();
            builder.Services.AddValidatorsFromAssemblyContaining<BillValidator>();

            builder.Services.AddCors();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "My API",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                  {
                    new OpenApiSecurityScheme
                    {
                      Reference = new OpenApiReference
                      {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                      }
                    },
                    new string[] { }
                  }
                });
            });
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var secret = builder.Configuration.GetValue<string>("Auth:Secret")!;
                var issuer = builder.Configuration.GetValue<string>("Auth:Issuer")!;
                var audience = builder.Configuration.GetValue<string>("Auth:Audience")!;
                var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = mySecurityKey
                };
            });
            builder.Services.AddAuthorization();
            builder.Services.AddValidatorsFromAssemblyContaining<BillValidator>();
            builder.Services.AddScoped<IBillService, BillService>();
            builder.Services.AddDbContext<DataContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.Use(async (context, next) =>
            {
                try
                {
                    Console.WriteLine($"{context.Request.Path}");
                    await next();
                    Console.WriteLine($"{context.Request.Path}");
                }
                catch (Exception ex)
                {
                    await Console.Out.WriteLineAsync(ex.Message);
                }
            });

            app.MapControllers();
            app.MapGet("/v2/CleareAll", (HttpContext reqestDelegate) =>
            {
                var name = reqestDelegate.GetRouteValue("CleareV2");
                var service = reqestDelegate.RequestServices.GetService<IBillService>();
                var clearBill = service?.Clear();
                return Results.Ok(clearBill);
            })
                .WithOpenApi();

            app.Run();
        }
    }
}