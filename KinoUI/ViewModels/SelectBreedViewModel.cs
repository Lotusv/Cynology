using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Windows.Media.Imaging;
using KinoDataLibrary;

namespace KinoUI.ViewModels
{
    class SelectBreedViewModel: INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members

        /// <summary>
        /// Raises the 'PropertyChanged' event when the value of a property of the view model has changed.
        /// </summary>
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        /// <summary>
        /// 'PropertyChanged' event that is raised when the value of a property of the view model has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        KinoDataSet _kinodataset;
        public SelectBreedViewModel(KinoDataSet ds)
        {
            _kinodataset = ds;
        }

        public KinoDataSet DataSet { get; set; }
        Int32 _selectedBreedID;

        public Int32 SelectedBreedID
        {
            get
            {
                return _selectedBreedID;
            }
            set
            {
                if (_selectedBreedID == value)
                {
                    return;
                }
                _selectedBreedID = value;
                OnPropertyChanged("SelectedBreedID");
            }
        }

    }
}
