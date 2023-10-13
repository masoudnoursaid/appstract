namespace Application.Services.Payment;

public abstract class PaymentClientService<T> where T : class
{
    protected abstract T SandBoxConfiguration();
    protected abstract T LiveConfiguration();
}