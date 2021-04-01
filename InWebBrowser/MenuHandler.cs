using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InWebBrowser
{
    public class MenuHandler : IContextMenuHandler
    {
        private string url;
        private string img;

        void IContextMenuHandler.OnBeforeContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model)
        {
            Console.WriteLine("Context menu opened");
            Console.WriteLine(parameters.MisspelledWord);

            if (model.Count > 0)
            {
                model.AddSeparator();
            }

            
            if (parameters.MediaType == ContextMenuMediaType.Image)
            {
                img = parameters.SourceUrl;
                model.AddItem((CefMenuCommand) 26501, "Save Image");
            }
            //model.AddItem((CefMenuCommand) 26502, "Close DevTools");


            //To disable context mode then clear
            // model.Clear();
        }

        bool IContextMenuHandler.OnContextMenuCommand(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, CefMenuCommand commandId, CefEventFlags eventFlags)
        {
            if (commandId == (CefMenuCommand) 26501)
            {
                //browser.GetHost().ShowDevTools();
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.FileName = "image.png";
                dialog.Filter = "Png image (*.png)|*.png|Gif Image (*.gif)|*.gif|JPEG image (*.jpg)|*.jpg";

                var result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Console.WriteLine("writing to: " + dialog.FileName);

                    var wClient = new WebClient();
                    wClient.DownloadFile(img, dialog.FileName);
                }
                return true;
            }
            if (commandId == (CefMenuCommand) 26502)
            {
                browser.GetHost().CloseDevTools();
                return true;
            }

            return false;
        }

        void IContextMenuHandler.OnContextMenuDismissed(IWebBrowser browserControl, IBrowser browser, IFrame frame)
        {
            //var chromiumWebBrowser = (ChromiumWebBrowser)browserControl;

            //chromiumWebBrowser.Dispatcher.Invoke(() =>
            //{
            //    chromiumWebBrowser.ContextMenu = null;
            //});
        }

        bool IContextMenuHandler.RunContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model, IRunContextMenuCallback callback)
        {
            return false;

            //var chromiumWebBrowser = (ChromiumWebBrowser)browserControl;

            ////IMenuModel is only valid in the context of this method, so need to read the values before invoking on the UI thread
            //var menuItems = GetMenuItems(model);

            //chromiumWebBrowser.Dispatcher.Invoke(() =>
            //{
            //    var menu = new ContextMenu
            //    {
            //        IsOpen = true
            //    };

            //    RoutedEventHandler handler = null;

            //    handler = (s, e) =>
            //    {
            //        menu.Closed -= handler;

            //        //If the callback has been disposed then it's already been executed
            //        //so don't call Cancel
            //        if(!callback.IsDisposed)
            //        { 
            //            callback.Cancel();
            //        }
            //    };

            //    menu.Closed += handler;

            //    foreach (var item in menuItems)
            //    {
            //        menu.Items.Add(new MenuItem
            //        {
            //            Header = item.Item1,
            //            Command = new RelayCommand(() => { callback.Continue(item.Item2, CefEventFlags.None); })
            //        });
            //    }
            //    chromiumWebBrowser.ContextMenu = menu;
            //});

            //return true;
        }

        private static IEnumerable<Tuple<string, CefMenuCommand>> GetMenuItems(IMenuModel model)
        {
            var list = new List<Tuple<string, CefMenuCommand>>();
            for (var i = 0; i < model.Count; i++)
            {
                var header = model.GetLabelAt(i);
                var commandId = model.GetCommandIdAt(i);
                list.Add(new Tuple<string, CefMenuCommand>(header, commandId));
            }

            return list;
        }
    }
}
