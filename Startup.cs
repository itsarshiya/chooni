using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Threading.Tasks;

public class ApiKeyRequirement : IAuthorizationRequirement { }

public class ApiKeyAuthorizationHandler : AuthorizationHandler<ApiKeyRequirement>
{
    private readonly IConfiguration _configuration;

    public ApiKeyAuthorizationHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ApiKeyRequirement requirement)
    {
        if (!context.User.HasClaim(c => c.Type == ClaimTypes.Authentication))
        {
            context.Fail();
            return Task.CompletedTask;
        }

        string validApiKey = _configuration.GetSection("AppSettings:ApiKey").Value;
        var apiKey = context.User.FindFirstValue(ClaimTypes.Authentication);

        if (apiKey == validApiKey)
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }

        return Task.CompletedTask;
    }
}

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ChooniContext>();


        services.AddControllers();

        services.AddAuthentication("ApiKey")
                .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>("ApiKey", options => { });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("ApiKeyPolicy", policy =>
            {
                policy.AuthenticationSchemes.Add("ApiKey");
                policy.Requirements.Add(new ApiKeyRequirement());
            });
        });

        services.AddSingleton<IAuthorizationHandler, ApiKeyAuthorizationHandler>();

        services.AddSwaggerGen(c =>
            {
                // Swagger configuration...

                c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "ApiKey",
                    Type = SecuritySchemeType.ApiKey,
                    Description = "API Key authentication using the 'Authorization' header",
                    Scheme = "ApiKey"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "ApiKey"
                    }
                },
                new List<string>()
            }
                });
            });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            app.UseExceptionHandler("/error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
