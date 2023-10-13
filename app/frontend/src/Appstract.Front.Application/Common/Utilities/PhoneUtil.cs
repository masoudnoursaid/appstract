using PhoneNumbers;

namespace Appstract.Front.Application.Common.Utilities;

public static class PhoneUtil
{
    private static readonly PhoneNumberUtil _phoneNumberUtil = PhoneNumberUtil.GetInstance();

    public static string GetCountryCode(string phoneNumber)
    {
        try
        {
            PhoneNumber phoneNumberProto = _phoneNumberUtil.Parse(phoneNumber, null);
            return _phoneNumberUtil.GetRegionCodeForNumber(phoneNumberProto);
        }
        catch
        {
            return string.Empty;
        }
    }

    public static string ToPhoneNumberFormat(this string phoneNumber)
    {
        string newPhoneNumber = phoneNumber;
        if (string.IsNullOrWhiteSpace(newPhoneNumber))
        {
            return string.Empty;
        }

        if (newPhoneNumber.StartsWith("+"))
        {
            newPhoneNumber = newPhoneNumber[1..];
        }

        if (ulong.TryParse(newPhoneNumber, out ulong num))
        {
            newPhoneNumber = $"+{num}";
        }

        try
        {
            PhoneNumber number = _phoneNumberUtil.Parse(newPhoneNumber, null);
            if (_phoneNumberUtil.IsValidNumber(number))
            {
                return _phoneNumberUtil.Format(number, PhoneNumberFormat.INTERNATIONAL);
            }
        }
        catch
        {
            return phoneNumber;
        }

        return phoneNumber;
    }

    public static string RawStyle(string phoneNumber)
    {
        try
        {
            if (IsValid(phoneNumber))
            {
                PhoneNumber number = _phoneNumberUtil.Parse(phoneNumber, null);
                return $"+{number.CountryCode}{number.NationalNumber}";
            }
        }
        catch
        {
            return phoneNumber;
        }

        return phoneNumber;
    }

    public static bool IsValid(string number)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                return false;
            }

            PhoneNumber phoneNumber = _phoneNumberUtil.Parse(number, null);
            if (_phoneNumberUtil.IsValidNumber(phoneNumber))
            {
                PhoneNumberType type = _phoneNumberUtil.GetNumberType(phoneNumber);

                if (type is PhoneNumberType.MOBILE or PhoneNumberType.FIXED_LINE
                    or PhoneNumberType.FIXED_LINE_OR_MOBILE)
                {
                    return true;
                }
            }
        }
        catch (Exception)
        {
            return false;
        }

        return false;
    }

    public static string NormalizePhoneNumber(this string phoneNumber)
    {
        return string.IsNullOrWhiteSpace(phoneNumber)
            ? phoneNumber
            : phoneNumber.Replace(" ", string.Empty)
                .Replace("-", string.Empty)
                .Replace("(", string.Empty)
                .Replace(")", string.Empty);
    }
}