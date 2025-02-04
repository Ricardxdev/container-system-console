using containers.Models;
using containers.Lists;
using Terminal.Gui;
using System.Data;
using containers.Helpers;

namespace containers.Engines
{
    public class ContainerRegistrationForm : Window
    {
        private ListView typesListView;
        private ComboBox stateField;
        private TextField capacityField;
        private TextField tareField;
        private HashSet<ContainerType> selectedTypes = new HashSet<ContainerType>();
        public ContainerType[] SelectedTypes => selectedTypes.ToArray();


        public event Action<ContainerType[], ContainerState, float, float> OnSubmit;

        public ContainerRegistrationForm() : base("Formulario de registro de contenedor.")
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            var typesLabel = new Label("Tipo de Contenedor:")
            {
                X = 2,
                Y = 2,
            };

            var typesList = Enum.GetNames(typeof(ContainerType));
            typesListView = new ListView(typesList)
            {
                X = Pos.Right(typesLabel) + 1,
                Y = Pos.Top(typesLabel),
                Width = 10,
                Height = 6,
                AllowsMarking = true, // Enable marking for multi-selection
                AllowsMultipleSelection = true,
                TextAlignment = TextAlignment.Centered,
                ColorScheme = Colors.Dialog,
            };

            var stateLabel = new Label("Estado del Contenedor:")
            {
                X = 2,
                Y = Pos.Bottom(typesLabel) + 7
            };

            stateField = new ComboBox(Enum.GetNames(typeof(ContainerState)))
            {
                X = Pos.Right(stateLabel) + 1,
                Y = Pos.Top(stateLabel),
                Width = 40,
                Height = 9,
            };

            var capacityLabel = new Label("Capacidad:            ")
            {
                X = 2,
                Y = Pos.Bottom(stateLabel) + 1
            };

            capacityField = new TextField("")
            {
                X = Pos.Right(capacityLabel) + 1,
                Y = Pos.Top(capacityLabel),
                Width = 40
            };

            var tareLabel = new Label("Tara:                 ")
            {
                X = 2,
                Y = Pos.Bottom(capacityLabel) + 1
            };

            tareField = new TextField("")
            {
                X = Pos.Right(tareLabel) + 1,
                Y = Pos.Top(tareLabel),
                Width = 40
            };

            var submitButton = new Button("Registrar")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(tareField) + 2
            };

            typesListView.SelectedItemChanged += (handler) =>
            {
                if (handler.Item >= 0)
                {
                    var selectedType = (ContainerType)Enum.Parse(typeof(ContainerType), typesList[handler.Item]);
                    if (!selectedTypes.Add(selectedType))
                    {
                        selectedTypes.Remove(selectedType);
                    }
                }
            };

            submitButton.Clicked += () =>
            {
                var capacityText = capacityField.Text.ToString();
                var tareText = tareField.Text.ToString();

                if (string.IsNullOrWhiteSpace(capacityText) || !float.TryParse(capacityText, out _))
                {
                    MessageBox.Query("Error de tipo", "La Capacidad debe ser un número", "OK");
                    return;
                }

                if (string.IsNullOrWhiteSpace(tareText) || !float.TryParse(tareText, out _))
                {
                    MessageBox.Query("Error de tipo", "La Tara debe ser un número", "OK");
                    return;
                }

                // Trigger the OnSubmit event with collected data
                OnSubmit?.Invoke(SelectedTypes, (ContainerState)stateField.SelectedItem, float.Parse(capacityField.Text.ToString() ?? "0"), float.Parse(tareField.Text.ToString() ?? "0"));
            };

