using containers.Models;
using Terminal.Gui;

namespace containers.Components
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

        public event Action<string, float, uint, float, UnitType, QuantityType, string, string?> OnFilterApplied;
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

            var containerIdLabel = new Label("ID Contenedor:    ")
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
        private ComboBox hasOwnerBox;

        public string ContainerCode => containerCodeField?.Text?.ToString()?.Trim() ?? string.Empty;
        public string ClientCode => clientCodeField?.Text?.ToString()?.Trim() ?? string.Empty;
        public string ArticleCode => articleCodeField?.Text?.ToString()?.Trim() ?? string.Empty;
        public UnitType? UnitType => unitTypeBox.SelectedItem >= 1 ? (UnitType)(unitTypeBox.SelectedItem - 1) : null;
        public QuantityType? QuantityType => quantityTypeBox.SelectedItem >= 1 ? (QuantityType)(quantityTypeBox.SelectedItem - 1) : null;
        public bool WithoutOwner => hasOwnerBox.SelectedItem == 1 ? true : false;

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

            var hasOwnerLabel = new Label("No Asignados")
            {
                X = Pos.Percent(20) - 20,
                Y = Pos.Bottom(articleCodeLabel) + 3,
                Width = 40,
                TextAlignment = TextAlignment.Centered,
            };
            hasOwnerBox = new ComboBox()
            {
                X = Pos.Percent(20) - 20,
                Y = Pos.Bottom(hasOwnerLabel) + 1,
                Width = 40,
                Height = 5,
                TextAlignment = TextAlignment.Centered,
            };
            var decisions = new List<string>() { "NO", "SI" };
            hasOwnerBox.SetSource(decisions);

            var unitTypeBoxLabel = new Label("|-- Tipo de Unidad --|")
            {
                X = Pos.Percent(50) - 20,
                Y = Pos.Bottom(containerCodeLabel) + 3,
                Width = 40,
                TextAlignment = TextAlignment.Centered,
            };

            unitTypeBox = new ComboBox()
            {
                X = Pos.Percent(50) - 20,
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
                X = Pos.Percent(80) - 20,
                Y = Pos.Top(unitTypeBoxLabel),
                Width = 40,
                TextAlignment = TextAlignment.Centered,
            };

            quantityTypeBox = new ComboBox()
            {
                X = Pos.Percent(80) - 20,
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
                 articleCodeLabel, articleCodeField, hasOwnerLabel, hasOwnerBox, unitTypeBoxLabel, unitTypeBox,
                 quantityTypeBoxLabel, quantityTypeBox, submitButton);
        }
    }
}