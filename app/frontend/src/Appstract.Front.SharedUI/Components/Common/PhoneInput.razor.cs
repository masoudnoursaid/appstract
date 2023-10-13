using Appstract.Front.Application.Common.Resources;
using Appstract.Front.Application.Common.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;
using MudBlazor;
using RESTCountries.NET.Models;

namespace Appstract.Front.SharedUI.Components.Common;

public partial class PhoneInput
{
    [Parameter] 
    public string Title { get; set; } = string.Empty;
    [Parameter] 
    public string Name { get; set; } = string.Empty;
    [Parameter] 
    public EventCallback<string> PhoneNumberChangedEvent { get; set; }
    [Parameter] 
    public string DefaultCountryCode { get; set; } = "+12";
    [Parameter] 
    public string DefaultPhoneNumber { get; set; } = string.Empty;
    [Parameter] 
    public List<string> AuthorizedNumbers { get; set; } = new();
    [Parameter]
    public bool IsDisabled { get; set; }

    [Inject] private ILogger Logger { get; set; } = null!;
    private string? _searchText;
    private List<Country> _countryList = new();
    private Country? _selectedCountry = new();
    private string _phoneNumber = string.Empty;
    private bool _valid = false;
    private MudAutocomplete<string> _phoneComponent = null!;
    private int _minimumQueryLengthToSearchCountry = 2;
    private bool _isPopoverOpen = false;
    private bool _isPopoverLoaded = true;
    
    public string GetSelectedCountryCode()
    {
        return GetCountryCode(_selectedCountry);
    }
    
    public List<Country> FilteredCountries { get; set; } = new();

    public string? SearchText
    {
        get => _searchText;
        set
        {
            _searchText = value;
            if (string.IsNullOrWhiteSpace(value))
            {
                FilteredCountries = AllCountries;
                return;
            }

            FilteredCountries = AllCountries.Where(x => x.Name.Common.ToLower().StartsWith(value.ToLower())).ToList();
        }
    }

    [Parameter]
    public List<Country> AllCountries
    {
        get => _countryList;
        set => _countryList = value;
    }

    [Parameter]
    public Country? SelectedCountry
    {
        get => _selectedCountry;
        set
        {
            _selectedCountry = value;
            DefaultCountryCode = GetCountryCode(value);
        }
    }

    [Parameter]
    public string PhoneNumber
    {
        get => _phoneNumber;
        set => _phoneNumber = value;
    }

    public void Focus()
    {
        _phoneComponent.FocusAsync();
    }

    protected override void OnInitialized()
    {
        _selectedCountry = FindBestMatch(DefaultCountryCode);
        base.OnInitialized();
    }

    private Task<IEnumerable<string>> SearchAuthorizedNumbers(string value)
    {
        if (_valid)
        {
            return Task.FromResult(Enumerable.Empty<string>());
        }

        if (string.IsNullOrEmpty(value))
        {
            return Task.FromResult(AuthorizedNumbers.AsEnumerable());
        }

        return Task.FromResult(AuthorizedNumbers.Where(a =>
            a.Contains(value, StringComparison.InvariantCultureIgnoreCase)));
    }

    private Country? FindBestMatch(string phone)
    {
        if (!string.IsNullOrEmpty(phone))
        {
            for (int i = 4; i > 1; i--)
            {
                if (phone.Length < i)
                {
                    continue;
                }

                string prefix = phone.Substring(0, i);
                Country? sCountry = _countryList
                    .Where(w => w.Idd.Suffixes.Any(a => (w.Idd.Root + a).StartsWith(prefix)))
                    .MaxBy(o => o.Area);

                if (sCountry != null)
                {
                    return sCountry;
                }
            }
        }

        return _countryList
            .Where(w => w.Idd.Suffixes.Any(a => (w.Idd.Root + a).StartsWith(DefaultCountryCode)))
            .MaxBy(o => o.Area);
    }

    private bool PhoneValidation()
    {
        try
        {
            if (PhoneUtil.IsValid(_phoneNumber))
            {
                _phoneComponent.Text = _phoneNumber.ToPhoneNumberFormat();
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            OnPhoneNumberChangedEvent(string.Empty);
            Logger.LogError(ex, "Phone validation error");
            return false;
        }
    }

    protected virtual void OnPhoneNumberChangedEvent(string phoneNumber)
    {
        PhoneNumberChangedEvent.InvokeAsync(phoneNumber);
    }

    public void OpenCountrySelection()
    {
        if (_isPopoverOpen)
        {
            _isPopoverOpen = false;
        }
        else
        {
            _isPopoverOpen = true;
            SearchText = string.Empty;
            FilteredCountries = AllCountries;
            _isPopoverLoaded = true;
        }
    }

    private bool IsValid
    {
        get
        {
            if (string.IsNullOrEmpty(PhoneNumber) || PhoneNumber == "+")
            {
                return false;
            }

            return _valid;
        }
    }

    private string GetErrorMessage()
    {
        if (string.IsNullOrEmpty(PhoneNumber) || PhoneNumber == "+")
        {
            return string.Empty;
        }

        if (!_valid)
        {
            return PhoneInputResource.EnterValidNumber;
        }

        return string.Empty;
    }

    private string GetCountryFlag(string phone)
    {
        Country? country = FindBestMatch(phone);
        if (country == null || string.IsNullOrEmpty(country.Flag.Svg))
        {
            return string.Empty;
        }

        return country.Flag.Svg;
    }

    private void CheckEnterKeyCallback(KeyboardEventArgs key)
    {
        FormatText();
        switch (key.Code)
        {
            case "Enter":
            case "NumpadEnter":
                RequestCallbackInfo();
                break;
        }
    }

    private void FormatText()
    {
        try
        {
            string number = _phoneComponent.Text;

            if (number.StartsWith("+"))
            {
                number = number[1..];
            }

            if (ulong.TryParse(number, out ulong value))
            {
                _phoneComponent.Text = $"+{value}";
            }

            _phoneNumber = _phoneComponent.Text;

            _selectedCountry = FindBestMatch(_phoneNumber);
            _valid = PhoneValidation();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Phone format text error");
        }
    }

    private Task ClearPhoneNumberCallback(MouseEventArgs arg)
    {
        _phoneNumber = string.Empty;
        _valid = false;
        OnPhoneNumberChangedEvent(string.Empty);
        return Task.CompletedTask;
    }

    private void SelectCountry(Country item)
    {
        _selectedCountry = item;
        if (_selectedCountry != null)
        {
            _phoneComponent.Text = GetCountryCode(_selectedCountry);
        }

        _isPopoverOpen = false;
        StateHasChanged();
        _phoneComponent.FocusAsync();
    }

    public static string GetCountryCode(Country? country)
    {
        if (country == null)
        {
            return string.Empty;
        }

        return country.Idd.Suffixes.ToList().Count == 1
            ? $"{country.Idd.Root}{country.Idd.Suffixes[0]}"
            : country.Idd.Root;
    }

    private void PhoneNumberChanged()
    {
        CheckEnterKeyCallback(new KeyboardEventArgs());
    }

    private void CloseCountrySelection()
    {
        if (_isPopoverOpen)
        {
            _isPopoverOpen = false;
        }
    }

    private void PhoneInputBlurred()
    {
        RequestCallbackInfo();
    }

    private void RequestCallbackInfo()
    {
        if (!_valid || string.IsNullOrEmpty(_phoneNumber))
        {
            return;
        }

        OnPhoneNumberChangedEvent(_phoneNumber);
    }
}