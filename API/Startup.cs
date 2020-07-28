using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DAL;
using Helpers;
using Logics;
using Logics.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace API
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
            services.AddControllers().AddNewtonsoftJson(s => 
            {
                s.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddDbContext<IRCContext>(options => options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=IRCProjects;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));
            services.AddTransient<IPositionLogic, PositionLogic>();
            services.AddTransient<IEmployeePositionLogic, EmployeePositionLogic>();
            services.AddTransient<IEmployeeLogic, EmployeeLogic>();
            services.AddTransient<ICityLogic, CityLogic>();
            services.AddTransient<IExternalMentorLogic, ExternalMentorLogic>();
            services.AddTransient<IExternalMentorContactLogic, ExternalMentorContactLogic>();
            services.AddTransient<ICompanyLogic, CompanyLogic>();
            services.AddTransient<ICompanyAuthLogic, CompanyAuthLogic>();
            services.AddTransient<IContactCompany, ContactCompanyLogic>();
            services.AddTransient<ILocationLogic, LocationLogic>();
            services.AddTransient<IScientificArea, ScientificAreaLogic>();
            services.AddTransient<IProjectProposal, ProjectProposalLogic>();
            services.AddTransient<IProjectLogic, ProjectLogic>();
            services.AddTransient<IProjectContractLogic, ProjectContractLogic>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
                            Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

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
