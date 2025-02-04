using containers.Lists;
using Terminal.Gui;

namespace containers.Engines
{
    static class GraphicEngine
    {
        // public void ShowBuyScreen()
        // {
        //     BillState = States.Buy;
        //     Top.RemoveAll();
        //     MenuBar();

        //     var win = new Window("Menu de Compra.")
        //     {
        //         X = 0,
        //         Y = 1,
        //         Width = Dim.Fill(),
        //         Height = Dim.Fill()
        //     };

        //     var client = CurrentBill.Client;

        //     var billIDLabel = new Label("Nro. Factura: ")
        //     {
        //         X = 10,
        //         Y = 2,
        //         Width = 40
        //     };
        //     billIDLabel.Text = billIDLabel.Text.ToString() + CurrentBill.ID;

        //     var clientIDLabel = new Label("C.I Cliente: ")
        //     {
        //         X = 10,
        //         Y = Pos.Bottom(billIDLabel) + 1,
        //         Width = 40
        //     };
        //     clientIDLabel.Text = clientIDLabel.Text.ToString() + client.License;

        //     var firstNameLabel = new Label("Nombre(s): ")
        //     {
        //         X = 10,
        //         Y = Pos.Bottom(clientIDLabel) + 1,
        //         Width = 40,
        //     };
        //     firstNameLabel.Text = firstNameLabel.Text.ToString() + client.FirstName;

        //     var lastNameLabel = new Label("Apellido(s): ")
        //     {
        //         X = 10,
        //         Y = Pos.Bottom(firstNameLabel) + 1,
        //         Width = 40
        //     };
        //     lastNameLabel.Text = lastNameLabel.Text.ToString() + client.LastName;

        //     var totalLabel = new Label("Total a Pagar: ")
        //     {
        //         X = 10,
        //         Y = Pos.Bottom(lastNameLabel) + 1,
        //         Width = 40
        //     };
        //     totalLabel.Text += CurrentBill.GetTotal().ToString();

        //     var addProductButton = new Button("Agregar Producto")
        //     {
        //         X = Pos.Percent(25) - 8,
        //         Y = Pos.Bottom(totalLabel) + 1,
        //         Width = 16
        //     };

        //     var buyButton = new Button("Pagar")
        //     {
        //         X = Pos.Percent(75) - 5,
        //         Y = Pos.Bottom(totalLabel) + 1,
        //         Width = 10
        //     };

        //     addProductButton.Clicked += () =>
        //     {
        //         ShowProducts((args, dataTable) =>
        //         {
        //             int rowIndex = args.Row;
        //             int colIndex = args.Col;

        //             if (colIndex == 6)
        //             {
        //                 var selectedRow = dataTable.Rows[rowIndex];
        //                 var code = selectedRow.Field<string>("Codigo");
        //                 var p = Products.Get(code);

        //                 string quantityInput = ShowInputDialog("Cantidad Requerida", "Ingrese la cantidad de productos a agregar:");
        //                 while (true)
        //                 {
        //                     if (!string.IsNullOrEmpty(quantityInput))
        //                     {
        //                         if (Int32.Parse(quantityInput) <= 0)
        //                         {
        //                             quantityInput = ShowInputDialog("La Cantidad debe ser positiva", "Ingrese la cantidad de productos a agregar:");
        //                         }
        //                         else
        //                         {
        //                             CurrentBill.Add(new BillProduct(Products.Get(code), Int32.Parse(quantityInput)));
        //                             break;
        //                         }
        //                     }
        //                     else
        //                     {
        //                         MessageBox.Query("Mensaje", "No se ha ingresado una cantidad, se cancela la solicitud.", "OK");
        //                         break;
        //                     }
        //                 }


        //                 ShowBuyScreen();
        //                 Application.Refresh();
        //             }
        //         });
        //     };

        //     buyButton.Clicked += () =>
        //     {
        //         ShowBillDialog(CurrentBill);
        //     };

        //     var dataTable = CurrentBill.GetDataTable();

