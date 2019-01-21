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
    public partial class FamiliarInventoryForm : Form
    {
        private List<PictureBox> bagFamiliarPictureBoxes = new List<PictureBox>();
        private Task task;

        public FamiliarInventoryForm(Task task)
        {
            InitializeComponent();

            this.task = task;
        }

        private void FamiliarInventoryForm_Load(object sender, EventArgs e)
        {
            foreach (PictureBox img in flowLayoutPanel5.Controls)
            {
                bagFamiliarPictureBoxes.Add(img);
            }

            display();
        }

        private void display()
        {
            imgFamiliar.Load(task.Familiar.ImgPath_Familiar);

            for (int i = 0; i < bagFamiliarPictureBoxes.Count; i++)
            {
                for (int j = 0; j < task.FamiliarBagItems.Count; j++)
                {
                    if (task.FamiliarBagItems[j].BagSlotIndex == (i))
                    {
                        bagFamiliarPictureBoxes[i].Load(task.FamiliarBagItems[j].ImgPath_Item);
                        toolTip1.SetToolTip(bagFamiliarPictureBoxes[i], task.FamiliarBagItems[j].ItemName);
                        break;
                    }
                }
            }

            for (int i = 0; i < task.Familiar.InventorySize; i++)
            {
                bagFamiliarPictureBoxes[i].Visible = true;
            }
        }

        private void FamiliarInventoryForm_Deactivate(object sender, EventArgs e)
        {
            //Close();
        }
    }
}
