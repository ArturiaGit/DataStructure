namespace List;

public class Node<T>
{
#pragma warning disable 8618
    public T Val { get; set; }
    public Node<T>? Next { get; set; }=null;
}