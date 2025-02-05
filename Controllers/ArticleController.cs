using System.Data;
using containers.Components;
using containers.Lists;
using containers.Models;
using contaniers.Components;
using Terminal.Gui;

namespace containers.Controllers
{
    public partial class Controller
    {
        public void OnArticleSearchHandler(Window win, TextField IDField)
        {
            ArticleList articleEntries = Containers.FilterArticlesBy((container, client, article) =>
            {
                if (article.ID == IDField.Text?.ToString()?.Trim()) return true;
                return false;
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
        }

        public void OnArticleFormSubmitHandler(string description, float weight, uint quantity, float unitPrice, UnitType unitType, QuantityType quantityType, string containerId, string? clientId)
        {
            if (containerId is null || Containers.Find(containerId) == null)
            {
                MessageBox.Query("Error", "Debe Agregar un Codigo de Contenedor Valido", "OK");
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
            if (clientId is null)
            {
                Containers.Find(containerId)?.Articles.Add(article);
            }
            else
            {
                ContainerNode? container = Containers.Find(containerId);
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
            View.ShowArticleForm(OnArticleFormSubmitHandler);
        }

        public void OnArticleCellActivatedHandler(TableView.CellActivatedEventArgs args, DataTable dataTable)
        {
            int rowIndex = args.Row;
            int colIndex = args.Col;
            var selectedRow = dataTable.Rows[rowIndex];
            var id = selectedRow.Field<string>(0) ?? "";
            var containerId = selectedRow.Field<string>(1) ?? "";
            var clientId = selectedRow.Field<string>(2) ?? "";
            if (clientId == "  --> Asignar <--  " && colIndex == 2) ArticleAssignOwnershipHandler(containerId, id);
            if (colIndex == 9) ArticleRemoveOwnershipHandler(containerId, clientId, id);
            if (colIndex == 10) ArticleRemoveNodeHandler(containerId, clientId, id);
        }

        public DataTable OnArticleFilterApplied(string containerId, string clientId, string articleId, UnitType? unitType, QuantityType? quantityType, bool WithoutOwner)
        {
            ArticleList filt = Containers.FilterArticlesBy((c, cl, art) =>
            {
                if (!(containerId == "") && !(c?.ID == containerId)) return false;
                if (!(clientId == "") && (cl?.ID != clientId)) return false;
                if (!(articleId == "") && (art.ID != articleId)) return false;
                if (unitType is not null && !(art.Unit == unitType)) return false;
                if (quantityType is not null && !(art.QuantityType == quantityType)) return false;
                if (art.ClientID is null && !WithoutOwner) return false;
                if (art.ClientID is not null && WithoutOwner) return false;

                return true;
            });

            return filt.GetDataTable();
        }

        public void ArticleClientsEventHandler(string containerID)
        {
            ClientList filt = Containers.FilterClientsBy((container, client) =>
            {
                if (container is null) return false;
                if (containerID.Trim() == "" || container.ID.Contains(containerID)) return true;
                return false;
            });

            View.ShowClients(filt.GetDataTable(), (clientID, containerID, type) =>
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
            }, null, ShowArticles, true);
            Application.Refresh();
        }

        public void ArticleAssignOwnershipHandler(string containerId, string id)
        {
            HashSet<string> clients = new HashSet<string>();
            Containers.GetClients().ForEach((cl) =>
            {
                clients.Add(cl.ID);
            });

            var dialog = new ListDialog("Elige el Cliente", "Nuevo propietario del articulo:", clients.ToArray(), (newClientId) =>
            {
                var container = Containers?.Find(containerId);
                var client = container?.Clients.Find(newClientId);
                if (client is null)
                {
                    client = Containers?.FilterClientsBy((c, cl) =>
                    {
                        if (cl.ID == newClientId) return true;
                        return false;
                    }).head;
                    client?.SetArticles(new ArticleList());
                    container?.Clients.Add(client);
                }
                container?.Articles.ForEach((art) =>
                {
                    if (art.ID == id)
                    {
                        art.ClientID = newClientId;
                        client?.Articles.Add(art);
                        container.Articles.Remove(art.ID);
                        MessageBox.Query("Mensaje", "Articulo reasignado correctamente", "OK");
                    }
                });
            })
            {
                Width = 30,
                Height = 20,
            };

            Application.Run(dialog);
            ShowArticles();
        }

        public void ArticleRemoveOwnershipHandler(string containerId, string clientId, string id)
        {
            var decision = Common.YesNoDialog("Confirmar Accion", "Desea Proceder?");
            if (decision)
            {
                Containers.FilterArticlesBy((c, cl, art) =>
                {
                    if (c.ID == containerId && cl?.ID == clientId && art.ID == id)
                    {
                        var container = Containers.Find(containerId);
                        art.ClientID = null;
                        container?.Articles.Add(art);
                        container?.Clients.Find(clientId)?.Articles.Remove(id);
                        return true;
                    }

                    return false;
                });
            }

            MessageBox.Query("Mensaje", "Operacion completada correctamente", "OK");
            ShowArticles();
        }

        public void ArticleRemoveNodeHandler(string containerId, string clientId, string id)
        {
            var decision = Common.YesNoDialog("Confirmar Accion", "Desea Eliminar este Articulo?");
            if (decision)
            {
                Containers.FilterArticlesBy((c, cl, art) =>
            {
                if (c.ID == containerId && cl?.ID == clientId && art.ID == id)
                {
                    cl.Articles.Remove(id);
                    return true;
                }
                else if (cl is null && art.ID == id)
                {
                    c.Articles.Remove(id);
                }

                return false;
            });

                MessageBox.Query("Mensaje", "Articulo eliminado correctamente", "OK");
                ShowArticles();
            }
        }
    }
}