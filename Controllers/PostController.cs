using Articles.core;
using Articles.data;
using Articles.data.SqlServerEF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Articles.Controllers
{
    [Authorize]
    
    public class PostController : Controller
    {
        private readonly IdataHelper<AuthorPost> dataHelper;
        private readonly IWebHostEnvironment webHost;
        private readonly IAuthorizationService authorizationService;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IdataHelper<Author> dataHelperForAuthor;
        private readonly IdataHelper<category> dataHelperForcategory;
        private readonly code.FilesHelper filesHelper;
        private int pageItem;
        private Task<AuthorizationResult> Result;
        private string UserId;


        public PostController(IdataHelper<AuthorPost> dataHelper, IWebHostEnvironment webHost, IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IdataHelper<Author> dataHelperForAuthor, IdataHelper< category> dataHelperForcategory)

        {
          

            this.dataHelper = dataHelper;
            this.webHost = webHost;
            this.authorizationService = authorizationService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.dataHelperForAuthor = dataHelperForAuthor;
            this.dataHelperForcategory = dataHelperForcategory;
            filesHelper = new code.FilesHelper(this.webHost);
            pageItem = 5;

            

        }
        public ActionResult Index(int? id)
        {
            SetUser();
            if (Result.Result.Succeeded)
            {
                if (id == 0 || id == null)
                {
                    return View(dataHelper.GetAllData().Take(pageItem));
                }
                else
                {
                    var data = dataHelper.GetAllData().Where(x => x.Id > id).Take(pageItem);
                    return View(data);


                }

            }



            else {
                if (id == 0 || id == null)
                {
                    return View(dataHelper.GetDataByUser(UserId).Take(pageItem));
                }
                else
                {
                    var data = dataHelper.GetDataByUser(UserId).Where(x => x.Id > id).Take(pageItem);
                    return View(data);


                }



            }


           
        }


        public ActionResult Search(string SearchItem)
        {
            SetUser();
            if (Result.Result.Succeeded)
            {
                if (SearchItem == null)
                {
                    return View("Index", dataHelper.GetAllData());
                }
                else
                {
                    return View("Index", dataHelper.Search(SearchItem));
                }
            }
            else
            {
                if (SearchItem == null)
                {
                    return View("Index", dataHelper.GetDataByUser(UserId));
                }
                else
                {
                    return View("Index", dataHelper.Search(SearchItem).Where(x => x.UserId == UserId).ToList());
                }
            }

        }









        // GET: PostController/Details/5
        public ActionResult Details(int id)
        {
            return View(dataHelper.Find(id));
        }

        // GET: PostController/Create
        public ActionResult Create()
        {
            SetUser();
            return View();
        }

        // POST: PostController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(coreview.AuthorPostview collection)
        {
            SetUser();
            try
            {
                var Post = new AuthorPost {
                    FullName = dataHelperForAuthor.GetAllData().Where(x => x.UserId == UserId).Select(x => x.FullName).First(),
                    AddedDate = DateTime.Now,
                    Author = collection.Author,
                    AuthorId = dataHelperForAuthor.GetAllData().Where(x => x.UserId == UserId).Select(x => x.Id).First(),
                    AuthodId = dataHelperForAuthor.GetAllData().Where(x => x.UserId == UserId).Select(x => x.Id).First(),
                    Category=collection.Category,
                    CategoryId = dataHelperForcategory.GetAllData().Where(x => x.name== collection.PostCategory).Select(x => x.Id).First(),
                    PostCategory=collection.PostCategory,
                    PostDescription=collection.PostDescription,
                    PostTitle=collection.PostTitle,
                    UserName= dataHelperForAuthor.GetAllData().Where(x => x.UserId == UserId).Select(x => x.UserName).First(),
                    UserId=UserId,
                  //  Id=collection.Id, 
                    PostImageUrl=filesHelper.UploadFile(collection.PostImageUrl, "Images")

                };
                dataHelper.Add(Post);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception x)
            {

                return View();
            }
        }

            // GET: PostController/Edit/5
            public ActionResult Edit(int id)
        {
            var Authorpost = dataHelper.Find(id);
            //var Authorpost = dbContext.AuthorPosts.AsNoTracking().FirstOrDefault(p => p.Id == postId);

            coreview.AuthorPostview authorPostview = new coreview.AuthorPostview
            {

                FullName = Authorpost.FullName,
                AddedDate = (DateTime)Authorpost.AddedDate,
                Author = Authorpost.Author,
                AuthorId = Authorpost.AuthorId,
                Category = Authorpost.Category,
                CategoryId = Authorpost.CategoryId,
                PostCategory = Authorpost.PostCategory,
                PostDescription = Authorpost.PostDescription,
                PostTitle = Authorpost.PostTitle,
                UserName = Authorpost.UserName,
                UserId = Authorpost.UserId,
                Id = Authorpost.Id,
              


            };

            return View(authorPostview);
        }

        // POST: PostController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(coreview.AuthorPostview collection)
        {
            try
            {
                SetUser();

                // Retrieve the existing entity without tracking
                var existingPost = dataHelper.Find(collection.Id);

                if (existingPost != null)
                {
                    // Update the properties of the existing entity
                    existingPost.FullName = dataHelperForAuthor.GetAllData().Where(x => x.UserId == UserId).Select(x => x.FullName).First();
                    existingPost.AddedDate = DateTime.Now;
                    existingPost.Author = collection.Author;
                    existingPost.AuthorId = dataHelperForAuthor.GetAllData().Where(x => x.UserId == UserId).Select(x => x.Id).First();
                    existingPost.Category = collection.Category;
                    existingPost.CategoryId = dataHelperForcategory.GetAllData().Where(x => x.name == collection.PostCategory).Select(x => x.Id).First();
                    existingPost.PostCategory = collection.PostCategory;
                    existingPost.PostDescription = collection.PostDescription;
                    existingPost.PostTitle = collection.PostTitle;
                    existingPost.UserName = dataHelperForAuthor.GetAllData().Where(x => x.UserId == UserId).Select(x => x.UserName).First();
                    existingPost.UserId = UserId;
                    existingPost.PostImageUrl = filesHelper.UploadFile(collection.PostImageUrl, "Images");

                    // Update the existing entity in the context
                    dataHelper.Edit(existingPost, existingPost.Id);

                   

                    return RedirectToAction(nameof(Index));
                }

                return View();
            }
            catch (Exception x)
            {
                return View();
            }
        }


        // GET: PostController/Delete/5
        public ActionResult Delete(int id)
        {




            return View(dataHelper.Find(id));
 
        }

        // POST: PostController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, AuthorPost collection)
        {
            try
            {

                dataHelper.Delete(id);
                string FilePath = "~/images/" + collection.PostImageUrl;
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
      private void SetUser() {

            Result = authorizationService.AuthorizeAsync(User, "Admin");
            UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

        }
    }
}

