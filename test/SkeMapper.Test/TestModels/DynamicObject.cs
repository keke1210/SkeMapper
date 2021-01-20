namespace SkeMapper.Test.TestModels
{
    public class DynamicObject
    {
        public DynamicObject()
        {

        }
        public Node Head { get; set; }
        public class Node
        {
            public int Data { get; set; }
            public Node Next { get; set; }

            public Node()
            {

            }

            // Constructor to create a new node 
            public Node(int d) 
            {
                Data = d;
                Next = null;
            }
        }

        public void Push(int new_data)
        {
            Node new_node = new Node(new_data);

            new_node.Next = Head;

            Head = new_node;
        }
    }

    public class DynamicObjectDTO
    {
        public DynamicObjectDTO()
        {

        }
        public NodeDto Head { get; set; }
        public class NodeDto
        {
            public int Data { get; set; }
            public NodeDto Next { get; set; }

            public NodeDto()
            {

            }

            // Constructor to create a new node 
            public NodeDto(int d) 
            {
                Data = d;
                Next = null;
            }
        }

        public void Push(int new_data)
        {
            NodeDto new_node = new NodeDto(new_data);

            new_node.Next = Head;

            Head = new_node;
        }
    }
}
