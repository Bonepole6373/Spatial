using NetTopologySuite.Geometries;

namespace Spatial.Models
{
	public class RoutePoint
	{
		public int RoutePointID { get; set; }

		public Point Point { get; set; }

		public double Miles { get; set; }

	}
}
