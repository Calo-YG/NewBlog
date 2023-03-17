﻿
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;
using System.IO;
using Y.Module.Modules;
using Y.Module;
using Y.Module.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.SwaggerUI;
using Calo.Blog.EntityCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using Calo.Blog.Common;
using Calo.Blog.Common.Authorization;
using Swashbuckle.AspNetCore.Filters;

namespace Calo.Blog.Host
{
    [DependOn(typeof(SqlSugarEnityCoreModule), typeof(CommonModule))]
    public class CaloBlogHostModule : YModule
    {
        public override void ConfigerService(ConfigerServiceContext context)
        {
            var configuration = context.GetConfiguartion();
            context.Services.AddMvc()
                           .AddRazorPagesOptions(options =>
                           {

                           })
                           .AddRazorRuntimeCompilation();

            context.Services.AddHttpContextAccessor();

            //添加cokkie认证和cokkie
            context.Services.AddAuthentication(context =>
            {
                //需要登录进行鉴权认证
                context.RequireAuthenticatedSignIn = true;
                context.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                context.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddCookie(options =>
            {
                //cokkie名称
                options.Cookie.Name = "Y.Authorization";
                //cokkie过期时间
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                //cokkie启用滑动过期时间
                options.SlidingExpiration = true;
            }).AddJwtBearer(options =>
            {
                var jwtsetting = configuration.GetSection("App.JWtSetting").Get<JwtSetting>();
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true, //是否验证Issuer
                    ValidIssuer = jwtsetting.Issuer, //发行人Issuer
                    ValidateAudience = true, //是否验证Audience
                    ValidAudience = jwtsetting.Audience,//
                    ValidateIssuerSigningKey = true, //是否验证SecurityKey
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtsetting.SecretKey)), //SecurityKey
                    ValidateLifetime = true, //是否验证失效时间
                    ClockSkew = TimeSpan.FromSeconds(30), //过期时间容错值，解决服务器端时间不同步问题（秒）
                    RequireExpirationTime = true,
                };
            });

            context.Services.AddSwaggerGen(options =>
            {
                options.OperationFilter<AddResponseHeadersFilter>();
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                options.OperationFilter<SecurityRequirementsOperationFilter>();
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "Calo API v1",
                    Title = "Calo API",
                    Description = "Web API for managing By Calo-YG",
                    TermsOfService = new Uri("https://gitee.com/wen-yaoguang"),
                    Contact = new OpenApiContact
                    {
                        Name = "Gitee 地址",
                        Url = new Uri("https://gitee.com/wen-yaoguang/Colo.Blog")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "个人博客",
                        Url = new Uri("https://www.se.cnblogs.com/lonely-wen/")
                    }
                });
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                options.OrderActionsBy(o => o.RelativePath);
            });
        }

        public override void InitApplication(InitApplicationContext context)
        {
            var app = context.GetApplicationBuilder();

            var env = (IHostingEnvironment)context.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //鉴权中间件
            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endOptions =>
            {
                endOptions.MapDefaultControllerRoute();
                endOptions.MapRazorPages();
            });

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Calo API V1");
                options.EnableDeepLinking();
                options.DocExpansion(DocExpansion.None);
                options.IndexStream = () =>
                {
                    var path = Path.Join(env.WebRootPath, "pages", "swagger.html");
                    return new FileInfo(path).OpenRead();
                };
            });

        }
    }
}
