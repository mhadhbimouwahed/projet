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
        
        
        
        _dbContext.Add(applicant);
        _dbContext.SaveChanges();
        return RedirectToAction("Index");
    }


    

    
    public IActionResult Details(int Id)
    {
        Applicant applicant = _dbContext.Applicants.Include(e => e.Experiences)
            .Where(a => a.Id == Id).FirstOrDefault();

        return View(applicant);
    }


    public IActionResult Edit()
    {
        throw new NotImplementedException();
    }

   
    public IActionResult Delete(int Id)
    {
        Applicant applicant = _dbContext.Applicants
            .Include(e => e.Experiences)
            .Where(a => a.Id == Id).FirstOrDefault();
        return View(applicant);
    }

    [HttpPost]
    public IActionResult Delete(Applicant applicant)
    {
        _dbContext.Attach(applicant);
        _dbContext.Entry(applicant).State = EntityState.Deleted;
        _dbContext.SaveChanges();
        return RedirectToAction("index");
    }
    
}