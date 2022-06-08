using System.Collections.Generic;

namespace Keepr.Interfaces
{
    public interface IRepository<TItem, TId>
    {
        List<TItem> GetAll();
        TItem GetById(TId id);
    }
}