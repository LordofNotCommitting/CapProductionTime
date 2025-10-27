using MGSC;
using ModConfigMenu.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CapProductionTime
{
    // Token: 0x02000006 RID: 6
    public class ModConfigGeneral
    {
        // Token: 0x0600001D RID: 29 RVA: 0x00002840 File Offset: 0x00000A40
        public ModConfigGeneral(string ModName, string ConfigPath)
        {
            this.ModName = ModName;
            this.ModData = new ModConfigData(ConfigPath);
            this.ModData.AddConfigHeader("STRING:General Settings", "general");
            this.ModData.AddConfigValue("general", "about", "STRING:if you produce an item more than this 'Production Speed Cap', production time will be set as if you are making 'Production Speed Cap' number of items \n");
            this.ModData.AddConfigValue("general", "Prod_Speed_Cap", 10, 1, 500, "STRING:Set Production Speed Cap", "STRING:Set the number of item where the production speed will cap at.");
            this.ModData.AddConfigValue("general", "about2", "STRING:<color=#f51b1b>The game must be restarted after setting then saving this config to take effect.</color>\n");
            this.ModData.RegisterModConfigData(ModName);
        }

        // Token: 0x04000011 RID: 17
        private string ModName;

        // Token: 0x04000012 RID: 18
        public ModConfigData ModData;

    }
}
