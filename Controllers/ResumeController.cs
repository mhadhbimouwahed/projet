using System.Linq;
using ApplicationManagement.Data;
using ApplicationManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace ApplicationManagement.Controllers;

public class ResumeController : Controller
{

    private readonly ResumeDbContext _dbContext;
    private readonly IWebHostEnvironment _webHost;
    
    
    public ResumeController(ResumeDbContext dbContext,IWebHostEnvironment webHost)
    {
        _webHost = webHost;
        _dbContext = dbContext;
    }
   
    // GET
    public IActionResult Index()
    {
        List<Applicant> applicants = new List<Applicant>();
        applicants = _dbContext.Applicants.ToList();
        
        return View(applicants);
        
    }

    [HttpGet]
    public IActionResult Create()
    {
        Applicant applicant = new Applicant();
        applicant.Experiences.Add(new Experience(){ExperienceId = 1});
        
        return View(applicant);
    }

    [HttpPost]
    public IActionResult Create(Applicant applicant)
    {
        applicant.Experiences.RemoveAll(n => n.YearsWorked == 0);
        string uniqueFileName = GetUploadedFileName(applicant);
        applicant.PhotoUrl = uniqueFileName;
        foreach (Experience experience in applicant.Experiences)
        {
            if (experience.CompanyName == null || experience.CompanyName.Length == 0)
            {
                applicant.Experiences.Remove(experience);
            }
        }
        _dbContext.Add(applicant);
        _dbContext.SaveChanges();
        return RedirectToAction("Index");
    }


    private string GetUploadedFileName(Applicant applicant)
    {
        string uniqueFileName = null;
        if (applicant.ProfilePhoto != null)
        {
            string uploadsFolder = Path.Combine(_webHost.WebRootPath, "images");
            uniqueFileName = Guid.NewGuid().ToString() + "_" + applicant.ProfilePhoto.FileName;
            string filePaht = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePaht,FileMode.Create))
            {
                applicant.ProfilePhoto.CopyTo(fileStream);
            }
        }

        return uniqueFileName;
    }

    public IActionResult Details(int Id)
    {
        Applicant applicant = _dbContext.Applicants.Include(e => e.Experiences)
            .Where(a => a.Id == Id).FirstOrDefault();

        return View(applicant);
    }

    
    
    
}