using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunescapeSlayerHelper
{
    public class AbilityPreset
    {
        private string id;
        private string name;
        private int style;
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

        public string Name { get => name; set => name = value; }
        public string ID { get => id; set => id = value; }
        public int Style { get => style; set => style = value; }
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

        public AbilityPreset(String name)
        {
            this.name = name;
        }
    }
}
