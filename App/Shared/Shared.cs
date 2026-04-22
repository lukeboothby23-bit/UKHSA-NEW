namespace UKHSA.Shared;

public class Paginated<T>
{
    List<T> _items;
    public List<T> Items
    {
        get { return _items; }
        set
        {
            _items = value
            .Skip((CurrentPage - 1) * PerPage)
            .Take(PerPage)
            .ToList();
        }
    }

    public int TotalItems { get; set; }
    public int CurrentPage { get; set; } = 1;
    public int PerPage { get; set; } = 20;
    public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PerPage);
}
