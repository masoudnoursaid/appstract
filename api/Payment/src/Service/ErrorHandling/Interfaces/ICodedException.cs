using ErrorHandling.Enums;

namespace ErrorHandling.Interfaces;

public interface ICodedException
{
    public CommonErrorCode GetCommonErrorCode();
}