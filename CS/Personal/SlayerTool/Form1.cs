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
    public partial class Form1 : Form
    {
        private List<PictureBox> bagPictureBoxes = new List<PictureBox>();
        private List<PictureBox> abilityPictureBoxes = new List<PictureBox>();
        private List<PictureBox> prayerPictureBoxes = new List<PictureBox>();
        private string url;

        public Form1()
        {
            InitializeComponent();

            FileHandler.loadItems();
            FileHandler.loadEquips();
            FileHandler.loadFamiliars();
            FileHandler.loadEquipPresets();
            FileHandler.loadAbilityPresets();

            FileHandler.loadTasks();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (PictureBox img in flowLayoutPanel1.Controls)
            {
                bagPictureBoxes.Add(img);
            }

            foreach (PictureBox img in flowLayoutPanel4.Controls)
            {
                abilityPictureBoxes.Add(img);
            }

            foreach (PictureBox img in flowLayoutPanel2.Controls)
            {
                prayerPictureBoxes.Add(img);
            }

            foreach (Task task in TasksHolder.Instance.TaskList)
            {
                cmbSearch.Items.Add(task.Name);
            }
        }

        private void cmbSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            toolTip1.RemoveAll();
            resetAll();

            int index = cmbSearch.SelectedIndex;

            if (index < 0)
            {
                return;
            }

            //get current task
            Task task = TasksHolder.Instance.TaskList[index];

            //general display
            lblTest.Text = task.Name;
            imgMonster.Load(task.ImgPath_Monster);

            lblInfo.Text = task.AdditionalInfo;
            url = task.Youtube_guide;

            //bag display
            for (int i = 0; i < bagPictureBoxes.Count; i++)
            {
                for (int j = 0; j < task.BagItems.Count; j++)
                {
                    if (task.BagItems[j].BagSlotIndex == (i))
                    {
                        bagPictureBoxes[i].Load(task.BagItems[j].ImgPath_Item);
                        toolTip1.SetToolTip(bagPictureBoxes[i], task.BagItems[j].ItemName);
                        break;
                    }
                }
            }

            //familiar display
            if (task.Familiar != null)
            {
                imgFamiliar.Load(task.Familiar.ImgPath_Familiar);
            }        
            
            //equips
            setEquipImage(task.Weapon, imgWeapon);
            setEquipImage(task.OffHand, imgOffhand);
            setEquipImage(task.Helm, imgHelm);
            setEquipImage(task.Torso, imgChest);
            setEquipImage(task.Legs, imgPants);
            setEquipImage(task.Boots, imgBoots);
            setEquipImage(task.Glove, imgGlove);
            setEquipImage(task.Amulet, imgAmulet);
            setEquipImage(task.Ring, imgRing);
            setEquipImage(task.Back, imgBack);
            setEquipImage(task.Ammo, imgAmmo);
            setEquipImage(task.Pocket, imgPocket);
            setEquipImage(task.Aura, imgAura);
            setEquipImage(task.Sigil, imgSigil);

            //abilitys
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


            //prayer display
            for (int i = 0; i < task.Prayers.Count; i++)
            {
                prayerPictureBoxes[i].Visible = true;
                prayerPictureBoxes[i].Load(task.Prayers[i].ImgPath_Prayer);
            }
        }

        private void resetAll()
        {
            lblTest.Text = null;
            imgMonster.Image = null;
            lblInfo.Text = null;
            url = null;
            imgFamiliar.Image = null;

            resetBagImages();
            resetEquipImages();
            resetAbilityImages();
            resetPrayerImages();
        }

        private void resetBagImages()
        {
            for (int i = 0; i < bagPictureBoxes.Count; i++)
            {
                bagPictureBoxes[i].Image = null;
            }
        }

        private void resetAbilityImages()
        {
            for (int i = 0; i < abilityPictureBoxes.Count; i++)
            {
                abilityPictureBoxes[i].Image = null;
            }
        }

        private void resetPrayerImages()
        {
            for (int i = 0; i < prayerPictureBoxes.Count; i++)
            {
                prayerPictureBoxes[i].Image = null;
                prayerPictureBoxes[i].Visible = false;
            }
        }

        private void resetEquipImages()
        {
            imgWeapon.Visible = false;
            imgOffhand.Visible = false;
            imgHelm.Visible = false;
            imgChest.Visible = false;
            imgPants.Visible = false;
            imgBoots.Visible = false;
            imgGlove.Visible = false;
            imgRing.Visible = false;
            imgAmulet.Visible = false;
            imgAmmo.Visible = false;
            imgAura.Visible = false;
            imgBack.Visible = false;
            imgPocket.Visible = false;
            imgSigil.Visible = false;
        }

        private void setAbilityImage(Ability slot, PictureBox img)
        {
            if (slot != null)
            {
                img.Load(slot.ImgPath_Ability);
            }
        }

        private void setEquipImage(EquipItem equip, PictureBox img)
        {
            if (equip != null)
            {
                img.Load(equip.ImgPath_Item);
                toolTip1.SetToolTip(img, equip.ItemName);
                img.Visible = true;
            }
        }

        //display familiar inventory
        private void imgFamiliar_Click(object sender, EventArgs e)
        {
            int index = cmbSearch.SelectedIndex;

            if (index < 0)
            {
                return;
            }

            //get current task
            Task task = TasksHolder.Instance.TaskList[index];

            if (task.Familiar == null)
            {
                return;
            }

            if (task.Familiar.InventorySize == 0)
            {
                return;
            }

            FamiliarInventoryForm frm = new FamiliarInventoryForm(task);
            frm.StartPosition = FormStartPosition.CenterScreen;
            if (Application.OpenForms[frm.Name] == null)
            {
                frm.Show();
            }
            else
            {
                Application.OpenForms[frm.Name].Activate();
            }
        }

        private void imgMap_Click(object sender, EventArgs e)
        {
            int index = cmbSearch.SelectedIndex;

            if (index < 0)
            {
                return;
            }

            //get current task
            Task task = TasksHolder.Instance.TaskList[index];

            if (task.ImgPath_Map == null)
            {
                return;
            }

            MapForm frm = new MapForm(task);
            frm.StartPosition = FormStartPosition.CenterScreen;
            if (Application.OpenForms[frm.Name] == null)
            {
                frm.Show();
            }
            else
            {
                Application.OpenForms[frm.Name].Activate();
            }
        }

        private void addNewTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewTaskForm frm = new NewTaskForm(this.cmbSearch);
            frm.StartPosition = FormStartPosition.CenterScreen;
            if (Application.OpenForms[frm.Name] == null)
            {
                frm.ShowDialog();
            }
            else
            {
                Application.OpenForms[frm.Name].Activate();
            }

            //force update
            cmbSearch.SelectedIndex = -1;
            cmbSearch.SelectedIndex = frm.updatedIndex();
        }

        private void lblGuide_LinkClicked(object sender, EventArgs e)
        {
            try
            {
                if (url == null)
                {
                    MessageBox.Show("No youtube guide set.");
                    return;
                }

                System.Diagnostics.Process.Start(url);
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid youtube link.");
            }
        }

        private void editTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = cmbSearch.SelectedIndex;
 
            if (index > -1)
            {
                //get current task
                Task task = TasksHolder.Instance.TaskList[index];

                NewTaskForm frm = new NewTaskForm(this.cmbSearch, task);
                frm.StartPosition = FormStartPosition.CenterScreen;
                if (Application.OpenForms[frm.Name] == null)
                {
                    frm.ShowDialog();
                }
                else
                {
                    Application.OpenForms[frm.Name].Activate();
                }

                //force update
                cmbSearch.SelectedIndex = -1;
                cmbSearch.SelectedIndex = frm.updatedIndex();
            }
            else
            {
                MessageBox.Show("No current task selected to edit.");
            }
        }

        private void deleteTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cmbSearch.SelectedIndex > -1)
            {
                DialogResult result = MessageBox.Show("Confirm delete of this task?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes)
                {
                    FileHandler.DeleteTask(TasksHolder.Instance.TaskList[cmbSearch.SelectedIndex]); //delete from file
                    TasksHolder.Instance.TaskList.RemoveAt(cmbSearch.SelectedIndex); //remove from task library

                    //remove from display
                    cmbSearch.Items.RemoveAt(cmbSearch.SelectedIndex);

                    cmbSearch.SelectedIndex = -1;
                    cmbSearch.SelectedText = null;
                    toolTip1.RemoveAll();
                    resetAll();
                }
            }
            else
            {
                MessageBox.Show("No current task selected to delete.");
            }
        }

        private void cmbSearch_Enter(object sender, EventArgs e)
        {
            cmbSearch.SelectAll();
        }

        /*
         * EDIT FORMS====================================================================
         */
        private void editItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditLibrary frm = new EditLibrary(0);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Text = "Edit Items";
            frm.ShowDialog();
        }

        private void editWeaponsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditLibrary frm = new EditLibrary(1);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Text = "Edit Weapons";
            frm.ShowDialog();
        }

        private void editOffhandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditLibrary frm = new EditLibrary(2);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Text = "Edit Offhands";
            frm.ShowDialog();
        }

        private void editHelmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditLibrary frm = new EditLibrary(3);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Text = "Edit Helms";
            frm.ShowDialog();
        }

        private void editTorsoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditLibrary frm = new EditLibrary(4);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Text = "Edit Torsos";
            frm.ShowDialog();
        }

        private void editLegsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditLibrary frm = new EditLibrary(5);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Text = "Edit Legs";
            frm.ShowDialog();
        }

        private void editBootsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditLibrary frm = new EditLibrary(6);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Text = "Edit Boots";
            frm.ShowDialog();
        }

        private void editGlovesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditLibrary frm = new EditLibrary(7);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Text = "Edit Gloves";
            frm.ShowDialog();
        }

        private void editAmuletToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditLibrary frm = new EditLibrary(8);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Text = "Edit Amulets";
            frm.ShowDialog();
        }

        private void editRingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditLibrary frm = new EditLibrary(9);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Text = "Edit Rings";
            frm.ShowDialog();
        }

        private void editBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditLibrary frm = new EditLibrary(10);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Text = "Edit Backs";
            frm.ShowDialog();
        }

        private void editAmmoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditLibrary frm = new EditLibrary(11);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Text = "Edit Ammo";
            frm.ShowDialog();
        }

        private void editPocketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditLibrary frm = new EditLibrary(12);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Text = "Edit Pocket";
            frm.ShowDialog();
        }

        private void editAurasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditLibrary frm = new EditLibrary(13);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Text = "Edit Auras";
            frm.ShowDialog();
        }

        private void editSigilsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditLibrary frm = new EditLibrary(14);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Text = "Edit Sigils";
            frm.ShowDialog();
        }

        private void editFamiliarsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditLibrary frm = new EditLibrary(15);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Text = "Edit Familiars";
            frm.ShowDialog();
        }

        private void editEquipmentPresetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditLibrary frm = new EditLibrary(16);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Text = "Edit Equip Presets";
            frm.ShowDialog();
        }

        private void editAbilityBarPresetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditLibrary frm = new EditLibrary(17);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Text = "Edit Ability Bar Presets";
            frm.ShowDialog();
        }
    }
}
