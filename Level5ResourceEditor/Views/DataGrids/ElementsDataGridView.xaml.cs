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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Level5ResourceEditor.ViewModels.Editor;

namespace Level5ResourceEditor.Views.DataGrids
{
    /// <summary>
    /// Logique d'interaction pour ElementsDataGridView.xaml
    /// </summary>
    public partial class ElementsDataGridView : UserControl
    {
        public ElementsDataGridView()
        {
            InitializeComponent();
        }

        private void DataGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            // We retrieve the ViewModel
            if (DataContext is Level5ResourceViewModel viewModel)
            {
                // We ask the ViewModel to create the right type of object (XRESTextureData, MaterialData, etc.)
                var newItem = viewModel.CreateItemForCurrentContext();

                // We tell the DataGrid to use this specific object
                e.NewItem = newItem;
            }
        }
    }
}
