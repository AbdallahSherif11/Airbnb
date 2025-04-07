
using Airbnb.Core.Entities.Identity;
using Airbnb.Core.Mapping.AccountMapping;
using Airbnb.Core.Mapping.HouseMapping;
using Airbnb.Core.Repositories.Contract;
using Airbnb.Core.Repositories.Contract.UnitOfWorks.Contract;
using Airbnb.Core.Services.Contract.BookingServices.Contract;
using Airbnb.Core.Services.Contract.HouseServices.Contract;
using Airbnb.Core.Services.Contract.IdentityServices.Contract;
using Airbnb.Core.Services.Contract.PaymentServices.Contract;
using Airbnb.Core.Services.Contract.Review.Contract;
using Airbnb.Repository.Data.Contexts;
using Airbnb.Repository.Repositories;
using Airbnb.Repository.Repositories.UnitOfWorks;
using Airbnb.Service.Services.AccountServices;
using Airbnb.Service.Services.BookingServices;
using Airbnb.Service.Services.HouseServices;
using Airbnb.Service.Services.PaymentServices;
using Airbnb.Service.Services.ReviewServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Stripe;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Text;



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



            // to access appsetting.json
            var configuration = builder.Configuration;

            builder.Services.AddSingleton<IConfiguration>(configuration);

            // DIC
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(HouseProfile), typeof(AccountProfile));
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                            .AddEntityFrameworkStores<AirbnbDbContext>()
                            .AddDefaultTokenProviders();

            builder.Services.AddScoped<IHouseService, HouseService>();
            builder.Services.AddScoped<IImageService, ImageService>();
            builder.Services.AddScoped<IAccountService, Airbnb.Service.Services.AccountServices.AccountService>();
            builder.Services.AddScoped<IReviewService, Airbnb.Service.Services.ReviewServices.ReviewService>();
            builder.Services.AddScoped<IBookingService, BookingService>();


            // ...
            builder.Services.AddScoped<IStripeService, StripeService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();

            builder.Services.AddHttpContextAccessor(); // For accessing wwwroot

            //JWT Validation

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

            // Add Stripe Service
            StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

            builder.Services.AddAuthorization();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(op => op.SwaggerEndpoint("/openapi/v1.json", "v1"));
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles(); // Serves files from wwwroot

            app.MapControllers();

            app.Run();
        }

    }
}
