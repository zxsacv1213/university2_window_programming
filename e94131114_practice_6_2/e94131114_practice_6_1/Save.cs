using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e94131114_practice_6_1
{
    public partial class Save : Component
    {
        public static int countSave = 0;

        public static List<List<int>> blocksRWTrans = new List<List<int>>();


        public Save()
        {
            InitializeComponent();
        }

        public Save(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
