using System.Diagnostics.CodeAnalysis;
using PhoneNumbers;

namespace Domain.ValueObjects;

public class Phone
{
    protected Phone(PhoneNumberUtil phoneUtil, PhoneNumber phoneNumberObject)
    {
        PhoneUtil = phoneUtil;
        PhoneNumberObject = phoneNumberObject;
        PhoneLibNumberType = phoneUtil.GetNumberType(phoneNumberObject);
        Number = phoneUtil.Format(phoneNumberObject, PhoneNumberFormat.E164);
        CountryCode = phoneUtil.GetRegionCodeForNumber(phoneNumberObject);
        IsMobile = IsMobileNumber() || IsAppStoresNumber(Number);
    }

    public PhoneNumberUtil PhoneUtil { get; }
    public PhoneNumber PhoneNumberObject { get; }

    /// <summary>
    /// Gets country E164 format number of the phone.
    /// </summary>
    public string Number { get; }

    /// <summary>
    /// Gets ISO2 country code of the phone number.
    /// </summary>
    public string CountryCode { get; }

    /// <summary>
    /// Gets ITU Recommended format starting with 00 followed by [country_code][area_code][destination].
    /// https://en.wikipedia.org/wiki/List_of_international_call_prefixes.
    /// </summary>
    public string ItuFormat => $"00{Trimmed}";

    /// <summary>
    /// Get E164 format number without the plus sign.
    /// </summary>
    /// <returns>Returns E164 format number without the plus sign.</returns>
    public string Trimmed => Number[1..];

    /// <summary>
    /// Gets a value indicating whether this phone number is a mobile number or not.
    /// </summary>
    public bool IsMobile { get; }

    protected PhoneNumberType PhoneLibNumberType { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Phone"/> class.
    /// number can be in any phone number format (E164, ITU, national).
    /// If number is int, it is presumed to be E164 without plus sign,
    /// and it will prepend "+" to it.
    /// defaultCountryCode is needed when number is in national format
    /// to parse the number correctly and prevent errors.
    /// </summary>
    /// <param name="number">number.</param>
    /// <param name="phone">phone number returned when number is valid.</param>
    /// <param name="defaultCountryCode">defaultCountryCode.</param>
    /// <example>
    /// new Phone(09127654321, 'IR') => +989127654321
    /// new Phone(0651022945, 'NL') => +31651022945.
    /// </example>
    /// <returns>return true when value is valid.</returns>
    public static bool TryParse(string number, [NotNullWhen(true)] out Phone? phone, string? defaultCountryCode = null)
    {
        PhoneNumberUtil? phoneUtil = PhoneNumberUtil.GetInstance();

        PhoneNumber phoneNumberObject;

        try
        {
            phoneNumberObject = phoneUtil.Parse(number, defaultCountryCode);
        }
        catch (NumberParseException)
        {
            phone = null;
            return false;
        }

        if (!phoneUtil.IsValidNumber(phoneNumberObject))
        {
            phone = null;
            return false;
        }

        phone = new Phone(phoneUtil, phoneNumberObject);
        return true;
    }

    public static bool TryParse(ulong number, [NotNullWhen(true)] out Phone? phone, string? defaultCountryCode = null)
    {
        bool result = TryParse($"+{number}", out Phone? phoneObject, defaultCountryCode);
        phone = phoneObject;
        return result;
    }

    /// <summary>
    /// Check that provided number is a valid number or not.
    /// </summary>
    /// <param name="number">Given number.</param>
    /// <param name="defaultCountryCode">Default country code (Optional).</param>
    /// <returns>Returns true if provided number is a valid number.</returns>
    public static bool IsValid(string number, string? defaultCountryCode = null)
    {
        return TryParse(number, out Phone? _, defaultCountryCode);
    }

    /// <summary>
    /// Check that two instances are equal or not.
    /// </summary>
    /// <param name="phoneNumber">Other phone number.</param>
    /// <returns>Returns true if this phone number and other phone number are equal.</returns>
    public bool Is(string phoneNumber)
    {
        bool result = TryParse(phoneNumber, out Phone? phone);

        if (result is false)
        {
            return false;
        }

        return Number == phone!.Number;
    }

    /// <summary>
    /// Check that two instances are equal or not.
    /// </summary>
    /// <param name="otherPhone">Other <see cref="Phone"/> instance.</param>
    /// <returns>Returns true if this phone number and other phone number are equal.</returns>
    public bool Is(Phone otherPhone)
    {
        return Number == otherPhone.Number;
    }

    /// <summary>
    /// Gets numeric value of the phone number.
    /// </summary>
    /// <returns>Returns numeric value of this phone number as a 64-bit unsigned integer.</returns>
    public ulong ToUInt64()
    {
        return ulong.Parse(Trimmed);
    }

    public override string ToString()
    {
        return Number;
    }

    private static bool IsAppStoresNumber(string number)
    {
        return new[]
        {
            "+18002752273", // Apple Store Customer Service
            "+18554664438", // Play Store Customer Service
        }.Any(a => a == number);
    }

    private bool IsMobileNumber()
    {
        return PhoneLibNumberType is PhoneNumberType.MOBILE or PhoneNumberType.FIXED_LINE_OR_MOBILE;
    }
}