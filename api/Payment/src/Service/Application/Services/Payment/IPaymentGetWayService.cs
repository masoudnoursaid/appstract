using Domain.Enums;

namespace Application.Services.Payment;

public interface IPaymentGetWayService
{
    SourceImplementedGetWay MyGetWayType { get; }
}