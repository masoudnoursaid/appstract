namespace Domain.Common.Models;

public abstract class ActionResultModel
{
    public bool Success { get; set; }
    protected IEnumerable<string>? Errors { get; set; }

    public virtual string GetErrorsStr()
    {
        return Errors == null ? string.Empty : string.Join(',', Errors.ToArray());
    }
}