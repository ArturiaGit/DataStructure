namespace List;

public interface IList<T> : IEnumerable<T>,IDisposable
{
    public void Add(T val);
    public void Add(int index, T val);
    public void AddRange(IEnumerable<T> enumerable);
    public void RemoveAt(int index);
    public void Remove(T val);
    public int Contains(T val);
    public void Update(int index, T val);
    public T GetVal(int index);
    public int Length { get; }
    public void Clear();
}