using Serilog;

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
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IBillService, BillService>();
            builder.Services.AddDbContext<DataContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

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