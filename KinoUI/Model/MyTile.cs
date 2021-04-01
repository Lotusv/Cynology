using System;
using System.Windows.Media;
using Telerik.Windows.Controls;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
//using System.Windows.UI.Xaml;

namespace KinoUI.Model
{
    public class MyTile :  ViewModelBase
    {

        private string _DisplayName;
        public string DisplayName
        {
            get
            {
                return this._DisplayName;
            }
            set
            {
                if (value != this._DisplayName)
                {
                    this._DisplayName = value;
                    this.OnPropertyChanged(() => this.DisplayName);
                }
            }
        }

        private ImageSource _Photo;
        public ImageSource Photo
        {
            get
            {
                return this._Photo;
            }
            set
            {
                if ((object)this._Photo != (object)value)
                {
                    this._Photo = value;
                    this.OnPropertyChanged(() => this.Photo);
                }
            }
        }

        private int _Width;
        public int Width
        {
            get
            {
                return this._Width;
            }
            set
            {
                if ((object)this._Width != (object)value)
                {
                    this._Width = value;
                    this.OnPropertyChanged(() => this.Width);
                }
            }
        }

        private int _Height;
        public int Height
        {
            get
            {
                return this._Height;
            }
            set
            {
                if ((object)this._Height != (object)value)
                {
                    this._Height = value;
                    this.OnPropertyChanged(() => this.Height);
                }
            }
        }

        private UserControl _userCOntrol;
        public UserControl UserControl 
        {
            get
            {
                return this._userCOntrol;
            }
            set
            {
                if ((object)this._userCOntrol != (object)value)
                {
                    this._userCOntrol = value;
                    //this.Content = value;
                    this.OnPropertyChanged(() => this.UserControl);
                }
            }
        }
    }
}
