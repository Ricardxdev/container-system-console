using containers.Models;
using containers.Lists;
using containers.Engines;
using Terminal.Gui;
using containers.Controllers;

namespace containers.Views
{
    public partial class View
    {
        public Action MenuBar;
        public void Home()
        {
            Application.Top.RemoveAll();
            MenuBar();
            Application.Refresh();

            var win = new Window("Pagina Principal")
            {
                X = 0,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };

            var tittleLabel = new Label(@"==================================================
TERMINAL CONTAINER MANAGER (TCM)
==================================================
  Container management system from the terminal
==================================================")
            {
                X = Pos.Center(),
                Y = 2,
                Width = 50,
                Height = 5,
                TextAlignment = TextAlignment.Centered,
            };

            win.Add(tittleLabel);
            Application.Top.Add(win);
        }
    }
}