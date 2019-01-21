using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RunescapeSlayerHelper
{
    public static class FileHandler
    {
        public const string taskXMLFile = "C:/Users/Duncan/Documents/Runescape/SlayerHelperImages/Files/tasks.xml";
        public const string itemXMLFile = "C:/Users/Duncan/Documents/Runescape/SlayerHelperImages/Files/items.xml";
        public const string equipXMLFile = "C:/Users/Duncan/Documents/Runescape/SlayerHelperImages/Files/equips.xml";
        public const string familiarXMLFile = "C:/Users/Duncan/Documents/Runescape/SlayerHelperImages/Files/familiars.xml";
        public const string equip_presetXMLFile = "C:/Users/Duncan/Documents/Runescape/SlayerHelperImages/Files/equip_presets.xml";
        public const string ability_presetXMLFile = "C:/Users/Duncan/Documents/Runescape/SlayerHelperImages/Files/ability_presets.xml";

        /*
         * TASK FILE ====================================================================================
         */

        public static void loadTasks()
        {
            XDocument doc = XDocument.Load(taskXMLFile);
            var tasks = from c in doc.Descendants("task")
                        select new
                        {
                            taskName = c.Element("name").Value,
                            ID = c.Element("id").Value,
                            imgPath_Monster = c.Element("imgpath_monster").Value,
                            youtube_guide = c.Element("youtube").Value,
                            imgPath_Map = c.Element("imgpath_map").Value,
                            additionalInfo = c.Element("info").Value,
                            //equips
                            weaponID = c.Element("weaponID").Value,
                            offhandID = c.Element("offhandID").Value,
                            helmID = c.Element("helmID").Value,
                            chestID = c.Element("chestID").Value,
                            legID = c.Element("legID").Value,
                            bootID = c.Element("bootID").Value,
                            gloveID = c.Element("gloveID").Value,
                            amuletID = c.Element("amuletID").Value,
                            ringID = c.Element("ringID").Value,
                            backID = c.Element("backID").Value,
                            ammoID = c.Element("ammoID").Value,
                            pocketID = c.Element("pocketID").Value,
                            auraID = c.Element("auraID").Value,
                            sigilID = c.Element("sigilID").Value,
                            //presets
                            equipPresetID = c.Element("equip_preset").Value,
                            abilityPresetID = c.Element("ability_preset").Value
                        };

            foreach (var t in tasks)
            {
                Task task = new Task(t.taskName);
                task.ID = t.ID;
                task.ImgPath_Monster = t.imgPath_Monster;

                if (t.youtube_guide.Equals("") == false)
                {
                    task.Youtube_guide = t.youtube_guide;
                }

                if (t.imgPath_Map.Equals("") == false)
                {
                    task.ImgPath_Map = t.imgPath_Map;
                }

                if (t.additionalInfo.Equals("") == false)
                {
                    task.AdditionalInfo = t.additionalInfo;
                }

                //EQUIPS===============================================

                task.Weapon = findEquipID(t.weaponID, EquipItemsHolder.Instance.WeaponList);
                task.OffHand = findEquipID(t.offhandID, EquipItemsHolder.Instance.OffhandList);
                task.Helm = findEquipID(t.helmID, EquipItemsHolder.Instance.HelmList);
                task.Torso = findEquipID(t.chestID, EquipItemsHolder.Instance.ChestList);
                task.Legs = findEquipID(t.legID, EquipItemsHolder.Instance.LegList);
                task.Boots = findEquipID(t.bootID, EquipItemsHolder.Instance.BootList);
                task.Glove = findEquipID(t.gloveID, EquipItemsHolder.Instance.GloveList);
                task.Amulet = findEquipID(t.amuletID, EquipItemsHolder.Instance.AmuletLit);
                task.Ring = findEquipID(t.ringID, EquipItemsHolder.Instance.RingList);
                task.Back = findEquipID(t.backID, EquipItemsHolder.Instance.BackList);
                task.Ammo = findEquipID(t.ammoID, EquipItemsHolder.Instance.AmmoList);
                task.Pocket = findEquipID(t.pocketID, EquipItemsHolder.Instance.PocketList);
                task.Aura = findEquipID(t.auraID, EquipItemsHolder.Instance.AuraList);
                task.Sigil = findEquipID(t.sigilID, EquipItemsHolder.Instance.SigilList);

                //BAG ITEMS===============================================
                var bagItems = from c in doc.Descendants("task").Descendants("bag").Descendants("bagItem")
                               where c.Parent.Parent.Element("id").Value == task.ID
                               select new
                                {
                                    itemName = c.Element("itemName").Value,
                                    ID = c.Element("bagID").Value,
                                    slot = c.Element("bagSlot").Value
                                };

                foreach (var bi in bagItems)
                {
                    string path = findItemID(bi.ID);
                    if (path != null)
                    {
                        Item item = new Item(bi.itemName);
                        item.ImgPath_Item = findItemID(bi.ID);
                        item.BagSlotIndex = int.Parse(bi.slot);
                        item.ID = bi.ID;

                        task.BagItems.Add(item);
                    } 
                }

                //FAMILIAR===============================================

                var familiars = from c in doc.Descendants("task").Descendants("familiars").Descendants("familiar")
                                where c.Parent.Parent.Element("id").Value == task.ID
                               select new
                               {
                                   FamiliarName = c.Element("familiarName").Value,
                                   ImgPath_Familiar = c.Element("imgpath_familiar").Value,
                                   InventorySize = c.Element("familiarSize").Value,
                                   ID = c.Element("familiarID").Value
                               };

                foreach (var i in familiars)
                {
                    Familiar familiar = new Familiar(i.FamiliarName);
                    familiar.ImgPath_Familiar = i.ImgPath_Familiar;
                    familiar.InventorySize = int.Parse(i.InventorySize);
                    familiar.ID = i.ID;

                    task.Familiar = familiar;
                }

                var familiarItems = from c in doc.Descendants("task").Descendants("familiars").Descendants("familiarBag").Descendants("familiarBagItem")
                               where c.Parent.Parent.Parent.Element("id").Value == task.ID
                               select new
                               {
                                   itemName = c.Element("familiarItemName").Value,
                                   ID = c.Element("familiarItemID").Value,
                                   slot = c.Element("familiarItemSlot").Value
                               };

                foreach (var fi in familiarItems)
                {
                    Item item = new Item(fi.itemName);
                    item.ImgPath_Item = findItemID(fi.ID);
                    item.BagSlotIndex = int.Parse(fi.slot);
                    item.ID = fi.ID;

                    task.FamiliarBagItems.Add(item);
                }

                //ABILITY BAR===============================================
                var abilitys = from c in doc.Descendants("task").Descendants("ability_bar")
                            where c.Parent.Element("id").Value == task.ID
                            select new
                            {
                                style = c.Element("style").Value,
                                slot0 = c.Element("slot0").Value,
                                slot1 = c.Element("slot1").Value,
                                slot2 = c.Element("slot2").Value,
                                slot3 = c.Element("slot3").Value,
                                slot4 = c.Element("slot4").Value,
                                slot5 = c.Element("slot5").Value,
                                slot6 = c.Element("slot6").Value,
                                slot7 = c.Element("slot7").Value,
                                slot8 = c.Element("slot8").Value,
                                slot9 = c.Element("slot9").Value,
                                slot10 = c.Element("slot10").Value,
                                slot11 = c.Element("slot11").Value,
                                slot12 = c.Element("slot12").Value,
                                slot13 = c.Element("slot13").Value
                            };

                foreach (var i in abilitys)
                {
                    task.Style = int.Parse(i.style);

                    task.Slot1 = newAbility(i.slot0);
                    task.Slot2 = newAbility(i.slot1);
                    task.Slot3 = newAbility(i.slot2);
                    task.Slot4 = newAbility(i.slot3);
                    task.Slot5 = newAbility(i.slot4);
                    task.Slot6 = newAbility(i.slot5);
                    task.Slot7 = newAbility(i.slot6);
                    task.Slot8 = newAbility(i.slot7);
                    task.Slot9 = newAbility(i.slot8);
                    task.Slot10 = newAbility(i.slot9);
                    task.Slot11 = newAbility(i.slot10);
                    task.Slot12 = newAbility(i.slot11);
                    task.Slot13 = newAbility(i.slot12);
                    task.Slot14 = newAbility(i.slot13);
                }

                //PRAYER===============================================
                var prayers = from c in doc.Descendants("task").Descendants("prayers").Descendants("prayer")
                               where c.Parent.Parent.Element("id").Value == task.ID
                               select new
                               {
                                   imgPath_Prayer = c.Element("imgpath_prayer").Value,
                                   slot = c.Element("prayerSlot").Value
                               };

                foreach (var p in prayers)
                {
                    Prayer prayer = new Prayer();
                    prayer.ImgPath_Prayer = p.imgPath_Prayer;
                    prayer.PrayerSlotIndex = int.Parse(p.slot);

                    task.Prayers.Add(prayer);
                }

                //PRESET===============================================
                for (int i = 0; i < EquipPresetsHolder.Instance.PresetList.Count; i++)
                {
                    if (t.equipPresetID.Equals(EquipPresetsHolder.Instance.PresetList[i].ID))
                    {
                        task.EquipPreset = EquipPresetsHolder.Instance.PresetList[i];
                    }
                }

                for (int i = 0; i < AbilityPresetHolder.Instance.PresetList.Count; i++)
                {
                    if (t.abilityPresetID.Equals(AbilityPresetHolder.Instance.PresetList[i].ID))
                    {
                        task.AbilityPreset = AbilityPresetHolder.Instance.PresetList[i];
                    }
                }

                TasksHolder.Instance.TaskList.Add(task);
            }
        }

        /*
        *  SAVING TASK =========================================================
        */
        public static void SaveNewTask(Task task)
        {
            var doc = XDocument.Load(taskXMLFile);

            var taskEl = new XElement("task",
                    new XElement("name", task.Name),
                    new XElement("id", task.ID),
                    new XElement("imgpath_monster", task.ImgPath_Monster),
                    new XElement("youtube", task.Youtube_guide),
                    new XElement("imgpath_map", task.ImgPath_Map),
                    new XElement("info", task.AdditionalInfo)
            );

            //EQUIPS===============================================
            taskEl.Add(saveEquip(task.Weapon, "weaponID"));
            taskEl.Add(saveEquip(task.OffHand, "offhandID"));
            taskEl.Add(saveEquip(task.Helm, "helmID"));
            taskEl.Add(saveEquip(task.Torso, "chestID"));
            taskEl.Add(saveEquip(task.Legs, "legID"));
            taskEl.Add(saveEquip(task.Boots, "bootID"));
            taskEl.Add(saveEquip(task.Glove, "gloveID"));
            taskEl.Add(saveEquip(task.Amulet, "amuletID"));
            taskEl.Add(saveEquip(task.Ring, "ringID"));
            taskEl.Add(saveEquip(task.Back, "backID"));
            taskEl.Add(saveEquip(task.Ammo, "ammoID"));
            taskEl.Add(saveEquip(task.Pocket, "pocketID"));
            taskEl.Add(saveEquip(task.Aura, "auraID"));
            taskEl.Add(saveEquip(task.Sigil, "sigilID"));

            //BAG ITEMS===============================================
            var bagEl = new XElement("bag");
            for (int i = 0; i < task.BagItems.Count; i++)
            {
                var bagItemEl = new XElement("bagItem",
                    new XElement("itemName", task.BagItems[i].ItemName),
                    new XElement("bagID", task.BagItems[i].ID),
                    new XElement("bagSlot", task.BagItems[i].BagSlotIndex)
                );

                bagEl.Add(bagItemEl);
            }

            taskEl.Add(bagEl);

            //FAMILIAR===============================================
            if (task.Familiar != null)
            {
                var familiarsEl = new XElement("familiars");
                var familiarEl = new XElement("familiar",
                   new XElement("familiarName", task.Familiar.FamiliarName),
                   new XElement("imgpath_familiar", task.Familiar.ImgPath_Familiar),
                   new XElement("familiarSize", task.Familiar.InventorySize),
                   new XElement("familiarID", task.Familiar.ID)
                );

                familiarsEl.Add(familiarEl);

                var bagfamEl = new XElement("familiarBag");
                for (int i = 0; i < task.FamiliarBagItems.Count; i++)
                {
                    var bagItemEl = new XElement("familiarBagItem",
                        new XElement("familiarItemName", task.FamiliarBagItems[i].ItemName),
                        new XElement("familiarItemID", task.FamiliarBagItems[i].ID),
                        new XElement("familiarItemSlot", task.FamiliarBagItems[i].BagSlotIndex)
                    );

                    bagfamEl.Add(bagItemEl);
                }

                familiarsEl.Add(bagfamEl);
                taskEl.Add(familiarsEl);
            }

            //ABILITY BAR===============================================

            var abilityEl = new XElement("ability_bar",
                    new XElement("style", task.Style)
            );

            abilityEl.Add(newAbility(task.Slot1, "slot0"));
            abilityEl.Add(newAbility(task.Slot2, "slot1"));
            abilityEl.Add(newAbility(task.Slot3, "slot2"));
            abilityEl.Add(newAbility(task.Slot4, "slot3"));
            abilityEl.Add(newAbility(task.Slot5, "slot4"));
            abilityEl.Add(newAbility(task.Slot6, "slot5"));
            abilityEl.Add(newAbility(task.Slot7, "slot6"));
            abilityEl.Add(newAbility(task.Slot8, "slot7"));
            abilityEl.Add(newAbility(task.Slot9, "slot8"));
            abilityEl.Add(newAbility(task.Slot10, "slot9"));
            abilityEl.Add(newAbility(task.Slot11, "slot10"));
            abilityEl.Add(newAbility(task.Slot12, "slot11"));
            abilityEl.Add(newAbility(task.Slot13, "slot12"));
            abilityEl.Add(newAbility(task.Slot14, "slot13"));

            taskEl.Add(abilityEl);

            //PRAYER===============================================
            var prayersEL = new XElement("prayers");
            for (int i = 0; i < task.Prayers.Count; i++)
            {
                var prayerEl = new XElement("prayer",
                    new XElement("imgpath_prayer", task.Prayers[i].ImgPath_Prayer),
                    new XElement("prayerSlot", task.Prayers[i].PrayerSlotIndex)
                );

                prayersEL.Add(prayerEl);
            }

            taskEl.Add(prayersEL);

            //PRESET===============================================
            taskEl.Add(setEquipPreset(task.EquipPreset));
            taskEl.Add(setAbilityPreset(task.AbilityPreset));

            doc.Element("tasks").Add(taskEl);

            doc.Save(taskXMLFile);
        }

        /*
        *  EDITING TASK =========================================================
        */
        public static void EditTask(Task task)
        {
            XDocument doc = XDocument.Load(taskXMLFile);
            var taskGeneral = (from c in doc.Descendants("task")
                         where c.Element("id").Value == task.ID
                         select c).ToList();

            foreach (var i in taskGeneral)
            {
                i.SetElementValue("name", task.Name);
                i.SetElementValue("imgpath_monster", task.ImgPath_Monster);
                i.SetElementValue("youtube", task.Youtube_guide);

                if (task.ImgPath_Map != null)
                {
                    i.SetElementValue("imgpath_map", task.ImgPath_Map);
                }
                
                i.SetElementValue("info", task.AdditionalInfo);

                if (task.EquipPreset != null)
                {
                    i.SetElementValue("equip_preset", task.EquipPreset.ID);
                }

                if (task.AbilityPreset != null)
                {
                    i.SetElementValue("ability_preset", task.AbilityPreset.ID);
                }
            }

            //EQUIPS===============================================
            var items = (from c in doc.Descendants("task")
                         where c.Element("id").Value == task.ID
                         select c).ToList();

            foreach (var i in items)
            {
                setEquip(task.Weapon, i, "weaponID");
                setEquip(task.OffHand, i, "offhandID");
                setEquip(task.Helm, i, "helmID");
                setEquip(task.Torso, i, "chestID");
                setEquip(task.Legs, i, "legID");
                setEquip(task.Boots, i, "bootID");
                setEquip(task.Glove, i, "gloveID");
                setEquip(task.Amulet, i, "amuletID");
                setEquip(task.Ring, i, "ringID");
                setEquip(task.Back, i, "backID");
                setEquip(task.Ammo, i, "ammoID");
                setEquip(task.Pocket, i, "pocketID");
                setEquip(task.Aura, i, "auraID");
                setEquip(task.Sigil, i, "sigilID");
            }

            //BAG ITEMS===============================================
            var clearBag = (from c in doc.Descendants("task").Descendants("bag").Descendants("bagItem")
                           where c.Parent.Parent.Element("id").Value == task.ID
                           select c).ToList();

            foreach (var tb in clearBag)
            {
                tb.Remove();
            }

            var bagItems = (from c in doc.Descendants("task").Descendants("bag")
                            where c.Parent.Element("id").Value == task.ID
                            select c).FirstOrDefault();

            for (int i = 0; i < task.BagItems.Count; i++)
            {
                var bagItemEl = new XElement("bagItem",
                    new XElement("itemName", task.BagItems[i].ItemName),
                    new XElement("bagID", task.BagItems[i].ID),
                    new XElement("bagSlot", task.BagItems[i].BagSlotIndex)
                );

                bagItems.Add(bagItemEl);
            }

            //FAMILIAR===============================================
            if (task.Familiar != null)
            {
                var familiars = (from c in doc.Descendants("task").Descendants("familiars").Descendants("familiar")
                                where c.Parent.Parent.Element("id").Value == task.ID
                                select c).ToList();

                foreach (var i in familiars)
                {
                    i.SetElementValue("familiarName", task.Familiar.FamiliarName);
                    i.SetElementValue("imgpath_familiar", task.Familiar.ImgPath_Familiar);
                    i.SetElementValue("familiarSize", task.Familiar.InventorySize);
                }
                
                var clearFamiliarBag = (from c in doc.Descendants("task").Descendants("familiars").Descendants("familiarBag").Descendants("familiarBagItem")
                                        where c.Parent.Parent.Parent.Element("id").Value == task.ID
                                        select c).ToList();

                foreach (var tb in clearFamiliarBag)
                {
                    tb.Remove();
                }
                
                var familiarBagItems = (from c in doc.Descendants("task").Descendants("familiars").Descendants("familiarBag")
                                where c.Parent.Parent.Element("id").Value == task.ID
                                select c).FirstOrDefault();

                for (int i = 0; i < task.FamiliarBagItems.Count; i++)
                {
                    var bagItemEl = new XElement("familiarBagItem",
                        new XElement("familiarItemName", task.FamiliarBagItems[i].ItemName),
                        new XElement("familiarItemID", task.FamiliarBagItems[i].ID),
                        new XElement("familiarItemSlot", task.FamiliarBagItems[i].BagSlotIndex)
                    );

                    familiarBagItems.Add(bagItemEl);
                }
            }

            //ABILITY BAR===============================================
            var abilitys = (from c in doc.Descendants("task").Descendants("ability_bar")
                         where c.Parent.Element("id").Value == task.ID
                         select c).ToList();

            foreach (var i in abilitys)
            {
                i.SetElementValue("style", task.Style);

                setAbility(task.Slot1, i, "slot0");
                setAbility(task.Slot2, i, "slot1");
                setAbility(task.Slot3, i, "slot2");
                setAbility(task.Slot4, i, "slot3");
                setAbility(task.Slot5, i, "slot4");
                setAbility(task.Slot6, i, "slot5");
                setAbility(task.Slot7, i, "slot6");
                setAbility(task.Slot8, i, "slot7");
                setAbility(task.Slot9, i, "slot8");
                setAbility(task.Slot10, i, "slot9");
                setAbility(task.Slot11, i, "slot10");
                setAbility(task.Slot12, i, "slot11");
                setAbility(task.Slot13, i, "slot12");
                setAbility(task.Slot14, i, "slot13");
            }

            //PRAYER===============================================
            var clearPrayer = (from c in doc.Descendants("task").Descendants("prayers").Descendants("prayer")
                           where c.Parent.Parent.Element("id").Value == task.ID
                           select c).ToList();

            foreach (var tb in clearPrayer)
            {
                tb.Remove();
            }

            var prayers = (from c in doc.Descendants("task").Descendants("prayers")
                            where c.Parent.Element("id").Value == task.ID
                            select c).FirstOrDefault();

            for (int i = 0; i < task.Prayers.Count; i++)
            {
                var prayerEl = new XElement("prayer",
                    new XElement("imgpath_prayer", task.Prayers[i].ImgPath_Prayer),
                    new XElement("prayerSlot", task.Prayers[i].PrayerSlotIndex)
                );

                prayers.Add(prayerEl);
            }

            doc.Save(taskXMLFile);
        }

        public static void editTaskEquip(Task task)
        {
            XDocument doc = XDocument.Load(taskXMLFile);
            var items = (from c in doc.Descendants("task")
                         where c.Element("id").Value == task.ID
                         select c).ToList();

            foreach (var i in items)
            {
                setEquip(task.Weapon, i, "weaponID");
                setEquip(task.OffHand, i, "offhandID");
                setEquip(task.Helm, i, "helmID");
                setEquip(task.Torso, i, "chestID");
                setEquip(task.Legs, i, "legID");
                setEquip(task.Boots, i, "bootID");
                setEquip(task.Glove, i, "gloveID");
                setEquip(task.Amulet, i, "amuletID");
                setEquip(task.Ring, i, "ringID");
                setEquip(task.Back, i, "backID");
                setEquip(task.Ammo, i, "ammoID");
                setEquip(task.Pocket, i, "pocketID");
                setEquip(task.Aura, i, "auraID");
                setEquip(task.Sigil, i, "sigilID");
            }

            doc.Save(taskXMLFile);
        }

        public static void editTaskAbility(Task task)
        {
            XDocument doc = XDocument.Load(taskXMLFile);
            var abilitys = (from c in doc.Descendants("task").Descendants("ability_bar")
                            where c.Parent.Element("id").Value == task.ID
                            select c).ToList();

            foreach (var i in abilitys)
            {
                i.SetElementValue("style", task.Style);

                setAbility(task.Slot1, i, "slot0");
                setAbility(task.Slot2, i, "slot1");
                setAbility(task.Slot3, i, "slot2");
                setAbility(task.Slot4, i, "slot3");
                setAbility(task.Slot5, i, "slot4");
                setAbility(task.Slot6, i, "slot5");
                setAbility(task.Slot7, i, "slot6");
                setAbility(task.Slot8, i, "slot7");
                setAbility(task.Slot9, i, "slot8");
                setAbility(task.Slot10, i, "slot9");
                setAbility(task.Slot11, i, "slot10");
                setAbility(task.Slot12, i, "slot11");
                setAbility(task.Slot13, i, "slot12");
                setAbility(task.Slot14, i, "slot13");
            }

            doc.Save(taskXMLFile);
        }

        public static void DeleteTask(Task task)
        {
            XDocument doc = XDocument.Load(taskXMLFile);
            var items = (from c in doc.Descendants("task")
                         where c.Element("id").Value == task.ID
                         select c).ToList();

            foreach (var i in items)
            {
                i.Remove();
            }

            doc.Save(taskXMLFile);
        }

        

        /*
         * ITEM FILE ====================================================================================
         */

        public static void loadItems()
        {
            XDocument doc = XDocument.Load(itemXMLFile);
            var items = from c in doc.Descendants("item")
                        select new
                        {
                            ItemName = c.Element("name").Value,
                            ImgPath_Item = c.Element("imgpath").Value,
                            ID = c.Element("id").Value
                        };

            foreach (var i in items)
            {
                Item item = new Item(i.ItemName);
                item.ImgPath_Item = i.ImgPath_Item;
                item.ID = i.ID;

                ItemsHolder.Instance.ItemList.Add(item);
            }
        }

        public static void SaveNewItem(Item item)
        {
            var doc = XDocument.Load(itemXMLFile);

            var itemEl = new XElement("item",
                    new XElement("name", item.ItemName),
                    new XElement("imgpath", item.ImgPath_Item),
                    new XElement("id", item.ID)
            );

            doc.Element("items").Add(itemEl);

            doc.Save(itemXMLFile);
        }

        public static void EditItem(Item item)
        {
            XDocument doc = XDocument.Load(itemXMLFile);
            var items = (from c in doc.Descendants("item")
                        where c.Element("id").Value == item.ID
                        select c).ToList();

            foreach (var i in items)
            {
                i.SetElementValue("name", item.ItemName);
                i.SetElementValue("imgpath", item.ImgPath_Item);
            }

            doc.Save(itemXMLFile);
        }

        public static void DeleteItem(Item item)
        {
            XDocument doc = XDocument.Load(itemXMLFile);
            var items = (from c in doc.Descendants("item")
                         where c.Element("id").Value == item.ID
                         select c).ToList();

            foreach (var i in items)
            {
                i.Remove();
            }

            doc.Save(itemXMLFile);
        }

        /*
         * EQUIP FILE ====================================================================================
         */

        public static void loadEquips()
        {
            XDocument doc = XDocument.Load(equipXMLFile);
            var items = from c in doc.Descendants("equip")
                        select new
                        {
                            ItemName = c.Element("name").Value,
                            ImgPath_Item = c.Element("imgpath").Value,
                            EquipType = c.Element("type").Value,
                            ID = c.Element("id").Value
                        };

            foreach (var i in items)
            {
                EquipItem equip = new EquipItem(i.ItemName);
                equip.ImgPath_Item = i.ImgPath_Item;
                equip.EquipType = int.Parse(i.EquipType);
                equip.ID = i.ID;

                switch (i.EquipType)
                {
                    case "1":
                        EquipItemsHolder.Instance.WeaponList.Add(equip);
                        break;
                    case "2":
                        EquipItemsHolder.Instance.OffhandList.Add(equip);
                        break;
                    case "3":
                        EquipItemsHolder.Instance.HelmList.Add(equip);
                        break;
                    case "4":
                        EquipItemsHolder.Instance.ChestList.Add(equip);
                        break;
                    case "5":
                        EquipItemsHolder.Instance.LegList.Add(equip);
                        break;
                    case "6":
                        EquipItemsHolder.Instance.BootList.Add(equip);
                        break;
                    case "7":
                        EquipItemsHolder.Instance.GloveList.Add(equip);
                        break;
                    case "8":
                        EquipItemsHolder.Instance.AmuletLit.Add(equip);
                        break;
                    case "9":
                        EquipItemsHolder.Instance.RingList.Add(equip);
                        break;
                    case "10":
                        EquipItemsHolder.Instance.BackList.Add(equip);
                        break;
                    case "11":
                        EquipItemsHolder.Instance.AmmoList.Add(equip);
                        break;
                    case "12":
                        EquipItemsHolder.Instance.PocketList.Add(equip);
                        break;
                    case "13":
                        EquipItemsHolder.Instance.AuraList.Add(equip);
                        break;
                    case "14":
                        EquipItemsHolder.Instance.SigilList.Add(equip);
                        break;
                    default:
                        break;
                }
            }
        }

        public static void SaveNewEquip(EquipItem equip)
        {
            var doc = XDocument.Load(equipXMLFile);

            var equipEl = new XElement("equip",
                    new XElement("name", equip.ItemName),
                    new XElement("imgpath", equip.ImgPath_Item),
                    new XElement("type", equip.EquipType),
                    new XElement("id", equip.ID)
            );

            doc.Element("equips").Add(equipEl);

            doc.Save(equipXMLFile);
        }

         public static void EditEquip(EquipItem equip)
        {
            XDocument doc = XDocument.Load(equipXMLFile);
            var items = (from c in doc.Descendants("equip")
                        where c.Element("id").Value == equip.ID
                        select c).ToList();

            foreach (var i in items)
            {
                i.SetElementValue("name", equip.ItemName);
                i.SetElementValue("imgpath", equip.ImgPath_Item);
            }

            doc.Save(equipXMLFile);
        }

        public static void DeleteEquip(EquipItem equip)
        {
            XDocument doc = XDocument.Load(equipXMLFile);
            var items = (from c in doc.Descendants("equip")
                         where c.Element("id").Value == equip.ID
                         select c).ToList();

            foreach (var i in items)
            {
                i.Remove();
            }

            doc.Save(equipXMLFile);
        }

        /*
         * FAMILIAR FILE ====================================================================================
         */

        public static void loadFamiliars()
        {
            XDocument doc = XDocument.Load(familiarXMLFile);
            var items = from c in doc.Descendants("familiar")
                        select new
                        {
                            FamiliarName = c.Element("name").Value,
                            ImgPath_Familiar = c.Element("imgpath").Value,
                            InventorySize = c.Element("size").Value,
                            ID = c.Element("id").Value
                        };

            foreach (var i in items)
            {
                Familiar familiar = new Familiar(i.FamiliarName);
                familiar.ImgPath_Familiar = i.ImgPath_Familiar;
                familiar.InventorySize = int.Parse(i.InventorySize);
                familiar.ID = i.ID;

                FamiliarHolder.Instance.FamiliarList.Add(familiar);
            }
        }

        public static void SaveNewFamiliar(Familiar familiar)
        {
            var doc = XDocument.Load(familiarXMLFile);

            var familiarEl = new XElement("familiar",
                    new XElement("name", familiar.FamiliarName),
                    new XElement("imgpath", familiar.ImgPath_Familiar),
                    new XElement("size", familiar.InventorySize),
                    new XElement("id", familiar.ID)
            );

            doc.Element("familiars").Add(familiarEl);

            doc.Save(familiarXMLFile);
        }

        public static void EditFamiliar(Familiar familiar)
        {
            XDocument doc = XDocument.Load(familiarXMLFile);
            var items = (from c in doc.Descendants("familiar")
                         where c.Element("id").Value == familiar.ID
                         select c).ToList();

            foreach (var i in items)
            {
                i.SetElementValue("name", familiar.FamiliarName);
                i.SetElementValue("imgpath", familiar.ImgPath_Familiar);
                i.SetElementValue("size", familiar.InventorySize);
            }

            doc.Save(familiarXMLFile);
        }

        public static void DeleteFamiliar(Familiar familiar)
        {
            XDocument doc = XDocument.Load(familiarXMLFile);
            var items = (from c in doc.Descendants("familiar")
                         where c.Element("id").Value == familiar.ID
                         select c).ToList();

            foreach (var i in items)
            {
                i.Remove();
            }

            doc.Save(familiarXMLFile);
        }

        /*
         * EQUIP PRESET FILE ====================================================================================
         */
         
        public static void loadEquipPresets()
        {
            XDocument doc = XDocument.Load(equip_presetXMLFile);
            var items = from c in doc.Descendants("equip_preset")
                        select new
                        {
                            presetName = c.Element("name").Value,
                            ID = c.Element("id").Value,
                            weaponID = c.Element("weaponID").Value,
                            offhandID = c.Element("offhandID").Value,
                            helmID = c.Element("helmID").Value,
                            chestID = c.Element("chestID").Value,
                            legID = c.Element("legID").Value,
                            bootID = c.Element("bootID").Value,
                            gloveID = c.Element("gloveID").Value,
                            amuletID = c.Element("amuletID").Value,
                            ringID = c.Element("ringID").Value,
                            backID = c.Element("backID").Value,
                            ammoID = c.Element("ammoID").Value,
                            pocketID = c.Element("pocketID").Value,
                            auraID = c.Element("auraID").Value,
                            sigilID = c.Element("sigilID").Value,
                        };

            foreach (var i in items)
            {
                EquipPresets preset = new EquipPresets(i.presetName);
                preset.ID = i.ID;

                preset.Weapon = findEquipID(i.weaponID, EquipItemsHolder.Instance.WeaponList);
                preset.OffHand = findEquipID(i.offhandID, EquipItemsHolder.Instance.OffhandList);
                preset.Helm = findEquipID(i.helmID, EquipItemsHolder.Instance.HelmList);
                preset.Torso = findEquipID(i.chestID, EquipItemsHolder.Instance.ChestList);
                preset.Legs = findEquipID(i.legID, EquipItemsHolder.Instance.LegList);
                preset.Boots = findEquipID(i.bootID, EquipItemsHolder.Instance.BootList);
                preset.Glove = findEquipID(i.gloveID, EquipItemsHolder.Instance.GloveList);
                preset.Amulet = findEquipID(i.amuletID, EquipItemsHolder.Instance.AmuletLit);
                preset.Ring = findEquipID(i.ringID, EquipItemsHolder.Instance.RingList);
                preset.Back = findEquipID(i.backID, EquipItemsHolder.Instance.BackList);
                preset.Ammo = findEquipID(i.ammoID, EquipItemsHolder.Instance.AmmoList);
                preset.Pocket = findEquipID(i.pocketID, EquipItemsHolder.Instance.PocketList);
                preset.Aura = findEquipID(i.auraID, EquipItemsHolder.Instance.AuraList);
                preset.Sigil = findEquipID(i.sigilID, EquipItemsHolder.Instance.SigilList);

                EquipPresetsHolder.Instance.PresetList.Add(preset);
            }
        }
        
        public static void SaveNewEquipPreset(EquipPresets preset)
        {
            var doc = XDocument.Load(equip_presetXMLFile);

            var presetEl = new XElement("equip_preset",
                    new XElement("name", preset.Name),
                    new XElement("id", preset.ID)
            );

            presetEl.Add(saveEquip(preset.Weapon, "weaponID"));
            presetEl.Add(saveEquip(preset.OffHand, "offhandID"));
            presetEl.Add(saveEquip(preset.Helm, "helmID"));
            presetEl.Add(saveEquip(preset.Torso, "chestID"));
            presetEl.Add(saveEquip(preset.Legs, "legID"));
            presetEl.Add(saveEquip(preset.Boots, "bootID"));
            presetEl.Add(saveEquip(preset.Glove, "gloveID"));
            presetEl.Add(saveEquip(preset.Amulet, "amuletID"));
            presetEl.Add(saveEquip(preset.Ring, "ringID"));
            presetEl.Add(saveEquip(preset.Back, "backID"));
            presetEl.Add(saveEquip(preset.Ammo, "ammoID"));
            presetEl.Add(saveEquip(preset.Pocket, "pocketID"));
            presetEl.Add(saveEquip(preset.Aura, "auraID"));
            presetEl.Add(saveEquip(preset.Sigil, "sigilID"));

            doc.Element("equip_presets").Add(presetEl);

            doc.Save(equip_presetXMLFile);
        }

        public static void EditEquipPreset(EquipPresets preset)
        {
            XDocument doc = XDocument.Load(equip_presetXMLFile);
            var items = (from c in doc.Descendants("equip_preset")
                         where c.Element("id").Value == preset.ID
                         select c).ToList();

            foreach (var i in items)
            {
                i.SetElementValue("name", preset.Name);

                setEquip(preset.Weapon, i, "weaponID");
                setEquip(preset.OffHand, i, "offhandID");
                setEquip(preset.Helm, i, "helmID");
                setEquip(preset.Torso, i, "chestID");
                setEquip(preset.Legs, i, "legID");
                setEquip(preset.Boots, i, "bootID");
                setEquip(preset.Glove, i, "gloveID");
                setEquip(preset.Amulet, i, "amuletID");
                setEquip(preset.Ring, i, "ringID");
                setEquip(preset.Back, i, "backID");
                setEquip(preset.Ammo, i, "ammoID");
                setEquip(preset.Pocket, i, "pocketID");
                setEquip(preset.Aura, i, "auraID");
                setEquip(preset.Sigil, i, "sigilID");
            }

            doc.Save(equip_presetXMLFile);
        }

        public static void DeleteEquipPreset(EquipPresets preset)
        {
            XDocument doc = XDocument.Load(equip_presetXMLFile);
            var items = (from c in doc.Descendants("equip_preset")
                         where c.Element("id").Value == preset.ID
                         select c).ToList();

            foreach (var i in items)
            {
                i.Remove();
            }

            doc.Save(equip_presetXMLFile);
        }

        /*
         * ABILITY BAR PRESET FILE ====================================================================================
         */
         
        public static void loadAbilityPresets()
        {
            XDocument doc = XDocument.Load(ability_presetXMLFile);
            var items = from c in doc.Descendants("ability_preset")
                        select new
                        {
                            presetName = c.Element("name").Value,
                            style = c.Element("style").Value,
                            ID = c.Element("id").Value,
                            slot0 = c.Element("slot0").Value,
                            slot1 = c.Element("slot1").Value,
                            slot2 = c.Element("slot2").Value,
                            slot3 = c.Element("slot3").Value,
                            slot4 = c.Element("slot4").Value,
                            slot5 = c.Element("slot5").Value,
                            slot6 = c.Element("slot6").Value,
                            slot7 = c.Element("slot7").Value,
                            slot8 = c.Element("slot8").Value,
                            slot9 = c.Element("slot9").Value,
                            slot10 = c.Element("slot10").Value,
                            slot11 = c.Element("slot11").Value,
                            slot12 = c.Element("slot12").Value, 
                            slot13 = c.Element("slot13").Value,
                        };

            foreach (var i in items)
            {
                AbilityPreset preset = new AbilityPreset(i.presetName);
                preset.Style = int.Parse(i.style);
                preset.ID = i.ID;
                
                preset.Slot1 = newAbility (i.slot0);
                preset.Slot2 = newAbility(i.slot1);
                preset.Slot3 = newAbility(i.slot2);
                preset.Slot4 = newAbility(i.slot3);
                preset.Slot5 = newAbility(i.slot4);
                preset.Slot6 = newAbility(i.slot5);
                preset.Slot7 = newAbility(i.slot6);
                preset.Slot8 = newAbility(i.slot7);
                preset.Slot9 = newAbility(i.slot8);
                preset.Slot10 = newAbility(i.slot9);
                preset.Slot11 = newAbility(i.slot10);
                preset.Slot12 = newAbility(i.slot11);
                preset.Slot13 = newAbility(i.slot12);
                preset.Slot14 = newAbility(i.slot13);
                
                AbilityPresetHolder.Instance.PresetList.Add(preset);
            }
        }

        public static void SaveNewAbilityPreset(AbilityPreset preset)
        {
            var doc = XDocument.Load(ability_presetXMLFile);

            var presetEl = new XElement("ability_preset",
                    new XElement("name", preset.Name),
                    new XElement("style", preset.Style),
                    new XElement("id", preset.ID)
            );

            presetEl.Add(newAbility(preset.Slot1, "slot0"));
            presetEl.Add(newAbility(preset.Slot2, "slot1"));
            presetEl.Add(newAbility(preset.Slot3, "slot2"));
            presetEl.Add(newAbility(preset.Slot4, "slot3"));
            presetEl.Add(newAbility(preset.Slot5, "slot4"));
            presetEl.Add(newAbility(preset.Slot6, "slot5"));
            presetEl.Add(newAbility(preset.Slot7, "slot6"));
            presetEl.Add(newAbility(preset.Slot8, "slot7"));
            presetEl.Add(newAbility(preset.Slot9, "slot8"));
            presetEl.Add(newAbility(preset.Slot10, "slot9"));
            presetEl.Add(newAbility(preset.Slot11, "slot10"));
            presetEl.Add(newAbility(preset.Slot12, "slot11"));
            presetEl.Add(newAbility(preset.Slot13, "slot12"));
            presetEl.Add(newAbility(preset.Slot14, "slot13"));

            doc.Element("ability_presets").Add(presetEl);

            doc.Save(ability_presetXMLFile);
        }

        public static void EditAbilityPreset(AbilityPreset preset)
        {
            XDocument doc = XDocument.Load(ability_presetXMLFile);
            var items = (from c in doc.Descendants("ability_preset")
                         where c.Element("id").Value == preset.ID
                         select c).ToList();

            foreach (var i in items)
            {
                i.SetElementValue("name", preset.Name);
                i.SetElementValue("style", preset.Style);

                setAbility(preset.Slot1, i, "slot0");
                setAbility(preset.Slot2, i, "slot1");
                setAbility(preset.Slot3, i, "slot2");
                setAbility(preset.Slot4, i, "slot3");
                setAbility(preset.Slot5, i, "slot4");
                setAbility(preset.Slot6, i, "slot5");
                setAbility(preset.Slot7, i, "slot6");
                setAbility(preset.Slot8, i, "slot7");
                setAbility(preset.Slot9, i, "slot8");
                setAbility(preset.Slot10, i, "slot9");
                setAbility(preset.Slot11, i, "slot10");
                setAbility(preset.Slot12, i, "slot11");
                setAbility(preset.Slot13, i, "slot12");
                setAbility(preset.Slot14, i, "slot13");
            }

            doc.Save(ability_presetXMLFile);
        }

        public static void DeleteAbilityPreset(AbilityPreset preset)
        {
            XDocument doc = XDocument.Load(ability_presetXMLFile);
            var items = (from c in doc.Descendants("ability_preset")
                         where c.Element("id").Value == preset.ID
                         select c).ToList();

            foreach (var i in items)
            {
                i.Remove();
            }

            doc.Save(ability_presetXMLFile);
        }

        /*
         * FILE HELPER FUNCTIONS ====================================================================================
         */

        private static EquipItem findEquipID(string id, List<EquipItem> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ID.Equals(id))
                {
                    return list[i];
                }
            }

            return null;
        }

        private static string findItemID(string id)
        {
            for (int i = 0; i < ItemsHolder.Instance.ItemList.Count; i++)
            {
                if (ItemsHolder.Instance.ItemList[i].ID.Equals(id))
                {
                    return ItemsHolder.Instance.ItemList[i].ImgPath_Item;
                }
            }

            return null;
        }

        private static XElement saveEquip(EquipItem equip, string name)
        {
            if (equip != null)
            {
                return new XElement(name, equip.ID);
            }
            else
            {
                return new XElement(name, "");
            }
        }

        private static void setEquip(EquipItem equip, XElement i, string name)
        {
            if (equip != null)
            {
                i.SetElementValue(name, equip.ID);
            }
            else
            {
                i.SetElementValue(name, "");
            }
        }

        private static void setAbility(Ability ability, XElement i, string name)
        {
            if (ability != null)
            {
                i.SetElementValue(name, ability.ImgPath_Ability);
            }
            else
            {
                i.SetElementValue(name, "");
            }
        }

        private static Ability newAbility(string path)
        {
            if (path.Equals(""))
            {
                return null;
            }

            Ability ability = new Ability();
            ability.ImgPath_Ability = path;
            return ability;
        }

        private static XElement newAbility(Ability slot, string elem)
        {
            if (slot != null)
            {
                return new XElement(elem, slot.ImgPath_Ability);
            }
            else
            {
                return new XElement(elem, "");
            }
        }

        private static XElement setEquipPreset(EquipPresets preset)
        {
            if (preset != null)
            {
                return new XElement("equip_preset", preset.ID);
            }
            else
            {
                return new XElement("equip_preset", "");
            }
        }

        private static XElement setAbilityPreset(AbilityPreset preset)
        {
            if (preset != null)
            {
                return new XElement("ability_preset", preset.ID);
            }
            else
            {
                return new XElement("ability_preset", "");
            }
        }
    }
}
