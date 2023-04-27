namespace HealthService.Dtos
{
  public class UserReadDto
  {
  public int Id { get; set; } 
   public string PersonalId { get; set; } 
        public string FullName { get; set; }
         public DateTime BirthDate { get; set; }
       public string Address { get; set; }

       public string PhoneNumber { get; set; }
    
       public string Email { get; set; }
  } 
}