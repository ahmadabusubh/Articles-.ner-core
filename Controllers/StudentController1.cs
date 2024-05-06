using Articles.code;
using Articles.core;
using Articles.data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Articles.Controllers
{
    [Authorize]

    public class StudentController : Controller
    {
        private readonly IdataHelper<Student> idataHelper;
        private readonly IWebHostEnvironment webHost;
        private readonly FilesHelper filesHelper;

        public StudentController(IdataHelper<Student> idataHelper, IWebHostEnvironment webHost)
        {
            this.idataHelper = idataHelper;
            this.webHost = webHost;
            filesHelper = new code.FilesHelper(webHost);
        }
        // GET: StudentController
        public ActionResult Index(string searchTerm)
        {
            var students = idataHelper.GetAllData();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                students = students.Where(s =>
                    s.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    s.StudentId.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    s.Grade.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }

            return View(students);
        }

        public ActionResult Search(string SearchItem)
        {
            if (SearchItem == null)
            {
                return View("Index", idataHelper.GetAllData());

            }
            else
            {
                return View("Index", idataHelper.Search(SearchItem));



            }


        }

        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(coreview.Studentview collection)
        {
            try
            {

                var St = new Student
                {

                    StudentId = collection.StudentId,
                    Grade = collection.Grade,
                    Name = collection.Name,
                    PostImageUrl = filesHelper.UploadFile(collection.PostImageUrl, "Imagess")



                };

                idataHelper.Add(St);


                return RedirectToAction(nameof(Index));


            }

            catch
            {
                return View();
            }
        }

        // GET: StudentController/Edit/5
        public ActionResult Edit(int id)
        {
            var result = idataHelper.Find(id);

            if (result != null)
            {
                coreview.Studentview student = new coreview.Studentview()
                {
                    StudentId = result.StudentId,
                    Name = result.Name,
                    Grade = result.Grade
                };

                return View(student);
            }
            else
            {
                // Handle the case where no student with the given id was found
                return NotFound();
            }
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, coreview.Studentview collection)
        {
            try
            {
                // Find the existing post by id
                var existingPost = idataHelper.Find(collection.Id);

                // Check if the existing post is not null
                if (existingPost != null)
                {
                    // Update the properties of the existing post
                    existingPost.Name = collection.Name;
                    existingPost.StudentId = collection.StudentId;
                    existingPost.Grade = collection.Grade;
                    existingPost.PostImageUrl = filesHelper.UploadFile(collection.PostImageUrl, "Imagess");

                    // Perform the edit operation
                    idataHelper.Edit(existingPost, id);

                    // Redirect to the Index action
                    return RedirectToAction(nameof(Index));
                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(idataHelper.Find(id));
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Student collection)
        {
            try
            {
                idataHelper.Delete(id);
                string FilePath = "~/Imagess/" + collection.PostImageUrl;
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
