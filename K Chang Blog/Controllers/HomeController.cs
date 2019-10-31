using K_Chang_Blog.Helpers;
using K_Chang_Blog.Models;
using PagedList;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace K_Chang_Blog.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(int? page, string searchStr)
        {
            //var publishedBlogPosts = db.BlogPosts.Where(b => b.Published).ToList();
            //return View(publishedBlogPosts);

            ViewBag.Search = searchStr;
            var blogList = IndexSearch(searchStr);

            int pageSize = 3; // display 3 blog posts at a time on this page 
            int pageNumber = (page ?? 1);
            //var listPosts = db.BlogPosts.AsQueryable();

            var publishedBlogPosts = db.BlogPosts.Where(b => b.Published).AsQueryable();

            return View(blogList.ToPagedList(pageNumber, pageSize));
        }

        public IQueryable<BlogPost> IndexSearch(string searchStr)
        {
            IQueryable<BlogPost> result = null;
            if (searchStr != null)
            {
                result = db.BlogPosts.Where(b => b.Published).AsQueryable();
                result = result.Where(p => p.Title.Contains(searchStr) ||
                p.BlogPostBody.Contains(searchStr) ||
                p.Comments.Any(c => c.CommentBody.Contains(searchStr) ||
                c.Author.FirstName.Contains(searchStr) ||
                c.Author.LastName.Contains(searchStr) ||
                c.Author.DisplayName.Contains(searchStr) ||
                c.Author.Email.Contains(searchStr)));
            }
            else
            {
                result = db.BlogPosts.Where(b => b.Published).AsQueryable();
            }
            return result.OrderByDescending(p => p.Created);
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            EmailModel model = new EmailModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(EmailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await EmailHelper.ComposeEmailAsync(model);
                    return RedirectToAction("Index", "Home");   //Takes the User home
                    //return View(new EmailModel());
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    await Task.FromResult(0);
                }

            }
            //return View(model);
            return RedirectToAction("Index", "Home");
        }
    }
}
