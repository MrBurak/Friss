using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BusinessLayer.Interfaces;
using BusinessLayer;
using DataLayer;
using DataLayer.Abstract;
using StorageLayer.Interfaces;
using StorageLayer;
using CoreApi.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;

namespace CoreApi
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
            });
            services.AddTransient<FileLogger>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton(typeof(ISqlRepository<>), typeof(SqlRepository<>));
            services.AddSingleton<IDocumentTypeBusiness, DocumentTypeBusiness>();
            services.AddSingleton<IValidation, Validation>();
            services.AddSingleton<IHelper, Helper>();
            services.AddSingleton<ISaver, LocalSaver>();
            services.AddSingleton<IUserBusiness, UserBusiness>();
            services.AddSingleton<IUserRoleBusiness, UserRoleBusiness>();
            services.AddSingleton<IDocumentBusiness, DocumentBusiness>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration.GetSection("TokenSettings:Audience").Value,
                    ValidIssuer = Configuration.GetSection("TokenSettings:Issuer").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("TokenSettings:SecretKey").Value))
                };
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseCors("AllowAll");
            app.UseMiddleware<ApiLoggingMiddleware>();
            app.UseMvc();
            app.UseStaticFiles();
        }
    }
}
