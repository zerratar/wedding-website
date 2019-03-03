using System;
using System.Collections.Generic;

namespace WeddingWebsite.BusinessLogic.Repositories
{
    public interface IRepository<T>
    {
        bool TryStore(T item);
        IReadOnlyList<T> All();

        T Find(Func<T, bool> predicate);
        void Replace(T oldItem, T newItem);
    }
}