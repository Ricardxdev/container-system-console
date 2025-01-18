namespace containers {
    class ContainerNode: Container {
        public ContainerNode? Next { get; private set; }

        public ContainerNode(Container container) : base(container.ID, container.Type, container.State, container.Capacity, container.Tare) {
            
        }

        public void SetNext(ContainerNode? next) {
            Next = next;
        }
    }

    class ContainerList {
        private ContainerNode? head;
        private ContainerNode? tail;

        public ContainerList() {
            head = null;
            tail = null;
        }

        public void Add(Container container) {
            ContainerNode newNode = new ContainerNode(container);
            if (head == null) {
                head = newNode;
                tail = newNode;
            } else {
                tail.SetNext(newNode);
                tail = newNode;
            }
        }

        public ContainerNode? Find(string ID) {
            ContainerNode? current = head;
            while (current != null) {
                if (current.ID == ID) {
                    return current;
                }
                current = current.Next;
            }
            return null;
        }

        public void Update(Container container) {
            ContainerNode? current = head;
            while (current != null) {
                if (current.ID == container.ID) {
                    current.SetType(container.Type);
                    current.SetState(container.State);
                    current.SetCapacity(container.Capacity);
                    current.SetTare(container.Tare);
                    break;
                }
                current = current.Next;
            }
        }

        public void Remove(string ID) {
            ContainerNode? current = head;
            ContainerNode? previous = null;
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

        public void Print() {
            ContainerNode? current = head;
            while (current != null) {
                Console.WriteLine($"Container: {current.ID}");
                current = current.Next;
            }
        }
    }

}