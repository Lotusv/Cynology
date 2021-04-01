using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebBrowserLib
{
    public class MenuHandler : IContextMenuHandler
    {
        private string url;
        private string img;

        public delegate void ImageSelectedEventHandler(string sampleParam);

        public event ImageSelectedEventHandler ImageSelectedEvent;

        public void RaiseImageSelectedEvent(string selimageurl)
        {
            // Your logic
            if (ImageSelectedEvent != null)
            {
                ImageSelectedEvent(selimageurl);
            }
        }


        void IContextMenuHandler.OnBeforeContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model)
        {
            Console.WriteLine("Context menu opened");
            Console.WriteLine(parameters.MisspelledWord);

            if (model.Count > 0)
            {
                model.AddSeparator();
            }
            model.AddItem((CefMenuCommand) 26503, "Close context menu");

            if (parameters.MediaType == ContextMenuMediaType.Image)
            {
                if (parameters.SourceUrl.ToString().Substring(0,4)=="http")
                {
                    img = parameters.SourceUrl;
                    model.AddItem((CefMenuCommand) 26501, "Save Image");
                    model.AddItem((CefMenuCommand) 26502, "Select image");
                   
                }

            }
            //
           

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
                RaiseImageSelectedEvent(img);
                return true;
            }
            if (commandId == (CefMenuCommand) 26503)
            {
                return true;
            }
            return false;
        }

        void IContextMenuHandler.OnContextMenuDismissed(IWebBrowser browserControl, IBrowser browser, IFrame frame)
        {
            var chromiumWebBrowser = (CefSharp.Wpf.ChromiumWebBrowser) browserControl;

            chromiumWebBrowser.Dispatcher.Invoke(() =>
            {
                chromiumWebBrowser.ContextMenu = null;
            });
        }

        bool IContextMenuHandler.RunContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model, IRunContextMenuCallback callback)
        {
            //return false;

            //var chromiumWebBrowser = (CefSharp.Wpf.ChromiumWebBrowser) browserControl;

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
            //        if (!callback.IsDisposed)
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

            return false;
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
