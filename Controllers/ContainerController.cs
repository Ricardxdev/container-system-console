using containers.Models;
using containers.Lists;
using Terminal.Gui;
using System.Data;
using containers.Helpers;
using containers.Components;
using contaniers.Components;

namespace containers.Controllers
{
    public partial class Controller
    {
        public void OnContainerSearchHandler(string? code)
        {
            Container? container = Containers.Find(code ?? "");
            if (container is not null)
            {
                MessageBox.Query("Datos del contenedor", container.ToString(), "OK");
            }
            else
            {
                int choice = MessageBox.Query("Contenedor no encontrado", "Este contenedor no se encuentra registrado. Desea llenar el formulario de registro?", "Si", "No");
                if (choice == 0)
                {
                    View.ShowContainerForm(OnContainerFormSubmitHandler);
                }
            }
        }

        public void OnContainerFormSubmitHandler(ContainerType[] types, ContainerState state, float capacity, float tare)
        {
            Container container = new Container(
                types,
                state,
                capacity,
                tare
            );
            Containers.Add(container);
            MessageBox.Query("Registro Completado", container.ToString(), "OK");
            View.ShowContainerForm(OnContainerFormSubmitHandler);
        }

        public void OnContainerCellActivatedHandler(TableView.CellActivatedEventArgs args, DataTable dataTable)
        {
            int rowIndex = args.Row;
            int colIndex = args.Col;
            var selectedRow = dataTable.Rows[rowIndex];
            var code = selectedRow.Field<string>(0) ?? "";
            if (colIndex == 6) ContainerClientsEventHandler(code);
            if (colIndex == 7) ContainerArticlesEventHandler(code);
            if (colIndex == 8) OnContainerRemoveNodeHandler(code);
        }

        public DataTable OnContainerFilterApplied(string containerID, string clientID, string articleID, ContainerType? type, ContainerState? state)
        {
            ContainerList filt = Containers.FilterBy((c) =>
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
        }

        public void OnContainerRemoveNodeHandler(string containerId)
        {
            var decision = Common.YesNoDialog("Confirmar Accion", "Eliminara este Contenedor y su contenido?");
            if (decision)
            {
                Containers.ForEach((c) =>
            {
                if (c.ID == containerId) Containers.Remove(containerId);
            });

                MessageBox.Query("Mensaje", "Contenedor eliminado correctamente", "OK");
                ShowContainers();
            }
        }
        public void ContainerClientsEventHandler(string ContainerID)
        {
            ClientList filt = Containers.FilterClientsBy((container, client) =>
            {
                if (container is null) return false;
                if (ContainerID.Trim() == "" || container.ID.Contains(ContainerID)) return true;
                return false;
            });

            View.ShowClients(filt.GetDataTable(), (clientID, containerID, type) =>
            {
                ClientList filt = Containers.FilterClientsBy((container, client) =>
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
                    var containerId = selectedRow.Field<string>(0) ?? "";
                    if (colIndex == 6) ClientArticlesEventHandler(ContainerID, clientId);
                    if (colIndex == 7) ContainerArticlesEventHandler(ContainerID);
                }, ShowContainers, true);
            Application.Refresh();
        }

        public void ContainerArticlesEventHandler(string ContainerID)
        {
            ArticleList filt = Containers.FilterArticlesBy((container, client, article) =>
            {
                if (container is null) return false;
                if (ContainerID.Trim() == "" || container.ID.Contains(ContainerID)) return true;
                return false;
            });

            View.ShowArticles(filt.GetDataTable(), (containerID, clientID, articleID, unitType, quantityType, withoutOwner) =>
            {
                ArticleList filt = Containers.FilterArticlesBy((c, cl, art) =>
                {
                    if (c?.ID != ContainerID) return false;
                    if (!(clientID == "") && (cl?.ID != clientID)) return false;
                    if (!(articleID == "") && (art.ID != articleID)) return false;
                    if (unitType is not null && !(art.Unit == unitType)) return false;
                    if (quantityType is not null && !(art.QuantityType == quantityType)) return false;
                    if (art.ClientID is null && !withoutOwner) return false;
                    if (art.ClientID is not null && withoutOwner) return false;

                    return true;
                });

                return filt.GetDataTable();
            }, OnArticleCellActivatedHandler, true, ShowContainers);

            Application.Refresh();
        }
    }

}