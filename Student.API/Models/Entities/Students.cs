namespace Student.API.Models.Entities
{
    public class Students
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Gender { get; set; }
        public required int Age { get; set; }
        public required int Class { get; set;}
        public required string City { get; set; }
    }
}

