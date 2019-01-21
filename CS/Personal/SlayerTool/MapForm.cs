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
    public partial class MapForm : Form
    {
        private Task task;

        public MapForm(Task task)
        {
            InitializeComponent();

            this.task = task;
        }

        private void MapForm_Load(object sender, EventArgs e)
        {
            if (task.ImgPath_Map != null)
            {
                pictureBox1.Load(task.ImgPath_Map);
            }
        }

        private void MapForm_Deactivate(object sender, EventArgs e)
        {
            //Close();
        }
    }
}
