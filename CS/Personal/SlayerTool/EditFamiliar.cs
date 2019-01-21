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
    public partial class EditFamiliar : Form
    {
        private string imageFamiliar;
        private string oldName;
        private ListBox lstLibrary;
        private Familiar familiar;

        //create constructor
        public EditFamiliar(ListBox list)
        {
            InitializeComponent();

            lstLibrary = list;

            btnDone.Enabled = true;
            btnDone.Visible = true;
        }

        //edit constructor
        public EditFamiliar(ListBox list, Familiar familiar)
        {
            InitializeComponent();

            lstLibrary = list;
            this.familiar = familiar;

            setupEdit();
        }

        private void setupEdit()
        {
            btnUpdate.Enabled = true;
            btnUpdate.Visible = true;

            txtName.Text = familiar.FamiliarName;
            oldName = familiar.FamiliarName;

            imgFamiliar.Load(familiar.ImgPath_Familiar);
            imageFamiliar = familiar.ImgPath_Familiar;

            familiarSize.Value = familiar.InventorySize;
        }

        private void imgFamiliar_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                imageFamiliar = openFileDialog1.FileName;
                try
                {
                    imgFamiliar.Image = Image.FromFile(imageFamiliar);
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

            if (imageFamiliar == null)
            {
                MessageBox.Show("Must have image!", "Error creating", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            for (int i = 0; i < FamiliarHolder.Instance.FamiliarList.Count; i++)
            {
                if (txtName.Text.Equals(FamiliarHolder.Instance.FamiliarList[i].FamiliarName))
                {
                    MessageBox.Show("Familiar already exists!", "Error creating", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            Familiar familiar = new Familiar(txtName.Text);
            Guid guid = Guid.NewGuid();
            familiar.ID = guid.ToString();
            familiar.ImgPath_Familiar = imageFamiliar;
            familiar.InventorySize = (int)familiarSize.Value;
            FamiliarHolder.Instance.FamiliarList.Add(familiar);

            //save to file
            FileHandler.SaveNewFamiliar(familiar);

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

            if (imageFamiliar == null)
            {
                MessageBox.Show("Must have image!", "Error creating", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string familiarName = checkName();
            if (familiarName != null)
            {
                familiar.FamiliarName = familiarName;
            }

            familiar.ImgPath_Familiar = imageFamiliar;
            familiar.InventorySize = (int)familiarSize.Value;

            updateAllItems(familiar.FamiliarName, familiar.ImgPath_Familiar, familiar.InventorySize);

            FileHandler.EditFamiliar(familiar);

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

        private void updateAllItems(string name, string imgPath, int size)
        {
            for (int i = 0; i < TasksHolder.Instance.TaskList.Count; i++)
            {
                if (TasksHolder.Instance.TaskList[i].Familiar != null)
                {
                    if (TasksHolder.Instance.TaskList[i].Familiar.ID.Equals(familiar.ID))
                    {
                        TasksHolder.Instance.TaskList[i].Familiar.FamiliarName = name;
                        TasksHolder.Instance.TaskList[i].Familiar.ImgPath_Familiar = imgPath;
                        TasksHolder.Instance.TaskList[i].Familiar.InventorySize = size;
                    }
                }
            }
        }
    }
}
