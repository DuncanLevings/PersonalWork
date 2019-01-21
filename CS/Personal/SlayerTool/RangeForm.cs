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
    public partial class RangeForm : Form
    {
        private PictureBox clickedAbilityPicture;
        private Image ability;
        private Object path;

        public RangeForm()
        {
            InitializeComponent();
        }

        public Image Ability { get => ability; }
        public Object Path { get => path; }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            clickedAbilityPicture = sender as PictureBox;
            ability = clickedAbilityPicture.Image;
            path = clickedAbilityPicture.Tag;
            Close();
        }
    }
}
