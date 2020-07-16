using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql;
using Swap.Api.Data;
using Swap.Api.Models;
using Swap.Api.Repository;
using Swap.Api.Service;
using Swap.Api.ValueObjects;
//using Swap.Api.Service.Interfaces;
using System.Text;

namespace Swap.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		[System.Obsolete]
		public void ConfigureServices(IServiceCollection services)
		{

			services.AddCors();
			
			var cs = new NpgsqlConnectionStringBuilder(Configuration.GetConnectionString("Development"));


			services.AddDbContext<DataContext>(options =>
			{
				options.UseNpgsql(cs.ConnectionString);
			});
			services.AddIdentityCore<ApplicationUser>()
				.AddSignInManager()
				.AddEntityFrameworkStores<DataContext>()
				.AddDefaultTokenProviders();

			//services.AddDefaultIdentity<IdentityUser<ApplicationUser>>()
			//	.AddEntityFrameworkStores<DataContext>()
			//	.AddDefaultTokenProviders();

			var appSettingSection = Configuration.GetSection("AppSettings");
			services.Configure<AppSettings>(appSettingSection);

			var appSettings = appSettingSection.Get<AppSettings>();

			var key = Encoding.ASCII.GetBytes(appSettings.Secret);

			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultSignInScheme = IdentityConstants.ExternalScheme;
			})
				.AddJwtBearer(x =>
		   {
			   x.RequireHttpsMetadata = true;
			   x.SaveToken = true;
			   x.TokenValidationParameters = new TokenValidationParameters
			   {
				   ValidateIssuerSigningKey = true,
				   IssuerSigningKey = new SymmetricSecurityKey(key),
				   ValidateIssuer = true,
				   ValidateAudience = true,
				   ValidAudience = appSettings.Audience,
				   ValidIssuer = appSettings.Issuer

			   };
		   }).AddIdentityCookies();
			
		

			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IRepository<Item>, Repository<Item>>();
			services.AddScoped<IItemService, ItemService>();
			services.AddScoped<IAuthService, AuthService>();

			services.AddMvc(x => x.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddFluentValidation();
			services.AddTransient<IValidator<RegisterUserVO>, RegisterUserValidation>();
			services.AddTransient<IValidator<LoginUserVO>, LoginUserValidation>();

			services.AddSwaggerGen(x =>
			{
				x.SwaggerDoc("v1", new OpenApiInfo { Title = "Exchange API", Version = "v1" });
			});
		}



		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		[System.Obsolete]
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

			var swaggerOptions = new SwaggerOptions();
			Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);
			app.UseSwagger(option => { option.RouteTemplate = swaggerOptions.JsonRoute; });
			app.UseSwaggerUI(option => { option.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description); });

			app.UseCors(x => x
				.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader());

			app.UseAuthentication();
			app.UseAuthorization();


			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}
}
