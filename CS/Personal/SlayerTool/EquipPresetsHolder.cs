using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunescapeSlayerHelper
{
    class EquipPresetsHolder
    {
        //singleton implementation
        private static EquipPresetsHolder instance;

        private List<EquipPresets> presetList = new List<EquipPresets>();

        private EquipPresetsHolder() { }

        public static EquipPresetsHolder Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EquipPresetsHolder();
                }
                return instance;
            }
        }

        internal List<EquipPresets> PresetList { get => presetList; }
        //--------------------
    }
}
