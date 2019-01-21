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
    public partial class EditAbilityBarPreset : Form
    {
        private string oldName;
        private ListBox lstLibrary;
        private AbilityPreset preset;
        private List<PictureBox> abilityPictureBoxes = new List<PictureBox>();
        private PictureBox clickedAbilityPicture;

        public EditAbilityBarPreset(ListBox list)
        {
            InitializeComponent();

            foreach (PictureBox img in flowLayoutPanel4.Controls)
            {
                abilityPictureBoxes.Add(img);
            }

            cmbStyle.Items.Add("Magic");
            cmbStyle.Items.Add("Ranged");
            cmbStyle.Items.Add("Melee");
            cmbStyle.SelectedIndex = 0;

            lstLibrary = list;

            btnDone.Enabled = true;
            btnDone.Visible = true;
        }

        //edit
        public EditAbilityBarPreset(ListBox list, AbilityPreset preset)
        {
            InitializeComponent();

            foreach (PictureBox img in flowLayoutPanel4.Controls)
            {
                abilityPictureBoxes.Add(img);
            }

            cmbStyle.Items.Add("Magic");
            cmbStyle.Items.Add("Ranged");
            cmbStyle.Items.Add("Melee");
            cmbStyle.SelectedIndex = preset.Style;

            lstLibrary = list;
            this.preset = preset;

            setupEdit();
        }

        private void setupEdit()
        {
            btnUpdate.Enabled = true;
            btnUpdate.Visible = true;

            txtName.Text = preset.Name;
            oldName = preset.Name;

            int index = lstLibrary.SelectedIndex;

            setAbilityImage(preset.Slot1, imgSlot1);
            setAbilityImage(preset.Slot2, imgSlot2);
            setAbilityImage(preset.Slot3, imgSlot3);
            setAbilityImage(preset.Slot4, imgSlot4);
            setAbilityImage(preset.Slot5, imgSlot5);
            setAbilityImage(preset.Slot6, imgSlot6);
            setAbilityImage(preset.Slot7, imgSlot7);
            setAbilityImage(preset.Slot8, imgSlot8);
            setAbilityImage(preset.Slot9, imgSlot9);
            setAbilityImage(preset.Slot10, imgSlot10);
            setAbilityImage(preset.Slot11, imgSlot11);
            setAbilityImage(preset.Slot12, imgSlot12);
            setAbilityImage(preset.Slot13, imgSlot13);
            setAbilityImage(preset.Slot14, imgSlot14);
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

            AbilityPreset preset = new AbilityPreset(txtName.Text);

            preset.Slot1 = newAbility(imgSlot1);
            preset.Slot2 = newAbility(imgSlot2);
            preset.Slot3 = newAbility(imgSlot3);
            preset.Slot4 = newAbility(imgSlot4);
            preset.Slot5 = newAbility(imgSlot5);
            preset.Slot6 = newAbility(imgSlot6);
            preset.Slot7 = newAbility(imgSlot7);
            preset.Slot8 = newAbility(imgSlot8);
            preset.Slot9 = newAbility(imgSlot9);
            preset.Slot10 = newAbility(imgSlot10);
            preset.Slot11 = newAbility(imgSlot11);
            preset.Slot12 = newAbility(imgSlot12);
            preset.Slot13 = newAbility(imgSlot13);
            preset.Slot14 = newAbility(imgSlot14);

            preset.Style = cmbStyle.SelectedIndex;
            Guid guid = Guid.NewGuid();
            preset.ID = guid.ToString();

            AbilityPresetHolder.Instance.PresetList.Add(preset);

            //save to file
            FileHandler.SaveNewAbilityPreset(preset);

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

            setAbilityBar();

            updateAllTasks();

            FileHandler.EditAbilityPreset(preset);

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

        private void setAbilityBar()
        {
            preset.Slot1 = setAbility(imgSlot1, preset.Slot1);
            preset.Slot2 = setAbility(imgSlot2, preset.Slot2);
            preset.Slot3 = setAbility(imgSlot3, preset.Slot3);
            preset.Slot4 = setAbility(imgSlot4, preset.Slot4);
            preset.Slot5 = setAbility(imgSlot5, preset.Slot5);
            preset.Slot6 = setAbility(imgSlot6, preset.Slot6);
            preset.Slot7 = setAbility(imgSlot7, preset.Slot7);
            preset.Slot8 = setAbility(imgSlot8, preset.Slot8);
            preset.Slot9 = setAbility(imgSlot9, preset.Slot9);
            preset.Slot10 = setAbility(imgSlot10, preset.Slot10);
            preset.Slot11 = setAbility(imgSlot11, preset.Slot11);
            preset.Slot12 = setAbility(imgSlot12, preset.Slot12);
            preset.Slot13 = setAbility(imgSlot13, preset.Slot13);
            preset.Slot14 = setAbility(imgSlot14, preset.Slot14);

            preset.Style = cmbStyle.SelectedIndex;
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

        private void setAbilityImage(Ability slot, PictureBox img)
        {
            if (slot != null)
            {
                img.Load(slot.ImgPath_Ability);
                img.Tag = slot.ImgPath_Ability;
            }
        }

        //update all tasks using preset
        private void updateAllTasks()
        {
            for (int i = 0; i < TasksHolder.Instance.TaskList.Count; i++)
            {
                if (TasksHolder.Instance.TaskList[i].AbilityPreset == preset)
                {
                    setAbilityBar(TasksHolder.Instance.TaskList[i]);

                    FileHandler.editTaskAbility(TasksHolder.Instance.TaskList[i]);
                }    
            }
        }

        private void pictureBox43_Click(object sender, EventArgs e)
        {
            clickedAbilityPicture = sender as PictureBox;

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
    }
}
