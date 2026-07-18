using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;


namespace ParsingWKTAndWKB
{
    internal class ParseWKTandWKB
    {
        static void Main()
        {
            // WKT

            // define feature
            string wktString = "POINT (73.8567 18.5204)";

            // define and initialise reader
            var wktReader = new WKTReader();
            Geometry polyFromWkt = wktReader.Read(wktString);

            // print feature
            Console.WriteLine("WKT");
            Console.WriteLine($"Geometry Type: {polyFromWkt.GeometryType}");
            Console.WriteLine($"X-coordinate: {((NetTopologySuite.Geometries.Point)polyFromWkt).X}, Y-coordinate: {((NetTopologySuite.Geometries.Point)polyFromWkt).Y}\n");


            // WKB

            // define feature
            byte[] wkbBytes = new byte[] {
                0x01, 0x01, 0x00, 0x00, 0x00,
                0xED, 0x9E, 0x3C, 0x2C, 0xD4, 0x76, 0x52, 0x40,
                0xA1, 0xD6, 0x34, 0xEF, 0x38, 0x85, 0x32, 0x40
            };

            // define and initialise reader
            var wkbReader = new WKBReader();
            Geometry pointFromWkb = wkbReader.Read(wkbBytes);

            // print feature
            Console.WriteLine("WKB");
            Console.WriteLine($"Geometry Type: {polyFromWkt.GeometryType}");
            Console.WriteLine($"X-coordinate: {((NetTopologySuite.Geometries.Point)polyFromWkt).X}, Y-coordinate: {((NetTopologySuite.Geometries.Point)polyFromWkt).Y}\n");
        }
    }
}
