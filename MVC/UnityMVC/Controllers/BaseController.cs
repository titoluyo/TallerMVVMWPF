using System.Web.Mvc;
using UnityTutorials.Models;

namespace UnityTutorials.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IUnitOfWorkManager UnitOfWorkManager;
        // GET: Base

        public BaseController(IUnitOfWorkManager unitOfWorkManager)
        {
            UnitOfWorkManager = unitOfWorkManager;
        }
    }
}