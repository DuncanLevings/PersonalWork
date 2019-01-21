using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunescapeSlayerHelper
{
    class EquipItemsHolder
    {
        //singleton implementation
        private static EquipItemsHolder instance;

        private List<EquipItem> weaponList = new List<EquipItem>();
        private List<EquipItem> offhandList = new List<EquipItem>();
        private List<EquipItem> helmList = new List<EquipItem>();
        private List<EquipItem> chestList = new List<EquipItem>();
        private List<EquipItem> legList = new List<EquipItem>();
        private List<EquipItem> bootList = new List<EquipItem>();
        private List<EquipItem> gloveList = new List<EquipItem>();
        private List<EquipItem> ringList = new List<EquipItem>();
        private List<EquipItem> amuletLit = new List<EquipItem>();
        private List<EquipItem> ammoList = new List<EquipItem>();
        private List<EquipItem> auraList = new List<EquipItem>();
        private List<EquipItem> backList = new List<EquipItem>();
        private List<EquipItem> pocketList = new List<EquipItem>();
        private List<EquipItem> sigilList = new List<EquipItem>();

        private EquipItemsHolder() { }

        public static EquipItemsHolder Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EquipItemsHolder();
                }
                return instance;
            }
        }

        internal List<EquipItem> WeaponList { get => weaponList; }
        internal List<EquipItem> OffhandList { get => offhandList; }
        internal List<EquipItem> HelmList { get => helmList; }
        internal List<EquipItem> ChestList { get => chestList; }
        internal List<EquipItem> LegList { get => legList; }
        internal List<EquipItem> BootList { get => bootList; }
        internal List<EquipItem> GloveList { get => gloveList; }
        internal List<EquipItem> RingList { get => ringList;  }
        internal List<EquipItem> AmuletLit { get => amuletLit; }
        internal List<EquipItem> AmmoList { get => ammoList; }
        internal List<EquipItem> AuraList { get => auraList; }
        internal List<EquipItem> BackList { get => backList; }
        internal List<EquipItem> PocketList { get => pocketList; }
        internal List<EquipItem> SigilList { get => sigilList; }
        
        //--------------------
    }
}
