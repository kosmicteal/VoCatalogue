using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace VoCatalogue
{

        /// <summary>
        /// Interaction logic for VocaList.xaml
        /// </summary>
        public partial class VocaList : Window
    {
        //private ObservableCollection<MyDataObject> _items = new ObservableCollection<MyDataObject>();
        public CollectionView view;

        //Loading the main window without a button
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(System.IntPtr hWnd, int nIndex);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetWindowLong(System.IntPtr hWnd, int nIndex, int dwNewLong);


        public VocaList()
        {
            InitializeComponent();

            lvUsers.ItemsSource = VoCatalogue.ProductData.GetList();

            view = (CollectionView)CollectionViewSource.GetDefaultView(lvUsers.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Category");
            if (view.GroupDescriptions.Count() == 0) view.GroupDescriptions.Add(groupDescription);

        }

        /// <summary>
        /// Update JSON when closing window
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ProductData.ExportJSON();
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        /// <summary>
        /// Allow to change data
        /// </summary>
        private void lvUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            instAdr.IsEnabled = true;
            actCode.IsEnabled = true;

            instAdr.Text = ((VoiceBank)((ListView)sender).SelectedItem).Link;
            actCode.Text = ((VoiceBank)((ListView)sender).SelectedItem).ActivationCode;

        }

        /// <summary>
        /// Apply changes into local list
        /// </summary>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string toChange = ((VoiceBank)(lvUsers).SelectedItem).Reference;
            ProductData.ModifyList(toChange, actCode.Text, instAdr.Text);
            view.Refresh();
        }


        /// <summary>
        /// Loads window without a button
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }

        /// <summary>
        /// Refresh EXE/ACTIVATION information on voicebank
        /// </summary>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string toChange = ((VoiceBank)(lvUsers).SelectedItem).Reference;
            ProductData.ModifyList(toChange, "", "");
            view.Refresh();
        }


    }
}
