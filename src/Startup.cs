using System.Text;
using API.Database;
using API.Database.Entities;
using API.Security;
using API.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using AuthenticationService = API.Services.AuthenticationService;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration) { Configuration = configuration; }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("DotsAndBoxes"),
                                              ServiceLifetime.Singleton);

            #region Authentication

            services.AddAuthentication("BasicAuthentication")
                    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
            var jwtSettings = Configuration.GetSection("JwtSettings");
            services.AddAuthentication(opt =>
                                       {
                                           opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                                           opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                                       })
                    .AddJwtBearer(opt =>
                                  {
                                      opt.TokenValidationParameters =
                                          new TokenValidationParameters
                                          {
                                              ValidateIssuer = true,
                                              ValidateAudience = true,
                                              ValidateLifetime = true,
                                              ValidateIssuerSigningKey = true,

                                              ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
                                              ValidAudience = jwtSettings.GetSection("validAudience").Value,
                                              IssuerSigningKey = new SymmetricSecurityKey(
                                                  Encoding.UTF8.GetBytes(jwtSettings.GetSection("securityKey").Value))
                                          };
                                  });

            #endregion

            services.AddScoped<UserService>();
            services.AddScoped<AuthenticationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseCors(
                builder =>
                {
                    builder.WithOrigins("https://localhost:5001")
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                }
            );

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });


            var context = app.ApplicationServices.GetService<ApiContext>();
            #if DEBUG
            AddTestData(context);
            #endif
        }

        private static void AddTestData(ApiContext context)
        {
            var testUser1 = new User("Erik", "i551CkevribikEUMBvlqgStoUnZsaIt6cevVixESsSM49sL53nOndg==");
            context.Users.Add(testUser1);

            var testUser2 = new User("Dennis", "2");
            context.Users.Add(testUser2);

            context.SaveChanges();
        }
    }
}