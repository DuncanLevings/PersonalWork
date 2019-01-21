namespace RunescapeSlayerHelper
{
    public class Item
    {
        private string id;
        private string itemName;
        private string imgPath_Item;
        private int bagSlotIndex;

        public string ItemName { get => itemName; set => itemName = value; }
        public string ImgPath_Item { get => imgPath_Item; set => imgPath_Item = value; }
        public int BagSlotIndex { get => bagSlotIndex; set => bagSlotIndex = value; }
        public string ID { get => id; set => id = value; }

        public Item(string itemName)
        {
            this.ItemName = itemName;
        }
    }
}
