namespace Domain.Exceptions
{
    public class ValidationError
    {
        public string Field { get; set; }
        public IEnumerable<String> Messages { get; set; }

    }
}