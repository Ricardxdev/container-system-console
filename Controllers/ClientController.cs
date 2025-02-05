using containers.Models;
using containers.Lists;
using Terminal.Gui;
using System.Data;
using containers.Components;
using contaniers.Components;

namespace containers.Controllers
{
    public partial class Controller
    {
        public void OnClientSearchHandler(Window win, TextField IDField)
        {
            ClientList clientEntries = Containers.FilterClientsBy((container, client) =>
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
        }

        public void OnClientFormSubmitHandler(string name, string phone, string address, ClientType type)
        {
            Client client = new Client(
                name,
                type,
                address,
                phone
            );
            Containers.Clients.Add(client);
            MessageBox.Query("Registro Completado", client.ToString(), "OK");
        }

        public void OnClientCellActivatedHandler(TableView.CellActivatedEventArgs args, DataTable dataTable)
        {
            int rowIndex = args.Row;
            int colIndex = args.Col;
            var selectedRow = dataTable.Rows[rowIndex];
            var clientId = selectedRow.Field<string>(0) ?? "";
            var containerId = selectedRow.Field<string>(1) ?? ""; // CHECK
            if (colIndex == 6) ClientArticlesEventHandler(containerId, clientId);
            if (colIndex == 7) ClientRemovedNodeHandler(containerId, clientId);
        }

        public DataTable OnClientFilterApplied(string? clientID, string? containerID, ClientType? type)
        {
            ClientList filt = Containers.FilterClientsBy((container, client) =>
            {
                if (clientID is not null && client.ID != clientID) return false;
                if (type is not null && client.Type != type) return false;
                if (containerID is not null && container is not null && container.ID != containerID) return false;
                if (containerID is not null && container is null) return false;
                return true;
            });

            return filt.GetDataTable();
        }

        public void ClientRemovedNodeHandler(string containerId, string clientId)
        {
            var decision = Common.YesNoDialog("Confirmar Accion", "Desea Eliminar este cliente?");
            if (decision)
            {
                Containers.FilterClientsBy((c, cl) =>
            {
                if (c is not null && (containerId == "" || c.ID == containerId) && cl?.ID == clientId)
                {
                    cl.Articles.ForEach((art) => {
                        art.ClientID = null;
                        c.Articles.Add(art);
                    });
                    c.Clients.Remove(clientId);
                    return true;
                }
                else if (c is null && cl?.ID == clientId)
                {
                    Containers.Clients.Remove(clientId);
                }

                return false;
            });

                MessageBox.Query("Mensaje", "Cliente eliminado correctamente", "OK");
                ShowClients();
            }
        }

        public void ClientArticlesEventHandler(string ContainerID, string ClientID)
        {
            ArticleList filt = Containers.FilterArticlesBy((container, client, article) =>
            {
                if (container is null) return false;
                if ((ContainerID.Trim() == "" || container.ID == ContainerID) && (ClientID.Trim() == "" || client?.ID == ClientID)) return true;
                return false;
            });

            View.ShowArticles(filt.GetDataTable(), (containerID, clientID, articleID, unitType, quantityType, withoutOwner) =>
            {
                ArticleList filt = Containers.FilterArticlesBy((c, cl, art) =>
                {
                    if (ContainerID.Trim() != "" && c?.ID != ContainerID) return false;
                    if (cl?.ID != ClientID) return false;
                    if (!(articleID == "") && (art.ID != articleID)) return false;
                    if (unitType is not null && !(art.Unit == unitType)) return false;
                    if (quantityType is not null && !(art.QuantityType == quantityType)) return false;
                    if (art.ClientID is null && !withoutOwner) return false;
                    if (art.ClientID is not null && withoutOwner) return false;

                    return true;
                });

                return filt.GetDataTable();
            }, OnArticleCellActivatedHandler, true, () =>
                {
                    var clients = Containers.FilterClientsBy((c, cl) =>
                    {
                        if (ContainerID == "" || c?.ID == ContainerID) return true;
                        return false;
                    });
                    var dataTable = clients.GetDataTable();
                    View.ShowClients(dataTable, OnClientFilterApplied, OnClientCellActivatedHandler, ShowContainers, true);
                });

            Application.Refresh();
        }
    }
}