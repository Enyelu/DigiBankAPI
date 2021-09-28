using BusinessLogic.Implementations;
using BusinessLogic.Interfaces;
using DataBaseConnections;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Models;
using Repository.Implementation;
using Repository.Interface;
using Repository.Interfaces;
using Utilities.TokenGeneration;

namespace DigiBank
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
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IBankAccountRepo, BankAccountRepo>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IUserLogic, UserLogic>();
            services.AddScoped<IBankAccountLogic, BankAccountLogic>();
            services.AddScoped<ITransactRepo, TransactRepo>();
            services.AddScoped<ITransactionLogic, TransactionLogic>();

            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<DigiBankContext>().AddDefaultTokenProviders();
            services.AddScoped<IUserLogic, UserLogic>();

            services.AddDbContext<DigiBankContext>(options => options.UseSqlite(Configuration.GetConnectionString("DefaultConnections")));
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 6;
                options.User.RequireUniqueEmail = true;
            });

            services.AddControllers();
            services.AddSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DigiBank v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
