using System.Linq.Expressions;

namespace BasicApiNet.Core.Specifications;

public class BaseSpecification<T> : ISpecifications<T>
{
    public Expression<Func<T, bool>> Criteria { get; }

    public BaseSpecification()
    {
    }

    public BaseSpecification(Expression<Func<T, bool>> Criteria)
    {
        this.Criteria = Criteria;
    }

    public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

    public Expression<Func<T, object>> OrderBy { get; private set; }

    public Expression<Func<T, object>> OrderByDescending { get; private set; }

    public int Take { get; private set; }
    public int Skip { get; private set; }
    public bool IsPagingEnabled { get; private set; }

    protected void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }

    public void AddOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }

    public void AddOrderByDescending(Expression<Func<T, object>> orderByDescending)
    {
        OrderByDescending = orderByDescending;
    }

    public void ApplyPaging(int take, int skip)
    {
        Take = take;
        //Skip = skip;
        IsPagingEnabled = true;
    }
}