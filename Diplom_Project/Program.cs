namespace Diplom_Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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
                var clearBill = service.Clear();
                return Results.Ok(clearBill);
            })
                .WithOpenApi();

            app.Run();
        }
    }
}