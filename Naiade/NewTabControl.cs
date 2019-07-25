using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Naiade
{
    public partial class NewTabControl : Component
    {
        public NewTabControl()
        {
            InitializeComponent();
        }

        public NewTabControl(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
