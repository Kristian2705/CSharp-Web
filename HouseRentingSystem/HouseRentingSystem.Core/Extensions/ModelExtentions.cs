using HouseRentingSystem.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HouseRentingSystem.Core.Extensions
{
    public static class ModelExtentions
    {
        public static string GetInformation(this IHouseModel house)
        {
            string info = house.Title.Replace(" ", "-") + GetAddress(house.Address);
            info = Regex.Replace(info, @"[^a-zA-Z0-9\-]", string.Empty);

            return info;
        }

        private static string GetAddress(string address)
        {
            address = string.Join("-", address.Split(" ").Take(3));

            return address;
        }
    }
}
