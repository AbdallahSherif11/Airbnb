
using Airbnb.Core.Entities.Identity;
using Airbnb.Core.Mapping.AccountMapping;
using Airbnb.Core.Mapping.HouseMapping;
using Airbnb.Core.Repositories.Contract;
using Airbnb.Core.Repositories.Contract.UnitOfWorks.Contract;
using Airbnb.Core.Services.Contract.BookingServices.Contract;
using Airbnb.Core.Services.Contract.HouseServices.Contract;
using Airbnb.Core.Services.Contract.IdentityServices.Contract;
using Airbnb.Core.Services.Contract.PaymentServices.Contract;
using Airbnb.Core.Services.Contract.MessageService.Contract;
using Airbnb.Core.Services.Contract.Review.Contract;
using Airbnb.Repository.Data.Contexts;
using Airbnb.Repository.Repositories;
using Airbnb.Repository.Repositories.UnitOfWorks;
using Airbnb.Service.Services.AccountServices;
using Airbnb.Service.Services.BookingServices;
using Airbnb.Service.Services.HouseServices;
using Airbnb.Service.Services.PaymentServices;
using Airbnb.Service.Services.MessageService;
using Airbnb.Service.Services.ReviewServices;
using Airbnb.Service.Services.SignalRServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Stripe;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using Airbnb.API.Errors;
using Airbnb.API.Middleware;
using Airbnb.Core.Services.Contract.WishListService.Contract;
using Airbnb.Service.Services.WishListService;
using Airbnb.Core.Services.Contract.AccountServices.Contract;



namespace Airbnb.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200") 
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });

            //for signalR
            builder.Services.AddSignalR();
            builder.Services.AddSingleton<IUserIdProvider, NameUserIdProvider>();

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            //builder.Services.AddOpenApi();

            // Replace builder.Services.AddOpenApi() with:
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Airbnb API", Version = "v1" });

                // Add JWT Bearer authentication to Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
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
            Array.Empty<string>()
        }
    });

                // Handle circular references
                c.UseAllOfToExtendReferenceSchemas();
                c.UseAllOfForInheritance();
                c.UseOneOfForPolymorphism();

                // Custom schema ID to avoid conflicts
                c.CustomSchemaIds(type => type.FullName);
            });


            // Add DbContext
            builder.Services.AddDbContext<AirbnbDbContext>(options =>
                        options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



            // to access appsetting.json
            var configuration = builder.Configuration;

            builder.Services.AddSingleton<IConfiguration>(configuration);

            // DIC
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(HouseProfile), typeof(AccountProfile), typeof(UserMappingProfile));
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                            .AddEntityFrameworkStores<AirbnbDbContext>()
                            .AddDefaultTokenProviders();

            builder.Services.AddScoped<IHouseService, HouseService>();
            builder.Services.AddScoped<IImageService, ImageService>();
            builder.Services.AddScoped<IAccountService, Airbnb.Service.Services.AccountServices.AccountService>();
            builder.Services.AddScoped<IReviewService, Airbnb.Service.Services.ReviewServices.ReviewService>();
            builder.Services.AddScoped<IBookingService, BookingService>();
            builder.Services.AddScoped<IWishListService, WishListService>();
            builder.Services.AddScoped<IImageUserService, UserImageService>();

            // ...
            builder.Services.AddScoped<IStripeService, StripeService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IMessageService, MessageService>();
            builder.Services.AddHttpContextAccessor(); // For accessing wwwroot

            // error validation handling
            builder.Services.Configure<ApiBehaviorOptions>(option =>
            {
                option.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(e => e.Value.Errors.Count > 0)
                                .SelectMany(x => x.Value.Errors)
                                .Select(x => x.ErrorMessage)
                                .ToArray();
                    var response = new ApiValidationErrorResponse()
                    {
                        Errors = errors,
                    };
                    return new BadRequestObjectResult(response);
                };
            });

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

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/chathub"))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            // Add Stripe Service
            StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

            builder.Services.AddAuthorization();

            var app = builder.Build();

            // Configure user-defined middleware
            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //app.MapOpenApi();
                //app.UseSwaggerUI(op => op.SwaggerEndpoint("/openapi/v1.json", "v1"));

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Airbnb API V1");
                });
            }

            //handel not found
            app.UseStatusCodePagesWithReExecute("/error/{0}");
            app.UseHttpsRedirection();

            app.UseCors(MyAllowSpecificOrigins); 

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles(); 

            app.MapControllers();

            app.MapHub<ChatHub>("/chathub");

            app.Run();
        }

    }
}
