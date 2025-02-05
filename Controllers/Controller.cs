using System.Data;
using containers.Lists;
using containers.Models;
using containers.Views;
using Terminal.Gui;

namespace containers.Controllers
{
    public partial class Controller
    {
        private ContainerList Containers;
        private Views.View View;
        public Controller(ContainerList containers)
        {
            Containers = containers;
            View = new Views.View();
            View.MenuBar = MenuBar;
        }

        public void Home() {
            View.Home();
        }

        public void ShowArticles()
        {
            var articles = Containers.FilterArticlesBy((c, cl, art) => art.ClientID != null);
            var dataTable = articles.GetDataTable();
            View.ShowArticles(dataTable, OnArticleFilterApplied, OnArticleCellActivatedHandler, false, null);
        }

        public void ShowClients()
        {
            var clients = Containers.GetClients();
            var dataTable = clients.GetDataTable();
            View.ShowClients(dataTable, OnClientFilterApplied, OnClientCellActivatedHandler, ShowContainers, false);
        }

        public void ShowContainers()
        {
            var dataTable = Containers.GetDataTable();
            View.ShowContainers(dataTable, OnContainerFilterApplied, OnContainerCellActivatedHandler);
        }

        public void SearchArticle() {
            View.SearchArticle(OnArticleSearchHandler);
        }

        public void SearchClient() {
            View.SearchClient(OnClientSearchHandler);
        }

        public void SearchContainer() {
            View.SearchContainer(OnContainerSearchHandler);
        }

        public void ShowArticleForm() {
            View.ShowArticleForm(OnArticleFormSubmitHandler);
        }

        public void ShowClientForm() {
            View.ShowClientForm(OnClientFormSubmitHandler);
        }
        
        public void ShowContainerForm() {
            View.ShowContainerForm(OnContainerFormSubmitHandler);
        }
    }
}