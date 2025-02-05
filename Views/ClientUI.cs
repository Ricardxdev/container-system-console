using System.Data;
using containers.Components;
using containers.Models;
using Terminal.Gui;

namespace containers.Views
{
    public partial class View
    {
        public void SearchClient(Action<Window, TextField> onSubmit)
        {
            Application.Top.RemoveAll();
            MenuBar();
            Application.Refresh();

            var form = new Window("Menu de Busqueda de Clientes por Cedula.")
            {
                X = 0,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };


            var clientIDLabel = new Label("|--  Ingrese la Cedula del Cliente(a)  --|")
            {
                X = Pos.Center() - 21,
                Y = 2,
            };
            var clientIDField = new TextField("")
            {
                X = Pos.Center() - 21,
                Y = Pos.Bottom(clientIDLabel) + 1,
                Width = 42
            };


            var submitButton = new Button("Buscar")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(clientIDField) + 2
            };


            submitButton.Clicked += () =>
            {
                onSubmit(form, clientIDField);
            };


            form.Add(clientIDLabel, clientIDField, submitButton);


            Application.Top.Add(form);
            Application.Refresh();
        }

        public void ShowClientForm(Action<string, string, string, ClientType> onSubmit)
        {
            Application.Top.RemoveAll();
            MenuBar();
            Application.Refresh();


            var form = new Window("Formulario de registro de cliente nuevo.")
            {
                X = 0,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };


            var firstNameLabel = new Label("Nombre(s):      ")
            {
                X = 2,
                Y = 2
            };
            var firstNameField = new TextField("")
            {
                X = Pos.Right(firstNameLabel) + 1,
                Y = Pos.Top(firstNameLabel),
                Width = 40
            };

            var phoneLabel = new Label("Teléfono:       ")
            {
                X = 2,
                Y = Pos.Bottom(firstNameLabel) + 1
            };
            var phoneField = new TextField("")
            {
                X = Pos.Right(phoneLabel) + 1,
                Y = Pos.Top(phoneLabel),
                Width = 40
            };

            var addressLabel = new Label("Dirección:      ")
            {
                X = 2,
                Y = Pos.Bottom(phoneLabel) + 1
            };
            var addressField = new TextField("")
            {
                X = Pos.Right(addressLabel) + 1,
                Y = Pos.Top(addressLabel),
                Width = 40
            };

            var typeLabel = new Label("Tipo de Cliente:")
            {
                X = 2,
                Y = Pos.Bottom(addressLabel) + 1,
            };
            var typesList = Enum.GetNames(typeof(ClientType));
            var typeComboBox = new ComboBox(typesList)
            {
                X = Pos.Right(typeLabel) + 1,
                Y = Pos.Top(typeLabel),
                Width = 20,
                Height = 6,
                TextAlignment = TextAlignment.Centered,
                ColorScheme = Colors.Dialog,
            };

            var submitButton = new Button("Registrar")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(typeLabel) + 7
            };

            submitButton.Clicked += () =>
            {
                if (!Helpers.Helpers.ValidateNumeric(phoneField.Text?.ToString() ?? ""))
                {
                    MessageBox.Query("Error de tipo", "El Telefono solo puede contener digitos", "OK");
                }
                else
                {
                    onSubmit(firstNameField.Text.ToString() ?? "", phoneField?.Text?.ToString() ?? "", addressField.Text.ToString() ?? "", (ClientType)typeComboBox.SelectedItem);
                }
            };

            form.Add(firstNameLabel, firstNameField, phoneLabel, phoneField, addressLabel, addressField, typeLabel, typeComboBox, submitButton);

            Application.Top.Add(form);
            Application.Refresh();
        }

        public void ShowClients(DataTable dataTable, Func<string?, string?, ClientType?, DataTable>? onSubmit, Action<TableView.CellActivatedEventArgs, DataTable>? onCellActivated, Action OnReturn, bool isSubView)
        {
            Application.Top.RemoveAll();
            MenuBar();
            Application.Refresh();

            var form = new ClientFilterForm()
            {
                X = 0,
                Y = 1,
                Width = Dim.Fill(),
                Height = 13,
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
                Text = "Tabla de Clientes",
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

                quitButton.Clicked += OnReturn;
            }

            form.OnFilterApplied += () =>
            {
                tableView.Table = onSubmit?.Invoke(form.ClientCode, form.ContainerCode, form.ClientType);
                tableView.SetNeedsDisplay();
            };

            tableView.CellActivated += (args) =>
            {
                onCellActivated?.Invoke(args, dataTable);
            };


            win.Add(tableView);
            Application.Top.Add(form, win);
            Application.Refresh();
        }
    }
}