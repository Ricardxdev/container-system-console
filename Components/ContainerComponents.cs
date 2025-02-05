using containers.Models;
using Terminal.Gui;

namespace containers.Components {
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
        public ContainerType? SelectedType => typeBox.SelectedItem >= 1 ? (ContainerType)(typeBox.SelectedItem - 1) : null;
        public ContainerState? SelectedState => stateBox.SelectedItem >= 1 ? (ContainerState)(stateBox.SelectedItem - 1) : null;

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
}