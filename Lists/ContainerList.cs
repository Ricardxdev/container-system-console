using containers.Engines;
using containers.Models;
using System.Data;

namespace containers.Lists
{
    public class ContainerNode : Container
    {
        public ContainerNode? Next { get; private set; }
        public ClientList Clients { get; private set; }

        public ContainerNode(Container container) : base(container.ID, container.Types, container.State, container.Capacity, container.Tare)
        {
            Clients = new ClientList();
        }

        public ContainerNode(Container container, ClientList clients) : base(container.ID, container.Types, container.State, container.Capacity, container.Tare)
        {
            Clients = clients;
        }

        public void SetNext(ContainerNode? next)
        {
            Next = next;
        }

        public void ToRow(DataTable dataTable)
        {
            dataTable.Rows.Add(ID, String.Join(", ", Types), State, Capacity, Tare, ContainerSearchEngine.GetTotalValueOfContainer(this), "  --> Click <--  ", "  --> Click <--  ", "  --> Sure? <--  ");
        }
    }

    public class ContainerList
    {
        public ClientList Clients { get; private set; }
        public ArticleList Articles { get; private set; }
        private ContainerNode? head;
        private ContainerNode? tail;

        public ContainerList()
        {
            Clients = new ClientList();
            Articles = new ArticleList();
            head = null;
            tail = null;
        }

        public void Add(Container container)
        {
            if (Find(container.ID) != null)
            {
                return;
            }
            ContainerNode newNode = new ContainerNode(container);
            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                tail?.SetNext(newNode);
                tail = newNode;
            }
        }

        public ContainerNode? Find(string ID)
        {
            ContainerNode? current = head;
            while (current != null)
            {
                if (current.ID == ID)
                {
                    return current;
                }
                current = current.Next;
            }
            return null;
        }

        public void Update(Container container)
        {
            ContainerNode? current = head;
            while (current != null)
            {
                if (current.ID == container.ID)
                {
                    current.SetTypes(container.Types);
                    current.SetState(container.State);
                    current.SetCapacity(container.Capacity);
                    current.SetTare(container.Tare);
                    break;
                }
                current = current.Next;
            }
        }

        public void Remove(string ID)
        {
            ContainerNode? current = head;
            ContainerNode? previous = null;
            while (current != null)
            {
                if (current.ID == ID)
                {
                    if (previous == null)
                    {
                        head = current.Next;
                    }
                    else
                    {
                        previous.SetNext(current.Next);
                    }
                    if (current == tail)
                    {
                        tail = previous;
                    }
                    break;
                }
                previous = current;
                current = current.Next;
            }
        }

        public ClientList GetClients()
        {
            var clients = new ClientList();
            ForEach((c) =>
            {
                c.Clients.ForEach(clients.Add);
            });
            Clients.ForEach(clients.Add);

            return clients;
        }

        public ArticleList GetArticles()
        {
            var articles = new ArticleList();
            ForEach((c) =>
            {
                c.Clients.ForEach((cl) =>
                {
                    cl.Articles.ForEach(articles.Add);
                });
            });
            Articles.ForEach(articles.Add);

            return articles;
        }

        public ClientList FilterClientsBy(Func<ContainerNode?, Client, bool> condition)
        {
            ClientList list = new ClientList();
            ForEach((c) =>
            {
                c.Clients.ForEach((cl) =>
                {
                    if (condition(c, cl))
                    {
                        list.Add(cl);
                    }
                });
            });
            Clients.ForEach((cl) =>
            {
                if (condition(null, cl))
                {
                    list.Add(cl);
                }
            });

            return list;
        }

        public ArticleList FilterArticlesBy(Func<ContainerNode?, Client?, Article, bool> condition)
        {
            ArticleList list = new ArticleList();
            ForEach((c) =>
            {
                c.Clients.ForEach((cl) =>
                {
                    cl.Articles.ForEach((art) =>
                    {
                        if (condition(c, cl, art))
                        {
                            list.Add(art);
                        }
                    });
                });
            });
            Articles.ForEach((art) =>
            {
                if (condition(null, null, art))
                {
                    list.Add(art);
                }
            });

            return list;
        }

        public void ForEach(Action<ContainerNode> action)
        {
            ContainerNode? current = head;
            while (current != null)
            {
                action(current);
                current = current.Next;
            }
        }

        public ContainerList FilterBy(Func<ContainerNode, bool> condition)
        {
            ContainerList list = new ContainerList();
            ForEach((c) =>
            {
                if (condition(c))
                {
                    list.Add(c);
                }
            });

            return list;
        }

        public DataTable GetDataTable()
        {
            var dataTable = new DataTable();
            Container.GenerateColumns(dataTable);

            ForEach((p) =>
            {
                p.ToRow(dataTable);
            });
            return dataTable;
        }

        public void Print()
        {
            ContainerNode? current = head;
            while (current != null)
            {
                Console.WriteLine($"Container: {current.ID}");
                current = current.Next;
            }
        }
    }

}