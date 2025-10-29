using HarmonyLib;
using MGSC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace CapProductionTime
{

    [HarmonyPatch(typeof(ItemProductionSystem), nameof(ItemProductionSystem.StartMagnumItemProduction))]
    public class CapSpeed
    {
        //steam mod ID 3594238447
        static int cap_value = Plugin.ConfigGeneral.ModData.GetConfigValue<int>("Prod_Speed_Cap", 9999);



        static void Postfix(MagnumCargo magnumCargo, MagnumProjects projects, MagnumProgression magnumSpaceship, SpaceTime time, Difficulty difficulty, ItemProduceReceipt receipt, int count, int lineIndex)
        {

            int time_cap = (count > cap_value ? cap_value : count);
            float prodlineProduceSpeedBonus = magnumSpaceship.ProdlineProduceSpeedBonus;
            int durationInHours_postfix = (int)Mathf.Max((receipt.ProduceTimeInHours + prodlineProduceSpeedBonus) * (1f / difficulty.Preset.MagnumCraftingTime), 1f) * time_cap;
            //magnumCargo.ItemProduceOrders[lineIndex].Single().DurationInHours = durationInHours_postfix;
            magnumCargo.ItemProduceOrders[lineIndex][magnumCargo.ItemProduceOrders[lineIndex].Count() - 1].DurationInHours = durationInHours_postfix;

        }


    }
}
