using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KinoUI.Pages
{
    /// <summary>
    /// Interaction logic for GeographyDirectoriesList.xaml
    /// </summary>
    public partial class GeographyDirectoriesList : UserControl
    {
        public GeographyDirectoriesList()
        {
            InitializeComponent();
            //foreach (FirstFloor.ModernUI.Windows.Controls.BBCodeBlock bb in lst.Items)
            //{
            //    bb.Foreground = new SolidColorBrush(Colors.Black);
            //}
        }

        public static readonly RoutedEvent BackEvent = EventManager.RegisterRoutedEvent(
    "Back", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(GeographyDirectoriesList));

        // Provide CLR accessors for the event 
        public event RoutedEventHandler Back
        {
            add { AddHandler(BackEvent, value); }
            remove { RemoveHandler(BackEvent, value); }
        }

        // This method raises the Tap event 
        void RaiseBackEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(GeographyDirectoriesList.BackEvent);
            RaiseEvent(newEventArgs);
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            RaiseBackEvent();
        }
    }
}
