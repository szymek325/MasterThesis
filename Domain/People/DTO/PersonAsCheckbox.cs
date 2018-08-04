namespace Domain.People.DTO
{
    public class PersonAsCheckbox
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Thumbnail { get; set; }
        public bool IsChecked { get; set; }
    }
}