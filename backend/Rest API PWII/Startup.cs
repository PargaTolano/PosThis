using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Rest_API_PWII.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace Rest_API_PWII
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title= "PosThis", Version = "v1" });
                });

            string connectionString = Configuration.GetConnectionString("SqlConnection");
            string keyStr = Configuration.GetValue<string>("SecretKey");
            byte[] key = Encoding.ASCII.GetBytes(keyStr);

            services.AddDbContext<PosThisDbContext>( options =>
                options.UseSqlServer(connectionString)
            );

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<PosThisDbContext>();

            services.AddAuthentication( x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer( x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder
                                    .WithOrigins("*")
                                    .AllowAnyMethod()
                                    .AllowAnyHeader());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI( c => c.SwaggerEndpoint("Swagger/v1/swagger.json","PosThis v1") );
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine( env.ContentRootPath, "static") ),
                RequestPath = "/static"
            });

            app.UseRouting();

            app.UseCors("AllowOrigin");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints( endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
