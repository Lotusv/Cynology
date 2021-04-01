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
using FirstFloor.ModernUI.Windows.Controls;

namespace KinoUI.Pages
{
    /// <summary>
    /// Interaction logic for DirectoriesListUC.xaml
    /// </summary>
    public partial class DirectoriesListUC : UserControl
    {
        public DirectoriesListUC()
        {
            InitializeComponent();
            //BBCodeBlock bb = new BBCodeBlock();
            
        }

        public static readonly RoutedEvent BackEvent = EventManager.RegisterRoutedEvent(
        "Back", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(DirectoriesListUC));

        // Provide CLR accessors for the event 
        public event RoutedEventHandler Back
        {
            add { AddHandler(BackEvent, value); }
            remove { RemoveHandler(BackEvent, value); }
        }

        // This method raises the Tap event 
        void RaiseBackEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(DirectoriesListUC.BackEvent);
            RaiseEvent(newEventArgs);
        }

        private void ModernTab_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            RaiseBackEvent();
        }
    }
}
