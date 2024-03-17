using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using HouseRentingSystem.Core.Models.Agent;

namespace HouseRentingSystem.Core.Models.House
{
    public class HouseDetailsServiceModel : HouseServiceModel
    {
        public string Description { get; set; } = null!;

        public string Category { get; set; } = null!;
        public AgentServiceModel Agent { get; set; } = null!;
    }
}
