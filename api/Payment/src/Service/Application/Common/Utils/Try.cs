namespace Application.Common.Utils;

public static class Try
{
    public static async Task<bool> HasException<TException>(this Func<Task> action) where TException : Exception
    {
        try
        {
            await action.Invoke();
        }
        catch (TException)
        {
            return true;
        }

        return false;
    }
}