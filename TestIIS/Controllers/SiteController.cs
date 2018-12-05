using Bogus;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestIIS.Models;

namespace TestIIS.Controllers
{
    public class SiteController : Controller
    {
        List<SelectItemViewModel> citys;
        SiteController()
        {
            citys = new List<SelectItemViewModel>();
            int ids = 1;
            var fakerItem = new Faker<SelectItemViewModel>()

                .RuleFor(o => o.Id, f => ids++)
                .RuleFor(o => o.Name, f => f.Address.City());

            for (int i = 0; i < 50; ++i)
            {
                SelectItemViewModel item = fakerItem.Generate();
                citys.Add(item);
            }
        }

        [HttpGet]
        public ActionResult VievItems()
        {

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return View(db.Items.ToList());
            }

        }


        public ActionResult PagginationPage(int page = 1)
        {
           
        }



        public ActionResult ChosenSelectPage()
        {
            List<SelectItemViewModel> model = new List<SelectItemViewModel>();
            int ids = 1;
            var fakerItem = new Faker<SelectItemViewModel>("uk")

                .RuleFor(o => o.Id, f => ids++)
                .RuleFor(o => o.Name, f => f.Address.City());

            for(int i = 0; i < 50; ++i)
            {
                SelectItemViewModel item = fakerItem.Generate();
                model.Add(item);
            }
          
            return View(model);
        }

        [HttpPost]
        public ActionResult ChosenSelectPage(int [] SelectListCityes)
        {
            if (SelectListCityes.Length == 5)
                return RedirectToAction("Contact", "Home");
            List<SelectItemViewModel> model = new List<SelectItemViewModel>();
            int ids = 1;
            var fakerItem = new Faker<SelectItemViewModel>("uk")

                .RuleFor(o => o.Id, f => ids++)
                .RuleFor(o => o.Name, f => f.Address.City());

            for (int i = 0; i < 50; ++i)
            {
                SelectItemViewModel item = fakerItem.Generate();
                model.Add(item);
            }

            return View(model);
        }


            // GET: Site
            public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(ItemsAddViewModel model)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Items.Add(new ItemsAddViewModel() { Author = model.Author, Title = model.Title, Description = model.Description });
                context.SaveChanges();

            }



            return View();
        }

        [HttpPost]
        public ActionResult FroalaUploadImage()
        {
            if (Request.Files["file"] != null)
            {
                HttpPostedFileBase MyFile = Request.Files["file"];

                // Setting location to upload files
                string TargetLocation = Server.MapPath("~/upload/images/");

                try
                {
                    if (MyFile.ContentLength > 0)
                    {
                        // Get File Extension
                        string Extension = Path.GetExtension(MyFile.FileName);

                        // Determining file name. You can format it as you wish.
                        string FileName = Path.GetFileName(MyFile.FileName);
                        FileName = Guid.NewGuid().ToString();

                        // Determining file size.
                        int FileSize = MyFile.ContentLength;

                        // Creating a byte array corresponding to file size.
                        byte[] FileByteArray = new byte[FileSize];

                        // Basic validation for file extension
                        string[] AllowedExtension = { ".gif", ".jpeg", ".jpg", ".png", ".svg", ".blob" };
                        string[] AllowedMimeType = { "image/gif", "image/jpeg", "image/pjpeg", "image/x-png", "image/png", "image/svg+xml" };

                        if (AllowedExtension.Contains(Extension) && AllowedMimeType.Contains(MimeMapping.GetMimeMapping(MyFile.FileName)))
                        {
                            // Posted file is being pushed into byte array.
                            MyFile.InputStream.Read(FileByteArray, 0, FileSize);

                            // Uploading properly formatted file to server.
                            MyFile.SaveAs(TargetLocation + FileName + Extension);
                            string json = "";
                            Hashtable resp = new Hashtable();
                            string urlPath = MapURL(TargetLocation) + FileName + Extension;

                            // Make the response an json object
                            resp.Add("link", urlPath);
                            resp.Add("Name", FileName + Extension);
                            json = JsonConvert.SerializeObject(resp);

                            // Clear and send the response back to the browser.
                            Response.Clear();
                            Response.ContentType = "application/json; charset=utf-8";
                            Response.Write(json);
                            Response.End();
                        }
                        else
                        {
                            // Handle validation errors
                        }
                    }
                }

                catch (Exception ex)
                {

                    // Handle errors
                }
            }
            return View();
        }
        private string MapURL(string path)
        {
            string appPath = Server.MapPath("/").ToLower();
            return string.Format("/{0}", path.ToLower().Replace(appPath, "").Replace(@"\", "/"));
        }

        [HttpGet]
        public ActionResult ItemShow()
        {
            return View();
        }
    }
}