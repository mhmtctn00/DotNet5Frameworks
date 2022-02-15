using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Authorization;
using Core.Utilities.Security.Authorization.JWT;
using Core.Utilities.Security.Encyption;
using DotNet5Framework.DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Threading.Tasks;

namespace DotNet5Framework.API
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
            services.AddDbContext<DotNet5FrameworkContext>();
            services.AddControllers().AddNewtonsoftJson(options => // PM> Install-Package Microsoft.AspNetCore.Mvc.NewtonsoftJson
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DotNet5Framework.API", Version = "v1" });
            });

            #region To Send a File
            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });
            #endregion

            #region Authentication Options
            // var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

            // services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options =>
            //    {
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuer = true,
            //            ValidateAudience = true,
            //            ValidateLifetime = true,
            //            ValidIssuer = tokenOptions.Issuer,
            //            ValidAudience = tokenOptions.Audience,
            //            ValidateIssuerSigningKey = true,
            //            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
            //        };
            //        options.Events = new JwtBearerEvents
            //        {
            //            OnMessageReceived = context =>
            //            {
            //                context.Request.Headers.TryGetValue("Authorization", out StringValues token);
            //                if (string.IsNullOrEmpty(token))
            //                {
            //                    context.Request.Query.TryGetValue("access_token", out token);
            //                    if (string.IsNullOrEmpty(token))
            //                    {
            //                        context.Request.Query.TryGetValue("token", out token);
            //                    }
            //                }
            //                string tokenStr = token.ToString().Replace("Bearer", "").Trim();
            //                if (!string.IsNullOrEmpty(tokenStr) && !context.Request.Path.Value.Contains("refreshtoken", System.StringComparison.CurrentCultureIgnoreCase) && !JwtHelper.VerifyJWT(tokenStr))
            //                {
            //                    return Task.FromException(new System.Exception("logout"));
            //                }
            //                if (!string.IsNullOrEmpty(tokenStr) && JwtHelper.IsClosed(tokenStr))
            //                {
            //                    return Task.FromException(new System.Exception("logout"));
            //                }
            //                context.Token = tokenStr;
            //                return Task.CompletedTask;
            //            },
            //            OnAuthenticationFailed = context =>
            //            {
            //                var te = context.Exception;
            //                return Task.CompletedTask;
            //            }
            //        };
            //    });
            #endregion

            services.AddDependencyResolvers(new ICoreModule[]
            {
                new CoreModule(),
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DotNet5Framework.API v1"));
            }

            app.ConfigureCustomMiddlewares();

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseHttpsRedirection();

            DefaultFilesOptions options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("index.html");
            app.UseDefaultFiles(options);

            // app.UseStaticFiles();

            // app.UseStaticFiles(new StaticFileOptions
            // {
            //     FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Images")),
            //     RequestPath = new Microsoft.AspNetCore.Http.PathString("/Images")
            // });

            app.UseRouting();

            //app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
