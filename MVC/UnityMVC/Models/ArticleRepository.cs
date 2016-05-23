using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace UnityTutorials.Models
{
    public interface IArticleRepository
    {
        IEnumerable<Article> GetAll();
        Article Add(Article item);
        void Update(Article item);
        void Delete(Article item);
        Article Get(Guid id);
    }

    public class ArticleRepository : IArticleRepository
    {
        private readonly WebDbContext _context;

        public ArticleRepository(IWebDbContext context)
        {
            this._context = context as WebDbContext;
        }

        public Article Get(Guid id)
        {
            return _context.Articles.FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<Article> GetAll()
        {
            return _context.Articles;
        }

        public Article Add(Article item)
        {
            this._context.Articles.Add(item);
            return item;
        }

        public void Update(Article item)
        {
            // Check there's not an object with same identifier already in context
            if (_context.Articles.Local.Select(x => x.Id == item.Id).Any())
            {
                throw new ApplicationException("Object already exists in context");
            }
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(Article item)
        {
            this._context.Articles.Remove(item);
        }
    }
}