using System;
using Terminal.Gui;

namespace contaniers.Components
{
    public static class Common
    {
        public static bool YesNoDialog(string title, string prompt)
        {
            bool result = false;

            var yesButton = new Button("Yes");
            yesButton.Clicked += () =>
            {
                result = true;
                Application.RequestStop();
            };

            var noButton = new Button("No");
            noButton.Clicked += () =>
            {
                result = false;
                Application.RequestStop(); // Cierra el diálogo y retorna control a la aplicación
            };


            var dialog = new Dialog(title, 50, 6, yesButton, noButton);

            var label = new Label(prompt)
            {
                X = 1,
                Y = 1,
                Width = Dim.Fill(),
                TextAlignment = TextAlignment.Centered
            };

            dialog.Add(label);
            Application.Run(dialog);

            return result;
        }
    }
};