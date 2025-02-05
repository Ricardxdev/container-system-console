using System.Data;
using containers.Components;
using containers.Lists;
using containers.Models;
using Terminal.Gui;

namespace containers.Views {
    public partial class View {
        public void SearchContainer(Action<string?> onSubmit)
        {
            Application.Top.RemoveAll();
            MenuBar();
            Application.Refresh();

            var form = new Window("Menu de Busqueda de Contenedores por Codigo.")
            {
                X = 0,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };


            var containerIDLabel = new Label("|--  Ingrese el codigo del contenedor  --|")
            {
                X = Pos.Center(),
                Y = 2
            };
            var containerIDField = new TextField("")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(containerIDLabel) + 1,
                Width = 40,
                TextAlignment = TextAlignment.Centered,
            };


            var submitButton = new Button("Buscar")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(containerIDField) + 2
            };


            submitButton.Clicked += () =>
            {
                onSubmit(containerIDField.Text.ToString());
            };


            form.Add(containerIDLabel, containerIDField, submitButton);


            Application.Top.Add(form);
            Application.Refresh();
        }

        public void ShowContainerForm(Action<ContainerType[], ContainerState, float, float> onSubmit)
        {
            Application.Top.RemoveAll();
            MenuBar();
            Application.Refresh();

            var form = new ContainerRegistrationForm()
            {
                X = 0,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };

            form.OnSubmit += onSubmit;

            Application.Top.Add(form);
            Application.Refresh();
        }

        public void ShowContainers(DataTable dataTable, Func<string, string, string, ContainerType?, ContainerState?, DataTable>? onSubmit, Action<TableView.CellActivatedEventArgs, DataTable> onCellActivated)
        {
            Application.Top.RemoveAll();
            MenuBar();
            Application.Refresh();

            var form = new ContainerFilterForm()
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

            win.Add(tableView);
            Application.Top.Add(form, win);

            form.OnFilterApplied += () =>
            {
                tableView.Table = onSubmit?.Invoke(form.ContainerCode, form.ClientCode, form.ArticleCode, form.SelectedType, form.SelectedState);
            };

            tableView.CellActivated += (args) =>
            {
                onCellActivated(args, dataTable);
            };
        }
    }
}