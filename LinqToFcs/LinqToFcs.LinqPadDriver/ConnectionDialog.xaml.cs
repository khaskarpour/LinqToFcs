using LINQPad.Extensibility.DataContext;
using System.IO;
using System.Windows;

namespace LinqToFcs.LinqPadDriver
{
    /// <summary>
    /// Interaction logic for ConnectionDIalog.xaml
    /// </summary>
    public partial class ConnectionDialog : Window
    {
        private readonly IConnectionInfo _cxInfo;
        
        public ConnectionDialog(IConnectionInfo cxInfo)
        {
            InitializeComponent();
            DataContext = _cxInfo = cxInfo;
        }

        private void BrowseFcsFile(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog()
            {
                Title = "Choose Fcs file",
                DefaultExt = ".fcs",
            };

            if (dialog.ShowDialog() != true)
            {
                return;
            }
         
            _cxInfo.DisplayName = Path.GetFileName(dialog.FileName);
            _cxInfo.DatabaseInfo.AttachFile = true;
            _cxInfo.DatabaseInfo.AttachFileName = dialog.FileName;
            _cxInfo.DatabaseInfo.DbVersion = LinqToFcs.Core.SupportedVersions.FCS3.ToString();
            _cxInfo.CustomTypeInfo.CustomAssemblyPath = typeof(LinqToFcs.Core.FcsReader).Assembly.Location;
            _cxInfo.CustomTypeInfo.CustomTypeName = typeof(LinqToFcs.Core.FcsReader).FullName;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
