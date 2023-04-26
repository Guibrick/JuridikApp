namespace JuridikApp.Models
{
    public class QueryRequest
    {
        public string? CaseDate { get; set; }
        public string? CasePlace { get; set; }
        public string? CaseClaimant { get; set; }
        public string? CaseDefendant { get; set; }
        public string? ApplicableLaw { get; set; }
        public string? ApplicableJurisprudence { get; set; }
        public string? ApplicableDoctrine { get; set; }
    }
}