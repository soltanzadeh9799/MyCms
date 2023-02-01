using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyCms.Controllers
{
    public class NewsController : Controller
    {
        MyCmsContext db = new MyCmsContext();
        private IPageGroupRepository pageGroupRepository;
        private IPageRepository pageRepository;
        private IPageCommentRepository pageCommentRepository;
        public NewsController()
        {
            pageGroupRepository = new PageGroupRepository(db);
            pageRepository = new PageRepository(db);
            pageCommentRepository = new PageCommentRepository(db);
        }
        // GET: News
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ShowGroups()
        {
            return PartialView(pageGroupRepository.GetGroupForView());
        }

        public ActionResult ShowGroupsInMenu()
        {
            return PartialView(pageGroupRepository.GetAllGroups());
        }

        public ActionResult TopNews()
        {
            return PartialView(pageRepository.TopNews());
        }

       

        [Route("Archive")]
        public ActionResult ArchiveNews()
        {
            return View(pageRepository.GetAllPage());
        }

        [Route("Group/{id}/{title}")]
        public ActionResult ShowNewsByGroupId(int id,string title)
        {
            ViewBag.name = title;
            return View(pageRepository.ShowPageByGroupId(id));  
        }

        [Route("News/{id}")]
        public ActionResult ShowNews(int id)
        {
            var News=pageRepository.GetPageById(id);
            if (News == null)
            {
               return HttpNotFound();
            }
            News.Visit += 1;
            pageRepository.UpdatePage(News);
            pageRepository.Save();

            return View(News);
        }

        public ActionResult AddComment(int id,string name,string email,string comment)
        {
            PageComment addComment = new PageComment()
            {
                PageID = id,
                Name = name,
                CreateDate = DateTime.Now,
                Email = email,
                Comment = comment

            };
            pageCommentRepository.AddComment(addComment);
            return PartialView("ShowComments", pageCommentRepository.GetCommentByNewsId(id));
        }

        public ActionResult ShowComments(int id)
        {
            return PartialView(pageCommentRepository.GetCommentByNewsId(id));   
        }
    }
}