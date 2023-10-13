namespace Application.Common.BaseTypes.Dto;

/// <summary>
/// Show the amount and currency in one string
/// </summary>
public interface IPaymentCostShowAble
{
    string GetCost();
}