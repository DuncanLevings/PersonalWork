using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RunescapeSlayerHelper
{
    public partial class NewTaskForm : Form
    {
        private string imageMonster;
        private string imageMap;
        private ComboBox cmbSearch;
        private PictureBox clickedBagPicture;
        private PictureBox clickedFamiliarPicture;
        private PictureBox clickedAbilityPicture;
        private PictureBox clickedPrayerPicture;
        private bool editing;
        private string oldName;
        private Task task;

        public NewTaskForm(ComboBox cmbSearch)
        {
            InitializeComponent();
            this.cmbSearch = cmbSearch;
            editing = false;
        }

        public NewTaskForm(ComboBox cmbSearch, Task task)
        {
            InitializeComponent();
            this.cmbSearch = cmbSearch;
            this.task = task;
            editing = true;
        }

        private void NewTaskForm_Load(object sender, EventArgs e)
        {
            //bag item search
            foreach (Item item in ItemsHolder.Instance.ItemList)
            {
                cmbItemSearch.Items.Add(item.ItemName);
                cmbItemSearchFamiliar.Items.Add(item.ItemName);
            }

            //equip item search
            updateSearches(EquipItemsHolder.Instance.WeaponList, cmbWeapon);
            updateSearches(EquipItemsHolder.Instance.OffhandList, cmbOffhand);
            updateSearches(EquipItemsHolder.Instance.HelmList, cmbHead);
            updateSearches(EquipItemsHolder.Instance.ChestList, cmbChest);
            updateSearches(EquipItemsHolder.Instance.LegList, cmbLeg);
            updateSearches(EquipItemsHolder.Instance.BootList, cmbBoot);
            updateSearches(EquipItemsHolder.Instance.GloveList, cmbGlove);
            updateSearches(EquipItemsHolder.Instance.AmuletLit, cmbAmulet);
            updateSearches(EquipItemsHolder.Instance.RingList, cmbRing);
            updateSearches(EquipItemsHolder.Instance.AmmoList, cmbAmmo);
            updateSearches(EquipItemsHolder.Instance.BackList, cmbCape);
            updateSearches(EquipItemsHolder.Instance.PocketList, cmbPocket);
            updateSearches(EquipItemsHolder.Instance.AuraList, cmbAura);
            updateSearches(EquipItemsHolder.Instance.SigilList, cmbSigil);

            //familiar search
            foreach (Familiar familiar in FamiliarHolder.Instance.FamiliarList)
            {
                cmbFamiliar.Items.Add(familiar.FamiliarName);
            }

            //equip preset search
            foreach (EquipPresets preset in EquipPresetsHolder.Instance.PresetList)
            {
                cmbEquipPreset.Items.Add(preset.Name);
            }

            //ability preset search
            foreach (AbilityPreset preset in AbilityPresetHolder.Instance.PresetList)
            {
                cmbStylePreset.Items.Add(preset.Name);
            }

            cmbStyle.Items.Add("Magic");
            cmbStyle.Items.Add("Ranged");
            cmbStyle.Items.Add("Melee");
            cmbStyle.SelectedIndex = 0;

            if (editing)
            {
                setupTaskEdit();
            }
            else
            {
                setupTaskNew();
            }
        }

        private void updateSearches(List<EquipItem> list, ComboBox search)
        {
            foreach (EquipItem item in list)
            {
                search.Items.Add(item.ItemName);
            }
        }

        private void setupTaskNew()
        {
            btnFinish.Enabled = false;
            btnEdit.Enabled = false;
            btnEdit.Visible = false;
        }

        private int setEquip(EquipItem equip, ComboBox search)
        {
            if (equip != null)
            {
                return search.Items.IndexOf(equip.ItemName);
            }

            return -1;
        }

        private void setAbilityImage(Ability slot, PictureBox img)
        {
            if (slot != null)
            {
                img.Load(slot.ImgPath_Ability);
                img.Tag = slot.ImgPath_Ability;
            }
        }

        private void setupTaskEdit()
        {
            btnFinish.Enabled = false;
            btnFinish.Visible = false;

            //name and monster image
            txtName.Text = task.Name;
            oldName = task.Name;

            imgMonster.Load(task.ImgPath_Monster);
            imageMonster = task.ImgPath_Monster;

            //youtube guide
            if (task.Youtube_guide != null)
            {
                txtGuide.Text = task.Youtube_guide;
            }

            //location
            if (task.ImgPath_Map != null)
            {
                imgMap.Load(task.ImgPath_Map);
            }

            //info
            if (task.AdditionalInfo != null)
            {
                txtInfo.Text = task.AdditionalInfo;
            }

            //bag items
            int pictureBoxIndex = 0;
            foreach (PictureBox img in flowLayoutPanel1.Controls)
            {
                for (int i = 0; i < task.BagItems.Count; i++)
                {
                    if (task.BagItems[i].BagSlotIndex == (pictureBoxIndex))
                    {
                        img.Load(task.BagItems[i].ImgPath_Item);
                        toolTip1.SetToolTip(img, task.BagItems[i].ItemName);
                        img.Tag = task.BagItems[i].ItemName + "=" + task.BagItems[i].ID;
                        break;
                    }
                }

                pictureBoxIndex++;
            }

            //familiar and familiar items
            if (task.Familiar != null)
            {
                cmbFamiliar.SelectedIndex = cmbFamiliar.Items.IndexOf(task.Familiar.FamiliarName);
            }
            
            int pictureBoxIndex1 = 0;
            foreach (PictureBox img in flowLayoutPanel5.Controls)
            {
                for (int i = 0; i < task.FamiliarBagItems.Count; i++)
                {
                    if (task.FamiliarBagItems[i].BagSlotIndex == (pictureBoxIndex1))
                    {
                        img.Load(task.FamiliarBagItems[i].ImgPath_Item);
                        toolTip1.SetToolTip(img, task.FamiliarBagItems[i].ItemName);
                        img.Tag = task.FamiliarBagItems[i].ItemName + "=" + task.FamiliarBagItems[i].ID;
                        break;
                    }
                }

                pictureBoxIndex1++;
            }

            //equipment
            cmbWeapon.SelectedIndex = setEquip(task.Weapon, cmbWeapon);
            cmbOffhand.SelectedIndex = setEquip(task.OffHand, cmbOffhand);
            cmbHead.SelectedIndex = setEquip(task.Helm, cmbHead);
            cmbChest.SelectedIndex = setEquip(task.Torso, cmbChest);
            cmbLeg.SelectedIndex = setEquip(task.Legs, cmbLeg);
            cmbBoot.SelectedIndex = setEquip(task.Boots, cmbBoot);
            cmbGlove.SelectedIndex = setEquip(task.Glove, cmbGlove);
            cmbAmulet.SelectedIndex = setEquip(task.Amulet, cmbAmulet);
            cmbRing.SelectedIndex = setEquip(task.Ring, cmbRing);
            cmbCape.SelectedIndex = setEquip(task.Back, cmbCape);
            cmbAmmo.SelectedIndex = setEquip(task.Ammo, cmbAmmo);
            cmbPocket.SelectedIndex = setEquip(task.Pocket, cmbPocket);
            cmbAura.SelectedIndex = setEquip(task.Aura, cmbAura);
            cmbSigil.SelectedIndex = setEquip(task.Sigil, cmbSigil);

            //ability bar
            setAbilityImage(task.Slot1, imgSlot1);
            setAbilityImage(task.Slot2, imgSlot2);
            setAbilityImage(task.Slot3, imgSlot3);
            setAbilityImage(task.Slot4, imgSlot4);
            setAbilityImage(task.Slot5, imgSlot5);
            setAbilityImage(task.Slot6, imgSlot6);
            setAbilityImage(task.Slot7, imgSlot7);
            setAbilityImage(task.Slot8, imgSlot8);
            setAbilityImage(task.Slot9, imgSlot9);
            setAbilityImage(task.Slot10, imgSlot10);
            setAbilityImage(task.Slot11, imgSlot11);
            setAbilityImage(task.Slot12, imgSlot12);
            setAbilityImage(task.Slot13, imgSlot13);
            setAbilityImage(task.Slot14, imgSlot14);

            cmbStyle.SelectedIndex = task.Style;

            //prayer
            int pictureBoxIndex3 = 0;
            foreach (PictureBox img in flowLayoutPanel6.Controls)
            {
                for (int i = 0; i < task.Prayers.Count; i++)
                {
                    if (task.Prayers[i].PrayerSlotIndex == (pictureBoxIndex3))
                    {
                        img.BackColor = Color.Aqua;
                        break;
                    }
                }

                pictureBoxIndex3++;
            }

            //equip preset
            if (task.EquipPreset != null)
            {
                cmbEquipPreset.SelectedIndex = EquipPresetsHolder.Instance.PresetList.IndexOf(task.EquipPreset);
            }

            //ability preset
            if (task.AbilityPreset != null)
            {
                cmbStylePreset.SelectedIndex = AbilityPresetHolder.Instance.PresetList.IndexOf(task.AbilityPreset);
                cmbStyle.SelectedIndex = AbilityPresetHolder.Instance.PresetList[cmbStylePreset.SelectedIndex].Style;
            }
        }

        //EDIT TASK ==============================================================================================================
        private void btnEdit_Click(object sender, EventArgs e)
        {
            //error checking
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Name cannot be empty!", "Error creating", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //name
            if (txtName.Text != null)
            {
                //name was not updated
                if (txtName.Text.Equals(oldName))
                {
                    task.Name = txtName.Text;
                }
                else
                {
                    //check if updated name conflicts
                    if (!cmbSearch.Items.Contains(txtName.Text))
                    {
                        task.Name = txtName.Text;

                        //remove old search name and add new
                        int index = cmbSearch.Items.IndexOf(oldName);
                        cmbSearch.Items.RemoveAt(index);
                        cmbSearch.Items.Insert(index, task.Name);
                    }
                    else
                    {
                        MessageBox.Show("Name already exists!", "Error creating", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
            }

            //monster image
            if (imageMonster != null)
            {
                task.ImgPath_Monster = imageMonster;
            }
            else
            {
                MessageBox.Show("Must have image!", "Error creating", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //youtube guide
            if (txtGuide.Text != null)
            {
                task.Youtube_guide = txtGuide.Text;
            }

            //location
            if (imageMap != null)
            {
                task.ImgPath_Map = imageMap;
            }

            //info
            if (txtInfo.Text != null)
            {
                task.AdditionalInfo = txtInfo.Text;
            }

            //bag items
            task.BagItems.Clear();

            setBagItems(task);

            //familiar and familiar items
            task.FamiliarBagItems.Clear();

            setFamiliar(task);

            //equipment
            setEquipment(task);

            //ability bar
            setAbilityBar(task);

            //prayer
            task.Prayers.Clear();
            setPrayers(task);

            //equip preset
            if (cmbEquipPreset.SelectedIndex > -1)
            {
                task.EquipPreset = EquipPresetsHolder.Instance.PresetList[cmbEquipPreset.SelectedIndex];
            }
            else
            {
                task.EquipPreset = null;
            }

            //ability preset
            if (cmbStylePreset.SelectedIndex > -1)
            {
                task.AbilityPreset = AbilityPresetHolder.Instance.PresetList[cmbStylePreset.SelectedIndex];
            }
            else
            {
                task.AbilityPreset = null;
            }

            FileHandler.EditTask(task);

            Close();
        }

        //NEW TASK ==============================================================================================================
        private void btnFinish_Click(object sender, EventArgs e)
        {
            //error checking
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Name cannot be empty!", "Error creating", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //general
            for (int i = 0; i < TasksHolder.Instance.TaskList.Count; i++)
            {
                if (txtName.Text.Equals(TasksHolder.Instance.TaskList[i].Name))
                {
                    MessageBox.Show("Name already exists!", "Error creating", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            Task task = new Task(txtName.Text);

            Guid guid = Guid.NewGuid();
            task.ID = guid.ToString();

            if (txtGuide.Text != null)
            {
                task.Youtube_guide = txtGuide.Text;
            }
            
            if (imageMap != null && imageMap.Equals(null) == false)
            {
                task.ImgPath_Map = imageMap;
            }

            if (txtInfo.Text != null)
            {
                task.AdditionalInfo = txtInfo.Text;
            }

            if (imageMonster != null && imageMonster.Equals(null) == false)
            {
                task.ImgPath_Monster = imageMonster;
            }
            else
            {
                MessageBox.Show("Must have image!", "Error creating", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //bag items
            setBagItems(task);

            //familiar and familiar items
            setFamiliar(task);

            //equipment
            setEquipment(task);

            //ability bar
            setAbilityBar(task);

            //prayer
            setPrayers(task);

            //equip preset
            if (cmbEquipPreset.SelectedIndex > -1)
            {
                task.EquipPreset = EquipPresetsHolder.Instance.PresetList[cmbEquipPreset.SelectedIndex];
            }

            //ability preset
            if (cmbStylePreset.SelectedIndex > -1)
            {
                task.AbilityPreset = AbilityPresetHolder.Instance.PresetList[cmbStylePreset.SelectedIndex];
            }

            TasksHolder.Instance.TaskList.Add(task);

            //save to file
            FileHandler.SaveNewTask(task);

            if (!cmbSearch.Items.Contains(task.Name))
            {
                cmbSearch.Items.Add(task.Name);
            }

            Close();
        }

        private void setBagItems(Task task)
        {
            int pictureBoxIndex = 0;
            foreach (PictureBox img in flowLayoutPanel1.Controls)
            {
                if (img.Tag != null)
                {
                    string[] data = img.Tag.ToString().Split('=');
                    Item item = new Item(data[0]);
                    item.ID = data[1];     
                    item.ImgPath_Item = img.ImageLocation;
                    item.BagSlotIndex = pictureBoxIndex;

                    task.BagItems.Add(item);
                }

                pictureBoxIndex++;
            }
        }

        private void setFamiliar(Task task)
        {
            if (cmbFamiliar.SelectedIndex > -1)
            {
                task.Familiar = FamiliarHolder.Instance.FamiliarList[cmbFamiliar.SelectedIndex];
            }

            int pictureBoxIndex = 0;
            foreach (PictureBox img in flowLayoutPanel5.Controls)
            {
                if (img.Tag != null)
                {
                    string[] data = img.Tag.ToString().Split('=');
                    Item item = new Item(data[0]);
                    item.ID = data[1];
                    item.ImgPath_Item = img.ImageLocation;
                    item.BagSlotIndex = pictureBoxIndex;
                    task.FamiliarBagItems.Add(item);
                }

                pictureBoxIndex++;
            }
        }

        private void setAbilityBar(Task task)
        {
            task.Slot1 = setAbility(imgSlot1, task.Slot1);
            task.Slot2 = setAbility(imgSlot2, task.Slot2);
            task.Slot3 = setAbility(imgSlot3, task.Slot3);
            task.Slot4 = setAbility(imgSlot4, task.Slot4);
            task.Slot5 = setAbility(imgSlot5, task.Slot5);
            task.Slot6 = setAbility(imgSlot6, task.Slot6);
            task.Slot7 = setAbility(imgSlot7, task.Slot7);
            task.Slot8 = setAbility(imgSlot8, task.Slot8);
            task.Slot9 = setAbility(imgSlot9, task.Slot9);
            task.Slot10 = setAbility(imgSlot10, task.Slot10);
            task.Slot11 = setAbility(imgSlot11, task.Slot11);
            task.Slot12 = setAbility(imgSlot12, task.Slot12);
            task.Slot13 = setAbility(imgSlot13, task.Slot13);
            task.Slot14 = setAbility(imgSlot14, task.Slot14);

            task.Style = cmbStyle.SelectedIndex;
        }

        private Ability setAbility(PictureBox img, Ability slot)
        {
            if (img.Tag != null)
            {
                //update old
                if (slot != null && slot.ImgPath_Ability.Equals(img.Tag))
                {
                    return slot;
                }
                else  //new
                {
                    return newAbility(img);
                }
            }

            return null;
        }

        private Ability newAbility(PictureBox img)
        {
            if (img.Tag != null)
            {
                Ability ability = new Ability();
                ability.ImgPath_Ability = img.Tag.ToString();

                return ability;
            }
            return null;
        }

        private void setPrayers(Task task)
        {
            int pictureBoxIndex2 = 0;
            foreach (PictureBox img in flowLayoutPanel6.Controls)
            {
                if (img.BackColor == Color.Aqua)
                {
                    Prayer prayer = new Prayer();
                    prayer.ImgPath_Prayer = img.Tag.ToString();
                    prayer.PrayerSlotIndex = pictureBoxIndex2;

                    task.Prayers.Add(prayer);
                }

                pictureBoxIndex2++;
            }
        }

        private EquipItem setEquip(int index, List<EquipItem> list)
        {
            if (index > -1)
            {
                return list[index];
            }

            return null;
        }

        private void setEquipment(Task task)
        {
            task.Weapon = setEquip(cmbWeapon.SelectedIndex, EquipItemsHolder.Instance.WeaponList);
            task.OffHand = setEquip(cmbOffhand.SelectedIndex, EquipItemsHolder.Instance.OffhandList);
            task.Helm = setEquip(cmbHead.SelectedIndex, EquipItemsHolder.Instance.HelmList);
            task.Torso = setEquip(cmbChest.SelectedIndex, EquipItemsHolder.Instance.ChestList);
            task.Legs = setEquip(cmbLeg.SelectedIndex, EquipItemsHolder.Instance.LegList);
            task.Boots = setEquip(cmbBoot.SelectedIndex, EquipItemsHolder.Instance.BootList);
            task.Glove = setEquip(cmbGlove.SelectedIndex, EquipItemsHolder.Instance.GloveList);
            task.Amulet = setEquip(cmbAmulet.SelectedIndex, EquipItemsHolder.Instance.AmuletLit);
            task.Ring = setEquip(cmbRing.SelectedIndex, EquipItemsHolder.Instance.RingList);
            task.Back = setEquip(cmbCape.SelectedIndex, EquipItemsHolder.Instance.BackList);
            task.Ammo = setEquip(cmbAmmo.SelectedIndex, EquipItemsHolder.Instance.AmmoList);
            task.Pocket = setEquip(cmbPocket.SelectedIndex, EquipItemsHolder.Instance.PocketList);
            task.Aura = setEquip(cmbAura.SelectedIndex, EquipItemsHolder.Instance.AuraList);
            task.Sigil = setEquip(cmbSigil.SelectedIndex, EquipItemsHolder.Instance.SigilList);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        //monster image
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                imageMonster = openFileDialog1.FileName;
                try
                {
                    imgMonster.Image = Image.FromFile(imageMonster);
                }
                catch (IOException)
                {
                }
            }
        }

        private void imgMap_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                imageMap = openFileDialog1.FileName;
                try
                {
                    imgMap.Image = Image.FromFile(imageMap);
                }
                catch (IOException)
                {
                }
            }
        }

        //bag picture click event
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            clickedBagPicture = sender as PictureBox;

            int index = cmbItemSearch.SelectedIndex; 
            if (index > -1)
            {
                clickedBagPicture.Load(ItemsHolder.Instance.ItemList[index].ImgPath_Item);
                toolTip1.SetToolTip(clickedBagPicture, ItemsHolder.Instance.ItemList[index].ItemName);
                clickedBagPicture.Tag = cmbItemSearch.SelectedText + "=" + ItemsHolder.Instance.ItemList[index].ID;
            }

            MouseEventArgs me = (MouseEventArgs)e;
            if (me.Button == MouseButtons.Right)
            {
                clickedBagPicture.Image = null;
                clickedBagPicture.Tag = null;
                toolTip1.SetToolTip(clickedBagPicture, null);
            }
        }

        //familiar picture click event
        private void pictureBox57_Click(object sender, EventArgs e)
        {
            clickedFamiliarPicture = sender as PictureBox;

            int index = cmbItemSearchFamiliar.SelectedIndex;
            if (index > -1)
            {
                clickedFamiliarPicture.Load(ItemsHolder.Instance.ItemList[index].ImgPath_Item);
                toolTip1.SetToolTip(clickedFamiliarPicture, ItemsHolder.Instance.ItemList[index].ItemName);
                clickedFamiliarPicture.Tag = cmbItemSearchFamiliar.SelectedText + "=" + ItemsHolder.Instance.ItemList[index].ID;
            }

            MouseEventArgs me = (MouseEventArgs)e;
            if (me.Button == MouseButtons.Right)
            {
                clickedFamiliarPicture.Image = null;
                clickedFamiliarPicture.Tag = null;
                toolTip1.SetToolTip(clickedFamiliarPicture, null);
            }
        }

        private void cmbItemSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cmbItemSearchFamiliar_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (editing)
            {
                return;
            }

            if (txtName.Text.Length > 0)
            {
                btnFinish.Enabled = true;
            }
            else
            {
                btnFinish.Enabled = false;
            }
        }

        //ability bar picture click event
        private void pictureBox43_Click(object sender, EventArgs e)
        {
            clickedAbilityPicture = sender as PictureBox;
            cmbStylePreset.SelectedIndex = -1;

            MouseEventArgs me = (MouseEventArgs)e;
            if (me.Button == MouseButtons.Right)
            {
                clickedAbilityPicture.Image = null;
                clickedAbilityPicture.Tag = null;
            }
            else
            {
                switch (cmbStyle.SelectedIndex)
                {
                    case 0:
                        MagicForm frm = new MagicForm();
                        frm.StartPosition = FormStartPosition.CenterScreen;
                        if (Application.OpenForms[frm.Name] == null)
                        {
                            frm.ShowDialog();
                        }
                        else
                        {
                            Application.OpenForms[frm.Name].Activate();
                        }

                        clickedAbilityPicture.Image = frm.Ability;
                        clickedAbilityPicture.Tag = frm.Path;
                        break;
                    case 1:
                        RangeForm frm2 = new RangeForm();
                        frm2.StartPosition = FormStartPosition.CenterScreen;
                        if (Application.OpenForms[frm2.Name] == null)
                        {
                            frm2.ShowDialog();
                        }
                        else
                        {
                            Application.OpenForms[frm2.Name].Activate();
                        }

                        clickedAbilityPicture.Image = frm2.Ability;
                        clickedAbilityPicture.Tag = frm2.Path;
                        break;
                    case 2:
                        MeleeForm frm3 = new MeleeForm();
                        frm3.StartPosition = FormStartPosition.CenterScreen;
                        if (Application.OpenForms[frm3.Name] == null)
                        {
                            frm3.ShowDialog();
                        }
                        else
                        {
                            Application.OpenForms[frm3.Name].Activate();
                        }

                        clickedAbilityPicture.Image = frm3.Ability;
                        clickedAbilityPicture.Tag = frm3.Path;
                        break;
                    default:
                        break;
                }
            }
        }

        //familiar
        private void cmbFamiliar_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cmbFamiliar.SelectedIndex;
            int size = FamiliarHolder.Instance.FamiliarList[index].InventorySize;

            //reset item search
            cmbItemSearchFamiliar.Visible = false;

            int familiarIndex = 0;
            foreach (PictureBox img in flowLayoutPanel5.Controls)
            {
                //reset first...
                img.Visible = false;

                if (familiarIndex < size)
                {
                    cmbItemSearchFamiliar.Visible = true;
                    img.Visible = true;
                }

                familiarIndex++;
            }
        }

        //prayer
        private void pictureBox89_Click(object sender, EventArgs e)
        {
            clickedPrayerPicture = sender as PictureBox;

            MouseEventArgs me = (MouseEventArgs)e;
            if (me.Button == MouseButtons.Right)
            {
                clickedPrayerPicture.BackColor = SystemColors.ControlDark;
            }
            else
            {
                clickedPrayerPicture.BackColor = Color.Aqua;
            }
        }

        public int updatedIndex()
        {
            return cmbSearch.Items.IndexOf(txtName.Text);
        }

        private void cmbStylePreset_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cmbStylePreset.SelectedIndex;

            if (index < 0)
            {
                return;
            }

            setAbilityImage(AbilityPresetHolder.Instance.PresetList[index].Slot1, AbilityPresetHolder.Instance.PresetList[index], imgSlot1);
            setAbilityImage(AbilityPresetHolder.Instance.PresetList[index].Slot2, AbilityPresetHolder.Instance.PresetList[index], imgSlot2);
            setAbilityImage(AbilityPresetHolder.Instance.PresetList[index].Slot3, AbilityPresetHolder.Instance.PresetList[index], imgSlot3);
            setAbilityImage(AbilityPresetHolder.Instance.PresetList[index].Slot4, AbilityPresetHolder.Instance.PresetList[index], imgSlot4);
            setAbilityImage(AbilityPresetHolder.Instance.PresetList[index].Slot5, AbilityPresetHolder.Instance.PresetList[index], imgSlot5);
            setAbilityImage(AbilityPresetHolder.Instance.PresetList[index].Slot6, AbilityPresetHolder.Instance.PresetList[index], imgSlot6);
            setAbilityImage(AbilityPresetHolder.Instance.PresetList[index].Slot7, AbilityPresetHolder.Instance.PresetList[index], imgSlot7);
            setAbilityImage(AbilityPresetHolder.Instance.PresetList[index].Slot8, AbilityPresetHolder.Instance.PresetList[index], imgSlot8);
            setAbilityImage(AbilityPresetHolder.Instance.PresetList[index].Slot9, AbilityPresetHolder.Instance.PresetList[index], imgSlot9);
            setAbilityImage(AbilityPresetHolder.Instance.PresetList[index].Slot10, AbilityPresetHolder.Instance.PresetList[index], imgSlot10);
            setAbilityImage(AbilityPresetHolder.Instance.PresetList[index].Slot11, AbilityPresetHolder.Instance.PresetList[index], imgSlot11);
            setAbilityImage(AbilityPresetHolder.Instance.PresetList[index].Slot12, AbilityPresetHolder.Instance.PresetList[index], imgSlot12);
            setAbilityImage(AbilityPresetHolder.Instance.PresetList[index].Slot13, AbilityPresetHolder.Instance.PresetList[index], imgSlot13);
            setAbilityImage(AbilityPresetHolder.Instance.PresetList[index].Slot14, AbilityPresetHolder.Instance.PresetList[index], imgSlot14);
        }

        private void setAbilityImage(Ability slot, AbilityPreset preset, PictureBox img)
        {
            if (preset != null)
            {
                cmbStyle.SelectedIndex = preset.Style;

                if (slot != null)
                {
                    img.Load(slot.ImgPath_Ability);
                    img.Tag = slot.ImgPath_Ability;
                }
                else
                {
                    img.Image = null;
                    img.Tag = null;
                }
            }
        }

        private int setEquipIndex(List<EquipItem> list, EquipItem presetEquip)
        {
            if (presetEquip != null)
            {
                return list.IndexOf(presetEquip);
            }
            return -1;
        }

        private void cmbEquipPreset_SelectionChangeCommitted(object sender, EventArgs e)
        {
            unBindEquipSelectionChanged();

            int index = cmbEquipPreset.SelectedIndex;

            if (index < 0)
            {
                return;
            }

            cmbWeapon.SelectedIndex = setEquipIndex(EquipItemsHolder.Instance.WeaponList, EquipPresetsHolder.Instance.PresetList[index].Weapon);
            cmbOffhand.SelectedIndex = setEquipIndex(EquipItemsHolder.Instance.OffhandList, EquipPresetsHolder.Instance.PresetList[index].OffHand);
            cmbHead.SelectedIndex = setEquipIndex(EquipItemsHolder.Instance.HelmList, EquipPresetsHolder.Instance.PresetList[index].Helm);
            cmbChest.SelectedIndex = setEquipIndex(EquipItemsHolder.Instance.ChestList, EquipPresetsHolder.Instance.PresetList[index].Torso);
            cmbLeg.SelectedIndex = setEquipIndex(EquipItemsHolder.Instance.LegList, EquipPresetsHolder.Instance.PresetList[index].Legs);
            cmbBoot.SelectedIndex = setEquipIndex(EquipItemsHolder.Instance.BootList, EquipPresetsHolder.Instance.PresetList[index].Boots);
            cmbGlove.SelectedIndex = setEquipIndex(EquipItemsHolder.Instance.GloveList, EquipPresetsHolder.Instance.PresetList[index].Glove);
            cmbAmulet.SelectedIndex = setEquipIndex(EquipItemsHolder.Instance.AmuletLit, EquipPresetsHolder.Instance.PresetList[index].Amulet);
            cmbRing.SelectedIndex = setEquipIndex(EquipItemsHolder.Instance.RingList, EquipPresetsHolder.Instance.PresetList[index].Ring);
            cmbCape.SelectedIndex = setEquipIndex(EquipItemsHolder.Instance.BackList, EquipPresetsHolder.Instance.PresetList[index].Back);
            cmbAmmo.SelectedIndex = setEquipIndex(EquipItemsHolder.Instance.AmmoList, EquipPresetsHolder.Instance.PresetList[index].Ammo);
            cmbPocket.SelectedIndex = setEquipIndex(EquipItemsHolder.Instance.PocketList, EquipPresetsHolder.Instance.PresetList[index].Pocket);
            cmbAura.SelectedIndex = setEquipIndex(EquipItemsHolder.Instance.AuraList, EquipPresetsHolder.Instance.PresetList[index].Aura);
            cmbSigil.SelectedIndex = setEquipIndex(EquipItemsHolder.Instance.SigilList, EquipPresetsHolder.Instance.PresetList[index].Sigil);
            
            reBindEquipSelectionChanged();
        }

        private void unBindEquipSelectionChanged()
        {
            cmbWeapon.SelectedIndexChanged -= new EventHandler(cmbWeapon_SelectedIndexChanged);
            cmbOffhand.SelectedIndexChanged -= new EventHandler(cmbOffhand_SelectedIndexChanged);
            cmbHead.SelectedIndexChanged -= new EventHandler(cmbHead_SelectedIndexChanged);
            cmbChest.SelectedIndexChanged -= new EventHandler(cmbChest_SelectedIndexChanged);
            cmbLeg.SelectedIndexChanged -= new EventHandler(cmbLeg_SelectedIndexChanged);
            cmbBoot.SelectedIndexChanged -= new EventHandler(cmbBoot_SelectedIndexChanged);
            cmbGlove.SelectedIndexChanged -= new EventHandler(cmbGlove_SelectedIndexChanged);
            cmbAmulet.SelectedIndexChanged -= new EventHandler(cmbAmulet_SelectedIndexChanged);
            cmbRing.SelectedIndexChanged -= new EventHandler(cmbRing_SelectedIndexChanged);
            cmbCape.SelectedIndexChanged -= new EventHandler(cmbCape_SelectedIndexChanged);
            cmbPocket.SelectedIndexChanged -= new EventHandler(cmbPocket_SelectedIndexChanged);
            cmbAmmo.SelectedIndexChanged -= new EventHandler(cmbAmmo_SelectedIndexChanged);
            cmbAura.SelectedIndexChanged -= new EventHandler(cmbAura_SelectedIndexChanged);
            cmbSigil.SelectedIndexChanged -= new EventHandler(cmbSigil_SelectedIndexChanged);
        }

        private void reBindEquipSelectionChanged()
        {
            cmbWeapon.SelectedIndexChanged += new EventHandler(cmbWeapon_SelectedIndexChanged);
            cmbOffhand.SelectedIndexChanged += new EventHandler(cmbOffhand_SelectedIndexChanged);
            cmbHead.SelectedIndexChanged += new EventHandler(cmbHead_SelectedIndexChanged);
            cmbChest.SelectedIndexChanged += new EventHandler(cmbChest_SelectedIndexChanged);
            cmbLeg.SelectedIndexChanged += new EventHandler(cmbLeg_SelectedIndexChanged);
            cmbBoot.SelectedIndexChanged += new EventHandler(cmbBoot_SelectedIndexChanged);
            cmbGlove.SelectedIndexChanged += new EventHandler(cmbGlove_SelectedIndexChanged);
            cmbAmulet.SelectedIndexChanged += new EventHandler(cmbAmulet_SelectedIndexChanged);
            cmbRing.SelectedIndexChanged += new EventHandler(cmbRing_SelectedIndexChanged);
            cmbCape.SelectedIndexChanged += new EventHandler(cmbCape_SelectedIndexChanged);
            cmbPocket.SelectedIndexChanged += new EventHandler(cmbPocket_SelectedIndexChanged);
            cmbAmmo.SelectedIndexChanged += new EventHandler(cmbAmmo_SelectedIndexChanged);
            cmbAura.SelectedIndexChanged += new EventHandler(cmbAura_SelectedIndexChanged);
            cmbSigil.SelectedIndexChanged += new EventHandler(cmbSigil_SelectedIndexChanged);
        }

        private void cmbWeapon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEquipPreset.SelectedIndex > -1)
            {
                if (EquipPresetsHolder.Instance.PresetList[cmbEquipPreset.SelectedIndex].Weapon != null)
                {
                    if (!EquipItemsHolder.Instance.WeaponList[cmbWeapon.SelectedIndex].ID.Equals
                    (EquipPresetsHolder.Instance.PresetList[cmbEquipPreset.SelectedIndex].Weapon.ID))
                    {
                        cmbEquipPreset.SelectedIndex = -1;
                    }
                }
                else
                {
                    cmbEquipPreset.SelectedIndex = -1;
                }
            }
        }

        private void cmbOffhand_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEquipPreset.SelectedIndex > -1)
            {
                if (EquipPresetsHolder.Instance.PresetList[cmbEquipPreset.SelectedIndex].OffHand != null)
                {
                    if (!EquipItemsHolder.Instance.OffhandList[cmbOffhand.SelectedIndex].ID.Equals
                    (EquipPresetsHolder.Instance.PresetList[cmbEquipPreset.SelectedIndex].OffHand.ID))
                    {
                        cmbEquipPreset.SelectedIndex = -1;
                    }
                }
                else
                {
                    cmbEquipPreset.SelectedIndex = -1;
                }
            }
        }

        private void cmbHead_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEquipPreset.SelectedIndex > -1)
            {
                if (EquipPresetsHolder.Instance.PresetList[cmbEquipPreset.SelectedIndex].Helm != null)
                {
                    if (!EquipItemsHolder.Instance.HelmList[cmbHead.SelectedIndex].ID.Equals
                    (EquipPresetsHolder.Instance.PresetList[cmbEquipPreset.SelectedIndex].Helm.ID))
                    {
                        cmbEquipPreset.SelectedIndex = -1;
                    }
                }
                else
                {
                    cmbEquipPreset.SelectedIndex = -1;
                }
            }
        }

        private void cmbChest_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEquipPreset.SelectedIndex > -1)
            {
                if (EquipPresetsHolder.Instance.PresetList[cmbEquipPreset.SelectedIndex].Torso != null)
                {
                    if (!EquipItemsHolder.Instance.ChestList[cmbChest.SelectedIndex].ID.Equals
                    (EquipPresetsHolder.Instance.PresetList[cmbEquipPreset.SelectedIndex].Torso.ID))
                    {
                        cmbEquipPreset.SelectedIndex = -1;
                    }
                }
                else
                {
                    cmbEquipPreset.SelectedIndex = -1;
                }
            }
        }

        private void cmbLeg_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEquipPreset.SelectedIndex > -1)
            {
                if (EquipPresetsHolder.Instance.PresetList[cmbEquipPreset.SelectedIndex].Legs != null)
                {
                    if (!EquipItemsHolder.Instance.LegList[cmbLeg.SelectedIndex].ID.Equals
                    (EquipPresetsHolder.Instance.PresetList[cmbEquipPreset.SelectedIndex].Legs.ID))
                    {
                        cmbEquipPreset.SelectedIndex = -1;
                    }
                }
                else
                {
                    cmbEquipPreset.SelectedIndex = -1;
                }
            }
        }

        private void cmbBoot_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEquipPreset.SelectedIndex > -1)
            {
                if (EquipPresetsHolder.Instance.PresetList[cmbEquipPreset.SelectedIndex].Boots != null)
                {
                    if (!EquipItemsHolder.Instance.BootList[cmbBoot.SelectedIndex].ID.Equals
                    (EquipPresetsHolder.Instance.PresetList[cmbEquipPreset.SelectedIndex].Boots.ID))
                    {
                        cmbEquipPreset.SelectedIndex = -1;
                    }
                }
                else
                {
                    cmbEquipPreset.SelectedIndex = -1;
                }
            }
        }

        private void cmbGlove_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEquipPreset.SelectedIndex > -1)
            {
                if (EquipPresetsHolder.Instance.PresetList[cmbEquipPreset.SelectedIndex].Glove != null)
                {
                    if (!EquipItemsHolder.Instance.GloveList[cmbGlove.SelectedIndex].ID.Equals
                    (EquipPresetsHolder.Instance.PresetList[cmbEquipPreset.SelectedIndex].Glove.ID))
                    {
                        cmbEquipPreset.SelectedIndex = -1;
                    }
                }
                else
                {
                    cmbEquipPreset.SelectedIndex = -1;
                }
            }
        }

        private void cmbAmulet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEquipPreset.SelectedIndex > -1)
            {
                if (EquipPresetsHolder.Instance.PresetList[cmbEquipPreset.SelectedIndex].Amulet != null)
                {
                    if (!EquipItemsHolder.Instance.AmuletLit[cmbAmulet.SelectedIndex].ID.Equals
                    (EquipPresetsHolder.Instance.PresetList[cmbEquipPreset.SelectedIndex].Amulet.ID))
                    {
                        cmbEquipPreset.SelectedIndex = -1;
                    }
                }
                else
                {
                    cmbEquipPreset.SelectedIndex = -1;
                }
            }
        }

        private void cmbRing_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEquipPreset.SelectedIndex > -1)
            {
                if (EquipPresetsHolder.Instance.PresetList[cmbEquipPreset.SelectedIndex].Ring != null)
                {
                    if (!EquipItemsHolder.Instance.RingList[cmbRing.SelectedIndex].ID.Equals
                    (EquipPresetsHolder.Instance.PresetList[cmbEquipPreset.SelectedIndex].Ring.ID))
                    {
                        cmbEquipPreset.SelectedIndex = -1;
                    }
                }
                else
                {
                    cmbEquipPreset.SelectedIndex = -1;
                }
            }
        }

        private void cmbCape_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEquipPreset.SelectedIndex > -1)
            {
                if (EquipPresetsHolder.Instance.PresetList[cmbEquipPreset.SelectedIndex].Back != null)
                {
                    if (!EquipItemsHolder.Instance.BackList[cmbCape.SelectedIndex].ID.Equals
                    (EquipPresetsHolder.Instance.PresetList[cmbEquipPreset.SelectedIndex].Back.ID))
                    {
                        cmbEquipPreset.SelectedIndex = -1;
                    }
                }
                else
                {
                    cmbEquipPreset.SelectedIndex = -1;
                }
            }
        }

        private void cmbPocket_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEquipPreset.SelectedIndex > -1)
            {
                if (EquipPresetsHolder.Instance.PresetList[cmbEquipPreset.SelectedIndex].Pocket != null)
                {
                    if (!EquipItemsHolder.Instance.PocketList[cmbPocket.SelectedIndex].ID.Equals
                    (EquipPresetsHolder.Instance.PresetList[cmbEquipPreset.SelectedIndex].Pocket.ID))
                    {
                        cmbEquipPreset.SelectedIndex = -1;
                    }
                }
                else
                {
                    cmbEquipPreset.SelectedIndex = -1;
                }
            }
        }

        private void cmbAmmo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEquipPreset.SelectedIndex > -1)
            {
                if (EquipPresetsHolder.Instance.PresetList[cmbEquipPreset.SelectedIndex].Ammo != null)
                {
                    if (!EquipItemsHolder.Instance.AmmoList[cmbAmmo.SelectedIndex].ID.Equals
                    (EquipPresetsHolder.Instance.PresetList[cmbEquipPreset.SelectedIndex].Ammo.ID))
                    {
                        cmbEquipPreset.SelectedIndex = -1;
                    }
                }
                else
                {
                    cmbEquipPreset.SelectedIndex = -1;
                }
            }
        }

        private void cmbAura_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEquipPreset.SelectedIndex > -1)
            {
                if (EquipPresetsHolder.Instance.PresetList[cmbEquipPreset.SelectedIndex].Aura != null)
                {
                    if (!EquipItemsHolder.Instance.AuraList[cmbAura.SelectedIndex].ID.Equals
                    (EquipPresetsHolder.Instance.PresetList[cmbEquipPreset.SelectedIndex].Aura.ID))
                    {
                        cmbEquipPreset.SelectedIndex = -1;
                    }
                }
                else
                {
                    cmbEquipPreset.SelectedIndex = -1;
                }
            }
        }

        private void cmbSigil_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEquipPreset.SelectedIndex > -1)
            {
                if (EquipPresetsHolder.Instance.PresetList[cmbEquipPreset.SelectedIndex].Sigil != null)
                {
                    if (!EquipItemsHolder.Instance.SigilList[cmbSigil.SelectedIndex].ID.Equals
                    (EquipPresetsHolder.Instance.PresetList[cmbEquipPreset.SelectedIndex].Sigil.ID))
                    {
                        cmbEquipPreset.SelectedIndex = -1;
                    }
                }
                else
                {
                    cmbEquipPreset.SelectedIndex = -1;
                }
            }
        }
    }
}
