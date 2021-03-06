﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeddingWebsite.BusinessLogic.Email;
using WeddingWebsite.BusinessLogic.Instagram;
using WeddingWebsite.BusinessLogic.Providers;
using WeddingWebsite.BusinessLogic.Repositories;
using WeddingWebsite.BusinessLogic.Responders;

namespace WeddingWebsite
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
            services.AddTransient<IEmailClient, EmailClient>();
            services.AddTransient<IEmailClientSettings, FileBasedEmailClientSettings>();
            services.AddTransient<IRSVPRepository, FileBasedRSVPRepository>();
            services.AddTransient<ICommentRepository, FileBasedCommentRepository>();
            services.AddTransient<IRSVPResponder, EmailRSVPResponder>();
            services.AddTransient<ICommentResponder, EmailCommentResponder>();
            services.AddTransient<IResponderDestinationProvider, EmailResponderDestinationProvider>();
            services.AddTransient<IRepositorySettingsProvider, JsonRepositorySettingsProvider>();
            services.AddTransient<IContactResponder, EmailContactResponder>();
            services.AddTransient<ISettings, DefaultSettings>();
            services.AddTransient<IInstagramApi, ScrapedInstagramApi>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", builder => builder.AllowAnyOrigin());
                options.AddPolicy("AllowAllMethods", builder => builder.AllowAnyMethod());
                options.AddPolicy("AllowAllHeaders", builder => builder.AllowAnyHeader());
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(builder =>
                builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin());

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
