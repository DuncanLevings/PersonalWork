using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunescapeSlayerHelper
{
    public class Task
    {
        //general
        private string id;
        private string name;
        private string imgPath_Monster;
        private string youtube_guide;
        private string imgPath_Map;
        private string additionalInfo;

        //equipment
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

        //bag
        private List<Item> bagItems = new List<Item>();

        //familiar
        private Familiar familiar;
        private List<Item> familiarBagItems = new List<Item>();

        //ability bar
        private Ability slot1;
        private Ability slot2;
        private Ability slot3;
        private Ability slot4;
        private Ability slot5;
        private Ability slot6;
        private Ability slot7;
        private Ability slot8;
        private Ability slot9;
        private Ability slot10;
        private Ability slot11;
        private Ability slot12;
        private Ability slot13;
        private Ability slot14;
        private int style;

        //prayers
        private List<Prayer> prayers = new List<Prayer>();

        //presets
        private EquipPresets equipPreset;
        private AbilityPreset abilityPreset;

        //general get set
        public string ID { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string ImgPath_Monster { get => imgPath_Monster; set => imgPath_Monster = value; }
        public string Youtube_guide { get => youtube_guide; set => youtube_guide = value; }
        public string ImgPath_Map { get => imgPath_Map; set => imgPath_Map = value; }
        public string AdditionalInfo { get => additionalInfo; set => additionalInfo = value; }

        //familiar get set
        internal Familiar Familiar { get => familiar; set => familiar = value; }

        //equipment get set
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

        //ability get set
        internal Ability Slot1 { get => slot1; set => slot1 = value; }
        internal Ability Slot2 { get => slot2; set => slot2 = value; }
        internal Ability Slot3 { get => slot3; set => slot3 = value; }
        internal Ability Slot4 { get => slot4; set => slot4 = value; }
        internal Ability Slot5 { get => slot5; set => slot5 = value; }
        internal Ability Slot6 { get => slot6; set => slot6 = value; }
        internal Ability Slot7 { get => slot7; set => slot7 = value; }
        internal Ability Slot8 { get => slot8; set => slot8 = value; }
        internal Ability Slot9 { get => slot9; set => slot9 = value; }
        internal Ability Slot10 { get => slot10; set => slot10 = value; }
        internal Ability Slot11 { get => slot11; set => slot11 = value; }
        internal Ability Slot12 { get => slot12; set => slot12 = value; }
        internal Ability Slot13 { get => slot13; set => slot13 = value; }
        internal Ability Slot14 { get => slot14; set => slot14 = value; }
        public int Style { get => style; set => style = value; }

        internal List<Item> BagItems { get => bagItems; }
        internal List<Item> FamiliarBagItems { get => familiarBagItems; }
        internal List<Prayer> Prayers { get => prayers; }

        //preset get set
        public EquipPresets EquipPreset { get => equipPreset; set => equipPreset = value; }
        public AbilityPreset AbilityPreset { get => abilityPreset; set => abilityPreset = value; }

        public Task (string vName)
        {
            this.Name = vName;
        }
    }
}
