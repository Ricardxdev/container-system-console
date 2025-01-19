using System.Text.RegularExpressions;

namespace containers
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Starting...");
            //Console.WriteLine("Preloading Containers, Clients and Articles from .dat files...");
            ContainerList containers = new ContainerList();
            PreloadContainers(containers);
            PreloadClients(containers);
            PreloadArticles(containers);

            // Console.WriteLine("Printing Containers, Clients and Articles...");
            // containers.ForEach((container) =>
            // {
            //     Console.WriteLine(">>>>#####<<<<");
            //     container.Print();
            //     container.Clients.ForEach((client) =>
            //     {
            //         client.Print();
            //         client.Articles.ForEach((article) =>
            //         {
            //             article.Print();
            //         });
            //         Console.WriteLine("--------------------------------------------------");
            //     });
            //     Console.WriteLine("________________________________________________________");
            // });
            // Console.WriteLine("Done!");
        }

        static void PreloadContainers(ContainerList containers)
        {
            string containersPath = "ContainerRecords.dat";

            foreach (string record in File.ReadAllLines(containersPath))
            {
                string[] fields = Regex.Split(record, "\'\\|\'");

                for (int i = 0; i < fields.Length; i++)
                {
                    fields[i] = fields[i].Trim('\''); // Elimina las comillas restantes
                }

                try
                {
                    string id = fields[0];
                    ContainerType[] types = [];//int.Parse(fields[1]);
                    ContainerState state = ContainerState.EMPTY;//fields[2];
                    float weight = float.Parse(fields[3]);
                    float tare = float.Parse(fields[4]);

                    Container container = new Container(id, types, state, weight, tare);
                    containers.Add(container);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al procesar registro en el archivo " + containersPath + ": " + ex.Message);
                }
            }
        }

        static void PreloadClients(ContainerList containers)
        {
            string clientsPath = "ClientRecords.dat";

            foreach (string record in File.ReadAllLines(clientsPath))
            {
                string[] fields = Regex.Split(record, "\'\\|\'");

                for (int i = 0; i < fields.Length; i++)
                {
                    fields[i] = fields[i].Trim('\''); // Elimina las comillas restantes
                }

                try
                {
                    string idContainer = fields[0];
                    string idClient = fields[1];
                    string name = fields[2];
                    ClientType type = ClientType.MINORIST;//fields[3];
                    string address = fields[4];
                    string phone = fields[5];

                    Client client = new Client(idClient, name, type, address, phone);
                    containers.Find(idContainer)?.Clients.Add(client);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al procesar registro en el archivo " + clientsPath + ": " + ex.Message);
                }
            }
        }

        static void PreloadArticles(ContainerList containers)
        {
            string articlesPath = "ArticleRecords.dat";

            foreach (string record in File.ReadAllLines(articlesPath))
            {
                string[] fields = Regex.Split(record, "\'\\|\'");

                for (int i = 0; i < fields.Length; i++)
                {
                    fields[i] = fields[i].Trim('\''); // Elimina las comillas restantes
                }

                try
                {
                    string idContainer = fields[0];
                    string idClient = fields[1];
                    string idArticle = fields[2];
                    string description = fields[3];
                    float weight = float.Parse(fields[4]);
                    uint quantity = uint.Parse(fields[5]);
                    QuantityType type = QuantityType.BOX;//fields[6];
                    float unitPrice = float.Parse(fields[7]);

                    Article article = new Article(idArticle, description, weight, UnitType.KG, quantity, type, unitPrice);
                    containers.Find(idContainer)?.Clients.Find(idClient)?.Articles.Add(article);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al procesar registro en el archivo " + articlesPath + ": " + ex.Message);
                }
            }
        }
    }
}