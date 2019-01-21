using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunescapeSlayerHelper
{
    class FamiliarHolder
    {
        //singleton implementation
        private static FamiliarHolder instance;

        private List<Familiar> familiarList = new List<Familiar>();

        private FamiliarHolder() { }

        public static FamiliarHolder Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FamiliarHolder();
                }
                return instance;
            }
        }

        internal List<Familiar> FamiliarList { get => familiarList; }
        //--------------------
    }
}
