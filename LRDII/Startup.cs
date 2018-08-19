using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LRDII.Infrastructure;
using LRDII.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReflectionIT.Mvc.Paging;

namespace LRDII
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
            // Register LRDII database context
            services.AddDbContext<LrdiiDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("LRDII"),
                optionsBuilders => optionsBuilders.MigrationsAssembly("LRDII")));

            services.AddMvc();
            services.AddScoped<IMemberService, MemberServiceController>();
            services.AddScoped<IShareService, ShareServiceController>();
            services.AddScoped<IShareTransactionService, ShareTransactionServiceController>();
            services.AddScoped<ILoanTransactionService, LoanTransactionServiceController>();
            services.AddScoped<ILoanRepaymentTransactionService, LoanRepaymentTransactionServiceController>();
            services.AddScoped(typeof(ILrdiiRepository<>), typeof(LrdiiRepository<>));
            services.AddPaging();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Member/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Member}/{action=Index}/{id?}");
            });
        }
    }
}
