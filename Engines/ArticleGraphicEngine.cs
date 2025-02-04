using containers.Models;
using containers.Lists;
using Terminal.Gui;
using System.Data;


namespace containers.Engines
{
    public class ArticleRegistration : Window
    {
        private TextField descriptionField;
        private TextField weightField;
        private ComboBox unitTypeBox;
        private TextField quantityField;
        private ComboBox quantityTypeBox;
        private TextField unitPriceField;
        private TextField containerIdField;
        private TextField clientIdField;
        public string? Description => descriptionField.Text.ToString() != "" ? descriptionField.Text.ToString() : null;
        public float? Weigth => float.TryParse(weightField.Text.ToString(), out _) ? float.Parse(weightField.Text.ToString() ?? "") : null;
        public uint? Quantity => uint.TryParse(quantityField.Text.ToString(), out _) ? uint.Parse(quantityField.Text.ToString() ?? "") : null;
        public float? UnitPrice => float.TryParse(unitPriceField.Text.ToString(), out _) ? float.Parse(unitPriceField.Text.ToString() ?? "") : null;
        public UnitType? SelectedUnitType => unitTypeBox.SelectedItem >= 1 ? (UnitType)(unitTypeBox.SelectedItem - 1) : null;
        public QuantityType? SelectedQuantityType => quantityTypeBox.SelectedItem >= 1 ? (QuantityType)(quantityTypeBox.SelectedItem - 1) : null;
        public string? ContainerID => containerIdField.Text.ToString() != "" ? containerIdField.Text.ToString() : null;
        public string? ClientID => clientIdField.Text.ToString() != "" ? clientIdField.Text.ToString() : null;

        public event Action<string, float, uint, float, UnitType, QuantityType, string?, string?> OnFilterApplied;
        public ArticleRegistration()
        {
            InitializeComponents();
        }

        void InitializeComponents()
        {
            Title = "Formulario de registro de nuevo articulo.";

            var descriptionLabel = new Label("Descripcion:      ")
            {
                X = 2,
                Y = 2
            };
            descriptionField = new TextField("")
            {
                X = Pos.Right(descriptionLabel) + 1,
                Y = Pos.Top(descriptionLabel),
                Width = 40
            };

            var weightLabel = new Label("Peso:             ")
            {
                X = 2,
                Y = Pos.Bottom(descriptionLabel) + 1
            };
            weightField = new TextField("")
            {
                X = Pos.Right(weightLabel) + 1,
                Y = Pos.Top(weightLabel),
                Width = 40
            };

            var unitTypeLabel = new Label("Unidad:           ")
            {
                X = 2,
                Y = Pos.Bottom(weightLabel) + 1
            };
            unitTypeBox = new ComboBox()
            {
                X = Pos.Right(unitTypeLabel) + 1,
                Y = Pos.Top(unitTypeLabel),
                Width = 40,
                Height = 5,
                TextAlignment = TextAlignment.Centered,
            };

            var sourceUnitTypes = new List<string>() { " " };
            sourceUnitTypes.AddRange(Enum.GetNames(typeof(UnitType)));
            unitTypeBox.SetSource(sourceUnitTypes);

            var quantityLabel = new Label("Cantidad:         ")
            {
                X = 2,
                Y = Pos.Bottom(unitTypeLabel) + 1
            };
            quantityField = new TextField("")
            {
                X = Pos.Right(quantityLabel) + 1,
                Y = Pos.Top(quantityLabel),
                Width = 40
            };

            var quantityTypeLabel = new Label("Tipo de Cantidad: ")
            {
                X = 2,
                Y = Pos.Bottom(quantityLabel) + 1
            };
            quantityTypeBox = new ComboBox()
            {
                X = Pos.Right(quantityTypeLabel) + 1,
                Y = Pos.Top(quantityTypeLabel),
                Width = 40,
                Height = 6,
                TextAlignment = TextAlignment.Centered,
            };

            var sourceQuantityTypes = new List<string>() { " " };
            sourceQuantityTypes.AddRange(Enum.GetNames(typeof(QuantityType)));
            quantityTypeBox.SetSource(sourceQuantityTypes);

            var unitPriceLabel = new Label("Precio Unitario:  ")
            {
                X = 2,
                Y = Pos.Bottom(quantityTypeLabel) + 1
            };
            unitPriceField = new TextField("")
            {
                X = Pos.Right(unitPriceLabel) + 1,
                Y = Pos.Top(unitPriceLabel),
                Width = 40
            };

            var containerIdLabel = new Label("ID Contenedor: (?)")
            {
                X = 2,
                Y = Pos.Bottom(unitPriceLabel) + 1
            };
            containerIdField = new TextField("")
            {
                X = Pos.Right(containerIdLabel) + 1,
                Y = Pos.Top(containerIdLabel),
                Width = 40
            };

            var clientIdLabel = new Label("ID Cliente:    (?)")
            {
                X = 2,
                Y = Pos.Bottom(containerIdLabel) + 1
            };
            clientIdField = new TextField("")
            {
                X = Pos.Right(clientIdLabel) + 1,
                Y = Pos.Top(clientIdLabel),
                Width = 40
            };

            var hintLabel = new Label("(?) -> Campo opcional")
            {
                X = 2,
                Y = Pos.Bottom(clientIdLabel) + 1,
            };

            var submitButton = new Button("Registrar")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(hintLabel) + 2
            };

