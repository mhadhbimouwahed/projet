using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationManagement.Models;

public class Experience
{

    public Experience()
    {
        
    }
    
    [Key]
    public int ExperienceId { get; set; }
    
    
    [ForeignKey("Applicant")]
    public int ApplicantId { get; set; }
    public virtual Applicant Applicant { get; private set; }
    
    
    [Required]
    [Range(1,25,ErrorMessage = "age must be between 1 and 25")]
    public int YearsWorked { get; set; }
    
    public string CompanyName { get; set; }
    public string Designation { get; set; }
    
    
}