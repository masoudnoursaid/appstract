using Application.Common.BaseTypes.Dto;

namespace Infrastructure.Services.PaymentWebHookHttpClient.Dto;


public record NotifyClientForPaymentResultDto(string PaymentId, string ProvidedId, bool Successful, string Status,
    string Amount, string InvoiceNumber) : IBaseDto;