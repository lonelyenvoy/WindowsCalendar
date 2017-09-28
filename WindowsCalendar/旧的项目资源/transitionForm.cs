using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsCalendar
{
    public partial class TransitionForm : Form
    {
        public TransitionForm()
        {
            InitializeComponent();
        }

        private void TransitionForm_Load(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.BackgroundImage = WindowsCalendar.Properties.Resources.background;
        }
    }
}
