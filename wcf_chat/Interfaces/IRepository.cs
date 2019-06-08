using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wcf_chat.Repository
{
    interface IRepository<TEntity> where TEntity:class
    {
        void Add(TEntity item);
        void AddConversation(string loginFirst, string loginSecond);
        TEntity FindByLogin(string login);
        IQueryable<TEntity> FindAllByLoginCont(string login);
        IQueryable<TEntity> FindAllByLogin(string login);
        IEnumerable<TEntity> Get();
        IQueryable<TEntity> GetConv(string LoginUserFirst, string LoginUserSecond);
        void Save();
    }
}
