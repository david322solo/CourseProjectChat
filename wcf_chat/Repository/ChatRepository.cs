using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wcf_chat.Context;

namespace wcf_chat.Repository
{
    public class ChatRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        ChatContext _db;
        DbSet<TEntity> _dbSet;
        public ChatRepository(ChatContext db)
        {
            _db = db;
            _dbSet = db.Set<TEntity>();
        }

        public void Add(TEntity item)
        {
            _dbSet.Add(item);
            _db.SaveChanges();
        }

        public TEntity FindByLogin(string login)
        {
           return _dbSet.Find(login);
        }

        public IQueryable<TEntity> FindAllByLogin(string login)
        {
            var q = from p in _db.users
                    where p.IdLogin.StartsWith(login)
                    select p;
            return (IQueryable<TEntity>)q;
        }

        public IQueryable<TEntity> FindAllByLoginCont(string login)
        {
            var contacts = _db.contacts.Where(lg => lg.LoginUser == login);
            return (IQueryable<TEntity>)contacts;
        }

        public IEnumerable<TEntity> Get()
        {
            return _dbSet;
        }

       

        public void Save()
        {
            _db.SaveChanges();
        }

        public void AddConversation(string loginFirst, string loginSecond)
        {
            var q = from p in _db.conversations
                    where (p.UserOne == loginFirst && p.UserTwo == loginSecond) || (p.UserOne == loginSecond && p.UserTwo == loginFirst)
                    select p;
            if (!q.Any())
            {
                _db.conversations.Add(new Conversation() { UserOne = loginFirst, UserTwo = loginSecond, CreatedAt = DateTime.Now });
                _db.SaveChanges();
            }
        }
        public IQueryable<TEntity> GetConv(string LoginUserFirst, string LoginUserSecond)
        {
            var q = from p in _db.conversations
                    where (p.UserOne == LoginUserFirst && p.UserTwo == LoginUserSecond) || 
                          (p.UserOne == LoginUserSecond && 
                            p.UserTwo == LoginUserFirst)
                    select p;
            return (IQueryable<TEntity>)q;
        }
    }
}