            // Add components to the window
            Add(typesLabel, typesListView, stateLabel, stateField, capacityLabel, capacityField, tareLabel, tareField, submitButton);
        }
    }
    public class ContainerFilterForm : Window
    {
        private TextField containerCodeField;
        private TextField clientCodeField;
        private TextField articleCodeField;
        private ComboBox typeBox;
        private ComboBox stateBox;

        public string ContainerCode => containerCodeField?.Text?.ToString()?.Trim() ?? string.Empty;
        public string ClientCode => clientCodeField?.Text?.ToString()?.Trim() ?? string.Empty;
        public string ArticleCode => articleCodeField?.Text?.ToString()?.Trim() ?? string.Empty;
        public int? SelectedType => typeBox.SelectedItem >= 1 ? typeBox.SelectedItem - 1 : null;
        public int? SelectedState => stateBox.SelectedItem >= 1 ? stateBox.SelectedItem - 1 : null;

        public event Action OnFilterApplied;

        public ContainerFilterForm() : base("Menu de Listado de Contenedores.")
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            var containerCodeLabel = new Label("|-- ID del Contenedor --|")
            {
                X = Pos.Percent(20) - 20,
                Y = 2,
                Width = 40,
                TextAlignment = TextAlignment.Centered,
            };
            containerCodeField = new TextField("")
            {
                X = Pos.Percent(20) - 20,
                Y = Pos.Bottom(containerCodeLabel) + 1,
                Width = 40,
                TextAlignment = TextAlignment.Centered,
            };

            var clientCodeLabel = new Label("|-- ID del Cliente --|")
            {
                X = Pos.Percent(50) - 20,
                Y = Pos.Top(containerCodeLabel),
                Width = 40,
                TextAlignment = TextAlignment.Centered,
            };
            clientCodeField = new TextField("")
            {
                X = Pos.Percent(50) - 20,
                Y = Pos.Bottom(clientCodeLabel) + 1,
                Width = 40,
                TextAlignment = TextAlignment.Centered,
            };

            var articleCodeLabel = new Label("|-- ID del Articulo --|")
            {
                X = Pos.Percent(80) - 20,
                Y = Pos.Top(containerCodeLabel),
                Width = 40,
                TextAlignment = TextAlignment.Centered,
            };
            articleCodeField = new TextField("")
            {
                X = Pos.Percent(80) - 20,
                Y = Pos.Bottom(articleCodeLabel) + 1,
                Width = 40,
                TextAlignment = TextAlignment.Centered,
            };

            var typeBoxLabel = new Label("|-- Filtrar Por Tipo --|")
            {
                X = Pos.Percent(25) - 20,
                Y = Pos.Bottom(containerCodeLabel) + 3,
                Width = 40,
                TextAlignment = TextAlignment.Centered,
            };

            typeBox = new ComboBox()
            {
                X = Pos.Percent(25) - 20,
                Y = Pos.Bottom(typeBoxLabel) + 1,
                Width = 40,
                Height = 10,
                TextAlignment = TextAlignment.Centered,
            };

            var sourceTypes = new List<string>() { " " };
            sourceTypes.AddRange(Enum.GetNames(typeof(ContainerType)));
            typeBox.SetSource(sourceTypes);

            var stateBoxLabel = new Label("|-- Filtrar Por Estado --|")
            {
                X = Pos.Percent(70) - 10,
                Y = Pos.Top(typeBoxLabel),
                Width = 40,
                TextAlignment = TextAlignment.Centered,
            };

            stateBox = new ComboBox()
            {
                X = Pos.Percent(70) - 10,
                Y = Pos.Bottom(stateBoxLabel) + 1,
                Width = 40,
                Height = 10,
                TextAlignment = TextAlignment.Centered,
            };

            var sourceStates = new List<string>() { " " };
            sourceStates.AddRange(Enum.GetNames(typeof(ContainerState)));
            stateBox.SetSource(sourceStates);

            var submitButton = new Button("Filtrar")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(typeBoxLabel) + 4
            };

            submitButton.Clicked += () =>
            {
                OnFilterApplied?.Invoke();
            };

            // Add components to the window
            Add(containerCodeLabel, containerCodeField, clientCodeLabel, clientCodeField,
                 articleCodeLabel, articleCodeField, typeBoxLabel, typeBox,
                 stateBoxLabel, stateBox, submitButton);
        }
    }

    public class ContainerGE
    {
        public static void Search(UI.UI ui)
        {
            Search(ui, (ui, codeField) =>
            {
                Container? container = ui.Containers.Find(codeField.Text.ToString() ?? "".Trim());
                if (container is not null)
                {
                    MessageBox.Query("Datos del contenedor", container.ToString(), "OK");
                }
                else
                {
                    int choice = MessageBox.Query("Contenedor no encontrado", "Este contenedor no se encuentra registrado. Desea llenar el formulario de registro?", "Si", "No");
                    if (choice == 0)
                    {
                        ShowForm(ui);
                    }
                }
            });
        }

        public static void Search(UI.UI ui, Action<UI.UI, TextField> onSubmit)
        {
            Application.Top.RemoveAll();
            ui.MenuBar();
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
                onSubmit(ui, containerIDField);
            };


            form.Add(containerIDLabel, containerIDField, submitButton);


            Application.Top.Add(form);
            Application.Refresh();
        }

        public static void ShowForm(UI.UI ui)
        {
            ShowForm(ui, (types, state, capacity, tare) =>
            {
                Container container = new Container(
                    types,
                    state,
                    capacity,
                    tare
                );
                ui.Containers.Add(container);
                MessageBox.Query("Registro Completado", container.ToString(), "OK");
                ShowForm(ui);
            });
        }

        public static void ShowForm(UI.UI ui, Action<ContainerType[], ContainerState, float, float> onSubmit)
        {
            Application.Top.RemoveAll();
            ui.MenuBar();
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

        public static void ShowContainers(UI.UI ui)
        {
            ShowContainers(ui, (args, dataTable) =>
                {
                    int rowIndex = args.Row;
                    int colIndex = args.Col;
                    var selectedRow = dataTable.Rows[rowIndex];
                    var code = selectedRow.Field<string>(0) ?? "";
                    if (colIndex == 6) ContainerClientsEventHandler(ui, code);
                    if (colIndex == 7) ContainerArticlesEventHandler(ui, code);
                });
        }

        public static void ShowContainers(UI.UI ui, Action<TableView.CellActivatedEventArgs, DataTable>? onCellActivated)
        {
            var dataTable = ui.Containers.GetDataTable();
            ShowContainers(ui, dataTable, (containerID, clientID, articleID, type, state) =>
            {
                ContainerList filt = ui.Containers.FilterBy((c) =>
                {
                    if (!(containerID.Trim() == "") && !(c.ID == containerID)) return false;
                    if (type is not null && !c.Types.Contains((ContainerType)type)) return false;
                    if (state is not null && !(c.State == (ContainerState)state)) return false;

                    ClientNode? filtClient = c.Clients.Find(clientID.Trim());
                    if (!(clientID.Trim() == "") && filtClient is null) return false;
                    ArticleNode? filtArticle = filtClient?.Articles.Find(articleID.Trim());
                    if (!(articleID.Trim() == "") && filtArticle is null) return false;

                    return true;
                });

                return filt.GetDataTable();
            }, onCellActivated);
        }

        public static void ShowContainers(UI.UI ui, DataTable dataTable, Func<string, string, string, int?, int?, DataTable>? onSubmit, Action<TableView.CellActivatedEventArgs, DataTable>? onCellActivated)
        {
            Application.Top.RemoveAll();
            ui.MenuBar();
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
                onCellActivated?.Invoke(args, dataTable);
            };
        }

        public static void ContainerClientsEventHandler(UI.UI ui, string ContainerID)
        {
            ClientList filt = ui.Containers.FilterClientsBy((container, client) =>
            {
                if (container is null) return false;
                if (ContainerID.Trim() == "" || container.ID.Contains(ContainerID)) return true;
                return false;
            });

            ClientGE.ShowClients(ui, filt.GetDataTable(), (clientID, containerID, type) =>
            {
                ClientList filt = ui.Containers.FilterClientsBy((container, client) =>
                {
                    if (clientID is not null && client.ID != clientID) return false;
                    if (type is not null && client.Type != type) return false;
                    if (container?.ID != ContainerID) return false;
                    return true;
                });
                return filt.GetDataTable();
            }, (args, dataTable) =>
                {
                    int rowIndex = args.Row;
                    int colIndex = args.Col;
                    var selectedRow = dataTable.Rows[rowIndex];
                    var clientId = selectedRow.Field<string>(0) ?? "";
                    //var containerId = selectedRow.Field<string>(0) ?? "";
                    if (colIndex == 6) ClientGE.ClientArticlesEventHandler(ui, ContainerID, clientId);
                }, true);
            Application.Refresh();
        }

        public static void ContainerArticlesEventHandler(UI.UI ui, string ContainerID)
        {
            ArticleList filt = ui.Containers.FilterArticlesBy((container, client, article) =>
            {
                if (container is null) return false;
                if (ContainerID.Trim() == "" || container.ID.Contains(ContainerID)) return true;
                return false;
            });

            ArticleGE.ShowArticles(ui, filt.GetDataTable(), (containerID, clientID, articleID, unitType, quantityType) =>
            {
                ArticleList filt = ui.Containers.FilterArticlesBy((c, cl, art) =>
                {
                    if (c?.ID != ContainerID) return false;
                    if (!(clientID == "") && (cl?.ID != clientID)) return false;
                    if (!(articleID == "") && (art.ID != articleID)) return false;
                    if (unitType is not null && !(art.Unit == unitType)) return false;
                    if (quantityType is not null && !(art.QuantityType == quantityType)) return false;

                    return true;
                });

                return filt.GetDataTable();
            }, null, true, () =>
                {
                    ShowContainers(ui);
                });

            Application.Refresh();
        }
    }

}