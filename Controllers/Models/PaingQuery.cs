using System.Linq.Expressions;
using System.Linq;
using ThanhTuan.IDP.Entities;
using System;

namespace ThanhTuan.IDP
{
  public enum OrderDir { ASC = 1, DESC = -1 };
  public abstract class PagingQuery<T>
  {
    public int Offset { get; set; }
    public int Limit { get; set; }
    public string OrderBy { get; set; }
    public OrderDir OrderDir { get; set; }
    public abstract Expression<Func<T, object>> GetOrderExpression();
    public IQueryable<T> ApplyOrderQuery(IQueryable<T> query)
    {
      if (OrderDir == OrderDir.ASC)
      {
        return query.OrderBy(GetOrderExpression());
      }
      return query.OrderByDescending(GetOrderExpression());
    }

    public IQueryable<T> ApplyPagingQuery(IQueryable<T> query)
    {
      return query.Skip(Offset).Take(Limit);
    }

    public IQueryable<T> BuildQuery(IQueryable<T> query)
    {
      query = ApplyPagingQuery(query);
      query = ApplyOrderQuery(query);
      return query;
    }
  }

  public class UserPagingQuery : PagingQuery<ApplicationUser>
  {
    public override Expression<Func<ApplicationUser, object>> GetOrderExpression()
    {
      var lower = OrderBy.ToLower();
      return lower switch
      {
        "username" => user => user.NormalizedUserName,
        "email" => user => user.NormalizedEmail,
        _ => user => user.Id,
      };
    }
  }
}