using containers.Models;
using System.Data;

namespace containers.Lists
{
    public class ArticleNode : Article
    {
        public ArticleNode? Next { get; private set; }

        public ArticleNode(Article article) : base(article.ID, article.Description, article.Weight, article.Unit, article.Quantity, article.QuantityType, article.UnitPrice, article.ContainerID, article.ClientID)
        {

        }

        public void SetNext(ArticleNode? next)
        {
            Next = next;
        }
    }

    public class ArticleList
    {
        private ArticleNode? head;
        private ArticleNode? tail;

        public ArticleList()
        {
            head = null;
            tail = null;
        }

        public void Add(Article article)
        {
            ArticleNode newNode = new ArticleNode(article);
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

        public ArticleNode? Find(string ID)
        {
            ArticleNode? current = head;
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

        public void Update(Article article)
        {
            ArticleNode? current = head;
            while (current != null)
            {
                if (current.ID == article.ID)
                {
                    current.SetDescription(article.Description);
                    current.SetWeight(article.Weight);
                    current.SetUnit(article.Unit);
                    current.SetQuantity(article.Quantity);
                    current.SetQuantityType(article.QuantityType);
                    current.SetUnitPrice(article.UnitPrice);
                    break;
                }
                current = current.Next;
            }
        }

        public void Remove(string ID)
        {
            ArticleNode? current = head;
            ArticleNode? previous = null;
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

        public void ForEach(Action<ArticleNode> action)
        {
            ArticleNode? current = head;
            while (current != null)
            {
                action(current);
                current = current.Next;
            }
        }

        public DataTable GetDataTable()
        {
            var dataTable = new DataTable();
            Article.GenerateColumns(dataTable);

            ForEach((p) =>
            {
                p.ToRow(dataTable);
            });
            return dataTable;
        }

        public void Print()
        {
            ArticleNode? current = head;
            while (current != null)
            {
                Console.WriteLine($"Article: {current.ID}");
                current = current.Next;
            }
        }
    }

}