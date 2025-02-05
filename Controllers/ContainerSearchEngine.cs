using containers.Models;
using containers.Lists;

namespace containers.Engines {
    class ContainerSearchEngine {
        public static ClientList ListClientInContainer(ContainerList containers, string ID) {
            ContainerNode? container = Search(containers, ID);
            if (container == null) {
                return new ClientList();
            }
            return container.Clients;
        }

        public static ArticleList ListArticlesInContainer(ContainerList containers, string ID) {
            ContainerNode? container = Search(containers, ID);
            if (container == null) {
                return new ArticleList();
            }

            ArticleList articles = new ArticleList();
            container.Clients.ForEach((client) => {
                client.Articles.ForEach((article) => {
                    articles.Add(article);
                });
            });
            return articles;
        }

        public static ContainerList ListContainersWithArticle(ContainerList containers, string articleID) {
            ContainerList result = new ContainerList();
            containers.ForEach((container) => {
                container.Clients.ForEach((client) => {
                    client.Articles.ForEach((article) => {
                        if (article.ID == articleID) {
                            result.Add(container);
                        }
                    });
                });
            });
            return result;
        }

        public static ContainerList ListContainersWithClient(ContainerList containers, string clientID) {
            ContainerList result = new ContainerList();
            containers.ForEach((container) => {
                container.Clients.ForEach((client) => {
                    if (client.ID == clientID) {
                        result.Add(container);
                    }
                });
            });
            return result;
        }

        public static ContainerList ListContainersWithState(ContainerList containers, ContainerState state) {
            ContainerList result = new ContainerList();
            containers.ForEach((container) => {
                if (container.State == state) {
                    result.Add(container);
                }
            });
            return result;
        }

        public static ContainerList ListEmptyContainers(ContainerList containers) {
            return ListContainersWithState(containers, ContainerState.EMPTY);
        }

        public static ContainerList ListContainersInMaintenance(ContainerList containers) {
            return ListContainersWithState(containers, ContainerState.IN_MAINTENANCE);
        }

        public static float GetTotalValueOfContainer(ContainerNode container) {

            float totalValue = 0;
            container.Clients.ForEach((client) => {
                client.Articles.ForEach((article) => {
                    totalValue += article.UnitPrice * article.Quantity;
                });
            });
            return totalValue;
        }

        public static float GetTotalValueOfContainer(ContainerList containers, string ID) {
            ContainerNode? container = Search(containers, ID);
            if (container == null) {
                return 0;
            }

            float totalValue = 0;
            container.Clients.ForEach((client) => {
                client.Articles.ForEach((article) => {
                    totalValue += article.UnitPrice * article.Quantity;
                });
            });
            return totalValue;
        }

        public static ContainerNode? Search(ContainerList containers, string ID) {
            return containers.Find(ID);
        }
    }
}