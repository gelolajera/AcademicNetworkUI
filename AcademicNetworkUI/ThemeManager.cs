using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Windows.Forms;

namespace AcademicNetworkUI
{
    internal class ThemeManager
    {
        private static MaterialSkinManager skinManager;

        public static void ApplyTheme(MaterialForm form)
        {
            if (skinManager == null)
            {
                skinManager = MaterialSkinManager.Instance;
                skinManager.Theme = MaterialSkinManager.Themes.LIGHT;
                skinManager.ColorScheme = new ColorScheme(
                    Primary.Blue600, Primary.Blue700,
                    Primary.Blue500, Accent.LightBlue200, TextShade.WHITE);
            }
            skinManager.AddFormToManage(form);
        }
    }
}
