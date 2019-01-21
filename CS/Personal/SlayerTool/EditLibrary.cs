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
    public partial class EditLibrary : Form
    {
        private int listType;
        public EditLibrary(int type)
        {
            InitializeComponent();
            listType = type;

            switch (type)
            {
                case 0:     //items
                    for (int i = 0; i < ItemsHolder.Instance.ItemList.Count; i++)
                    {
                        lstLibrary.Items.Add(ItemsHolder.Instance.ItemList[i].ItemName);
                    }
                    break;
                case 1:     //weapons
                    for (int i = 0; i < EquipItemsHolder.Instance.WeaponList.Count; i++)
                    {
                        lstLibrary.Items.Add(EquipItemsHolder.Instance.WeaponList[i].ItemName);
                    }
                    break;
                case 2:     //offhand
                    for (int i = 0; i < EquipItemsHolder.Instance.OffhandList.Count; i++)
                    {
                        lstLibrary.Items.Add(EquipItemsHolder.Instance.OffhandList[i].ItemName);
                    }
                    break;
                case 3:     //helm
                    for (int i = 0; i < EquipItemsHolder.Instance.HelmList.Count; i++)
                    {
                        lstLibrary.Items.Add(EquipItemsHolder.Instance.HelmList[i].ItemName);
                    }
                    break;
                case 4:     //chest
                    for (int i = 0; i < EquipItemsHolder.Instance.ChestList.Count; i++)
                    {
                        lstLibrary.Items.Add(EquipItemsHolder.Instance.ChestList[i].ItemName);
                    }
                    break;
                case 5:     //leg
                    for (int i = 0; i < EquipItemsHolder.Instance.LegList.Count; i++)
                    {
                        lstLibrary.Items.Add(EquipItemsHolder.Instance.LegList[i].ItemName);
                    }
                    break;
                case 6:     //boot
                    for (int i = 0; i < EquipItemsHolder.Instance.BootList.Count; i++)
                    {
                        lstLibrary.Items.Add(EquipItemsHolder.Instance.BootList[i].ItemName);
                    }
                    break;
                case 7:     //glove
                    for (int i = 0; i < EquipItemsHolder.Instance.GloveList.Count; i++)
                    {
                        lstLibrary.Items.Add(EquipItemsHolder.Instance.GloveList[i].ItemName);
                    }
                    break;
                case 8:     //amulet
                    for (int i = 0; i < EquipItemsHolder.Instance.AmuletLit.Count; i++)
                    {
                        lstLibrary.Items.Add(EquipItemsHolder.Instance.AmuletLit[i].ItemName);
                    }
                    break;
                case 9:     //ring
                    for (int i = 0; i < EquipItemsHolder.Instance.RingList.Count; i++)
                    {
                        lstLibrary.Items.Add(EquipItemsHolder.Instance.RingList[i].ItemName);
                    }
                    break;
                case 10:    //back
                    for (int i = 0; i < EquipItemsHolder.Instance.BackList.Count; i++)
                    {
                        lstLibrary.Items.Add(EquipItemsHolder.Instance.BackList[i].ItemName);
                    }
                    break;
                case 11:    //ammo
                    for (int i = 0; i < EquipItemsHolder.Instance.AmmoList.Count; i++)
                    {
                        lstLibrary.Items.Add(EquipItemsHolder.Instance.AmmoList[i].ItemName);
                    }
                    break;
                case 12:    //pocket
                    for (int i = 0; i < EquipItemsHolder.Instance.PocketList.Count; i++)
                    {
                        lstLibrary.Items.Add(EquipItemsHolder.Instance.PocketList[i].ItemName);
                    }
                    break;
                case 13:    //aura
                    for (int i = 0; i < EquipItemsHolder.Instance.AuraList.Count; i++)
                    {
                        lstLibrary.Items.Add(EquipItemsHolder.Instance.AuraList[i].ItemName);
                    }
                    break;
                case 14:    //sigil
                    for (int i = 0; i < EquipItemsHolder.Instance.SigilList.Count; i++)
                    {
                        lstLibrary.Items.Add(EquipItemsHolder.Instance.SigilList[i].ItemName);
                    }
                    break;
                case 15:    //familiar
                    for (int i = 0; i < FamiliarHolder.Instance.FamiliarList.Count; i++)
                    {
                        lstLibrary.Items.Add(FamiliarHolder.Instance.FamiliarList[i].FamiliarName);
                    }
                    break;
                case 16:    //equip preset
                    for (int i = 0; i < EquipPresetsHolder.Instance.PresetList.Count; i++)
                    {
                        lstLibrary.Items.Add(EquipPresetsHolder.Instance.PresetList[i].Name);
                    }
                    break;
                case 17:    //ability preset
                    for (int i = 0; i < AbilityPresetHolder.Instance.PresetList.Count; i++)
                    {
                        lstLibrary.Items.Add(AbilityPresetHolder.Instance.PresetList[i].Name);
                    }
                    break;
                default:
                    break;
            }
        }

        //search listbox
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string myString = textBox1.Text;
            for (int i = 0; i <= lstLibrary.Items.Count - 1; i++)
            {
                if (lstLibrary.Items[i].ToString().ToLower().Contains(myString))
                {
                    lstLibrary.SetSelected(i, true);
                    break;
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstLibrary.SelectedIndex > -1)
            {
                DialogResult result = MessageBox.Show("Confirm delete of this item?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes)
                {
                    //remove from list
                    switch (listType)
                    {
                        case 0:     //items
                            removeFromTasks(); //remove from all tasks
                            FileHandler.DeleteItem(ItemsHolder.Instance.ItemList[lstLibrary.SelectedIndex]); //delete from file
                            ItemsHolder.Instance.ItemList.RemoveAt(lstLibrary.SelectedIndex); //remove from library
                            break;
                        case 1:     //weapon
                            removeFromTasks(); //remove from all tasks
                            FileHandler.DeleteEquip(EquipItemsHolder.Instance.WeaponList[lstLibrary.SelectedIndex]); //delete from file
                            EquipItemsHolder.Instance.WeaponList.RemoveAt(lstLibrary.SelectedIndex); //remove from library
                            break;
                        case 2:     //offhand
                            removeFromTasks(); //remove from all tasks
                            FileHandler.DeleteEquip(EquipItemsHolder.Instance.OffhandList[lstLibrary.SelectedIndex]); //delete from file
                            EquipItemsHolder.Instance.OffhandList.RemoveAt(lstLibrary.SelectedIndex); //remove from library
                            break;
                        case 3:     //helm
                            removeFromTasks(); //remove from all tasks
                            FileHandler.DeleteEquip(EquipItemsHolder.Instance.HelmList[lstLibrary.SelectedIndex]); //delete from file
                            EquipItemsHolder.Instance.HelmList.RemoveAt(lstLibrary.SelectedIndex); //remove from library
                            break;
                        case 4:     //chest
                            removeFromTasks(); //remove from all tasks
                            FileHandler.DeleteEquip(EquipItemsHolder.Instance.ChestList[lstLibrary.SelectedIndex]); //delete from file
                            EquipItemsHolder.Instance.ChestList.RemoveAt(lstLibrary.SelectedIndex); //remove from library
                            break;
                        case 5:     //leg
                            removeFromTasks(); //remove from all tasks
                            FileHandler.DeleteEquip(EquipItemsHolder.Instance.LegList[lstLibrary.SelectedIndex]); //delete from file
                            EquipItemsHolder.Instance.LegList.RemoveAt(lstLibrary.SelectedIndex); //remove from library
                            break;
                        case 6:     //boot
                            removeFromTasks(); //remove from all tasks
                            FileHandler.DeleteEquip(EquipItemsHolder.Instance.BootList[lstLibrary.SelectedIndex]); //delete from file
                            EquipItemsHolder.Instance.BootList.RemoveAt(lstLibrary.SelectedIndex); //remove from library
                            break;
                        case 7:     //glove
                            removeFromTasks(); //remove from all tasks
                            FileHandler.DeleteEquip(EquipItemsHolder.Instance.GloveList[lstLibrary.SelectedIndex]); //delete from file
                            EquipItemsHolder.Instance.GloveList.RemoveAt(lstLibrary.SelectedIndex); //remove from library
                            break;
                        case 8:     //amulet
                            removeFromTasks(); //remove from all tasks
                            FileHandler.DeleteEquip(EquipItemsHolder.Instance.AmuletLit[lstLibrary.SelectedIndex]); //delete from file
                            EquipItemsHolder.Instance.AmuletLit.RemoveAt(lstLibrary.SelectedIndex); //remove from library
                            break;
                        case 9:     //ring
                            removeFromTasks(); //remove from all tasks
                            FileHandler.DeleteEquip(EquipItemsHolder.Instance.RingList[lstLibrary.SelectedIndex]); //delete from file
                            EquipItemsHolder.Instance.RingList.RemoveAt(lstLibrary.SelectedIndex); //remove from library
                            break;
                        case 10:    //back
                            removeFromTasks(); //remove from all tasks
                            FileHandler.DeleteEquip(EquipItemsHolder.Instance.BackList[lstLibrary.SelectedIndex]); //delete from file
                            EquipItemsHolder.Instance.BackList.RemoveAt(lstLibrary.SelectedIndex); //remove from library
                            break;
                        case 11:    //ammo
                            removeFromTasks(); //remove from all tasks
                            FileHandler.DeleteEquip(EquipItemsHolder.Instance.AmmoList[lstLibrary.SelectedIndex]); //delete from file
                            EquipItemsHolder.Instance.AmmoList.RemoveAt(lstLibrary.SelectedIndex); //remove from library
                            break;
                        case 12:    //pocket
                            removeFromTasks(); //remove from all tasks
                            FileHandler.DeleteEquip(EquipItemsHolder.Instance.PocketList[lstLibrary.SelectedIndex]); //delete from file
                            EquipItemsHolder.Instance.PocketList.RemoveAt(lstLibrary.SelectedIndex); //remove from library
                            break;
                        case 13:    //aura
                            removeFromTasks(); //remove from all tasks
                            FileHandler.DeleteEquip(EquipItemsHolder.Instance.AuraList[lstLibrary.SelectedIndex]); //delete from file
                            EquipItemsHolder.Instance.AuraList.RemoveAt(lstLibrary.SelectedIndex); //remove from library
                            break;
                        case 14:    //sigil
                            removeFromTasks(); //remove from all tasks
                            FileHandler.DeleteEquip(EquipItemsHolder.Instance.SigilList[lstLibrary.SelectedIndex]); //delete from file
                            EquipItemsHolder.Instance.SigilList.RemoveAt(lstLibrary.SelectedIndex); //remove from library
                            break;
                        case 15:    //familiar
                            removeFromTasks(); //remove from all tasks
                            FileHandler.DeleteFamiliar(FamiliarHolder.Instance.FamiliarList[lstLibrary.SelectedIndex]); //delete from file
                            FamiliarHolder.Instance.FamiliarList.RemoveAt(lstLibrary.SelectedIndex); //remove from library
                            break;
                        case 16:    //equip preset 
                            removeFromTasks(); //remove from all tasks
                            FileHandler.DeleteEquipPreset(EquipPresetsHolder.Instance.PresetList[lstLibrary.SelectedIndex]); //delete from file
                            EquipPresetsHolder.Instance.PresetList.RemoveAt(lstLibrary.SelectedIndex); //remove from library
                            break;
                        case 17:    //ability preset
                            removeFromTasks(); //remove from all tasks
                            FileHandler.DeleteAbilityPreset(AbilityPresetHolder.Instance.PresetList[lstLibrary.SelectedIndex]); //delete from file
                            AbilityPresetHolder.Instance.PresetList.RemoveAt(lstLibrary.SelectedIndex); //remove from library
                            break;
                        default:
                            break;
                    }

                    //remove from display
                    lstLibrary.Items.RemoveAt(lstLibrary.SelectedIndex);
                }
                else
                {
                    return;
                }
            }
        }

        private void removeFromTasks()
        {
            switch (listType)
            {
                case 0:     //items
                    for (int i = 0; i < TasksHolder.Instance.TaskList.Count; i++)
                    {
                        TasksHolder.Instance.TaskList[i].BagItems.RemoveAll(x => x.ID == ItemsHolder.Instance.ItemList[lstLibrary.SelectedIndex].ID);
                        TasksHolder.Instance.TaskList[i].FamiliarBagItems.RemoveAll(x => x.ID == ItemsHolder.Instance.ItemList[lstLibrary.SelectedIndex].ID);
                    }
                    break;
                case 1:     //weapon
                    for (int i = 0; i < TasksHolder.Instance.TaskList.Count; i++)
                    {
                        if (TasksHolder.Instance.TaskList[i].Weapon != null)
                        {
                            if (TasksHolder.Instance.TaskList[i].Weapon.ID.Equals(EquipItemsHolder.Instance.WeaponList[lstLibrary.SelectedIndex].ID))
                            {
                                TasksHolder.Instance.TaskList[i].Weapon = null;
                            }
                        }
                    }
                    //remove from presets
                    for (int i = 0; i < EquipPresetsHolder.Instance.PresetList.Count; i++)
                    {
                        if (EquipPresetsHolder.Instance.PresetList[i].Weapon != null)
                        {
                            if (EquipPresetsHolder.Instance.PresetList[i].Weapon.ID.Equals(EquipItemsHolder.Instance.WeaponList[lstLibrary.SelectedIndex].ID))
                            {
                                EquipPresetsHolder.Instance.PresetList[i].Weapon = null;
                            }
                        }
                    }
                    break;
                case 2:     //offhand
                    for (int i = 0; i < TasksHolder.Instance.TaskList.Count; i++)
                    {
                        if (TasksHolder.Instance.TaskList[i].OffHand != null)
                        {
                            if (TasksHolder.Instance.TaskList[i].OffHand.ID.Equals(EquipItemsHolder.Instance.OffhandList[lstLibrary.SelectedIndex].ID))
                            {
                                TasksHolder.Instance.TaskList[i].OffHand = null;
                            }
                        }
                    }
                    //remove from presets
                    for (int i = 0; i < EquipPresetsHolder.Instance.PresetList.Count; i++)
                    {
                        if (EquipPresetsHolder.Instance.PresetList[i].OffHand != null)
                        {
                            if (EquipPresetsHolder.Instance.PresetList[i].OffHand.ID.Equals(EquipItemsHolder.Instance.OffhandList[lstLibrary.SelectedIndex].ID))
                            {
                                EquipPresetsHolder.Instance.PresetList[i].OffHand = null;
                            }
                        }
                    }
                    break;
                case 3:     //helm
                    for (int i = 0; i < TasksHolder.Instance.TaskList.Count; i++)
                    {
                        if (TasksHolder.Instance.TaskList[i].Helm != null)
                        {
                            if (TasksHolder.Instance.TaskList[i].Helm.ID.Equals(EquipItemsHolder.Instance.HelmList[lstLibrary.SelectedIndex].ID))
                            {
                                TasksHolder.Instance.TaskList[i].Helm = null;
                            }
                        }
                    }
                    //remove from presets
                    for (int i = 0; i < EquipPresetsHolder.Instance.PresetList.Count; i++)
                    {
                        if (EquipPresetsHolder.Instance.PresetList[i].Helm != null)
                        {
                            if (EquipPresetsHolder.Instance.PresetList[i].Helm.ID.Equals(EquipItemsHolder.Instance.HelmList[lstLibrary.SelectedIndex].ID))
                            {
                                EquipPresetsHolder.Instance.PresetList[i].Helm = null;
                            }
                        }
                    }
                    break;
                case 4:     //chest
                    for (int i = 0; i < TasksHolder.Instance.TaskList.Count; i++)
                    {
                        if (TasksHolder.Instance.TaskList[i].Torso != null)
                        {
                            if (TasksHolder.Instance.TaskList[i].Torso.ID.Equals(EquipItemsHolder.Instance.ChestList[lstLibrary.SelectedIndex].ID))
                            {
                                TasksHolder.Instance.TaskList[i].Torso = null;
                            }
                        }
                    }
                    //remove from presets
                    for (int i = 0; i < EquipPresetsHolder.Instance.PresetList.Count; i++)
                    {
                        if (EquipPresetsHolder.Instance.PresetList[i].Torso != null)
                        {
                            if (EquipPresetsHolder.Instance.PresetList[i].Torso.ID.Equals(EquipItemsHolder.Instance.ChestList[lstLibrary.SelectedIndex].ID))
                            {
                                EquipPresetsHolder.Instance.PresetList[i].Torso = null;
                            }
                        }
                    }
                    break;
                case 5:     //leg
                    for (int i = 0; i < TasksHolder.Instance.TaskList.Count; i++)
                    {
                        if (TasksHolder.Instance.TaskList[i].Legs != null)
                        {
                            if (TasksHolder.Instance.TaskList[i].Legs.ID.Equals(EquipItemsHolder.Instance.LegList[lstLibrary.SelectedIndex].ID))
                            {
                                TasksHolder.Instance.TaskList[i].Legs = null;
                            }
                        }
                    }
                    //remove from presets
                    for (int i = 0; i < EquipPresetsHolder.Instance.PresetList.Count; i++)
                    {
                        if (EquipPresetsHolder.Instance.PresetList[i].Legs != null)
                        {
                            if (EquipPresetsHolder.Instance.PresetList[i].Legs.ID.Equals(EquipItemsHolder.Instance.LegList[lstLibrary.SelectedIndex].ID))
                            {
                                EquipPresetsHolder.Instance.PresetList[i].Legs = null;
                            }
                        }
                    }
                    break;
                case 6:     //boot
                    for (int i = 0; i < TasksHolder.Instance.TaskList.Count; i++)
                    {
                        if (TasksHolder.Instance.TaskList[i].Boots != null)
                        {
                            if (TasksHolder.Instance.TaskList[i].Boots.ID.Equals(EquipItemsHolder.Instance.BootList[lstLibrary.SelectedIndex].ID))
                            {
                                TasksHolder.Instance.TaskList[i].Boots = null;
                            }
                        }
                    }
                    //remove from presets
                    for (int i = 0; i < EquipPresetsHolder.Instance.PresetList.Count; i++)
                    {
                        if (EquipPresetsHolder.Instance.PresetList[i].Boots != null)
                        {
                            if (EquipPresetsHolder.Instance.PresetList[i].Boots.ID.Equals(EquipItemsHolder.Instance.BootList[lstLibrary.SelectedIndex].ID))
                            {
                                EquipPresetsHolder.Instance.PresetList[i].Boots = null;
                            }
                        }
                    }
                    break;
                case 7:     //glove
                    for (int i = 0; i < TasksHolder.Instance.TaskList.Count; i++)
                    {
                        if (TasksHolder.Instance.TaskList[i].Glove != null)
                        {
                            if (TasksHolder.Instance.TaskList[i].Glove.ID.Equals(EquipItemsHolder.Instance.GloveList[lstLibrary.SelectedIndex].ID))
                            {
                                TasksHolder.Instance.TaskList[i].Glove = null;
                            }
                        }
                    }
                    //remove from presets
                    for (int i = 0; i < EquipPresetsHolder.Instance.PresetList.Count; i++)
                    {
                        if (EquipPresetsHolder.Instance.PresetList[i].Glove != null)
                        {
                            if (EquipPresetsHolder.Instance.PresetList[i].Glove.ID.Equals(EquipItemsHolder.Instance.GloveList[lstLibrary.SelectedIndex].ID))
                            {
                                EquipPresetsHolder.Instance.PresetList[i].Glove = null;
                            }
                        }
                    }
                    break;
                case 8:     //amulet
                    for (int i = 0; i < TasksHolder.Instance.TaskList.Count; i++)
                    {
                        if (TasksHolder.Instance.TaskList[i].Amulet != null)
                        {
                            if (TasksHolder.Instance.TaskList[i].Amulet.ID.Equals(EquipItemsHolder.Instance.AmuletLit[lstLibrary.SelectedIndex].ID))
                            {
                                TasksHolder.Instance.TaskList[i].Amulet = null;
                            }
                        }
                    }
                    //remove from presets
                    for (int i = 0; i < EquipPresetsHolder.Instance.PresetList.Count; i++)
                    {
                        if (EquipPresetsHolder.Instance.PresetList[i].Amulet != null)
                        {
                            if (EquipPresetsHolder.Instance.PresetList[i].Amulet.ID.Equals(EquipItemsHolder.Instance.AmuletLit[lstLibrary.SelectedIndex].ID))
                            {
                                EquipPresetsHolder.Instance.PresetList[i].Amulet = null;
                            }
                        }
                    }
                    break;
                case 9:     //ring
                    for (int i = 0; i < TasksHolder.Instance.TaskList.Count; i++)
                    {
                        if (TasksHolder.Instance.TaskList[i].Ring != null)
                        {
                            if (TasksHolder.Instance.TaskList[i].Ring.ID.Equals(EquipItemsHolder.Instance.RingList[lstLibrary.SelectedIndex].ID))
                            {
                                TasksHolder.Instance.TaskList[i].Ring = null;
                            }
                        }
                    }
                    //remove from presets
                    for (int i = 0; i < EquipPresetsHolder.Instance.PresetList.Count; i++)
                    {
                        if (EquipPresetsHolder.Instance.PresetList[i].Ring != null)
                        {
                            if (EquipPresetsHolder.Instance.PresetList[i].Ring.ID.Equals(EquipItemsHolder.Instance.RingList[lstLibrary.SelectedIndex].ID))
                            {
                                EquipPresetsHolder.Instance.PresetList[i].Ring = null;
                            }
                        }
                    }
                    break;
                case 10:    //back
                    for (int i = 0; i < TasksHolder.Instance.TaskList.Count; i++)
                    {
                        if (TasksHolder.Instance.TaskList[i].Back != null)
                        {
                            if (TasksHolder.Instance.TaskList[i].Back.ID.Equals(EquipItemsHolder.Instance.BackList[lstLibrary.SelectedIndex].ID))
                            {
                                TasksHolder.Instance.TaskList[i].Back = null;
                            }
                        }
                    }
                    //remove from presets
                    for (int i = 0; i < EquipPresetsHolder.Instance.PresetList.Count; i++)
                    {
                        if (EquipPresetsHolder.Instance.PresetList[i].Back != null)
                        {
                            if (EquipPresetsHolder.Instance.PresetList[i].Back.ID.Equals(EquipItemsHolder.Instance.BackList[lstLibrary.SelectedIndex].ID))
                            {
                                EquipPresetsHolder.Instance.PresetList[i].Back = null;
                            }
                        }
                    }
                    break;
                case 11:    //ammo
                    for (int i = 0; i < TasksHolder.Instance.TaskList.Count; i++)
                    {
                        if (TasksHolder.Instance.TaskList[i].Ammo != null)
                        {
                            if (TasksHolder.Instance.TaskList[i].Ammo.ID.Equals(EquipItemsHolder.Instance.AmmoList[lstLibrary.SelectedIndex].ID))
                            {
                                TasksHolder.Instance.TaskList[i].Ammo = null;
                            }
                        }
                    }
                    //remove from presets
                    for (int i = 0; i < EquipPresetsHolder.Instance.PresetList.Count; i++)
                    {
                        if (EquipPresetsHolder.Instance.PresetList[i].Ammo != null)
                        {
                            if (EquipPresetsHolder.Instance.PresetList[i].Ammo.ID.Equals(EquipItemsHolder.Instance.AmmoList[lstLibrary.SelectedIndex].ID))
                            {
                                EquipPresetsHolder.Instance.PresetList[i].Ammo = null;
                            }
                        }
                    }
                    break;
                case 12:    //pocket
                    for (int i = 0; i < TasksHolder.Instance.TaskList.Count; i++)
                    {
                        if (TasksHolder.Instance.TaskList[i].Pocket != null)
                        {
                            if (TasksHolder.Instance.TaskList[i].Pocket.ID.Equals(EquipItemsHolder.Instance.PocketList[lstLibrary.SelectedIndex].ID))
                            {
                                TasksHolder.Instance.TaskList[i].Pocket = null;
                            }
                        }
                    }
                    //remove from presets
                    for (int i = 0; i < EquipPresetsHolder.Instance.PresetList.Count; i++)
                    {
                        if (EquipPresetsHolder.Instance.PresetList[i].Pocket != null)
                        {
                            if (EquipPresetsHolder.Instance.PresetList[i].Pocket.ID.Equals(EquipItemsHolder.Instance.PocketList[lstLibrary.SelectedIndex].ID))
                            {
                                EquipPresetsHolder.Instance.PresetList[i].Pocket = null;
                            }
                        }
                    }
                    break;
                case 13:    //aura
                    for (int i = 0; i < TasksHolder.Instance.TaskList.Count; i++)
                    {
                        if (TasksHolder.Instance.TaskList[i].Aura != null)
                        {
                            if (TasksHolder.Instance.TaskList[i].Aura.ID.Equals(EquipItemsHolder.Instance.AuraList[lstLibrary.SelectedIndex].ID))
                            {
                                TasksHolder.Instance.TaskList[i].Aura = null;
                            }
                        }
                    }
                    //remove from presets
                    for (int i = 0; i < EquipPresetsHolder.Instance.PresetList.Count; i++)
                    {
                        if (EquipPresetsHolder.Instance.PresetList[i].Aura != null)
                        {
                            if (EquipPresetsHolder.Instance.PresetList[i].Aura.ID.Equals(EquipItemsHolder.Instance.AuraList[lstLibrary.SelectedIndex].ID))
                            {
                                EquipPresetsHolder.Instance.PresetList[i].Aura = null;
                            }
                        }
                    }
                    break;
                case 14:    //sigil
                    for (int i = 0; i < TasksHolder.Instance.TaskList.Count; i++)
                    {
                        if (TasksHolder.Instance.TaskList[i].Sigil != null)
                        {
                            if (TasksHolder.Instance.TaskList[i].Sigil.ID.Equals(EquipItemsHolder.Instance.SigilList[lstLibrary.SelectedIndex].ID))
                            {
                                TasksHolder.Instance.TaskList[i].Sigil = null;
                            }
                        }
                    }
                    //remove from presets
                    for (int i = 0; i < EquipPresetsHolder.Instance.PresetList.Count; i++)
                    {
                        if (EquipPresetsHolder.Instance.PresetList[i].Sigil != null)
                        {
                            if (EquipPresetsHolder.Instance.PresetList[i].Sigil.ID.Equals(EquipItemsHolder.Instance.SigilList[lstLibrary.SelectedIndex].ID))
                            {
                                EquipPresetsHolder.Instance.PresetList[i].Sigil = null;
                            }
                        }
                    }
                    break;
                case 15:    //familiar
                    for (int i = 0; i < TasksHolder.Instance.TaskList.Count; i++)
                    {
                        if (TasksHolder.Instance.TaskList[i].Familiar != null)
                        {
                            if (TasksHolder.Instance.TaskList[i].Familiar.ID.Equals(FamiliarHolder.Instance.FamiliarList[lstLibrary.SelectedIndex].ID))
                            {
                                TasksHolder.Instance.TaskList[i].Familiar = null;
                                TasksHolder.Instance.TaskList[i].FamiliarBagItems.Clear();
                            }
                        }
                    }
                    break;
                case 16:    //equip preset 
                    for (int i = 0; i < TasksHolder.Instance.TaskList.Count; i++)
                    {
                        if (TasksHolder.Instance.TaskList[i].EquipPreset != null)
                        {
                            if (TasksHolder.Instance.TaskList[i].EquipPreset == EquipPresetsHolder.Instance.PresetList[lstLibrary.SelectedIndex])
                            {
                                TasksHolder.Instance.TaskList[i].EquipPreset = null;
                            }
                        }
                    }
                    break;
                case 17:    //ability preset
                    for (int i = 0; i < TasksHolder.Instance.TaskList.Count; i++)
                    {
                        if (TasksHolder.Instance.TaskList[i].AbilityPreset != null)
                        {
                            if (TasksHolder.Instance.TaskList[i].AbilityPreset == AbilityPresetHolder.Instance.PresetList[lstLibrary.SelectedIndex])
                            {
                                TasksHolder.Instance.TaskList[i].AbilityPreset = null;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        //exit button
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lstLibrary.SelectedIndex < 0)
            {
                return;
            }

            Form frm = null;
            switch (listType)
            {
                case 0:     //items
                    frm = new EditItem(listType, lstLibrary, ItemsHolder.Instance.ItemList[lstLibrary.SelectedIndex]);
                    break;
                case 1:     //weapon
                    frm = new EditItem(listType, lstLibrary, EquipItemsHolder.Instance.WeaponList[lstLibrary.SelectedIndex]);
                    break;
                case 2:     //offhand
                    frm = new EditItem(listType, lstLibrary, EquipItemsHolder.Instance.OffhandList[lstLibrary.SelectedIndex]);
                    break;
                case 3:     //helm
                    frm = new EditItem(listType, lstLibrary, EquipItemsHolder.Instance.HelmList[lstLibrary.SelectedIndex]);
                    break;
                case 4:     //chest
                    frm = new EditItem(listType, lstLibrary, EquipItemsHolder.Instance.ChestList[lstLibrary.SelectedIndex]);
                    break;
                case 5:     //leg
                    frm = new EditItem(listType, lstLibrary, EquipItemsHolder.Instance.LegList[lstLibrary.SelectedIndex]);
                    break;
                case 6:     //boot
                    frm = new EditItem(listType, lstLibrary, EquipItemsHolder.Instance.BootList[lstLibrary.SelectedIndex]);
                    break;
                case 7:     //glove
                    frm = new EditItem(listType, lstLibrary, EquipItemsHolder.Instance.GloveList[lstLibrary.SelectedIndex]);
                    break;
                case 8:     //amulet
                    frm = new EditItem(listType, lstLibrary, EquipItemsHolder.Instance.AmuletLit[lstLibrary.SelectedIndex]);
                    break;
                case 9:     //ring
                    frm = new EditItem(listType, lstLibrary, EquipItemsHolder.Instance.RingList[lstLibrary.SelectedIndex]);
                    break;
                case 10:    //back
                    frm = new EditItem(listType, lstLibrary, EquipItemsHolder.Instance.BackList[lstLibrary.SelectedIndex]);
                    break;
                case 11:    //ammo
                    frm = new EditItem(listType, lstLibrary, EquipItemsHolder.Instance.AmmoList[lstLibrary.SelectedIndex]);
                    break;
                case 12:    //pocket
                    frm = new EditItem(listType, lstLibrary, EquipItemsHolder.Instance.PocketList[lstLibrary.SelectedIndex]);
                    break;
                case 13:    //aura
                    frm = new EditItem(listType, lstLibrary, EquipItemsHolder.Instance.AuraList[lstLibrary.SelectedIndex]);
                    break;
                case 14:    //sigil
                    frm = new EditItem(listType, lstLibrary, EquipItemsHolder.Instance.SigilList[lstLibrary.SelectedIndex]);
                    break;
                case 15:    //familiar
                    Form frm2 = new EditFamiliar(lstLibrary, FamiliarHolder.Instance.FamiliarList[lstLibrary.SelectedIndex]);
                    frm2.StartPosition = FormStartPosition.CenterScreen;
                    if (Application.OpenForms[frm2.Name] == null)
                    {
                        frm2.ShowDialog();
                    }
                    else
                    {
                        Application.OpenForms[frm2.Name].Activate();
                    }
                    break;
                case 16:    //equip preset 
                    Form frm3 = new EditEquipPreset(lstLibrary, EquipPresetsHolder.Instance.PresetList[lstLibrary.SelectedIndex]);
                    frm3.StartPosition = FormStartPosition.CenterScreen;
                    if (Application.OpenForms[frm3.Name] == null)
                    {
                        frm3.ShowDialog();
                    }
                    else
                    {
                        Application.OpenForms[frm3.Name].Activate();
                    }
                    break;
                case 17:    //ability preset
                    Form frm4 = new EditAbilityBarPreset(lstLibrary, AbilityPresetHolder.Instance.PresetList[lstLibrary.SelectedIndex]);
                    frm4.StartPosition = FormStartPosition.CenterScreen;
                    if (Application.OpenForms[frm4.Name] == null)
                    {
                        frm4.ShowDialog();
                    }
                    else
                    {
                        Application.OpenForms[frm4.Name].Activate();
                    }
                    break;
                default:
                    break;
            }

            if (frm != null)
            {
                frm.StartPosition = FormStartPosition.CenterScreen;
                if (Application.OpenForms[frm.Name] == null)
                {
                    frm.ShowDialog();
                }
                else
                {
                    Application.OpenForms[frm.Name].Activate();
                }
            }      
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (listType == 15)
            {
                Form frm = new EditFamiliar(lstLibrary);
                frm.StartPosition = FormStartPosition.CenterScreen;
                if (Application.OpenForms[frm.Name] == null)
                {
                    frm.ShowDialog();
                }
                else
                {
                    Application.OpenForms[frm.Name].Activate();
                }
            }
            else if (listType == 16)
            {
                Form frm = new EditEquipPreset(lstLibrary);
                frm.StartPosition = FormStartPosition.CenterScreen;
                if (Application.OpenForms[frm.Name] == null)
                {
                    frm.ShowDialog();
                }
                else
                {
                    Application.OpenForms[frm.Name].Activate();
                }
            }
            else if (listType == 17)
            {
                Form frm = new EditAbilityBarPreset(lstLibrary);
                frm.StartPosition = FormStartPosition.CenterScreen;
                if (Application.OpenForms[frm.Name] == null)
                {
                    frm.ShowDialog();
                }
                else
                {
                    Application.OpenForms[frm.Name].Activate();
                }
            }
            else
            {
                EditItem frm = new EditItem(listType, lstLibrary);
                frm.StartPosition = FormStartPosition.CenterScreen;
                if (Application.OpenForms[frm.Name] == null)
                {
                    frm.ShowDialog();
                }
                else
                {
                    Application.OpenForms[frm.Name].Activate();
                }
            }
        }
    }
}
