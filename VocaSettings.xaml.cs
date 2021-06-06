using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VoCatalogue
{
    /// <summary>
    /// Interaction logic for VocaSettings.xaml
    /// </summary>
    public partial class VocaSettings : Window
    {
        public VocaSettings()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Save Settings UI when closing window
        /// </summary>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            Properties.Settings.Default.ai_tonio_v2 = (bool)alter_toniov2.IsChecked;
            Properties.Settings.Default.ai_prima_v2 = (bool)alter_primav2.IsChecked;
            Properties.Settings.Default.ai_sonika_v2 = (bool)alter_sonikav2.IsChecked;
            Properties.Settings.Default.ai_bigal_v2 = (bool)alter_bigalv2.IsChecked;
            Properties.Settings.Default.ai_sweetann_v2 = (bool)alter_sweetannv2.IsChecked;
            Properties.Settings.Default.language = SoftLang.SelectedIndex;

            Properties.Settings.Default.Save();

        }

        /// <summary>
        /// Load Settings UI
        /// </summary>
        private void Window_Initialized(object sender, EventArgs e)
        {
            alter_toniov2.IsChecked = Properties.Settings.Default.ai_tonio_v2;
            alter_primav2.IsChecked = Properties.Settings.Default.ai_prima_v2;
            alter_sonikav2.IsChecked = Properties.Settings.Default.ai_sonika_v2;
            alter_bigalv2.IsChecked = Properties.Settings.Default.ai_bigal_v2;
            alter_sweetannv2.IsChecked = Properties.Settings.Default.ai_sweetann_v2;

            SoftLang.SelectedIndex = Properties.Settings.Default.language;
        }

        /// <summary>
        /// Generic navigation system
        /// </summary>
        private void GeneralRequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.ToString());
        }


    }
}
