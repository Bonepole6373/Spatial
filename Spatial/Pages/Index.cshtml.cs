using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using Spatial.Data;
using Spatial.Models;
using System.Collections.Generic;
using System.Linq;

namespace Spatial.Pages
{
	public class IndexModel : PageModel
	{
		public MySQLContext MySQLContext { get; }
		public MariaDBContext MariaDBContext { get; }
		public MSSQLContext MSSQLContext { get; }

		public IndexModel(MySQLContext mysqlcontext, MariaDBContext mariadbcontext, MSSQLContext mssqlcontext)
		{
			MySQLContext = mysqlcontext;
			MariaDBContext = mariadbcontext;
			MSSQLContext = mssqlcontext;
		}

		public List<RoutePoint> MySQLPoints { get; set; }
		public List<RoutePoint> MariaDBPoints { get; set; }
		public List<RoutePoint> MSSQLPoints { get; set; }

		public async System.Threading.Tasks.Task<IActionResult> OnGetAsync()
		{
			var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

			var myLocation = geometryFactory.CreatePoint(new Coordinate(y: 18.481188, x: -69.938951));

			MySQLPoints = await MySQLContext.MySQLPoints
								.Select(x => new RoutePoint() { RoutePointID = x.RoutePointID, Miles = x.Point.Distance(myLocation) })
								.ToListAsync();

			MariaDBPoints = await MariaDBContext.MariaPoints
								  .Select(x => new RoutePoint() { RoutePointID = x.RoutePointID, Miles = x.Point.Distance(myLocation) })
								  .ToListAsync();

			MSSQLPoints = await MSSQLContext.MSSQLPoints
								.Select(x => new RoutePoint() { RoutePointID = x.RoutePointID, Miles = x.Point.Distance(myLocation) })
								.ToListAsync();

			return Page();
		}
	}
}