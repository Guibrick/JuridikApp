namespace JuridikApp.Models
{
    public class Query
    {
        public string QueryId { get; set; }
        public string CaseDate { get; set; }
        public string CasePlace { get; set; }
        public string CaseClaimant { get; set; }
        public string CaseDefendant { get; set; }
        public string ApplicableLaw { get; set; }
        public string ApplicableJurisprudence { get; set; }
        public string Judgement { get; set; }
    }
}