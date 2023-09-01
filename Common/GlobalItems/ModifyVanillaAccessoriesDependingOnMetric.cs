using GlasiasFirstMod.Common.Config;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Localization;

namespace GlasiasFirstMod.Common.GlobalItems
{
    public class ModifyVanillaAccessoriesDependingOnMetric : GlobalInfoDisplay
    {
        public override void ModifyDisplayValue(InfoDisplay currentDisplay, ref string displayValue)
        {
            bool metricLengthAndSpeedEnabled = ModContent.GetInstance<MetricImperialToggles>().MetricLengthAndSpeed;
            bool militaryTimeEnabled = ModContent.GetInstance<MetricImperialToggles>().MilitaryTime;
            Player currentPlayer = Main.LocalPlayer;
            double postitionOfCenterX = 16*Main.maxTilesX /2;
            double surface = Main.worldSurface * 16;

            if (currentDisplay.Equals(InfoDisplay.Stopwatch) && metricLengthAndSpeedEnabled) {

                double velocityInBlocksPerTick = Math.Sqrt(Math.Pow(currentPlayer.velocity.X, 2) + Math.Pow(Main.LocalPlayer.velocity.Y, 2));
                displayValue =  $"{(int)(1.609 * 1.609 * 131.6736 / 41.5 * velocityInBlocksPerTick)} km/h";
            }

            if (currentDisplay.Equals(InfoDisplay.DepthMeter) && metricLengthAndSpeedEnabled) {

                double absoluteDistanceToSurface = Math.Truncate (0.0381 * Math.Abs (currentPlayer.position.Y - surface) * 10)/10;
                // displayValue = $"y pos {currentPlayer.position.ToTileCoordinates16().Y}, surface at: {surface}";
                
                if (absoluteDistanceToSurface < 1.00) {
                    displayValue = $"{Language.GetTextValue("GameUI.DepthLevel")}";
                }
                else  if (currentPlayer.ZoneSkyHeight)
                {
                    
                    displayValue = $"{absoluteDistanceToSurface} m {Language.GetTextValue("GameUI.LayerSpace")}";
                    
                }
                else if (currentPlayer.ZoneOverworldHeight) {
                    displayValue = $"{absoluteDistanceToSurface} m {Language.GetTextValue("GameUI.LayerSurface")}";
                }
                else if (currentPlayer.ZoneRockLayerHeight)
                {
                    displayValue = $"{absoluteDistanceToSurface} m {Language.GetTextValue("GameUI.LayerCaverns")}";
                }
                else if (currentPlayer.ZoneDirtLayerHeight)
                {
                    displayValue = $"{absoluteDistanceToSurface} m {Language.GetTextValue("GameUI.LayerUnderground")}";
                }
                else if (currentPlayer.ZoneUnderworldHeight)
                {
                    displayValue = $"{absoluteDistanceToSurface} m {Language.GetTextValue("GameUI.LayerUnderworld")}";
                }
                

            }

            if (currentDisplay.Equals(InfoDisplay.Compass) && metricLengthAndSpeedEnabled) {
                double absoluteDistanceFromCenter = Math.Truncate(0.0381 * Math.Abs(currentPlayer.position.X - postitionOfCenterX) * 10) / 10;
                //displayValue = $"x pos {currentPlayer.position.X}, center at {postitionOfCenterX}, world size tiles: {Main.maxTilesX}";
                
                if (absoluteDistanceFromCenter < 1.00)
                {
                    displayValue = $"{Language.GetTextValue("GameUI.CompassCenter")}";
                }
                else if (currentPlayer.position.X < postitionOfCenterX) {
                    displayValue = $"{absoluteDistanceFromCenter} m {Language.GetTextValue("CreativePowers.WindWest")}";
                }
                else if (currentPlayer.position.X > postitionOfCenterX)
                {
                    displayValue = $"{absoluteDistanceFromCenter} m {Language.GetTextValue("CreativePowers.WindEast")}";
                }
                
                
            }

            if (currentDisplay.Equals(InfoDisplay.Watches) && militaryTimeEnabled)  {
                float hoursPastMidnight = Utils.GetDayTimeAs24FloatStartingFromMidnight();

                int hours = (int)hoursPastMidnight;
                int minutes = (int)((hoursPastMidnight - hours) * 60);
                hours %= 24;
                displayValue = $"{hours}:{(minutes < 10 ? "0" : "")}{minutes}";


            }

            base.ModifyDisplayValue(currentDisplay, ref displayValue);
        }
    }
}
