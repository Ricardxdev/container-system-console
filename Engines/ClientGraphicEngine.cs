using containers.Models;
using containers.Lists;
using Terminal.Gui;
using System.Data;

namespace containers.Engines
{
    public class ClientFilterForm : Window
    {
        private TextField clientCodeField;
        private TextField containerCodeField;
        private ComboBox typeBox;
        public string? ClientCode => clientCodeField.Text.ToString() != "" ? clientCodeField.Text.ToString() : null;
        public string? ContainerCode => containerCodeField.Text.ToString() != "" ? containerCodeField.Text.ToString() : null;
        public ClientType? ClientType => typeBox.SelectedItem >= 1 ? (ClientType)(typeBox.SelectedItem - 1) : null;
        public event Action OnFilterApplied;
        public ClientFilterForm()
        {
            InitializeComponents();
        }
        void InitializeComponents()
        {
            var clientCodeLabel = new Label("|-- ID del Cliente --|")
            {
                X = Pos.Percent(20) - 20,
                Y = 2,
                Width = 40,
                TextAlignment = TextAlignment.Centered,
            };
            clientCodeField = new TextField("")
            {
                X = Pos.Percent(20) - 20,
                Y = Pos.Bottom(clientCodeLabel) + 1,
                Width = 40,
                TextAlignment = TextAlignment.Centered,
            };

            var containerCodeLabel = new Label("|-- ID del Contenedor --|")
            {
                X = Pos.Percent(50) - 20,
                Y = 2,
                Width = 40,
                TextAlignment = TextAlignment.Centered,
            };
            containerCodeField = new TextField("")
            {
                X = Pos.Percent(50) - 20,
                Y = Pos.Bottom(containerCodeLabel) + 1,
                Width = 40,
                TextAlignment = TextAlignment.Centered,
            };

            var typeBoxLabel = new Label("|-- Tipo de Cliente --|")
            {
                X = Pos.Percent(80) - 20,
                Y = 2,
                Width = 40,
                TextAlignment = TextAlignment.Centered,
            };

            typeBox = new ComboBox()
            {
                X = Pos.Percent(80) - 20,
                Y = Pos.Bottom(typeBoxLabel) + 1,
                Width = 40,
                Height = 10,
                TextAlignment = TextAlignment.Centered,
            };

            var sourceTypes = new List<string>() { " " };
            sourceTypes.AddRange(Enum.GetNames(typeof(ClientType)));
            typeBox.SetSource(sourceTypes);

            var submitButton = new Button("Filtrar")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(containerCodeField) + 2
            };

            submitButton.Clicked += () =>
            {
                OnFilterApplied?.Invoke();
            };

