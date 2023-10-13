using Newtonsoft.Json;

namespace Application.Settings.Environments;

public abstract class EnvironmentSetting
{
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}