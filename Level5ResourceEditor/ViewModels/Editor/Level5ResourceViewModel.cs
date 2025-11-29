using System;
using System.IO;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ImaginationGUI.ViewModels;
using StudioElevenLib.Level5.Resource;
using StudioElevenLib.Level5.Resource.RES;
using StudioElevenLib.Level5.Resource.XRES;
using StudioElevenLib.Level5.Resource.Types;
using StudioElevenLib.Level5.Resource.Types.Scene3D;
using Level5ResourceEditor.Models;

namespace Level5ResourceEditor.ViewModels.Editor
{
    public class Level5ResourceViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private ObservableCollection<TypeListViewItem> _typeListViewItems;
        private TypeListViewItem _selectedTypeListViewItem;
        private ObservableCollection<RESElement> _elements;
        private RESElement _selectedElement;
        private object _resource;
        private bool _isXRES;

        public ObservableCollection<TypeListViewItem> TypeListViewItems
        {
            get => _typeListViewItems;
            set
            {
                _typeListViewItems = value;
                OnPropertyChanged(nameof(TypeListViewItems));
            }
        }

        public TypeListViewItem SelectedTypeListViewItem
        {
            get => _selectedTypeListViewItem;
            set
            {
                _selectedTypeListViewItem = value;
                OnPropertyChanged(nameof(SelectedTypeListViewItem));
                UpdateElementsList();
            }
        }

        public ObservableCollection<RESElement> Elements
        {
            get => _elements;
            set
            {
                _elements = value;
                OnPropertyChanged(nameof(Elements));
            }
        }

        public RESElement SelectedElement
        {
            get => _selectedElement;
            set
            {
                _selectedElement = value;
                OnPropertyChanged(nameof(SelectedElement));
            }
        }

        public ICommand AddElementCommand { get; private set; }
        public ICommand DeleteElementCommand { get; private set; }

        public Level5ResourceViewModel()
        {
            TypeListViewItems = new ObservableCollection<TypeListViewItem>();
            Elements = new ObservableCollection<RESElement>();

            AddElementCommand = new RelayCommand(AddElement, _ => SelectedTypeListViewItem != null);
            DeleteElementCommand = new RelayCommand(DeleteElement, _ => SelectedElement != null);

            InitializeEmptyResource();
        }

        private void InitializeEmptyResource()
        {
            _resource = new RES();
            _isXRES = false;

            var res = _resource as RES;
            if (res != null)
            {
                res.Items = new System.Collections.Generic.Dictionary<RESType, System.Collections.Generic.List<RESElement>>();
            }

            RefreshTypeList();
        }

        public void LoadFile(string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLower();

            if (extension == ".xres")
            {
                _resource = new XRES(File.ReadAllBytes(filePath));
                _isXRES = true;
            }
            else
            {
                _resource = new RES(File.ReadAllBytes(filePath));
                _isXRES = false;
            }

            RefreshTypeList();
        }

        private void RefreshTypeList()
        {
            TypeListViewItems.Clear();

            var items = _isXRES
                ? (_resource as XRES)?.Items
                : (_resource as RES)?.Items;

            if (items == null)
                return;

            foreach (var kvp in items)
            {
                string displayName = kvp.Key.ToString();

                // Special case for TextureData
                if (kvp.Key == RESType.TextureData)
                {
                    displayName = _isXRES ? "TextureData (XRES)" : "TextureData (RES)";
                }

                TypeListViewItems.Add(new TypeListViewItem
                {
                    DisplayName = $"{displayName} - {kvp.Value.Count}",
                    Type = kvp.Key,
                    ElementCount = kvp.Value.Count
                });
            }
        }

        private void UpdateElementsList()
        {
            Elements.Clear();

            if (SelectedTypeListViewItem == null)
                return;

            var items = _isXRES
                ? (_resource as XRES)?.Items
                : (_resource as RES)?.Items;

            if (items != null && items.ContainsKey(SelectedTypeListViewItem.Type))
            {
                foreach (var element in items[SelectedTypeListViewItem.Type])
                {
                    Elements.Add(element);
                }
            }
        }

        private void AddElement(object parameter)
        {
            if (SelectedTypeListViewItem == null)
                return;

            var items = _isXRES
                ? (_resource as XRES)?.Items
                : (_resource as RES)?.Items;

            if (items == null)
                return;

            // Create new element based on type
            RESElement newElement = CreateNewElement(SelectedTypeListViewItem.Type);

            if (!items.ContainsKey(SelectedTypeListViewItem.Type))
            {
                items[SelectedTypeListViewItem.Type] = new System.Collections.Generic.List<RESElement>();
            }

            items[SelectedTypeListViewItem.Type].Add(newElement);
            Elements.Add(newElement);

            // Update count
            SelectedTypeListViewItem.ElementCount++;
            SelectedTypeListViewItem.DisplayName = $"{SelectedTypeListViewItem.Type.ToString()} - {SelectedTypeListViewItem.ElementCount}";
        }

        private void DeleteElement(object parameter)
        {
            if (SelectedElement == null || SelectedTypeListViewItem == null)
                return;

            var items = _isXRES
                ? (_resource as XRES)?.Items
                : (_resource as RES)?.Items;

            if (items != null && items.ContainsKey(SelectedTypeListViewItem.Type))
            {
                items[SelectedTypeListViewItem.Type].Remove(SelectedElement);
                Elements.Remove(SelectedElement);

                // Update count
                SelectedTypeListViewItem.ElementCount--;
                SelectedTypeListViewItem.DisplayName = $"{SelectedTypeListViewItem.Type} - {SelectedTypeListViewItem.ElementCount}";
            }
        }

        private RESElement CreateNewElement(RESType type)
        {
            switch (type)
            {
                case RESType.TextureData:
                    if (_isXRES)
                    {
                        return new XRESTextureData
                        {
                            Name = "NewTexture"
                        };
                    }
                    else
                    {
                        return new RESTextureData
                        {
                            Name = "NewTexture"
                        };
                    }

                case RESType.MaterialData:
                    return new ResMaterialData
                    {
                        Name = "NewMaterial",
                        Images = new RESImageEntry[4]
                    };

                default:
                    return new RESElement { Name = $"New{type}" };
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}