            Add(clientCodeLabel, clientCodeField, containerCodeLabel, containerCodeField, typeBoxLabel, typeBox, submitButton);
        }
    }
    static class ClientGE
    {
        public static void Search(UI.UI ui)
        {
            Search(ui, (ui, win, IDField) =>
            {
                ClientList clientEntries = ui.Containers.FilterClientsBy((container, client) =>
                {
                    if (client.ID == IDField?.Text?.ToString()?.Trim())
                    {
                        client.ContainerID = container?.ID;
                        return true;
                    }
                    return false;
                });

                var dataTable = new DataTable();
                Client.GenerateColumns(dataTable);
                clientEntries.ForEach((cl) =>
                {
                    cl.ToRow(dataTable);
                });

                var tableView = new TableView()
                {
                    Text = "Tabla de Clientes",
                    X = 0,
                    Y = Pos.Bottom(IDField) + 4,
                    Width = Dim.Fill(),
                    Height = Dim.Fill(),
                    Table = dataTable
                };

                win.Add(tableView);
                win.SetNeedsDisplay();
                Application.Refresh();
            });
        }

        public static void Search(UI.UI ui, Action<UI.UI, Window, TextField> onSubmit)
        {
            Application.Top.RemoveAll();
            ui.MenuBar();
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
                onSubmit(ui, form, clientIDField);
            };


            form.Add(clientIDLabel, clientIDField, submitButton);


            Application.Top.Add(form);
            Application.Refresh();
        }

        public static void ShowForm(UI.UI ui)
        {
            ShowForm(ui, (nameField, phoneField, addressField, typeComboBox) =>
            {
                Client client = new Client(
                    nameField?.Text?.ToString()?.Trim() ?? "",
                    (ClientType)typeComboBox.SelectedItem,
                    addressField?.Text?.ToString()?.Trim() ?? "",
                    phoneField?.Text?.ToString()?.Trim() ?? ""
                );
                ui.Containers.Clients.Add(client);
                MessageBox.Query("Registro Completado", client.ToString(), "OK");
            });
        }

        public static void ShowForm(UI.UI ui, Action<TextField, TextField, TextField, ComboBox> onSubmit)
        {
            Application.Top.RemoveAll();
            ui.MenuBar();
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
                    onSubmit(firstNameField, phoneField, addressField, typeComboBox);
                }
            };

            form.Add(firstNameLabel, firstNameField, phoneLabel, phoneField, addressLabel, addressField, typeLabel, typeComboBox, submitButton);

            Application.Top.Add(form);
            Application.Refresh();
        }

        public static void ShowClients(UI.UI ui)
        {
            ShowClients(ui, null, (args, dataTable) =>
                {
                    int rowIndex = args.Row;
                    int colIndex = args.Col;
                    var selectedRow = dataTable.Rows[rowIndex];
                    var clientId = selectedRow.Field<string>(0) ?? "";
                    //var containerId = selectedRow.Field<string>(0) ?? "";
                    if (colIndex == 6) ClientArticlesEventHandler(ui, "", clientId);
                }, false);
        }

        public static void ShowClients(UI.UI ui, string containerId, bool isSubView)
        {
            var clients = ui.Containers.FilterClientsBy((c, cl) =>
            {
                if (containerId == "" || c?.ID == containerId) return true;
                return false;
            });
            var dataTable = clients.GetDataTable();
            ShowClients(ui, dataTable, (args, dataTable) =>
                {
                    int rowIndex = args.Row;
                    int colIndex = args.Col;
                    var selectedRow = dataTable.Rows[rowIndex];
                    var clientId = selectedRow.Field<string>(0) ?? "";
                    var containerId = selectedRow.Field<string>(1) ?? "";
                    if (colIndex == 6) ClientArticlesEventHandler(ui, containerId, clientId);
                }, isSubView);
        }

        public static void ShowClients(UI.UI ui, DataTable? dataTable, Action<TableView.CellActivatedEventArgs, DataTable> onCellActivated, bool isSubView)
        {
            if (dataTable is null)
            {
                var clients = ui.Containers.GetClients();
                dataTable = clients.GetDataTable();
            }

            ShowClients(ui, dataTable, (clientID, containerID, type) =>
            {
                ClientList filt = ui.Containers.FilterClientsBy((container, client) =>
                {
                    if (clientID is not null && client.ID != clientID) return false;
                    if (type is not null && client.Type != type) return false;
                    if (containerID is not null && container is not null && container.ID != containerID) return false;
                    if (containerID is not null && container is null) return false;
                    return true;
                });

                return filt.GetDataTable();
            }, onCellActivated, isSubView);
        }

        public static void ShowClients(UI.UI ui, DataTable dataTable, Func<string?, string?, ClientType?, DataTable>? onSubmit, Action<TableView.CellActivatedEventArgs, DataTable>? onCellActivated, bool isSubView)
        {
            Application.Top.RemoveAll();
            ui.MenuBar();
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

                quitButton.Clicked += () =>
                {
                    ContainerGE.ShowContainers(ui);
                };
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

        public static void ClientArticlesEventHandler(UI.UI ui, string ContainerID, string ClientID)
        {
            ArticleList filt = ui.Containers.FilterArticlesBy((container, client, article) =>
            {
                if (container is null) return false;
                if ((ContainerID.Trim() == "" || container.ID == ContainerID) && (ClientID.Trim() == "" || client?.ID == ClientID)) return true;
                return false;
            });

            ArticleGE.ShowArticles(ui, filt.GetDataTable(), (containerID, clientID, articleID, unitType, quantityType) =>
            {
                ArticleList filt = ui.Containers.FilterArticlesBy((c, cl, art) =>
                {
                    if (ContainerID.Trim() != "" && c?.ID != ContainerID) return false;
                    if (cl?.ID != ClientID) return false;
                    if (!(articleID == "") && (art.ID != articleID)) return false;
                    if (unitType is not null && !(art.Unit == unitType)) return false;
                    if (quantityType is not null && !(art.QuantityType == quantityType)) return false;

                    return true;
                });

                return filt.GetDataTable();
            }, null, true, () =>
                {
                    ShowClients(ui, ContainerID, ContainerID != "" ? true : false);
                });

            Application.Refresh();
        }
    }
}