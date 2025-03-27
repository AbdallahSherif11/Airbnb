
using Airbnb.Core.Entities.Identity;
using Airbnb.Core.Mapping.HouseMapping;
using Airbnb.Core.Repositories.Contract;
using Airbnb.Core.Repositories.Contract.UnitOfWorks.Contract;
using Airbnb.Core.Services.Contract.HouseServices.Contract;
using Airbnb.Repository.Data.Contexts;
using Airbnb.Repository.Repositories;
using Airbnb.Repository.Repositories.UnitOfWorks;
using Airbnb.Service.Services.HouseServices;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;



namespace Airbnb.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            // Add DbContext
            builder.Services.AddDbContext<AirbnbDbContext>(options =>
            options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // DIC
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(HouseProfile));
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                            .AddEntityFrameworkStores<AirbnbDbContext>()
                            .AddDefaultTokenProviders();
            //builder.Services.AddScoped<IHouseRepository, HouseRepository>();    

            builder.Services.AddScoped<IHouseService, HouseService>();
            builder.Services.AddScoped<IImageService, ImageService>();
            builder.Services.AddHttpContextAccessor(); // For accessing wwwroot


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(op => op.SwaggerEndpoint("/openapi/v1.json", "v1"));
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles(); // Serves files from wwwroot

            app.MapControllers();

            app.Run();
        }

    }
}
