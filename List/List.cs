using System.Collections;

namespace List;

public class List<T> : IList<T>
{
    private readonly Node<T> _headNode;
    private int _size;

    public List()
    {
        _headNode = new Node<T>();
        _size = 0;
    }

    public List(IEnumerable<T> enumerable)
    {
        if (enumerable is null)
            throw new ArgumentNullException(nameof(enumerable), $"{nameof(enumerable)} is null");

        _headNode = new();
        AddRange(enumerable);
    }

    public int Length => _size;

    /// <summary>
    /// 将新元素添加到线性表的末尾
    /// </summary>
    /// <param name="val">新元素</param>
    /// <exception cref="ArgumentNullException">如果新元素的值为NULL，则抛出异常</exception>
    public void Add(T val)
    {
        if(val is null)
            throw new ArgumentNullException(nameof(val),$"{nameof(val)} is null");

        Node<T> node = _headNode;
        while (node.Next is not null)
            node = node.Next;

        node.Next = new()
        {
            Val = val,
            Next = null
        };
        _size++;
    }

    /// <summary>
    /// 将新元素添加到线性表中指定的位置
    /// </summary>
    /// <param name="index">新元素的索引</param>
    /// <param name="val">新元素的值</param>
    /// <exception cref="ArgumentOutOfRangeException">如果新元素的索引超出的范围，则抛出异常</exception>
    /// <exception cref="ArgumentNullException">如果新元素的值为NULL，则抛出异常</exception>
    public void Add(int index, T val)
    {
        if (index < 0 || index >= _size)
            throw new ArgumentOutOfRangeException(nameof(index), $"{nameof(index)} is out range");

        if (val is null)
            throw new ArgumentNullException(nameof(val), $"{nameof(val)} is null");

        Node<T> node = GetNodeAtIndex(index);

        node.Next = new()
        {
            Val = val,
            Next = node.Next
        };
        _size++;
    }

    /// <summary>
    /// 将一组新元素添加到线性表的末尾
    /// </summary>
    /// <param name="enumerable">新元素集合</param>
    /// <exception cref="ArgumentNullException">如果集合为NULL，则抛出异常</exception>
    public void AddRange(IEnumerable<T> enumerable)
    {
        if (enumerable is null)
            throw new ArgumentNullException(nameof(enumerable), $"{nameof(enumerable)} is null");

        if (enumerable is ICollection<T> collection)
        {
            int count = collection.Count;
            if (count != 0)
                foreach (T c in collection)
                    Add(c);
        }
        else
        {
            using IEnumerator<T> enumerator = enumerable.GetEnumerator();
            while (enumerator.MoveNext())
                Add(enumerator.Current);
        }
    }

    /// <summary>
    /// 删除线性表中指定索引处的元素
    /// </summary>
    /// <param name="index">被删除元素的索引</param>
    /// <exception cref="ArgumentOutOfRangeException">如果索引超出线性表的范围，则抛出异常</exception>
    public void RemoveAt(int index)
    {
        if (index < 0 || index >= _size)
            throw new ArgumentOutOfRangeException(nameof(index), $"{nameof(index)} is out range");

        Node<T> removeNode = GetNodeAtIndex(index+1);
        Node<T> beforeNode = index - 1 < 0 ? _headNode : GetNodeAtIndex(index);

        beforeNode.Next = removeNode.Next;
        _size--;
    }

    /// <summary>
    /// 通过元素的值删除线性表中的特定元素
    /// </summary>
    /// <param name="val">需要被删除的元素的值</param>
    /// <exception cref="ArgumentNullException">如果被删除的元素的值为NULl，则抛出异常</exception>
    /// <exception cref="InvalidOperationException">如果元素不存在则抛出异常</exception>
    public void Remove(T val)
    {
        if (val is null)
            throw new ArgumentNullException(nameof(val), $"{nameof(val)} is null");

        int index = Contains(val);
        if(index == -1)
            throw new InvalidOperationException("Cannot find the element in the current context.");

        RemoveAt(index);
    }

    /// <summary>
    /// 查询元素是否在线性表中
    /// </summary>
    /// <param name="val">需要查找的元素</param>
    /// <returns>存在放回该元素的索引，如果不存在则返回-1</returns>
    /// <exception cref="ArgumentNullException">如果需要查找的数据元素的值为NULL，则抛出异常</exception>
    public int Contains(T val)
    {
        if (val is null)
            throw new ArgumentNullException(nameof(val), $"{nameof(val)} is null");

        int i = 0;
        Node<T>? node = _headNode.Next;
        while (node is not null)
        {
            if(node.Val!.Equals(val))
                return i;

            node = node.Next;
            i++;
        }

        return -1;
    }

    /// <summary>
    /// 更新线性表中结点元素的值
    /// </summary>
    /// <param name="index">需要更新值的结点索引</param>
    /// <param name="val">结点的新值</param>
    /// <exception cref="ArgumentOutOfRangeException">如果传入的索引超出线性表范围，则抛出异常</exception>
    /// <exception cref="ArgumentNullException">传入的值为NULL，则抛出异常</exception>
    public void Update(int index, T val)
    {
        if (index < 0 || index >= _size)
            throw new ArgumentOutOfRangeException($"{nameof(index)}");
        if(val is null)
            throw new ArgumentNullException(nameof(val), $"{nameof(val)} is null");

        Node<T> node = GetNodeAtIndex(index+1);
        node.Val = val;
    }

    /// <summary>
    /// 通过索引获取链表中元素的值
    /// </summary>
    /// <param name="index">结点元素的索引</param>
    /// <returns>返回结点的值</returns>
    /// <exception cref="ArgumentOutOfRangeException">如果索引超出规定的范围，则抛出异常</exception>
    public T GetVal(int index)
    {
        if(index < 0 || index >= _size)
            throw new ArgumentOutOfRangeException(nameof(index),$"{nameof(index)} is out range");

        return GetNodeAtIndex(index+1).Val;
    }

    /// <summary>
    /// 将线性表清空
    /// </summary>
    public void Clear()
    {
        _headNode.Next = null;
        _size = 0;
    }

    public void Dispose()
    {
        Clear();
        GC.SuppressFinalize(this);
    }

    public IEnumerator<T> GetEnumerator()
    {
        Node<T>? node = _headNode.Next;
        while (node is not null)
        {
            yield return node.Val;
            node = node.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// 私有方法，通过索引获取单链表中的结点
    /// </summary>
    /// <param name="index">结点元素的索引</param>
    /// <returns>位于指定索引处的结点</returns>
    /// <exception cref="ArgumentOutOfRangeException">如果索引超出规定范围则抛出异常</exception>
    /// <exception cref="InvalidOperationException">如果链表中不存在该元素则抛出异常</exception>
    private Node<T> GetNodeAtIndex(int index)
    {
        if (index < 0 || index - 1 >= _size)
            throw new ArgumentOutOfRangeException(nameof(index), $"{nameof(index)} is out of range");

        Node<T> currentNode = _headNode;
        for (int i = 0; i < index; i++)
        {
            currentNode = currentNode.Next ?? throw new InvalidOperationException("Index is out of the bounds of the list.");
        }

        return currentNode;
    }
}