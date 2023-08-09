using PhoneNumbers;

namespace Domain.ValueObjects;

public class Mobile : Phone
{
    private Mobile(PhoneNumberUtil phoneUtil, PhoneNumber phoneNumberObject)
        : base(phoneUtil, phoneNumberObject)
    {
    }

    public static bool TryParse(string number, out Mobile? mobile, string? defaultCountryCode = null)
    {
        bool result = Phone.TryParse(number, out Phone? phone, defaultCountryCode);

        if (!result || phone is null)
        {
            mobile = null;
            return false;
        }

        if (!phone.IsMobile)
        {
            mobile = null;
            return false;
        }

        mobile = new Mobile(phone.PhoneUtil, phone.PhoneNumberObject);
        return true;
    }

    public static bool TryParse(ulong number, out Mobile? mobile, string? defaultCountryCode = null)
    {
        string numberAsString = $"+{number}";
        bool result = TryParse(numberAsString, out Mobile? mobileObject, defaultCountryCode);
        mobile = mobileObject;
        return result;
    }
}