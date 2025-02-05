using System.Data;
using containers.Components;
using containers.Engines;
using containers.Lists;
using containers.Models;
using Terminal.Gui;

namespace containers.Views
{
    public partial class View
    {
        public void SearchArticle(Action<Window, TextField> onSubmit)
        {
            Application.Top.RemoveAll();
            MenuBar();
            Application.Refresh();

            var form = new Window("Menu de Busqueda de Articulos por Codigo.")
            {
                X = 0,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };


            var articleIDLabel = new Label("|-- Ingrese el codigo del article --|")
            {
                X = Pos.Center(),
                Y = 2
            };
            var articleIDField = new TextField("")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(articleIDLabel) + 1,
                Width = 40
            };


            var submitButton = new Button("Buscar")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(articleIDField) + 2
            };


            submitButton.Clicked += () =>
            {
                onSubmit(form, articleIDField);
            };


            form.Add(articleIDLabel, articleIDField, submitButton);


            Application.Top.Add(form);
            Application.Refresh();
        }

        public void ShowArticleForm(Action<string, float, uint, float, UnitType, QuantityType, string, string?> onSubmit)
        {
            Application.Top.RemoveAll();
            MenuBar();
            Application.Refresh();


            var form = new ArticleRegistration()
            {
                X = 0,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };

            form.OnFilterApplied += onSubmit;

            Application.Top.Add(form);
            Application.Refresh();
        }

        public void ShowArticles(DataTable dataTable, Func<string, string, string, UnitType?, QuantityType?, bool, DataTable>? onSubmit, Action<TableView.CellActivatedEventArgs, DataTable>? onCellActivated, bool isSubView, Action? OnBack)
        {
            Application.Top.RemoveAll();
            MenuBar();
            Application.Refresh();

            var form = new ArticleFilterForm()
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = 20,
            };
            var win = new Window()
            {
                X = 0,
                Y = Pos.Bottom(form) + 1,
                Width = Dim.Fill(),
                Height = Dim.Fill(),
            };
            var tableView = new TableView()
            {
                Text = "Tabla de Contenedores",
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill(),
                Table = dataTable
            };

            if (isSubView)
            {
                var quitButton = new Button("Volver <--")
                {
                    X = 1,
                    Y = 0
                };

                form.Add(quitButton);

                quitButton.Clicked += OnBack;
            }

            form.OnFilterApplied += () =>
            {
                tableView.Table = onSubmit?.Invoke(form.ContainerCode, form.ClientCode, form.ArticleCode, form.UnitType, form.QuantityType, form.WithoutOwner);
                ShowArticles(tableView.Table, onSubmit, onCellActivated, isSubView, OnBack);
            };

            tableView.CellActivated += (args) =>
            {
                onCellActivated?.Invoke(args, dataTable);
            };

            win.Add(tableView);
            Application.Top.Add(form, win);
        }
    }
}