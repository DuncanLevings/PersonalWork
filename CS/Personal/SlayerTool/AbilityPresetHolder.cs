using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunescapeSlayerHelper
{
    class AbilityPresetHolder
    {
        //singleton implementation
        private static AbilityPresetHolder instance;

        private List<AbilityPreset> presetList = new List<AbilityPreset>();

        private AbilityPresetHolder() { }

        public static AbilityPresetHolder Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AbilityPresetHolder();
                }
                return instance;
            }
        }

        internal List<AbilityPreset> PresetList { get => presetList; }
        //--------------------
    }
}
