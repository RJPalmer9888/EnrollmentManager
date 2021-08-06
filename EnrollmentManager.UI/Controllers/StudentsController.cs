using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EnrollmentManager.DATA;
using EnrollmentManager.UI.Utilities;
using EnrollmentManager.UI.Models;
using System.Drawing;
using PagedList;

namespace EnrollmentManager.UI.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private db_a745f4_enrollmentmanagerEntities db = new db_a745f4_enrollmentmanagerEntities();

        // GET: Students
        #region Indexes
        public ActionResult Index(string searchString, int? page = 1)
        {
            @Session["grid"] = false;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            ViewBag.pageSize = pageSize;
            var students = db.Students.Include(s => s.StudentStatus).OrderBy(s => s.LastName).ToList();

            #region Search functionality
            if (!String.IsNullOrEmpty(searchString))
            {
                return View((from t in students
                             where t.FullName.ToLower().Contains(searchString.ToLower())
                             select t).ToPagedList(pageNumber, pageSize));
            }

            return View((from t in students
                         select t).ToPagedList(pageNumber, pageSize));


            #endregion
        }

        [HttpGet]
        public ActionResult IndexGrid(string searchString, int? page = 1)
        {
            Session["grid"] = true;
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            ViewBag.pageSize = pageSize;
            var students = db.Students.Include(s => s.StudentStatus).OrderBy(s => s.LastName).ToList();

            #region Search functionality
            if (!String.IsNullOrEmpty(searchString))
            {
                return View((from t in students
                             where t.FullName.ToLower().Contains(searchString.ToLower())
                             select t).ToPagedList(pageNumber, pageSize));
            }

            return View((from t in students
                         select t).ToPagedList(pageNumber, pageSize));


            #endregion
        }
        #endregion

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.Grid = Session["grid"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentID,FirstName,LastName,Major,Address,City,State,ZipCode,Phone,Email,PhotoUrl,SSID")] Student student, HttpPostedFileBase studentImage)
        {
            if (ModelState.IsValid)
            {
                #region File Upload
                string file = "NoImage.png";

                if (studentImage != null)
                {
                    file = studentImage.FileName;
                    string ext = file.Substring(file.LastIndexOf('.'));
                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif" };

                    //Check that the uploaded file is in our list of acceptable exts and file size <= 4mb max from ASP.NET
                    if (goodExts.Contains(ext.ToLower()) && studentImage.ContentLength <= 4194303)
                    {
                        //Create a new file name (using a GUID)
                        file = Guid.NewGuid() + ext;

                        #region Resize Image
                        string savePath = Server.MapPath("~/imgstore/students/");

                        Image convertedImage = Image.FromStream(studentImage.InputStream);

                        int maxImageSize = 500;

                        int maxThumbSize = 100;

                        ImageUtility.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);
                        #endregion
                    }

                    //no matter what, update the PhotoUrl witht he value of the file variable
                    
                }
                student.PhotoUrl = file;
                #endregion
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", student.SSID);
            return View(student);
        }

        // GET: Students/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", student.SSID);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentID,FirstName,LastName,Major,Address,City,State,ZipCode,Phone,Email,PhotoUrl,SSID")] Student student, HttpPostedFileBase studentImageEdit)
        {
            if (ModelState.IsValid)
            {
                #region File Upload
                string file = "NoImage.png";
                if (student.PhotoUrl != "NoImage.png" && student.PhotoUrl != null)
                {
                    file = student.PhotoUrl;
                }
                

                if (studentImageEdit != null)
                {
                    file = studentImageEdit.FileName;
                    string ext = file.Substring(file.LastIndexOf('.'));
                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif" };

                    //Check that the uploaded file is in our list of acceptable exts and file size <= 4mb max from ASP.NET
                    if (goodExts.Contains(ext.ToLower()) && studentImageEdit.ContentLength <= 4194303)
                    {
                        //Create a new file name (using a GUID)
                        file = Guid.NewGuid() + ext;

                        #region Resize Image
                        string savePath = Server.MapPath("~/imgstore/students/");

                        Image convertedImage = Image.FromStream(studentImageEdit.InputStream);

                        int maxImageSize = 500;

                        int maxThumbSize = 100;

                        ImageUtility.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);
                        #endregion
                    }

                    //no matter what, update the PhotoUrl witht he value of the file variable
                    
                }
                student.PhotoUrl = file;
                #endregion
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", student.SSID);
            return View(student);
        }

        // GET: Students/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
