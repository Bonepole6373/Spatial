using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Spatial.Data;
using System;

namespace Spatial
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<MySQLContext>(options =>
				options.UseMySql(Configuration.GetConnectionString("MySQLConnection"), x => x.UseNetTopologySuite().ServerVersion(new Version(8, 0, 21), ServerType.MySql))
			);

			services.AddDbContext<MariaDBContext>(options =>
				options.UseMySql(Configuration.GetConnectionString("MariaDBConnection"), x => x.UseNetTopologySuite().ServerVersion(new Version(10, 5, 5), ServerType.MariaDb))
			);

			services.AddDbContext<MSSQLContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("MSSQLConnection"), x => x.UseNetTopologySuite())
			);

			services.AddRazorPages();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
			}

			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
			});
		}
	}
}
