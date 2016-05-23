using System;
using System.Web.Mvc;
using UnityTutorials.Models;

namespace UnityTutorials.Controllers
{
    public class ArticleController : BaseController
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleController(IUnitOfWorkManager unitOfWorkManager, IArticleRepository articleRepository)
            : base(unitOfWorkManager)
        {
            this._articleRepository = articleRepository;
        }

        public ActionResult Test()
        {
            // Create a article
            using (var unitOfWork = this.UnitOfWorkManager.NewUnitOfWork())
            {
                this._articleRepository.Add(new Article()
                {
                    Id = Guid.NewGuid(),
                    Title = "Title Text",
                    Summary = "Summary Text",
                    ArticleContent = "Html content",
                    ViewCount = 0,
                    CreatedDate = DateTime.Now,
                    CreatedByUsername = "admin",
                    Tags = "mvc, unity, asp.net"
                });

                unitOfWork.Commit();
            }

            return View();
        }
    }
}