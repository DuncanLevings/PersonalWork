using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunescapeSlayerHelper
{
    public class Familiar
    {
        private string id;
        private string familiarName;
        private string imgPath_Familiar;
        private int inventorySize;

        public string FamiliarName { get => familiarName; set => familiarName = value; }
        public string ImgPath_Familiar { get => imgPath_Familiar; set => imgPath_Familiar = value; }
        public int InventorySize { get => inventorySize; set => inventorySize = value; }
        public string ID { get => id; set => id = value; }

        public Familiar(string familiarName)
        {
            this.FamiliarName = familiarName;
        }
    }
}
