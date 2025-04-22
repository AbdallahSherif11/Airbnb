using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.Helpers
{
    public static class AmenityMapper
    {
        public static readonly Dictionary<string, int> NameToId = new(StringComparer.OrdinalIgnoreCase)
        {
            ["Wi-Fi"] = 1,
            ["Air Conditioning"] = 2,
            ["Hair Dryer"] = 3,
            ["Washer"] = 4,
            ["Dryer"] = 5,
            ["TV"] = 6,
            ["Heating"] = 7,
            ["Iron"] = 8,
            ["Kitchen"] = 9,
            ["Bluetooth Sound System"] = 10,
            ["Coffee Maker"] = 11,
            ["Parking"] = 12,
            ["Safe"] = 13,
            ["Smoke Alarm"] = 14,
            ["Microwave"] = 15,
            ["Hot Water"] = 16
        };
    }
}
