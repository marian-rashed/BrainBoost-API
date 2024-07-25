namespace HelperPlan.DTO.Paylink
{
    public class EnvironmentPaylink
    {
        public string? apiId { get; set; }
        public string? secretKey { get; set; }
        public string? url { get; set; }
        public string? persistToken { get; set; }
    }
    public record tokenPaylink(string id_token);
}
