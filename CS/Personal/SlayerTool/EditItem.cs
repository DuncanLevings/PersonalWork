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
    public partial class EditItem : Form
    {
        private int listType;
        private string imageItem;
        private string oldName;
        private ListBox lstLibrary;
        private Item item;
        private EquipItem equip;

        //create constructor
        public EditItem(int type, ListBox list)
        {
            InitializeComponent();

            listType = type;
            lstLibrary = list;

            btnDone.Enabled = true;
            btnDone.Visible = true;
        }

        //bag items
        public EditItem(int type, ListBox list, Item item)
        {
            InitializeComponent();

            listType = type;
            lstLibrary = list;
            this.item = item;

            setupEdit();
        }

        //equip items
        public EditItem(int type, ListBox list, EquipItem equip)
        {
            InitializeComponent();

            listType = type;
            lstLibrary = list;
            this.equip = equip;

            setupEdit();
        }

        private void setupEdit()
        {
            btnUpdate.Enabled = true;
            btnUpdate.Visible = true;

            switch (listType)
            {
                case 0:     //items
                    txtName.Text = item.ItemName;
                    oldName = item.ItemName;

                    imgItem.Load(item.ImgPath_Item);
                    imageItem = item.ImgPath_Item;
                    break;
                case 1:     //weapon
                    setupEquip();
                    break;
                case 2:     //offhand
                    setupEquip();
                    break;
                case 3:     //helm
                    setupEquip();
                    break;
                case 4:     //chest
                    setupEquip();
                    break;
                case 5:     //leg
                    setupEquip();
                    break;
                case 6:     //boot
                    setupEquip();
                    break;
                case 7:     //glove
                    setupEquip();
                    break;
                case 8:     //amulet
                    setupEquip();
                    break;
                case 9:     //ring
                    setupEquip();
                    break;
                case 10:    //back
                    setupEquip();
                    break;
                case 11:    //ammo
                    setupEquip();
                    break;
                case 12:    //pocket
                    setupEquip();
                    break;
                case 13:    //aura
                    setupEquip();
                    break;
                case 14:    //sigil
                    setupEquip();
                    break;
                default:
                    break;
            }
        }

        private void setupEquip()
        {
            txtName.Text = equip.ItemName;
            oldName = equip.ItemName;

            imgItem.Load(equip.ImgPath_Item);
            imageItem = equip.ImgPath_Item;
        }

        private void imgItem_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                imageItem = openFileDialog1.FileName;
                try
                {
                    imgItem.Image = Image.FromFile(imageItem);
                }
                catch (IOException)
                {
                }
            }
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

            if (imageItem == null)
            {
                MessageBox.Show("Must have image!", "Error creating", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            switch (listType)
            {
                case 0:     //items
                    for (int i = 0; i < ItemsHolder.Instance.ItemList.Count; i++)
                    {
                        if (txtName.Text.Equals(ItemsHolder.Instance.ItemList[i].ItemName))
                        {
                            MessageBox.Show("Item already exists!", "Error creating", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }

                    Item item = new Item(txtName.Text);
                    Guid guiditem = Guid.NewGuid();
                    item.ID = guiditem.ToString();
                    item.ImgPath_Item = imageItem;
                    ItemsHolder.Instance.ItemList.Add(item);

                    //save to file
                    FileHandler.SaveNewItem(item);
                    break;
                case 1:     //weapon
                    for (int i = 0; i < EquipItemsHolder.Instance.WeaponList.Count; i++)
                    {
                        if (txtName.Text.Equals(EquipItemsHolder.Instance.WeaponList[i].ItemName))
                        {
                            MessageBox.Show("Item already exists!", "Error creating", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }

                    EquipItem weapon = new EquipItem(txtName.Text);
                    Guid guidweapon = Guid.NewGuid();
                    weapon.ID = guidweapon.ToString();
                    weapon.ImgPath_Item = imageItem;
                    weapon.EquipType = 1;
                    EquipItemsHolder.Instance.WeaponList.Add(weapon);

                    //save to file
                    FileHandler.SaveNewEquip(weapon);
                    break;
                case 2:     //offhand
                    for (int i = 0; i < EquipItemsHolder.Instance.OffhandList.Count; i++)
                    {
                        if (txtName.Text.Equals(EquipItemsHolder.Instance.OffhandList[i].ItemName))
                        {
                            MessageBox.Show("Item already exists!", "Error creating", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }

                    EquipItem offhand = new EquipItem(txtName.Text);
                    Guid guidoffhand = Guid.NewGuid();
                    offhand.ID = guidoffhand.ToString();
                    offhand.ImgPath_Item = imageItem;
                    offhand.EquipType = 2;
                    EquipItemsHolder.Instance.OffhandList.Add(offhand);

                    //save to file
                    FileHandler.SaveNewEquip(offhand);
                    break;
                case 3:     //helm
                    for (int i = 0; i < EquipItemsHolder.Instance.HelmList.Count; i++)
                    {
                        if (txtName.Text.Equals(EquipItemsHolder.Instance.HelmList[i].ItemName))
                        {
                            MessageBox.Show("Item already exists!", "Error creating", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }

                    EquipItem helm = new EquipItem(txtName.Text);
                    Guid guidhelm = Guid.NewGuid();
                    helm.ID = guidhelm.ToString();
                    helm.ImgPath_Item = imageItem;
                    helm.EquipType = 3;
                    EquipItemsHolder.Instance.HelmList.Add(helm);

                    //save to file
                    FileHandler.SaveNewEquip(helm);
                    break;
                case 4:     //chest
                    for (int i = 0; i < EquipItemsHolder.Instance.ChestList.Count; i++)
                    {
                        if (txtName.Text.Equals(EquipItemsHolder.Instance.ChestList[i].ItemName))
                        {
                            MessageBox.Show("Item already exists!", "Error creating", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }

                    EquipItem chest = new EquipItem(txtName.Text);
                    Guid guidchest = Guid.NewGuid();
                    chest.ID = guidchest.ToString();
                    chest.ImgPath_Item = imageItem;
                    chest.EquipType = 4;
                    EquipItemsHolder.Instance.ChestList.Add(chest);

                    //save to file
                    FileHandler.SaveNewEquip(chest);
                    break;
                case 5:     //leg
                    for (int i = 0; i < EquipItemsHolder.Instance.LegList.Count; i++)
                    {
                        if (txtName.Text.Equals(EquipItemsHolder.Instance.LegList[i].ItemName))
                        {
                            MessageBox.Show("Item already exists!", "Error creating", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }

                    EquipItem leg = new EquipItem(txtName.Text);
                    Guid guidleg = Guid.NewGuid();
                    leg.ID = guidleg.ToString();
                    leg.ImgPath_Item = imageItem;
                    leg.EquipType = 5;
                    EquipItemsHolder.Instance.LegList.Add(leg);

                    //save to file
                    FileHandler.SaveNewEquip(leg);
                    break;
                case 6:     //boot
                    for (int i = 0; i < EquipItemsHolder.Instance.BootList.Count; i++)
                    {
                        if (txtName.Text.Equals(EquipItemsHolder.Instance.BootList[i].ItemName))
                        {
                            MessageBox.Show("Item already exists!", "Error creating", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }

                    EquipItem boot = new EquipItem(txtName.Text);
                    Guid guidboot = Guid.NewGuid();
                    boot.ID = guidboot.ToString();
                    boot.ImgPath_Item = imageItem;
                    boot.EquipType = 6;
                    EquipItemsHolder.Instance.BootList.Add(boot);

                    //save to file
                    FileHandler.SaveNewEquip(boot);
                    break;
                case 7:     //glove
                    for (int i = 0; i < EquipItemsHolder.Instance.GloveList.Count; i++)
                    {
                        if (txtName.Text.Equals(EquipItemsHolder.Instance.GloveList[i].ItemName))
                        {
                            MessageBox.Show("Item already exists!", "Error creating", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }

                    EquipItem glove = new EquipItem(txtName.Text);
                    Guid guidglove = Guid.NewGuid();
                    glove.ID = guidglove.ToString();
                    glove.ImgPath_Item = imageItem;
                    glove.EquipType = 7;
                    EquipItemsHolder.Instance.GloveList.Add(glove);

                    //save to file
                    FileHandler.SaveNewEquip(glove);
                    break;
                case 8:     //amulet
                    for (int i = 0; i < EquipItemsHolder.Instance.AmuletLit.Count; i++)
                    {
                        if (txtName.Text.Equals(EquipItemsHolder.Instance.AmuletLit[i].ItemName))
                        {
                            MessageBox.Show("Item already exists!", "Error creating", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }

                    EquipItem amulet = new EquipItem(txtName.Text);
                    Guid guidamulet = Guid.NewGuid();
                    amulet.ID = guidamulet.ToString();
                    amulet.ImgPath_Item = imageItem;
                    amulet.EquipType = 8;
                    EquipItemsHolder.Instance.AmuletLit.Add(amulet);

                    //save to file
                    FileHandler.SaveNewEquip(amulet);
                    break;
                case 9:     //ring
                    for (int i = 0; i < EquipItemsHolder.Instance.RingList.Count; i++)
                    {
                        if (txtName.Text.Equals(EquipItemsHolder.Instance.RingList[i].ItemName))
                        {
                            MessageBox.Show("Item already exists!", "Error creating", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }

                    EquipItem ring = new EquipItem(txtName.Text);
                    Guid guidring = Guid.NewGuid();
                    ring.ID = guidring.ToString();
                    ring.ImgPath_Item = imageItem;
                    ring.EquipType = 9;
                    EquipItemsHolder.Instance.RingList.Add(ring);

                    //save to file
                    FileHandler.SaveNewEquip(ring);
                    break;
                case 10:    //back
                    for (int i = 0; i < EquipItemsHolder.Instance.BackList.Count; i++)
                    {
                        if (txtName.Text.Equals(EquipItemsHolder.Instance.BackList[i].ItemName))
                        {
                            MessageBox.Show("Item already exists!", "Error creating", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }

                    EquipItem back = new EquipItem(txtName.Text);
                    Guid guidback = Guid.NewGuid();
                    back.ID = guidback.ToString();
                    back.ImgPath_Item = imageItem;
                    back.EquipType = 10;
                    EquipItemsHolder.Instance.BackList.Add(back);

                    //save to file
                    FileHandler.SaveNewEquip(back);
                    break;
                case 11:    //ammo
                    for (int i = 0; i < EquipItemsHolder.Instance.AmmoList.Count; i++)
                    {
                        if (txtName.Text.Equals(EquipItemsHolder.Instance.AmmoList[i].ItemName))
                        {
                            MessageBox.Show("Item already exists!", "Error creating", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }

                    EquipItem ammo = new EquipItem(txtName.Text);
                    Guid guidammo = Guid.NewGuid();
                    ammo.ID = guidammo.ToString();
                    ammo.ImgPath_Item = imageItem;
                    ammo.EquipType = 11;
                    EquipItemsHolder.Instance.AmmoList.Add(ammo);

                    //save to file
                    FileHandler.SaveNewEquip(ammo);
                    break;
                case 12:    //pocket
                    for (int i = 0; i < EquipItemsHolder.Instance.PocketList.Count; i++)
                    {
                        if (txtName.Text.Equals(EquipItemsHolder.Instance.PocketList[i].ItemName))
                        {
                            MessageBox.Show("Item already exists!", "Error creating", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }

                    EquipItem pocket = new EquipItem(txtName.Text);
                    Guid guidpocket = Guid.NewGuid();
                    pocket.ID = guidpocket.ToString();
                    pocket.ImgPath_Item = imageItem;
                    pocket.EquipType = 12;
                    EquipItemsHolder.Instance.PocketList.Add(pocket);

                    //save to file
                    FileHandler.SaveNewEquip(pocket);
                    break;
                case 13:    //aura
                    for (int i = 0; i < EquipItemsHolder.Instance.AuraList.Count; i++)
                    {
                        if (txtName.Text.Equals(EquipItemsHolder.Instance.AuraList[i].ItemName))
                        {
                            MessageBox.Show("Item already exists!", "Error creating", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }

                    EquipItem aura = new EquipItem(txtName.Text);
                    Guid guidaura = Guid.NewGuid();
                    aura.ID = guidaura.ToString();
                    aura.ImgPath_Item = imageItem;
                    aura.EquipType = 13;
                    EquipItemsHolder.Instance.AuraList.Add(aura);

                    //save to file
                    FileHandler.SaveNewEquip(aura);
                    break;
                case 14:    //sigil
                    for (int i = 0; i < EquipItemsHolder.Instance.SigilList.Count; i++)
                    {
                        if (txtName.Text.Equals(EquipItemsHolder.Instance.SigilList[i].ItemName))
                        {
                            MessageBox.Show("Item already exists!", "Error creating", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }

                    EquipItem sigil = new EquipItem(txtName.Text);
                    Guid guidsigil = Guid.NewGuid();
                    sigil.ID = guidsigil.ToString();
                    sigil.ImgPath_Item = imageItem;
                    sigil.EquipType = 14;
                    EquipItemsHolder.Instance.SigilList.Add(sigil);

                    //save to file
                    FileHandler.SaveNewEquip(sigil);
                    break;
                default:
                    break;
            }

            lstLibrary.Items.Add(txtName.Text);

            Close();
        }

        //for editing existing
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //error checking
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Name cannot be empty!", "Error creating", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (imageItem == null)
            {
                MessageBox.Show("Must have image!", "Error creating", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            switch (listType)
            {
                case 0:     //items
                    string itemName = checkName();
                    if (itemName != null)
                    {
                        item.ItemName = itemName;
                    }

                    item.ImgPath_Item = imageItem;

                    updateAllItems(item.ItemName, item.ImgPath_Item);

                    FileHandler.EditItem(item);
                    break;
                case 1:     //weapon
                    updateEquip();

                    updateAllEquip(equip.ItemName, equip.ImgPath_Item);

                    FileHandler.EditEquip(equip);
                    break;
                case 2:     //offhand
                    updateEquip();

                    updateAllEquip(equip.ItemName, equip.ImgPath_Item);

                    FileHandler.EditEquip(equip);
                    break;
                case 3:     //helm
                    updateEquip();

                    updateAllEquip(equip.ItemName, equip.ImgPath_Item);

                    FileHandler.EditEquip(equip);
                    break;
                case 4:     //chest
                    updateEquip();

                    updateAllEquip(equip.ItemName, equip.ImgPath_Item);

                    FileHandler.EditEquip(equip);
                    break;
                case 5:     //leg
                    updateEquip();

                    updateAllEquip(equip.ItemName, equip.ImgPath_Item);

                    FileHandler.EditEquip(equip);
                    break;
                case 6:     //boot
                    updateEquip();

                    updateAllEquip(equip.ItemName, equip.ImgPath_Item);

                    FileHandler.EditEquip(equip);
                    break;
                case 7:     //glove
                    updateEquip();

                    updateAllEquip(equip.ItemName, equip.ImgPath_Item);

                    FileHandler.EditEquip(equip);
                    break;
                case 8:     //amulet
                    updateEquip();

                    updateAllEquip(equip.ItemName, equip.ImgPath_Item);

                    FileHandler.EditEquip(equip);
                    break;
                case 9:     //ring
                    updateEquip();

                    updateAllEquip(equip.ItemName, equip.ImgPath_Item);

                    FileHandler.EditEquip(equip);
                    break;
                case 10:    //back
                    updateEquip();

                    updateAllEquip(equip.ItemName, equip.ImgPath_Item);

                    FileHandler.EditEquip(equip);
                    break;
                case 11:    //ammo
                    updateEquip();

                    updateAllEquip(equip.ItemName, equip.ImgPath_Item);

                    FileHandler.EditEquip(equip);
                    break;
                case 12:    //pocket
                    updateEquip();

                    updateAllEquip(equip.ItemName, equip.ImgPath_Item);

                    FileHandler.EditEquip(equip);
                    break;
                case 13:    //aura
                    updateEquip();

                    updateAllEquip(equip.ItemName, equip.ImgPath_Item);

                    FileHandler.EditEquip(equip);
                    break;
                case 14:    //sigil
                    updateEquip();

                    updateAllEquip(equip.ItemName, equip.ImgPath_Item);

                    FileHandler.EditEquip(equip);
                    break;
                default:
                    break;
            }

            Close();
        }

        private void updateEquip()
        {
            string equipName = checkName();
            if (equipName != null)
            {
                equip.ItemName = equipName;
            }

            equip.ImgPath_Item = imageItem;
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

        private void updateAllItems(string name, string imgPath)
        {
            for (int i = 0; i < TasksHolder.Instance.TaskList.Count; i++)
            {
                for (int j = 0; j < TasksHolder.Instance.TaskList[i].BagItems.Count; j++)
                {
                    if (TasksHolder.Instance.TaskList[i].BagItems[j].ID.Equals(item.ID))
                    {
                        TasksHolder.Instance.TaskList[i].BagItems[j].ItemName = name;
                        TasksHolder.Instance.TaskList[i].BagItems[j].ImgPath_Item = imgPath;
                    }
                }
                for (int j = 0; j < TasksHolder.Instance.TaskList[i].FamiliarBagItems.Count; j++)
                {
                    if (TasksHolder.Instance.TaskList[i].FamiliarBagItems[j].ID.Equals(item.ID))
                    {
                        TasksHolder.Instance.TaskList[i].FamiliarBagItems[j].ItemName = name;
                        TasksHolder.Instance.TaskList[i].FamiliarBagItems[j].ImgPath_Item = imgPath;
                    }
                }
            }
        }

        private void updateAllEquip(string name, string imgPath)
        {
            switch (listType)
            {
                case 1:     //weapon
                    for (int i = 0; i < TasksHolder.Instance.TaskList.Count; i++)
                    {
                        if (TasksHolder.Instance.TaskList[i].Weapon != null)
                        {
                            if (TasksHolder.Instance.TaskList[i].Weapon.ID.Equals(equip.ID))
                            {
                                TasksHolder.Instance.TaskList[i].Weapon.ItemName = name;
                                TasksHolder.Instance.TaskList[i].Weapon.ImgPath_Item = imgPath;
                            }
                        }
                    }
                    break;
                case 2:     //offhand
                    for (int i = 0; i < TasksHolder.Instance.TaskList.Count; i++)
                    {
                        if (TasksHolder.Instance.TaskList[i].OffHand != null)
                        {
                            if (TasksHolder.Instance.TaskList[i].OffHand.ID.Equals(equip.ID))
                            {
                                TasksHolder.Instance.TaskList[i].OffHand.ItemName = name;
                                TasksHolder.Instance.TaskList[i].OffHand.ImgPath_Item = imgPath;
                            }
                        }
                    }
                    break;
                case 3:     //helm
                    for (int i = 0; i < TasksHolder.Instance.TaskList.Count; i++)
                    {
                        if (TasksHolder.Instance.TaskList[i].Helm != null)
                        {
                            if (TasksHolder.Instance.TaskList[i].Helm.ID.Equals(equip.ID))
                            {
                                TasksHolder.Instance.TaskList[i].Helm.ItemName = name;
                                TasksHolder.Instance.TaskList[i].Helm.ImgPath_Item = imgPath;
                            }
                        }
                    }
                    break;
                case 4:     //chest
                    for (int i = 0; i < TasksHolder.Instance.TaskList.Count; i++)
                    {
                        if (TasksHolder.Instance.TaskList[i].Torso != null)
                        {
                            if (TasksHolder.Instance.TaskList[i].Torso.ID.Equals(equip.ID))
                            {
                                TasksHolder.Instance.TaskList[i].Torso.ItemName = name;
                                TasksHolder.Instance.TaskList[i].Torso.ImgPath_Item = imgPath;
                            }
                        }
                    }
                    break;
                case 5:     //leg
                    for (int i = 0; i < TasksHolder.Instance.TaskList.Count; i++)
                    {
                        if (TasksHolder.Instance.TaskList[i].Legs != null)
                        {
                            if (TasksHolder.Instance.TaskList[i].Legs.ID.Equals(equip.ID))
                            {
                                TasksHolder.Instance.TaskList[i].Legs.ItemName = name;
                                TasksHolder.Instance.TaskList[i].Legs.ImgPath_Item = imgPath;
                            }
                        }
                    }
                    break;
                case 6:     //boot
                    for (int i = 0; i < TasksHolder.Instance.TaskList.Count; i++)
                    {
                        if (TasksHolder.Instance.TaskList[i].Boots != null)
                        {
                            if (TasksHolder.Instance.TaskList[i].Boots.ID.Equals(equip.ID))
                            {
                                TasksHolder.Instance.TaskList[i].Boots.ItemName = name;
                                TasksHolder.Instance.TaskList[i].Boots.ImgPath_Item = imgPath;
                            }
                        }
                    }
                    break;
                case 7:     //glove
                    for (int i = 0; i < TasksHolder.Instance.TaskList.Count; i++)
                    {
                        if (TasksHolder.Instance.TaskList[i].Glove != null)
                        {
                            if (TasksHolder.Instance.TaskList[i].Glove.ID.Equals(equip.ID))
                            {
                                TasksHolder.Instance.TaskList[i].Glove.ItemName = name;
                                TasksHolder.Instance.TaskList[i].Glove.ImgPath_Item = imgPath;
                            }
                        }
                    }
                    break;
                case 8:     //amulet
                    for (int i = 0; i < TasksHolder.Instance.TaskList.Count; i++)
                    {
                        if (TasksHolder.Instance.TaskList[i].Amulet != null)
                        {
                            if (TasksHolder.Instance.TaskList[i].Amulet.ID.Equals(equip.ID))
                            {
                                TasksHolder.Instance.TaskList[i].Amulet.ItemName = name;
                                TasksHolder.Instance.TaskList[i].Amulet.ImgPath_Item = imgPath;
                            }
                        }
                    }
                    break;
                case 9:     //ring
                    for (int i = 0; i < TasksHolder.Instance.TaskList.Count; i++)
                    {
                        if (TasksHolder.Instance.TaskList[i].Ring != null)
                        {
                            if (TasksHolder.Instance.TaskList[i].Ring.ID.Equals(equip.ID))
                            {
                                TasksHolder.Instance.TaskList[i].Ring.ItemName = name;
                                TasksHolder.Instance.TaskList[i].Ring.ImgPath_Item = imgPath;
                            }
                        }
                    }
                    break;
                case 10:    //back
                    for (int i = 0; i < TasksHolder.Instance.TaskList.Count; i++)
                    {
                        if (TasksHolder.Instance.TaskList[i].Back != null)
                        {
                            if (TasksHolder.Instance.TaskList[i].Back.ID.Equals(equip.ID))
                            {
                                TasksHolder.Instance.TaskList[i].Back.ItemName = name;
                                TasksHolder.Instance.TaskList[i].Back.ImgPath_Item = imgPath;
                            }
                        }
                    }
                    break;
                case 11:    //ammo
                    for (int i = 0; i < TasksHolder.Instance.TaskList.Count; i++)
                    {
                        if (TasksHolder.Instance.TaskList[i].Ammo != null)
                        {
                            if (TasksHolder.Instance.TaskList[i].Ammo.ID.Equals(equip.ID))
                            {
                                TasksHolder.Instance.TaskList[i].Ammo.ItemName = name;
                                TasksHolder.Instance.TaskList[i].Ammo.ImgPath_Item = imgPath;
                            }
                        }
                    }
                    break;
                case 12:    //pocket
                    for (int i = 0; i < TasksHolder.Instance.TaskList.Count; i++)
                    {
                        if (TasksHolder.Instance.TaskList[i].Pocket != null)
                        {
                            if (TasksHolder.Instance.TaskList[i].Pocket.ID.Equals(equip.ID))
                            {
                                TasksHolder.Instance.TaskList[i].Pocket.ItemName = name;
                                TasksHolder.Instance.TaskList[i].Pocket.ImgPath_Item = imgPath;
                            }
                        }
                    }
                    break;
                case 13:    //aura
                    for (int i = 0; i < TasksHolder.Instance.TaskList.Count; i++)
                    {
                        if (TasksHolder.Instance.TaskList[i].Aura != null)
                        {
                            if (TasksHolder.Instance.TaskList[i].Aura.ID.Equals(equip.ID))
                            {
                                TasksHolder.Instance.TaskList[i].Aura.ItemName = name;
                                TasksHolder.Instance.TaskList[i].Aura.ImgPath_Item = imgPath;
                            }
                        }
                    }
                    break;
                case 14:    //sigil
                    for (int i = 0; i < TasksHolder.Instance.TaskList.Count; i++)
                    {
                        if (TasksHolder.Instance.TaskList[i].Sigil != null)
                        {
                            if (TasksHolder.Instance.TaskList[i].Sigil.ID.Equals(equip.ID))
                            {
                                TasksHolder.Instance.TaskList[i].Sigil.ItemName = name;
                                TasksHolder.Instance.TaskList[i].Sigil.ImgPath_Item = imgPath;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
