using Clean.Architecture.Core.Usecase;
using Clean.Architecture.Core.Usecase.Interface;
using Clean.Architecture.Core.Usecase.Interface.External;
using Clean.Architecture.Infrastructure.Database.InMemory;
using Clean.Architecture.Infrastructure.Database.InMemory.Context;
using System.Text.Json.Serialization;

namespace Clean.Architecture.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));//For swagger to display the enum values as string.
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton(typeof(ParishDBContext));
            builder.Services.AddSingleton<ParishPersistence>();
            builder.Services.AddScoped<IParishPersistence>(x => x.GetRequiredService<ParishPersistence>());
            builder.Services.AddScoped<IParishnerPersistence>(x => x.GetRequiredService<ParishPersistence>());
            builder.Services.AddScoped<ICreateParishUsecase, CreateParishUsecase>();
            builder.Services.AddScoped<ICreateParishnerUsecases, CreateParishnerUsecases>();
            builder.Services.AddScoped<IGetParishnerUsecases, GetParishnerUsecases>();
            builder.Services.AddScoped<IGetParishUsecases, GetParishUsecases>();
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
