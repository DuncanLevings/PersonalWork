using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RunescapeSlayerHelper
{
    public partial class EditEquipPreset : Form
    {
        private string oldName;
        private ListBox lstLibrary;
        private EquipPresets preset;

        public EditEquipPreset(ListBox list)
        {
            InitializeComponent();

            lstLibrary = list;

            setupSearches();

            btnDone.Enabled = true;
            btnDone.Visible = true;
        }

        //edit
        public EditEquipPreset(ListBox list, EquipPresets preset)
        {
            InitializeComponent();

            lstLibrary = list;
            this.preset = preset;

            setupSearches();
            setupEdit();
        }

        private void setupSearches()
        {
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
        }

        private void updateSearches(List<EquipItem> list, ComboBox search)
        {
            foreach (EquipItem item in list)
            {
                search.Items.Add(item.ItemName);
            }
        }

        private int setIndex(EquipItem equip, List<EquipItem> list, EquipItem presetEquip)
        {
            if (equip != null)
            {
                return list.IndexOf(presetEquip);
            }
            return -1;
        }

        private EquipItem setEquip(int index, List<EquipItem> list)
        {
            if (index > -1)
            {
                return list[index];
            }

            return null;
        }

        private void setupEdit()
        {
            btnUpdate.Enabled = true;
            btnUpdate.Visible = true;

            txtName.Text = preset.Name;
            oldName = preset.Name;

            int index = lstLibrary.SelectedIndex;

            cmbWeapon.SelectedIndex = setIndex(preset.Weapon, EquipItemsHolder.Instance.WeaponList, EquipPresetsHolder.Instance.PresetList[index].Weapon);
            cmbOffhand.SelectedIndex = setIndex(preset.OffHand, EquipItemsHolder.Instance.OffhandList, EquipPresetsHolder.Instance.PresetList[index].OffHand);
            cmbHead.SelectedIndex = setIndex(preset.Helm, EquipItemsHolder.Instance.HelmList, EquipPresetsHolder.Instance.PresetList[index].Helm);
            cmbChest.SelectedIndex = setIndex(preset.Torso, EquipItemsHolder.Instance.ChestList, EquipPresetsHolder.Instance.PresetList[index].Torso);
            cmbLeg.SelectedIndex = setIndex(preset.Legs, EquipItemsHolder.Instance.LegList, EquipPresetsHolder.Instance.PresetList[index].Legs);
            cmbBoot.SelectedIndex = setIndex(preset.Boots, EquipItemsHolder.Instance.BootList, EquipPresetsHolder.Instance.PresetList[index].Boots);
            cmbGlove.SelectedIndex = setIndex(preset.Glove, EquipItemsHolder.Instance.GloveList, EquipPresetsHolder.Instance.PresetList[index].Glove);
            cmbAmulet.SelectedIndex = setIndex(preset.Amulet, EquipItemsHolder.Instance.AmuletLit, EquipPresetsHolder.Instance.PresetList[index].Amulet);
            cmbRing.SelectedIndex = setIndex(preset.Ring, EquipItemsHolder.Instance.RingList, EquipPresetsHolder.Instance.PresetList[index].Ring);
            cmbCape.SelectedIndex = setIndex(preset.Back, EquipItemsHolder.Instance.BackList, EquipPresetsHolder.Instance.PresetList[index].Back);
            cmbAmmo.SelectedIndex = setIndex(preset.Ammo, EquipItemsHolder.Instance.AmmoList, EquipPresetsHolder.Instance.PresetList[index].Ammo);
            cmbPocket.SelectedIndex = setIndex(preset.Pocket, EquipItemsHolder.Instance.PocketList, EquipPresetsHolder.Instance.PresetList[index].Pocket);
            cmbAura.SelectedIndex = setIndex(preset.Aura, EquipItemsHolder.Instance.AuraList, EquipPresetsHolder.Instance.PresetList[index].Aura);
            cmbSigil.SelectedIndex = setIndex(preset.Sigil, EquipItemsHolder.Instance.SigilList, EquipPresetsHolder.Instance.PresetList[index].Sigil);
        }

        //for creating new
        private void btnDone_Click(object sender, EventArgs e)
        {
            //error checking
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Name cannot be empty!", "Error creating", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            for (int i = 0; i < EquipPresetsHolder.Instance.PresetList.Count; i++)
            {
                if (txtName.Text.Equals(EquipPresetsHolder.Instance.PresetList[i].Name))
                {
                    MessageBox.Show("Preset name already exists!", "Error creating", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            EquipPresets preset = new EquipPresets(txtName.Text);

            preset.Weapon = setEquip(cmbWeapon.SelectedIndex, EquipItemsHolder.Instance.WeaponList);
            preset.OffHand = setEquip(cmbOffhand.SelectedIndex, EquipItemsHolder.Instance.OffhandList);
            preset.Helm = setEquip(cmbHead.SelectedIndex, EquipItemsHolder.Instance.HelmList);
            preset.Torso = setEquip(cmbChest.SelectedIndex, EquipItemsHolder.Instance.ChestList);
            preset.Legs = setEquip(cmbLeg.SelectedIndex, EquipItemsHolder.Instance.LegList);
            preset.Boots = setEquip(cmbBoot.SelectedIndex, EquipItemsHolder.Instance.BootList);
            preset.Glove = setEquip(cmbGlove.SelectedIndex, EquipItemsHolder.Instance.GloveList);
            preset.Amulet = setEquip(cmbAmulet.SelectedIndex, EquipItemsHolder.Instance.AmuletLit);
            preset.Ring = setEquip(cmbRing.SelectedIndex, EquipItemsHolder.Instance.RingList);
            preset.Back = setEquip(cmbCape.SelectedIndex, EquipItemsHolder.Instance.BackList);
            preset.Ammo = setEquip(cmbAmmo.SelectedIndex, EquipItemsHolder.Instance.AmmoList);
            preset.Pocket = setEquip(cmbPocket.SelectedIndex, EquipItemsHolder.Instance.PocketList);
            preset.Aura = setEquip(cmbAura.SelectedIndex, EquipItemsHolder.Instance.AuraList);
            preset.Sigil = setEquip(cmbSigil.SelectedIndex, EquipItemsHolder.Instance.SigilList);

            Guid guid = Guid.NewGuid();
            preset.ID = guid.ToString();

            EquipPresetsHolder.Instance.PresetList.Add(preset);

            //save to file
            FileHandler.SaveNewEquipPreset(preset);

            lstLibrary.Items.Add(txtName.Text);

            Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //error checking
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Name cannot be empty!", "Error creating", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string presetName = checkName();
            if (presetName != null)
            {
                preset.Name = presetName;
            }

            preset.Weapon = setEquip(cmbWeapon.SelectedIndex, EquipItemsHolder.Instance.WeaponList);
            preset.OffHand = setEquip(cmbOffhand.SelectedIndex, EquipItemsHolder.Instance.OffhandList);
            preset.Helm = setEquip(cmbHead.SelectedIndex, EquipItemsHolder.Instance.HelmList);
            preset.Torso = setEquip(cmbChest.SelectedIndex, EquipItemsHolder.Instance.ChestList);
            preset.Legs = setEquip(cmbLeg.SelectedIndex, EquipItemsHolder.Instance.LegList);
            preset.Boots = setEquip(cmbBoot.SelectedIndex, EquipItemsHolder.Instance.BootList);
            preset.Glove = setEquip(cmbGlove.SelectedIndex, EquipItemsHolder.Instance.GloveList);
            preset.Amulet = setEquip(cmbAmulet.SelectedIndex, EquipItemsHolder.Instance.AmuletLit);
            preset.Ring = setEquip(cmbRing.SelectedIndex, EquipItemsHolder.Instance.RingList);
            preset.Back = setEquip(cmbCape.SelectedIndex, EquipItemsHolder.Instance.BackList);
            preset.Ammo = setEquip(cmbAmmo.SelectedIndex, EquipItemsHolder.Instance.AmmoList);
            preset.Pocket = setEquip(cmbPocket.SelectedIndex, EquipItemsHolder.Instance.PocketList);
            preset.Aura = setEquip(cmbAura.SelectedIndex, EquipItemsHolder.Instance.AuraList);
            preset.Sigil = setEquip(cmbSigil.SelectedIndex, EquipItemsHolder.Instance.SigilList);

            updateAllTasks();

            FileHandler.EditEquipPreset(preset);

            Close();
        }

        private string checkName()
        {
            //name was not updated
            if (txtName.Text.Equals(oldName))
            {
                return txtName.Text;
            }
            else
            {
                //check if updated name conflicts
                if (!lstLibrary.Items.Contains(txtName.Text))
                {
                    //remove old search name and add new
                    int index = lstLibrary.Items.IndexOf(oldName);
                    lstLibrary.Items.RemoveAt(index);
                    lstLibrary.Items.Insert(index, txtName.Text);

                    return txtName.Text;
                }
                else
                {
                    MessageBox.Show("Name already exists!", "Error creating", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return null;
                }
            }
        }

        //update all tasks using preset items
        private void updateAllTasks()
        {
            for (int i = 0; i < TasksHolder.Instance.TaskList.Count; i++)
            {
                if (TasksHolder.Instance.TaskList[i].EquipPreset == preset)
                {
                    TasksHolder.Instance.TaskList[i].Weapon = updateEquipInTask(TasksHolder.Instance.TaskList[i].Weapon, preset.Weapon);
                    TasksHolder.Instance.TaskList[i].OffHand = updateEquipInTask(TasksHolder.Instance.TaskList[i].OffHand, preset.OffHand);
                    TasksHolder.Instance.TaskList[i].Helm = updateEquipInTask(TasksHolder.Instance.TaskList[i].Helm, preset.Helm);
                    TasksHolder.Instance.TaskList[i].Torso = updateEquipInTask(TasksHolder.Instance.TaskList[i].Torso, preset.Torso);
                    TasksHolder.Instance.TaskList[i].Legs = updateEquipInTask(TasksHolder.Instance.TaskList[i].Legs, preset.Legs);
                    TasksHolder.Instance.TaskList[i].Glove = updateEquipInTask(TasksHolder.Instance.TaskList[i].Glove, preset.Glove);
                    TasksHolder.Instance.TaskList[i].Boots = updateEquipInTask(TasksHolder.Instance.TaskList[i].Boots, preset.Boots);
                    TasksHolder.Instance.TaskList[i].Ring = updateEquipInTask(TasksHolder.Instance.TaskList[i].Ring, preset.Ring);
                    TasksHolder.Instance.TaskList[i].Amulet = updateEquipInTask(TasksHolder.Instance.TaskList[i].Amulet, preset.Amulet);
                    TasksHolder.Instance.TaskList[i].Back = updateEquipInTask(TasksHolder.Instance.TaskList[i].Back, preset.Back);
                    TasksHolder.Instance.TaskList[i].Ammo = updateEquipInTask(TasksHolder.Instance.TaskList[i].Ammo, preset.Ammo);
                    TasksHolder.Instance.TaskList[i].Pocket = updateEquipInTask(TasksHolder.Instance.TaskList[i].Pocket, preset.Pocket);
                    TasksHolder.Instance.TaskList[i].Aura = updateEquipInTask(TasksHolder.Instance.TaskList[i].Aura, preset.Aura);
                    TasksHolder.Instance.TaskList[i].Sigil = updateEquipInTask(TasksHolder.Instance.TaskList[i].Sigil, preset.Sigil);

                    FileHandler.editTaskEquip(TasksHolder.Instance.TaskList[i]);
                }
            }
        }

        private EquipItem updateEquipInTask(EquipItem item, EquipItem presetItem)
        {
            if (item != null)
            {
                if (!item.ID.Equals(presetItem.ID))
                {
                    return presetItem;
                }
                else
                {
                    return item;
                }
            }
            else if (presetItem != null)
            {
                return presetItem;
            }

            return null;
        }
    }
}
