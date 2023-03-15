namespace DuckDuckGoose.Models;

// this class based on the properties in Python's Flask-SQLAlchemy
// (https://flask-sqlalchemy.palletsprojects.com/en/3.0.x/api/#flask_sqlalchemy.pagination.Pagination)
public class Pagination<T>
{
    public int Page { get; set; }
    public int PerPage { get; set; }
    public IEnumerable<T> Items { get; set; }
    public int Total { get; set; }

    public int First => PerPage * (Page - 1) + 1;
    public int Last => Math.Min(PerPage * Page, Total);
    public int Pages => (int) Math.Ceiling((decimal) Total / PerPage);
    public bool HasPrev => Page != 1;
    public int? PrevNum => HasPrev ? Page - 1 : null;
    public bool HasNext => Page == Pages;
    public int? NextNum => HasNext ? Page + 1 : null;

    // copied wholesale from Flask-SQLAlchemy's source code
    // (https://github.com/pallets-eco/flask-sqlalchemy/blob/3.0.x/src/flask_sqlalchemy/pagination.py)
    public IEnumerable<int?> IterPages(int leftEdge = 2, int leftCurrent = 2, int rightCurrent = 4, int rightEdge = 2)
    {
        int pagesEnd = Pages + 1;
        
        if (pagesEnd == 1)
        {
            yield break;
        }

        int leftEnd = Math.Min(1 + leftEdge, pagesEnd);
        for (int i = 1; i < leftEnd; i++)
        {
            yield return i;
        }

        if (leftEnd == pagesEnd)
        {
            yield break;
        }

        int midStart = Math.Max(leftEnd, Page - leftCurrent);
        int midEnd = Math.Min(Page + rightCurrent + 1, pagesEnd);

        if (midStart - leftEnd > 0)
        {
            yield return null;
        }

        for (int i = midStart; i < midEnd; i++)
        {
            yield return i;
        }

        if (midEnd == pagesEnd)
        {
            yield break;
        }

        int rightStart = Math.Max(midEnd, pagesEnd - rightEdge);

        if (rightStart - midEnd > 0)
        {
            yield return null;
        }

        for (int i = rightStart; i < pagesEnd; i++)
        {
            yield return i;
        }
    }

    public static Pagination<T> Paginate(IEnumerable<T> items, int page, int perPage)
    {
        return new Pagination<T>
        {
            Page = page,
            PerPage = perPage,
            Items = items
                .Skip((page - 1) * perPage)
                .Take(perPage),
            Total = items.Count(),
        };
    }
}
