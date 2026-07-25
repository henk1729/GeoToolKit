using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoundingBoxAndPointInPolygon
{
    internal class BBoxAndPIPClass
    {
        static void Main()
        {
            GeometryFactory factory = new GeometryFactory();
            // create polygon for city
            Polygon city = CreateComplexPolygon(factory);
            // create bounding box for city
            Envelope bbox = city.EnvelopeInternal;

            // declare locations to check
            Coordinate[] locations = [new(4, 5), new(6, 1), new(8, 1)];

            // iterate through locations
            foreach(Coordinate location in locations)
            {
                // if bounding box does not contain point => outside
                if (!bbox.Contains(location))
                {
                    Console.WriteLine($"Point: ({location.X}, {location.Y})-> Location: Outside");
                }
                // else, check if the point is actually inside by point in polygon method
                else
                {
                    bool isActuallyInside = city.Contains(factory.CreatePoint(location));
                    Console.WriteLine($"Point: ({location.X}, {location.Y})-> Location: {(isActuallyInside ? "Inside" : "Outside")}");
                }
            }
        }

        private static Polygon CreateComplexPolygon(GeometryFactory factory)
        {
            return factory.CreatePolygon(new[]
            {
                new Coordinate(1, 1),
                new Coordinate(2, 0.5),
                new Coordinate(3, 2),
                new Coordinate(4, 1),
                new Coordinate(5, 0.5),
                new Coordinate(6, 2),
                new Coordinate(7, 3),
                new Coordinate(6, 5),
                new Coordinate(5, 6.5),
                new Coordinate(3, 7),
                new Coordinate(1, 6),
                new Coordinate(1.5, 4),
                new Coordinate(1, 3),
                new Coordinate(1, 1)
            });
        }
    }
}
