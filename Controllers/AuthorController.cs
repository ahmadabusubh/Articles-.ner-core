using Articles.core;
using Articles.data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace Articles.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IdataHelper<Author> dataHelper;
        private readonly IWebHostEnvironment webHost;
        private readonly code.FilesHelper filesHelper;
        private int pageItem;

        public AuthorController(IdataHelper<Author>dataHelper,IWebHostEnvironment webHost)
        {
            this.dataHelper = dataHelper;
            this.webHost = webHost;
            filesHelper = new code.FilesHelper(this.webHost);
            pageItem = 5;
        }
        public ActionResult Index(int ?id)
        {
            if (id == 0 || id == null)
            {
                return View(dataHelper.GetAllData().Take(pageItem));
            }
            else {
                var data = dataHelper.GetAllData().Where(x => x.Id > id).Take(pageItem);
                return View(data);


            }



            
        }
        public ActionResult Search(string SearchItem)
        {
            if (SearchItem == null)
            {
                return View("Index", dataHelper.GetAllData());

            }
            else {
                return View("Index",dataHelper.Search(SearchItem));

            
            
            }


        }

        // GET: AuthorController/Details/5


        // GET: AuthorController/Create


        // GET: AuthorController/Edit/5
        public ActionResult Edit(int id)
        {
            var author = dataHelper.Find(id);
            coreview.Authorview authorview = new coreview.Authorview
            {
                Id = author.Id,
                Bio = author.Bio,
                Facebook = author.Facebook,
                FullName = author.FullName,
                Twitter = author.Twitter,
                Instagram = author.Instagram,
                UserId = author.UserId,
                UserName = author.UserName,
                



        };
            return View(authorview );
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, coreview.Authorview collection)
        {
            try
            {
                var author = new Author {
                    Id = collection.Id,
                    Bio = collection.Bio,
                    Facebook = collection.Facebook,
                    FullName = collection.FullName,
                    Twitter = collection.Twitter,
                    Instagram = collection.Instagram,
                    UserId = collection.UserId,
                    UserName = collection.UserName,
                    ProfileImageUrl = filesHelper.UploadFile(collection.ProfileImageUrl, "Images")
                
                
                };
                dataHelper.Edit(author,id);

                return RedirectToAction(nameof(Index));
            }
            catch 
            {
                return View();
            }
        }

        // GET: AuthorController/Delete/5
        public ActionResult Delete(int id)
        {
            var author = dataHelper.Find(id);
            coreview.Authorview authorview = new coreview.Authorview
            {
                Id = author.Id,
                Bio = author.Bio,
                Facebook = author.Facebook,
                FullName = author.FullName,
                Twitter = author.Twitter,
                Instagram = author.Instagram,
                UserId = author.UserId,
                UserName = author.UserName,




            };
            return View();
        }

        // POST: AuthorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Author collection)
        {
            try
            {
                dataHelper.Delete(id);
                string FilePath = "~/images/" + collection.ProfileImageUrl;
                if (System.IO.File.Exists(FilePath))
                {
                    System.IO.File.Delete(FilePath);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