            submitButton.Clicked += () =>
            {
                if (Description is null)
                {
                    MessageBox.Query("Error", "La descripcion no puede estar vacia", "OK");
                }
                else if (Weigth is null)
                {
                    MessageBox.Query("Error de tipo", "El peso debe ser un número", "OK");
                }
                else if (SelectedUnitType is null)
                {
                    MessageBox.Query("Error", "Debe Seleccionar un tipo de Unidad", "OK");
                }
                else if (Quantity is null)
                {
                    MessageBox.Query("Error de tipo", "La cantidad debe ser un número entero positivo", "OK");
                }
                else if (SelectedQuantityType is null)
                {
                    MessageBox.Query("Error", "Debe Seleccionar un tipo de Cantidad", "OK");
                }
                else if (UnitPrice is null)
                {
                    MessageBox.Query("Error de tipo", "El precio debe ser un número", "OK");
                }
                else
                {
                    OnFilterApplied?.Invoke(Description, (float)Weigth, (uint)Quantity, (float)UnitPrice, (UnitType)SelectedUnitType, (QuantityType)SelectedQuantityType, ContainerID, ClientID);
                }
            };

            Add(descriptionLabel, descriptionField, weightLabel, weightField, unitTypeLabel, unitTypeBox, quantityLabel, quantityField, quantityTypeLabel, quantityTypeBox, unitPriceLabel, unitPriceField, containerIdLabel, containerIdField, clientIdLabel, clientIdField, hintLabel, submitButton);
        }
    }

    public class ArticleFilterForm : Window
    {
        private TextField containerCodeField;
        private TextField clientCodeField;
        private TextField articleCodeField;
        private ComboBox unitTypeBox;
        private ComboBox quantityTypeBox;

        public string ContainerCode => containerCodeField?.Text?.ToString()?.Trim() ?? string.Empty;
        public string ClientCode => clientCodeField?.Text?.ToString()?.Trim() ?? string.Empty;
        public string ArticleCode => articleCodeField?.Text?.ToString()?.Trim() ?? string.Empty;
        public UnitType? UnitType => unitTypeBox.SelectedItem >= 1 ? (UnitType)(unitTypeBox.SelectedItem - 1) : null;
        public QuantityType? QuantityType => quantityTypeBox.SelectedItem >= 1 ? (QuantityType)(quantityTypeBox.SelectedItem - 1) : null;

        public event Action OnFilterApplied;

        public ArticleFilterForm() : base("Menu de Listado de Articulos.")
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

            var unitTypeBoxLabel = new Label("|-- Tipo de Unidad --|")
            {
                X = Pos.Percent(25) - 20,
                Y = Pos.Bottom(containerCodeLabel) + 3,
                Width = 40,
                TextAlignment = TextAlignment.Centered,
            };

            unitTypeBox = new ComboBox()
            {
                X = Pos.Percent(25) - 20,
                Y = Pos.Bottom(unitTypeBoxLabel) + 1,
                Width = 40,
                Height = 5,
                TextAlignment = TextAlignment.Centered,
            };

            var sourceTypes = new List<string>() { " " };
            sourceTypes.AddRange(Enum.GetNames(typeof(UnitType)));
            unitTypeBox.SetSource(sourceTypes);

            var quantityTypeBoxLabel = new Label("|-- Tipo de Cantidad --|")
            {
                X = Pos.Percent(70) - 10,
                Y = Pos.Top(unitTypeBoxLabel),
                Width = 40,
                TextAlignment = TextAlignment.Centered,
            };

            quantityTypeBox = new ComboBox()
            {
                X = Pos.Percent(70) - 10,
                Y = Pos.Bottom(quantityTypeBoxLabel) + 1,
                Width = 40,
                Height = 6,
                TextAlignment = TextAlignment.Centered,
            };

            var sourceStates = new List<string>() { " " };
            sourceStates.AddRange(Enum.GetNames(typeof(QuantityType)));
            quantityTypeBox.SetSource(sourceStates);

            var submitButton = new Button("Filtrar")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(unitTypeBoxLabel) + 4
            };

            submitButton.Clicked += () =>
            {
                OnFilterApplied?.Invoke();
            };

            Add(containerCodeLabel, containerCodeField, clientCodeLabel, clientCodeField,
                 articleCodeLabel, articleCodeField, unitTypeBoxLabel, unitTypeBox,
                 quantityTypeBoxLabel, quantityTypeBox, submitButton);
        }
    }

    static class ArticleGE
    {
        public static void Search(UI.UI ui)
        {
            Search(ui, (ui, win, IDField) =>
            {
                ArticleList articleEntries = ui.Containers.FilterArticlesBy((container, client, article) =>
                {
                    if (article.ID == IDField.Text?.ToString()?.Trim())
                    {
                        article.ContainerID = container?.ID;
                        article.ClientID = client?.ID;
                        return true;
                    }
                    return false;
                });
                ui.Containers.Articles.ForEach((art) =>
                {
                    if (art.ID == IDField.Text?.ToString()?.Trim())
                    {
                        articleEntries.Add(art);
                    }
                });

                var dataTable = new DataTable();
                Article.GenerateColumns(dataTable);
                articleEntries.ForEach((art) =>
                {
                    art.ToRow(dataTable);
                });

                var tableView = new TableView()
                {
                    Text = "Tabla de Articulos",
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
                onSubmit(ui, form, articleIDField);
            };


            form.Add(articleIDLabel, articleIDField, submitButton);


            Application.Top.Add(form);
            Application.Refresh();
        }

        public static void ShowForm(UI.UI ui)
        {
            ShowForm(ui, (description, weight, quantity, unitPrice, unitType, quantityType, containerId, clientId) =>
            {
                if (containerId is null && clientId is not null)
                {
                    MessageBox.Query("Error", "No puede agregar un Articulo a un cliente sin especificar el contenedor", "OK");
                    return;
                }

                Article article = new Article(
                    description,
                    weight,
                    unitType,
                    quantity,
                    quantityType,
                    unitPrice,
                    containerId,
                    clientId
                );
                if (containerId is null)
                {
                    ui.Containers.Articles.Add(article);
                }
                else
                {
                    ContainerNode? container = ui.Containers.Find(containerId);
                    if (container is null)
                    {
                        MessageBox.Query("Error", $"Contenedor {containerId} no encontrado", "OK");
                        return;
                    }
                    ClientNode? client = container.Clients.Find(clientId ?? "");
                    if (client is null)
                    {
                        MessageBox.Query("Error", $"Cliente {clientId} no encontrado", "OK");
                        return;
                    }

                    client.Articles.Add(article);
                }

                MessageBox.Query("Registro Completado", article.ToString(), "OK");
                ShowForm(ui);
            });
        }

        public static void ShowForm(UI.UI ui, Action<string, float, uint, float, UnitType, QuantityType, string?, string?> onSubmit)
        {
            Application.Top.RemoveAll();
            ui.MenuBar();
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

        public static void ShowArticles(UI.UI ui)
        {
            ShowArticles(ui, (args, dataTable) =>
                {
                    // int rowIndex = args.Row;
                    // int colIndex = args.Col;
                    // var selectedRow = dataTable.Rows[rowIndex];
                    // var code = selectedRow.Field<string>(0) ?? "";
                    // if (colIndex == 1) ContainerClientsEventHandler(ui, code);
                    // if (colIndex == 2) ArticleClientsEventHandler(ui, code);
                });
        }

        public static void ShowArticles(UI.UI ui, Action<TableView.CellActivatedEventArgs, DataTable>? onCellActivated)
        {
            var articles = ui.Containers.GetArticles();
            var dataTable = articles.GetDataTable();
            ShowArticles(ui, dataTable, (containerID, clientID, articleID, unitType, quantityType) =>
            {
                ArticleList filt = ui.Containers.FilterArticlesBy((c, cl, art) =>
                {
                    if (!(containerID == "") && !(c?.ID == containerID)) return false;
                    if (!(clientID == "") && (cl?.ID != clientID)) return false;
                    if (!(articleID == "") && (art.ID != articleID)) return false;
                    if (unitType is not null && !(art.Unit == unitType)) return false;
                    if (quantityType is not null && !(art.QuantityType == quantityType)) return false;

                    return true;
                });

                return filt.GetDataTable();
            }, onCellActivated, false, null);
        }

        public static void ShowArticles(UI.UI ui, DataTable dataTable, Func<string, string, string, UnitType?, QuantityType?, DataTable>? onSubmit, Action<TableView.CellActivatedEventArgs, DataTable>? onCellActivated, bool isSubView, Action? OnBack)
        {
            Application.Top.RemoveAll();
            ui.MenuBar();
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
                tableView.Table = onSubmit?.Invoke(form.ContainerCode, form.ClientCode, form.ArticleCode, form.UnitType, form.QuantityType);
            };

            tableView.CellActivated += (args) =>
            {
                onCellActivated?.Invoke(args, dataTable);
            };

            win.Add(tableView);
            Application.Top.Add(form, win);
        }

        public static void ArticleClientsEventHandler(UI.UI ui, string containerID)
        {
            ClientList filt = ui.Containers.FilterClientsBy((container, client) =>
            {
                if (container is null) return false;
                if (containerID.Trim() == "" || container.ID.Contains(containerID)) return true;
                return false;
            });

            ClientGE.ShowClients(ui, filt.GetDataTable(), (clientID, containerID, type) =>
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
            }, null, true);
            Application.Refresh();
        }
    }

}