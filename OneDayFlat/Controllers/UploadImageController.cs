using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneDayFlat.Data;
using OneDayFlat.Models;

namespace OneDayFlat.Controllers
{
    public class UploadImageController : Controller
    {
        OneDayFlatContext _context;
        IHostingEnvironment _appEnvironment;

        public UploadImageController(OneDayFlatContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        public IActionResult ImageIndex()
        {
            return View(_context.Image.ToList());
        }
        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                // путь к папке Files
                string path = "/Files/" + uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                tblImage file = new tblImage { Name = uploadedFile.FileName, Path = path };
                _context.Image.Add(file);
                _context.SaveChanges();
            }

            return RedirectToAction("ImageIndex");
        }
        public IActionResult CreateImage()
        {
            return View(_context.Flat.ToList());
        }

        [HttpPost]
        public IActionResult ImageCreate(FlatViewImage pvm)
        {
            Flat flat = new Flat { City = pvm.City };
            if (pvm.Image != null)
            {
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(pvm.Image.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)pvm.Image.Length);
                }
                // установка массива байтов
                flat.Image = imageData;
            }
            _context.Flat.Add(flat);
            _context.SaveChanges();

            return RedirectToAction("ImageCreate");
        }
    }
}
