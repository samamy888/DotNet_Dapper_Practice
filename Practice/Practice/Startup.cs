using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Practice.DB;
using Practice.Helper;
using Practice.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
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
            services.AddSingleton<Repository>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Practice", Version = "v1" });
            });
            services.AddScoped<EFService>();
            services.AddSingleton<JwtHelper>();
            services.AddCors(options =>
            {

                options.AddPolicy("any", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    // �����ҥ��ѮɡA�^�����Y�|�]�t WWW-Authenticate ���Y�A�o�̷|��ܥ��Ѫ��Բӿ��~��]
                    options.IncludeErrorDetails = true; // �w�]�Ȭ� true�A���ɷ|�S�O����

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // �z�L�o���ŧi�A�N�i�H�q "sub" ���Ȩó]�w�� User.Identity.Name
                        NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
                        // �z�L�o���ŧi�A�N�i�H�q "roles" ���ȡA�åi�� [Authorize] �P�_����
                        RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",

                        // �@��ڭ̳��|���� Issuer
                        ValidateIssuer = true,
                        ValidIssuer = Configuration.GetValue<string>("JwtSettings:Issuer"),

                        // �q�`���ӻݭn���� Audience
                        ValidateAudience = false,
                        //ValidAudience = "JwtAuth", // �����ҴN���ݭn��g

                        // �@��ڭ̳��|���� Token �����Ĵ���
                        ValidateLifetime = true,

                        // �p�G Token ���]�t key �~�ݭn���ҡA�@�볣�u��ñ���Ӥw
                        ValidateIssuerSigningKey = false,

                        // "1234567890123456" ���ӱq IConfiguration ���o
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("JwtSettings:SignKey")))
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Practice v1"));

            app.UseHttpsRedirection();
            app.UseCors("any");
            app.UseRouting();

            //JWT�n�Ϊ��A�n�g�bUseAuthorization���e�A�����ҦA���v
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
