using System.ComponentModel.DataAnnotations;

namespace HealthService.Models
{
    public class HealthInsurance
    {
        [Key]
        [Required]
   public int Id { get; set; } 
   

   [Required]
    public string PersonalId { get; set; } 
    
    [Required]
     public string FullName { get; set; }
     
     [Required]
        [DataType(DataType.Date)]
      public DateTime BirthDate { get; set; }

       [Required]
       public string Address { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
       public string PhoneNumber { get; set; }
        
        [Required]
        [EmailAddress]
       public string Email { get; set; }
}
}