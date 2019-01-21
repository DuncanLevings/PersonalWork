using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunescapeSlayerHelper
{
    class Prayer
    {
        private string imgPath_Prayer;
        private int prayerSlotIndex;

        public string ImgPath_Prayer { get => imgPath_Prayer; set => imgPath_Prayer = value; }
        public int PrayerSlotIndex { get => prayerSlotIndex; set => prayerSlotIndex = value; }

        public Prayer()
        {
        }
    }
}
