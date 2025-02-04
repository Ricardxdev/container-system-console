using containers.Models;
using containers.Lists;
using System.Text.RegularExpressions;
using Terminal.Gui;
using System.Diagnostics;
using System.Runtime.InteropServices;
using containers.Engines;

namespace containers
{
    class Program
    {

        static void Main(string[] args)
        {
            // Application.Init();

            // var playerView = new AudioPlayerComponent()
            // {
            //     X = Pos.Center(),
            //     Y = Pos.Center(),
            //     Width = Dim.Fill() - 160,
            //     Height = Dim.Fill() - 40,
            // };

            // Application.Top.Add(playerView);

            // Application.Run();
            Console.Clear();
            ContainerList containers = new ContainerList();
            PreloadContainers(containers);
            PreloadClients(containers);
            PreloadArticles(containers);

            Application.Init();
            Colors.Base = Themes.BlueOnBlack();
            var ui = new UI.UI(containers);
            ui.Home();

            Application.Run();
            Application.Shutdown();
        }

        static void PreloadContainers(ContainerList containers)
        {
            string containersPath = "./Data/ContainerRecords.dat";

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
                    ContainerType[] types = [ContainerType.FEU, ContainerType.FR];//int.Parse(fields[1]);
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
            string clientsPath = "./Data/ClientRecords.dat";

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

                    Client client = new Client(idClient, name, type, address, phone, idContainer);
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
            string articlesPath = "./Data/ArticleRecords.dat";

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
                    
                    Article article = new Article(idArticle, description, weight, UnitType.KG, quantity, type, unitPrice, idContainer, idClient);
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