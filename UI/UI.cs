using containers.Models;
using containers.Lists;
using containers.Engines;
using Terminal.Gui;

namespace containers.UI
{
    public class UI
    {
        public ContainerList Containers;

        public UI(ContainerList containerList)
        {
            Containers = containerList;
        }

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

        public void MenuBar()
        {
            var menu = new MenuBar(
            [
            new MenuBarItem("_Aplicación",
            [
                new MenuItem("_Salir", "", () => Application.RequestStop())
            ]),
            new MenuBarItem("_Contenedores",
            [
                new MenuItem("_Buscar Contenedor", "", () =>
                {
                    ContainerGE.Search(this);
                }),
                new MenuItem("_Registrar Contenedor", "", () =>
                {
                    ContainerGE.ShowForm(this);
                }),
                new MenuItem("_Listar Contenedores", "", () =>
                {
                    ContainerGE.ShowContainers(this);
                }),
            ]),
            new MenuBarItem("_Clientes",
            [
                new MenuItem("_Buscar Cliente", "", () =>
                {
                    ClientGE.Search(this);
                }),
                new MenuItem("_Registrar Cliente", "", () =>
                {
                    ClientGE.ShowForm(this);
                }),
                new MenuItem("_Listar Clientes", "", () =>
                {
                    ClientGE.ShowClients(this);
                }),
            ]),
            new MenuBarItem("_Articulos",
            [
                new MenuItem("_Buscar Articulo", "", () =>
                {
                    ArticleGE.Search(this);
                }),
                new MenuItem("_Registrar Articulo", "", () =>
                {
                    ArticleGE.ShowForm(this);
                }),
                new MenuItem("_Listar Articulos", "", () =>
                {
                    ArticleGE.ShowArticles(this);
                }),
                new MenuItem("_Listar Articulos No Asignados", "", () =>
                {
                    ArticleGE.ShowArticles(this);
                }),
            ])
            ]);

            Application.Top.Add(menu);
        }
    }
}