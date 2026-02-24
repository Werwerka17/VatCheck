namespace VatCheck.Api
{
    public class WhitelistResponse
    {
        public WhitelistResult? Result { get; set; }
    }

    public class WhitelistResult
    {
        public WhitelistSubject? Subject { get; set; }
    }

    public class WhitelistSubject
    {
        public string? Name { get; set; }
        public string? Nip { get; set; }
        public string? StatusVat { get; set; }
    }
}
