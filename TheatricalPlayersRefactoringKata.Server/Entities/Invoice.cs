namespace TheatricalPlayersRefactoringKata.Server.Entities
{
    public class Invoice
    {
        public string? Customer { get; set; }
        public List<Performance>? Performances { get; set; }

    }
}
