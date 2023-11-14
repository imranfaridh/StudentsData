using System.ComponentModel.DataAnnotations;

namespace StudentsData.Dto
{
    public class ListDTO
    {
        public int Id { get; set; }
        [Required]
     
        public string Name { get; set; }
        public string Location { get; set; }
        
    }
}
