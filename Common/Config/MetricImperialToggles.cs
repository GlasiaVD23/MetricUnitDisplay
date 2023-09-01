using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace GlasiasFirstMod.Common.Config
{
    public class MetricImperialToggles : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [DefaultValue(false)]
        public bool MetricLengthAndSpeed;

        [DefaultValue(false)]
        public bool MilitaryTime;
    }
}
