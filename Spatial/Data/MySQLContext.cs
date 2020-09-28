using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using Spatial.Models;
using System.Collections.Generic;

namespace Spatial.Data
{
	public class MySQLContext : DbContext
	{
		public MySQLContext(DbContextOptions<MySQLContext> options) : base(options)
		{
		}

		public virtual DbSet<RoutePoint> MySQLPoints { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<RoutePoint>().ToTable("mysqlpoints");

			var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

			modelBuilder.Entity<RoutePoint>().HasData(
				new List<RoutePoint>()
				{
					new RoutePoint(){RoutePointID = 1, Point = geometryFactory.CreatePoint(new Coordinate(y:18.4839233,x:-69.9388777))},
					new RoutePoint(){RoutePointID = 2, Point = geometryFactory.CreatePoint(new Coordinate(y:18.4826214,x:-69.9118804))},
					new RoutePoint(){RoutePointID = 3, Point = geometryFactory.CreatePoint(new Coordinate(y:18.4718075,x:-69.9334673))},
					new RoutePoint(){RoutePointID = 4, Point = geometryFactory.CreatePoint(new Coordinate(y:19.4336164,x:-99.1353659))},
					new RoutePoint(){RoutePointID = 5, Point = geometryFactory.CreatePoint(new Coordinate(y:51.5285582,x:-0.2416804))}
				});
		}
	}
}
