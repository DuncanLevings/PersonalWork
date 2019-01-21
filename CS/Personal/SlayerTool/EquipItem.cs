using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunescapeSlayerHelper
{
    public class EquipItem
    {
        private string id;
        private string itemName;
        private string imgPath_Item;
        private int equipType;

        public string ItemName { get => itemName; set => itemName = value; }
        public string ImgPath_Item { get => imgPath_Item; set => imgPath_Item = value; }
        public string ID { get => id; set => id = value; }
        public int EquipType { get => equipType; set => equipType = value; }

        public EquipItem(string itemName)
        {
            this.ItemName = itemName;
        }
    }
}
