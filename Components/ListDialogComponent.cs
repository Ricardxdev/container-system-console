namespace containers.Components
{
    using System;
    using Terminal.Gui;

    public class ListDialog : Dialog
    {
        private ListView listView;
        private string[] items;

        public ListDialog(string title, string message, string[] items, Action<string> OnSelect) : base(title)
        {
            this.items = items;

            listView = new ListView(items)
            {
                X = 0,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill() - 3
            };

            // Crear botón para seleccionar
            var selectButton = new Button("Select")
            {
                X = Pos.Center() - 10,
                Y = Pos.Bottom(listView) + 1
            };
            selectButton.Clicked += () =>
            {
                if (listView.SelectedItem >= 0)
                {
                    string selectedItem = items[listView.SelectedItem];
                    OnSelect(selectedItem);
                    Application.RequestStop(); // Cierra el diálogo
                }
                else
                {
                    MessageBox.ErrorQuery(50, 7, "Error", "Por favor selecciona un elemento de la lista.", "Ok");
                }
            };

            var cancelButton = new Button("Cancel")
            {
                X = Pos.Center() + 2,
                Y = Pos.Bottom(listView) + 1
            };
            cancelButton.Clicked += () => Application.RequestStop(); // Cierra el diálogo

            // Agregar componentes al diálogo
            Add(listView, selectButton, cancelButton);
        }
    }

}