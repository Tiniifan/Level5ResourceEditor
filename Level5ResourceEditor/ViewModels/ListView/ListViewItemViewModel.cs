using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImaginationGUI.ViewModels;
using System.Windows.Controls;

namespace Level5ResourceEditor.ViewModels.ListView
{
    public class ListViewItemViewModel : BaseViewModel
    {
        private string _header;
        public string Header
        {
            get => _header;
            set => SetProperty(ref _header, value);
        }

        private object _tag;
        public object Tag
        {
            get => _tag;
            set => SetProperty(ref _tag, value);
        }

        private ContextMenu _contextMenu;
        public ContextMenu ContextMenu
        {
            get => _contextMenu;
            set => SetProperty(ref _contextMenu, value);
        }

        public ListViewItemViewModel()
        {
        }
    }
}
