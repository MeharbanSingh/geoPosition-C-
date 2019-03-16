using System;
using System.Device.Location;

namespace WhereAmI
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Add a Reference to System.Device.dll
            // 2. Use the GeoCoordinate Watcher

            Console.WriteLine("Starting GeoCoordinate Watcher...");
            var watcher = new GeoCoordinateWatcher();

            watcher.StatusChanged += (s, e) =>
            {
                Console.WriteLine($"GeoCordinateWatcher:StatusChanged:{e.Status}");
            };
            watcher.PositionChanged += (s, e) =>
            {
                watcher.Stop();
                Console.WriteLine($"GeoCordinateWatcher:PositionChanged:{e.Position.Location}");

                // 3. Use the Map Image REST API
                MapImage.Show(e.Position.Location);
            };

            watcher.MovementThreshold = 100;

            watcher.Start();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
