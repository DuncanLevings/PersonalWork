using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunescapeSlayerHelper
{
    public class EquipPresets
    {
        private string id;
        private string name;
        private EquipItem weapon;
        private EquipItem offHand;
        private EquipItem helm;
        private EquipItem torso;
        private EquipItem legs;
        private EquipItem boots;
        private EquipItem glove;
        private EquipItem back;
        private EquipItem amulet;
        private EquipItem ring;
        private EquipItem ammo;
        private EquipItem pocket;
        private EquipItem aura;
        private EquipItem sigil;

        public string Name { get => name; set => name = value; }
        public string ID { get => id; set => id = value; }
        internal EquipItem Weapon { get => weapon; set => weapon = value; }
        internal EquipItem OffHand { get => offHand; set => offHand = value; }
        internal EquipItem Helm { get => helm; set => helm = value; }
        internal EquipItem Torso { get => torso; set => torso = value; }
        internal EquipItem Legs { get => legs; set => legs = value; }
        internal EquipItem Boots { get => boots; set => boots = value; }
        internal EquipItem Glove { get => glove; set => glove = value; }
        internal EquipItem Back { get => back; set => back = value; }
        internal EquipItem Amulet { get => amulet; set => amulet = value; }
        internal EquipItem Ring { get => ring; set => ring = value; }
        internal EquipItem Ammo { get => ammo; set => ammo = value; }
        internal EquipItem Pocket { get => pocket; set => pocket = value; }
        internal EquipItem Aura { get => aura; set => aura = value; }
        internal EquipItem Sigil { get => sigil; set => sigil = value; }

        public EquipPresets(string vName)
        {
            this.Name = vName;
        }
    }
}
