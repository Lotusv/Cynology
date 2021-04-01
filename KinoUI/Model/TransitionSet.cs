using Telerik.Windows.Controls;
using Telerik.Windows.Controls.TransitionControl;

namespace KinoUI.Model
{
    public class TransitionSet : ViewModelBase
    {
        private TransitionProvider _Transition;
        public TransitionProvider Transition
        {
            get
            {
                return this._Transition;
            }
            set
            {
                if (this._Transition != value)
                {
                    this._Transition = value;
                    this.OnPropertyChanged(() => this.Transition);
                }
            }
        }
    }
}
