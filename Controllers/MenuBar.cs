using Terminal.Gui;

namespace containers.Controllers
{
    public partial class Controller
    {
        public AudioPlayer player = new AudioPlayer("./Media", "mp3", false);
        public void MenuBar()
        {
            var menu = new MenuBar(
            [
            new MenuBarItem("_Aplicación",
            [
                new MenuItem("_Salir", "", () => Application.RequestStop())
            ]),
            new MenuBarItem("_Contenedores",
            [
                new MenuItem("_Buscar Contenedor", "", () =>
                {
                    SearchContainer();
                }),
                new MenuItem("_Registrar Contenedor", "", () =>
                {
                    ShowContainerForm();
                }),
                new MenuItem("_Listar Contenedores", "", () =>
                {
                    ShowContainers();
                }),
            ]),
            new MenuBarItem("_Clientes",
            [
                new MenuItem("_Buscar Cliente", "", () =>
                {
                    SearchClient();
                }),
                new MenuItem("_Registrar Cliente", "", () =>
                {
                    ShowClientForm();
                }),
                new MenuItem("_Listar Clientes", "", () =>
                {
                    ShowClients();
                }),
            ]),
            new MenuBarItem("_Articulos",
            [
                new MenuItem("_Buscar Articulo", "", () =>
                {
                    SearchArticle();
                }),
                new MenuItem("_Registrar Articulo", "", () =>
                {
                    ShowArticleForm();
                }),
                new MenuItem("_Listar Articulos", "", () =>
                {
                    ShowArticles();
                }),
            ]),
            new MenuBarItem("_Musica",
            [
                new MenuItem("_Reproducir/Saltar", "", () =>
                {
                    player.Play();
                }),
                new MenuItem("_Detener", "", () =>
                {
                    player.Stop();
                }),
            ])
            ]);


            Application.Top.Add(menu);
        }
    }
}