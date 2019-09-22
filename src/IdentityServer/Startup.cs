// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using IdentityServer4;
using System.Security.Cryptography.X509Certificates;

namespace IdentityServer
{
    public class Startup
    {
        public IHostingEnvironment Environment { get; }

        public Startup(IHostingEnvironment environment)
        {
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {



            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("http://localhost:5003")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddAuthentication()
            .AddGoogle("Google", options =>
            {
                options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                options.ClientId = "259593265329-ld8s2dh7bvio2a66v9q57l1ruhg1rgut.apps.googleusercontent.com";
                options.ClientSecret = "xiOpk3EpEcVxRvFCcmE2Nveh";
            })
            .AddOpenIdConnect("oidc", "Auth0", options =>
            {
                options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                options.SignOutScheme = IdentityServerConstants.SignoutScheme;
                options.SaveTokens = true;

                options.Authority = "https://tanver-custom.eu.auth0.com/";
                options.ClientId = "v0DhnUIjGo6wZWqFqjv8ehTtkHS3ppqJ";

                //options.TokenValidationParameters = new TokenValidationParameters
                //{
                //	NameClaimType = "name",
                //	RoleClaimType = "role"
                //};
            });
            // uncomment, if you want to add an MVC-based UI
            services.AddMvc()
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);



            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseSuccessEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;

            })
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApis())
                .AddInMemoryClients(Config.GetClients())
                .AddTestUsers(Config.GetUsers())
                .AddDeveloperSigningCredential();

            //if (Environment.IsDevelopment())
            //{
            //	builder.AddDeveloperSigningCredential();
            //}
            //else
            //{

            //	throw new Exception("need to configure key material");
            //}

        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("default");
            // uncomment if you want to support static files
            app.UseStaticFiles();

            app.UseIdentityServer();

            // uncomment, if you want to add an MVC-based UI
            app.UseMvcWithDefaultRoute();
        }
    }
}
