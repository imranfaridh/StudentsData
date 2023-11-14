using StudentsData.Dto;

namespace StudentsData.Data
{
    public class ListData
    {
        public static List<ListDTO> listItems = new List<ListDTO>
            {
                new ListDTO {Id = 1,Name="Imran", Location = "Chennai"},
                new ListDTO {Id = 2,Name = "Raj", Location = "Tiruttani"}
                
            };
    }

}
