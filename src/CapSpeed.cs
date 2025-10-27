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

        static bool Prefix(MagnumCargo magnumCargo, MagnumProjects projects, MagnumProgression magnumSpaceship, SpaceTime time, Difficulty difficulty, ItemProduceReceipt receipt, int count, int lineIndex)
        {
            
            MagnumProject withModifications = projects.GetWithModifications(receipt.OutputItem);
            float prodlineProduceSpeedBonus = magnumSpaceship.ProdlineProduceSpeedBonus;
            int durationInHours = (int)Mathf.Max((receipt.ProduceTimeInHours + prodlineProduceSpeedBonus) * (1f / difficulty.Preset.MagnumCraftingTime), 1f) * (count > cap_value ? cap_value : count);
            if (!magnumCargo.ItemProduceOrders.ContainsKey(lineIndex))
            {
                magnumCargo.ItemProduceOrders[lineIndex] = new List<ProduceOrder>();
            }
            ProduceOrder produceOrder = new ProduceOrder
            {
                OrderId = ((withModifications != null) ? withModifications.CustomRecord.Id : receipt.OutputItem),
                Count = count,
                DurationInHours = durationInHours,
                StartTime = time.Time
            };
            foreach (ItemQuantity itemQuantity in receipt.RequiredItems)
            {
                for (int i = 0; i < itemQuantity.Count; i++)
                {
                    produceOrder.RequiredItems.Add(itemQuantity.ItemId);
                }
            }
            magnumCargo.ItemProduceOrders[lineIndex].Add(produceOrder);
            foreach (ItemQuantity itemQuantity2 in receipt.RequiredItems)
            {
                MagnumCargoSystem.RemoveSpecificCargo(magnumCargo, itemQuantity2.ItemId, (short)(count * itemQuantity2.Count));
            }
            return false;
        }



    }
}
