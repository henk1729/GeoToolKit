using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO.Converters;

namespace GeoJSONParser
{
    internal class Parser
    {
        private void ParseGeoJSONObject()
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, "ParksNearMe.json");

            // 1. Read all the raw GeoJSON data
            string rawGeoJsonText = File.ReadAllText(filePath);

            // 2. Instruct the JSON serializer how to read GeoJSON objects
            var serializationOptions = new JsonSerializerOptions();
            serializationOptions.Converters.Add(new GeoJsonConverterFactory());

            // 3. Collect the features (individual objects like point, line, polygon) into a FeatureCollection object
            var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(rawGeoJsonText, serializationOptions);

            if(featureCollection != null)
            {
                // 4. Iterate through the features
                foreach (var feature in featureCollection)
                {
                    string featureName = feature.Attributes.Exists("name") ? feature.Attributes["name"]?.ToString() : "Unnamed feature";
                    
                    Geometry featureData = feature.Geometry;
                    if (featureData is Point node)
                    {
                        Console.WriteLine($"Point: {featureName}");
                    }
                    else if (featureData is LineString line)
                    {
                        Console.WriteLine($"Line: {featureName}");
                    }
                    else if (featureData is Polygon area)
                    {
                        Console.WriteLine($"Polygon: {featureName}");
                    }

                    foreach(Coordinate coordinate in featureData.Coordinates)
                    {
                        Console.Write($"({coordinate.X}, {coordinate.Y}), ");
                    }
                    Console.WriteLine();
                    string[] propertyKeys = feature.Attributes.GetNames();
                    foreach(string propertyKey in propertyKeys)
                    {
                        Console.WriteLine($"{propertyKey}: {feature.Attributes[propertyKey]}");
                    }
                    Console.WriteLine("---------------------------------------------------------");
                    Console.WriteLine();
                }
            }
        }

        static void Main()
        {
            var parser = new Parser();
            parser.ParseGeoJSONObject();
        }
    }
}
