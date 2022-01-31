using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationManagement.Models;

public class Applicant
{
    [Key]
    public int Id { get; set; }

    [Required] [StringLength(150)] public string Name { get; set; } = "";

    [Required] [StringLength(10)] public string Gender { get; set; } = "";
    
    [Required]
    [Range(25,55,ErrorMessage = "Currently, We have no postion vacant for your age")]
    [DisplayName("Age in Years")]
    public int Age { get; set; }


    [Required] [StringLength(50)] public string Qualification { get; set; } = "";
    
    [Required]
    [Range(1,25,ErrorMessage = "Currently, We have no positions vacant for your experience")]
    public int TotalExperience { get; set; }

    public virtual List<Experience> Experiences { get; set; } = new List<Experience>();
    
    
    
    
    

}