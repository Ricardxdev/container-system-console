using containers.Models;
using System.Data;

namespace containers.Lists {
    public class ClientNode: Client {
        public ClientNode? Next { get; private set; }
        public ArticleList Articles { get; private set; }

        public ClientNode(Client client) : base(client.ID, client.Name, client.Type, client.Address, client.Phone, client.ContainerID) {
            Articles = new ArticleList();
        }

        public ClientNode(Client client, ArticleList articles) : base(client.ID, client.Name, client.Type, client.Address, client.Phone, client.ContainerID) {
            Articles = articles;
        }

        public void SetNext(ClientNode? next) {
            Next = next;
        }
    }

    public class ClientList {
        private ClientNode? head;
        private ClientNode? tail;

        public ClientList() {
            head = null;
            tail = null;
        }

        public void Add(Client client) {
            ClientNode newNode = new ClientNode(client);
            if (head == null) {
                head = newNode;
                tail = newNode;
            } else {
                tail.SetNext(newNode);
                tail = newNode;
            }
        }

        public ClientNode? Find(string ID) {
            ClientNode? current = head;
            while (current != null) {
                if (current.ID == ID) {
                    return current;
                }
                current = current.Next;
            }
            return null;
        }

        public void Update(Client client) {
            ClientNode? current = head;
            while (current != null) {
                if (current.ID == client.ID) {
                    current.SetName(client.Name);
                    current.SetType(client.Type);
                    current.SetAddress(client.Address);
                    current.SetPhone(client.Phone);
                    break;
                }
                current = current.Next;
            }
        }

        public void Remove(string ID) {
            ClientNode? current = head;
            ClientNode? previous = null;
            while (current != null) {
                if (current.ID == ID) {
                    if (previous == null) {
                        head = current.Next;
                    } else {
                        previous.SetNext(current.Next);
                    }
                    if (current == tail) {
                        tail = previous;
                    }
                    break;
                }
                previous = current;
                current = current.Next;
            }
        }

        public void ForEach(Action<ClientNode> action) {
            ClientNode? current = head;
            while (current != null) {
                action(current);
                current = current.Next;
            }
        }

        public DataTable GetDataTable()
        {
            var dataTable = new DataTable();
            Client.GenerateColumns(dataTable);

            ForEach((p) =>
            {
                p.ToRow(dataTable);
            });
            return dataTable;
        }

        public void Print() {
            ClientNode? current = head;
            while (current != null) {
                Console.WriteLine($"Client: {current.ID}");
                current = current.Next;
            }
        }
    }

}