using Appstract.Front.Application.Common.Extensions;
using Appstract.Front.Domain.Enums;
using Appstract.Front.Domain.Models.PurchaseHistory;

namespace Appstract.Web.Infrastructure.MockData;

public class PurchaseHistoryMockData
{
     private readonly List<PurchaseHistoryModel> _purchaseHistories = new()
    {
        new PurchaseHistoryModel
        {
            Date = DateTime.Parse("2022-11-02 12:23:00"),
            TransactionId = "e5cd9096-505c-4dd8-b280-1ccb94f30375",
            ExternalReference = "7833a988-6f3f-4864-b5cb-99f3cbd7db40",
            Status = PaymentStatusType.Failed.ToString(),
            BalanceBefore = null,
            BalanceAfter = null,
            Amount = 200000,
            Currency = "IRR",
            PaymentMethod = "Credit Card"
        },
        new PurchaseHistoryModel
        {
            Date = DateTime.Parse("2023-02-01 15:23:00"),
            TransactionId = "c1677213-7f6e-45ed-b7be-c052cc4684f7",
            ExternalReference = "192e763c-c25b-421a-98e4-0b1825d6e5f9",
            Status = PaymentStatusType.Successful.ToString(),
            BalanceBefore = 200.23m,
            BalanceAfter = 200.23m,
            Amount = 200.23m,
            Currency = "USD",
            PaymentMethod = "Credit Card"
        },
        new PurchaseHistoryModel
        {
            Date = DateTime.Parse("2022-02-09 18:23:00"),
            TransactionId = "56403559-a2fc-46e9-90ea-4fe85c53f448",
            ExternalReference = "fe53b8fd-de21-488e-a61b-0bcb2dbcb940",
            Status = PaymentStatusType.Cancelled.ToString(),
            BalanceBefore = null,
            BalanceAfter = null,
            Amount = 200000,
            Currency = "MYR",
            PaymentMethod = "Credit Card"
        },
        new PurchaseHistoryModel
        {
            Date = DateTime.Parse("2022-01-01 12:23:00"),
            TransactionId = "63ff46d1-d361-4ef6-b1c1-85f31d5f730c",
            ExternalReference = "9f583394-21f2-4b6c-aa5a-a6175e6f9673",
            Status = PaymentStatusType.Cancelled.ToString(),
            BalanceBefore = null,
            BalanceAfter = null,
            Amount = 200.00m,
            Currency = "EUR",
            PaymentMethod = "Credit Card"
        },
        new PurchaseHistoryModel
        {
            Date = DateTime.Parse("2023-02-01 15:23:00"),
            TransactionId = "a982f403-d400-4770-ac04-4d17767251e7",
            ExternalReference = "c36adf33-8555-4f8c-b074-dc7c961964ac",
            Status = PaymentStatusType.Successful.ToString(),
            BalanceBefore = 200.23m,
            BalanceAfter = 200.23m,
            Amount = 200.00m,
            Currency = "USD",
            PaymentMethod = "Credit Card"
        },
        new PurchaseHistoryModel
        {
            Date = DateTime.Parse("2022-02-01 15:23:00"),
            TransactionId = "3dc3c013-90f0-4319-82f9-46fab70383fa",
            ExternalReference = "fa072534-aff1-4f05-83de-a138c1b5d72a",
            Status = PaymentStatusType.Successful.ToString(),
            BalanceBefore = 200.23m,
            BalanceAfter = 200.23m,
            Amount = 200.00m,
            Currency = "USD",
            PaymentMethod = "Credit Card"
        },
        new PurchaseHistoryModel
        {
            Date = DateTime.Parse("02-NOV-2022 12:23:00"),
            TransactionId = "183765dd-f33f-476f-bc7b-d1026153443a",
            ExternalReference = "b69aac2f-af54-4f4c-9040-272d0e175d74",
            Status = PaymentStatusType.Failed.ToString(),
            BalanceBefore = null,
            BalanceAfter = null,
            Amount = 200000,
            Currency = "IRR",
            PaymentMethod = "Credit Card"
        },
        new PurchaseHistoryModel
        {
            Date = DateTime.Parse("02-NOV-2022 12:23:00"),
            TransactionId = "d7830542-1b61-46b7-a38b-cffbcf9af591",
            ExternalReference = "2ff6303b-1178-4a67-9567-d88a98fe5714",
            Status = PaymentStatusType.Failed.ToString(),
            BalanceBefore = null,
            BalanceAfter = null,
            Amount = 200000,
            Currency = "IRR",
            PaymentMethod = "Credit Card"
        },
        new PurchaseHistoryModel
        {
            Date = DateTime.Parse("09-FEB-2022 18:23:00"),
            TransactionId = "f661e23c-10f9-4c17-8d84-3c18d5fa6947",
            ExternalReference = "df40fc45-8f22-42d1-a3b9-05d059afb726",
            Status = PaymentStatusType.Cancelled.ToString(),
            BalanceBefore = null,
            BalanceAfter = null,
            Amount = 200.00m,
            Currency = "MYR",
            PaymentMethod = "Credit Card"
        },
        new PurchaseHistoryModel
        {
            Date = DateTime.Parse("01-FEB-2023 15:23:00"),
            TransactionId = "b78a2d8b-a086-437d-aca4-73d1fa01ce9a",
            ExternalReference = "0ceec301-fd95-4efd-b5c2-8c0028ef44e4",
            Status = PaymentStatusType.Successful.ToString(),
            BalanceBefore = 200.23m,
            BalanceAfter = 200.23m,
            Amount = 200.00m,
            Currency = "USD",
            PaymentMethod = "Credit Card"
        },
        new PurchaseHistoryModel
        {
            Date = DateTime.Parse("02-FEB-2023 15:23:00"),
            TransactionId = "b58a2d8b-a086-437d-aca4-73d1fa01ce9a",
            ExternalReference = "0ce3c301-fd95-4efd-b5c2-8c0028ef44e4",
            Status = PaymentStatusType.Successful.ToString(),
            BalanceBefore = 200.23m,
            BalanceAfter = 200.23m,
            Amount = 200.00m,
            Currency = "USD",
            PaymentMethod = "Credit Card"
        },
        new PurchaseHistoryModel
        {
            Date = DateTime.Parse("03-FEB-2023 15:23:00"),
            TransactionId = "b58a2d5b-a086-437d-aca4-73d1fa01ce9a",
            ExternalReference = "0ce4c301-fd95-4efd-b5c2-8c0028ef44e4",
            Status = PaymentStatusType.Cancelled.ToString(),
            BalanceBefore = null,
            BalanceAfter = null,
            Amount = 200.00m,
            Currency = "USD",
            PaymentMethod = "Credit Card"
        },
        new PurchaseHistoryModel
        {
            Date = DateTime.Parse("04-FEB-2023 15:23:00"),
            TransactionId = "b58a2d8b-a086-437d-aca4-73d1fa01ce9a",
            ExternalReference = "0ce0c301-fd95-4efd-b5c2-8c0028ef44e4",
            Status = PaymentStatusType.Failed.ToString(),
            BalanceBefore = null,
            BalanceAfter = null,
            Amount = 200.00m,
            Currency = "USD",
            PaymentMethod = "Credit Card"
        },
    };

