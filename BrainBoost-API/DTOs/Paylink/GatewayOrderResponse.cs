namespace BrainBoost_API.DTOs.Paylink
{
    public class GatewayOrderResponse
    {
        public GatewayOrderRequest? GatewayOrderRequest { get; set; }
        public double? Amount { get; set; }
        public string? TransactionNo { get; set; }
        public string? OrderStatus { get; set; }
        public List<PaymentError>? PaymentErrors { get; set; }
        public string? Url { get; set; }
        public string? QrUrl { get; set; }
        public string? MobileUrl { get; set; }
        public string? CheckUrl { get; set; }
        public bool? Success { get; set; }
        public bool? DigitalOrder { get; set; }
        public double? ForeignCurrencyRate { get; set; }
        public PaymentReceipt? PaymentReceipt { get; set; }
        public string? Metadata { get; set; }
    }
    public class GatewayOrderRequest
    {
        public double? Amount { get; set; }
        public string? OrderNumber { get; set; }
        public string? CallBackUrl { get; set; }
        public string? ClientEmail { get; set; }
        public string? ClientName { get; set; }
        public string? ClientMobile { get; set; }
        public string? Note { get; set; }
        public string? CancelUrl { get; set; }
        public List<Product>? Products { get; set; }
        public string? SupportedCardBrands { get; set; }
        public string? Currency { get; set; }
        public string? SmsMessage { get; set; }
        public string? DisplayPending { get; set; }
        public string? Receivers { get; set; }
        public string? PartnerPortion { get; set; }
        public string? Metadata { get; set; }
    }

    public class PaymentReceipt
    {
        public string ReceiptUrl { get; set; }
        public string Passcode { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }
        public string BankCardNumber { get; set; }
    }
    public class PaymentError
    {
        public string? ErrorCode { get; set; }
        public string? ErrorTitle { get; set; }
        public string? ErrorMessage { get; set; }
        public long ErrorTime { get; set; }
    }
    public class Product
    {
        public string? Title { get; set; }
        public double? Price { get; set; }
        public int? Qty { get; set; }
        public string? Description { get; set; }
        public bool? IsDigital { get; set; }
        public string? ImageSrc { get; set; }
        public double? SpecificVat { get; set; }
        public double? ProductCost { get; set; }
    }
}
