using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.TransitionControl;

namespace KinoUI.Model
{
    public class TransitionItems : ViewModelBase, ISupportInitialize
    {
        public TransitionItems()
        {
            this.SelectNext = new DelegateCommand(this.SelectNextItem);
            this.currentTransitionIndex = 0;

            this._MyTiles = new ObservableCollection<MyTile>();
            this._Transitions = new ObservableCollection<TransitionSet>();
        }

        private int currentTransitionIndex;

        private void SelectNextItem(object param)
        {
            if (this.MyTiles != null && this.MyTiles.Count() > 1)
            {
                this.SelectTransition();

                int index = this.MyTiles.IndexOf(this.SelectedMyTile);
                index++;
                if (index >= this.MyTiles.Count())
                {
                    index = 0;
                }
                this.SelectedMyTile = this.MyTiles[index];
            }
            
        }

        //public TransitionItems()
        //{
        //    this.SelectNext = new DelegateCommand(this.SelectNextItem);
        //    this.currentTransitionIndex = 0;

        //    this._MyTiles = new ObservableCollection<UIElement>();
        //    this._Transitions = new ObservableCollection<TransitionSet>();
        //}

        



        private void SelectTransition()
        {
            if (this.Transitions != null && this.Transitions.Count() > 0)
            {
                this.currentTransitionIndex++;
                if (this.currentTransitionIndex >= this.Transitions.Count())
                {
                    this.currentTransitionIndex = 0;
                }
                this.CurrentTransition = this.Transitions[this.currentTransitionIndex].Transition;
            }
            else
            {
                this.CurrentTransition = null;
            }
        }

        private TransitionProvider _CurrentTransition;
        public TransitionProvider CurrentTransition
        {
            get
            {
                return this._CurrentTransition;
            }
            private set
            {
                if (value != this._CurrentTransition)
                {
                    this._CurrentTransition = value;
                    this.OnPropertyChanged("CurrentTransition");
                }
            }
        }

        private ICommand _SelectNext;
        public ICommand SelectNext
        {
            get
            {
                return this._SelectNext;
            }
            private set
            {
                this._SelectNext = value;
            }
        }


        private MyTile _SelectedMyTile;
        public MyTile SelectedMyTile
        {
            get
            {
                return this._SelectedMyTile;
            }
            set
            {
                if ((object)this._SelectedMyTile != value)
                {
                    this._SelectedMyTile = value;
                    this.OnPropertyChanged("SelectedMyTile");
                }
            }
        }

        //private UIElement _SelectedMyTile;
        //public UIElement SelectedMyTile
        //{
        //    get
        //    {
        //        return this._SelectedMyTile;
        //    }
        //    set
        //    {
        //        if ((object)this._SelectedMyTile != value)
        //        {
        //            this._SelectedMyTile = value;
        //            this.OnPropertyChanged("SelectedMyTile");
        //        }
        //    }
        //}


        private ObservableCollection<TransitionSet> _Transitions;
        public ObservableCollection<TransitionSet> Transitions
        {
            get
            {
                return this._Transitions;
            }
        }

        private ObservableCollection<MyTile> _MyTiles;
        public ObservableCollection<MyTile> MyTiles
        {
            get
            {
                return this._MyTiles;
            }
        }

        //private ObservableCollection<UIElement> _MyTiles;
        //public ObservableCollection<UIElement> MyTiles
        //{
        //    get
        //    {
        //        return this._MyTiles;
        //    }
        //}

        public void BeginInit()
        {
        }

        public void EndInit()
        {
            if ((object)this.MyTiles != null && this.MyTiles.Count() > 0)
            {
                this.SelectedMyTile = this.MyTiles.FirstOrDefault();
            }
        }
    }


    //public class TransitionItems : ViewModelBase, ISupportInitialize
    //{
    //    public TransitionItems()
    //    {
    //        this.SelectNext = new DelegateCommand(this.SelectNextItem);
    //        this.currentTransitionIndex = 0;

    //        this._MyTiles = new ObservableCollection<MyTile>();
    //        this._Transitions = new ObservableCollection<TransitionSet>();
    //    }

    //    private int currentTransitionIndex;

    //    private void SelectNextItem(object param)
    //    {
    //        if (this.MyTiles != null && this.MyTiles.Count() > 1)
    //        {
    //            this.SelectTransition();

    //            int index = this.MyTiles.IndexOf(this.SelectedMyTile);
    //            index++;
    //            if (index >= this.MyTiles.Count())
    //            {
    //                index = 0;
    //            }
    //            this.SelectedMyTile = this.MyTiles[index];
    //        }
    //    }


    //    private void SelectTransition()
    //    {
    //        if (this.Transitions != null && this.Transitions.Count() > 0)
    //        {
    //            this.currentTransitionIndex++;
    //            if (this.currentTransitionIndex >= this.Transitions.Count())
    //            {
    //                this.currentTransitionIndex = 0;
    //            }
    //            this.CurrentTransition = this.Transitions[this.currentTransitionIndex].Transition;
    //        }
    //        else
    //        {
    //            this.CurrentTransition = null;
    //        }
    //    }

    //    private TransitionProvider _CurrentTransition;
    //    public TransitionProvider CurrentTransition
    //    {
    //        get
    //        {
    //            return this._CurrentTransition;
    //        }
    //        private set
    //        {
    //            if (value != this._CurrentTransition)
    //            {
    //                this._CurrentTransition = value;
    //                this.OnPropertyChanged("CurrentTransition");
    //            }
    //        }
    //    }

    //    private ICommand _SelectNext;
    //    public ICommand SelectNext
    //    {
    //        get
    //        {
    //            return this._SelectNext;
    //        }
    //        private set
    //        {
    //            this._SelectNext = value;
    //        }
    //    }


    //    private MyTile _SelectedMyTile;
    //    public MyTile SelectedMyTile
    //    {
    //        get
    //        {
    //            return this._SelectedMyTile;
    //        }
    //        set
    //        {
    //            if ((object)this._SelectedMyTile != value)
    //            {
    //                this._SelectedMyTile = value;
    //                this.OnPropertyChanged("SelectedMyTile");
    //            }
    //        }
    //    }

     

    //    private ObservableCollection<TransitionSet> _Transitions;
    //    public ObservableCollection<TransitionSet> Transitions
    //    {
    //        get
    //        {
    //            return this._Transitions;
    //        }
    //    }

    //    private ObservableCollection<MyTile> _MyTiles;
    //    public ObservableCollection<MyTile> MyTiles
    //    {
    //        get
    //        {
    //            return this._MyTiles;
    //        }
    //    }

    //    public void BeginInit()
    //    {
    //    }

    //    public void EndInit()
    //    {
    //        if ((object)this.MyTiles != null && this.MyTiles.Count() > 0)
    //        {
    //            this.SelectedMyTile = this.MyTiles.FirstOrDefault();
    //        }
    //    }
    //}
}