    public async Task<List<PurchaseHistoryModel>> GetPurchaseHistoryFilter(PurchaseHistoryRequestFilter request)
    {
        List<PurchaseHistoryModel> filteredData = _purchaseHistories;

        if (request.FromDate != null && request.ToDate != null)
        {
            filteredData = filteredData
                .Where(x => x.Date >= request.FromDate && x.Date <= request.ToDate).ToList();
        }

        if (request.Status.Count > 0)
        {
            filteredData = filteredData.Where(x => request.Status.Contains(x.Status)).ToList();
        }

        return await Task.FromResult(filteredData);
    }

    public async Task<PurchaseHistoryDetailsModel> GetPurchaseHistoryDetail(string transactionId)
    {
        PurchaseHistoryModel purchaseHistory = _purchaseHistories.First(h => h.TransactionId == transactionId);

        decimal exchangeRate = purchaseHistory.Currency == "MYR" ? 1 : 4.75m;

        PurchaseHistoryDetailsModel result = new PurchaseHistoryDetailsModel
        {
            PaymentStatusName =
                (PaymentStatusType?)purchaseHistory.Status.ToEnum(typeof(PaymentStatusType)) ??
                PaymentStatusType.Failed,
            TrxKey = purchaseHistory.TransactionId,
            PaymentAmountInOrderCurrency = purchaseHistory.Amount,
            PaymentAmountInBaseCurrency = purchaseHistory.Amount / exchangeRate,
            PaymentCurrency = purchaseHistory.Currency,
            BaseCurrency = "MYR",
            ReferenceId = purchaseHistory.ExternalReference,
            PaymentMethodName = "Stripe",
            BalanceBefore = 1000.00m,
            BalanceAfter = purchaseHistory.Amount * exchangeRate,
            ExchangeRate = exchangeRate
        };

        return await Task.FromResult(result);
    }
}