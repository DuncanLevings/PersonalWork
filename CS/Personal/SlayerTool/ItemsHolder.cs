using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunescapeSlayerHelper
{
    public sealed class ItemsHolder
    {
        //singleton implementation
        private static ItemsHolder instance;

        private List<Item> itemList = new List<Item>();

        private ItemsHolder() { }

        public static ItemsHolder Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ItemsHolder();
                }
                return instance;
            }
        }

        internal List<Item> ItemList { get => itemList; }
        //--------------------
    }
}