        //     var tableView = new TableView()
        //     {
        //         Text = "Productos a Pagar",
        //         X = 0,
        //         Y = Pos.Bottom(addProductButton) + 2,
        //         Width = Dim.Fill(),
        //         Height = Dim.Fill(),
        //         Table = dataTable
        //     };

        //     tableView.CellActivated += (args) =>
        //     {
        //         int rowIndex = args.Row;
        //         int colIndex = args.Col;
        //         var selectedRow = dataTable.Rows[rowIndex];
        //         var code = selectedRow.Field<string>("Codigo");

        //         BillProduct? bill;
        //         switch (colIndex)
        //         {
        //             case 5:
        //                 bill = CurrentBill.Get(code);
        //                 bill.UpdateQuantity(bill.Quantity + 1);
        //                 CurrentBill.Update(bill);
        //                 break;
        //             case 6:
        //                 bill = CurrentBill.Get(code);
        //                 bill.UpdateQuantity(bill.Quantity - 1);
        //                 CurrentBill.Update(bill);
        //                 break;
        //             case 7:
        //                 CurrentBill.Delete(code);
        //                 break;
        //         }

        //         ShowBuyScreen();
        //         Application.Refresh();
        //     };

        //     win.Add(billIDLabel, clientIDLabel, firstNameLabel, lastNameLabel, totalLabel, addProductButton, buyButton, tableView);
        //     Top.Add(win);
        //     Application.Refresh();
        // }

        static string ShowInputDialog(string title, string prompt)
        {
            string result = null;


            var okButton = new Button("OK");
            okButton.Clicked += () => Application.RequestStop();

            var cancelButton = new Button("Cancelar");
            cancelButton.Clicked += () =>
                {
                    result = null;
                    Application.RequestStop();
                };


            var dialog = new Dialog(title, 50, 10, okButton, cancelButton);


            var label = new Label(prompt)
            {
                X = 1,
                Y = 1,
                Width = Dim.Fill()
            };


            var textField = new TextField("")
            {
                X = 1,
                Y = Pos.Bottom(label) + 1,
                Width = Dim.Fill() - 2
            };

            dialog.Add(label, textField);


            Application.Run(dialog);


            if (okButton.HasFocus)
            {
                result = textField.Text.ToString();
            }

            return result;
        }
    }

    static class Themes
    {
        public static Terminal.Gui.ColorScheme GreyOnBlack()
        {
            var greyOnBlack = new Terminal.Gui.ColorScheme();
            greyOnBlack.Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.DarkGray, Terminal.Gui.Color.Black);
            greyOnBlack.HotNormal = new Terminal.Gui.Attribute(Terminal.Gui.Color.DarkGray, Terminal.Gui.Color.Black);
            greyOnBlack.Focus = new Terminal.Gui.Attribute(Terminal.Gui.Color.Black, Terminal.Gui.Color.DarkGray);
            greyOnBlack.HotFocus = new Terminal.Gui.Attribute(Terminal.Gui.Color.Black, Terminal.Gui.Color.DarkGray);
            greyOnBlack.Disabled = new Terminal.Gui.Attribute(Terminal.Gui.Color.DarkGray, Terminal.Gui.Color.Black);
            return greyOnBlack;
        }

        public static Terminal.Gui.ColorScheme BlueOnBlack()
        {
            var blueOnBlack = new Terminal.Gui.ColorScheme();
            blueOnBlack.Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightBlue, Terminal.Gui.Color.Black);
            blueOnBlack.HotNormal = new Terminal.Gui.Attribute(Terminal.Gui.Color.Cyan, Terminal.Gui.Color.Black);
            blueOnBlack.Focus = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightBlue, Terminal.Gui.Color.BrightYellow);
            blueOnBlack.HotFocus = new Terminal.Gui.Attribute(Terminal.Gui.Color.Cyan, Terminal.Gui.Color.BrightYellow);
            blueOnBlack.Disabled = new Terminal.Gui.Attribute(Terminal.Gui.Color.Gray, Terminal.Gui.Color.Black);
            return blueOnBlack;
        }
    }
}