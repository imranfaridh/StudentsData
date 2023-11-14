using System.ComponentModel.DataAnnotations;

namespace StudentsData.Models
{
    public class ListModels
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }   

    }
}
