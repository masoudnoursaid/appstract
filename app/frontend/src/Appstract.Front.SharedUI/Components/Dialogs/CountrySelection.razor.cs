using Microsoft.AspNetCore.Components;
using MudBlazor;
using RESTCountries.NET.Models;

namespace Appstract.Front.SharedUI.Components.Dialogs;

public partial class CountrySelection
{
    private string? _searchText;

    [CascadingParameter] 
    private MudDialogInstance MudDialog { get; set; } = null!;

    public Country SelectedCountry { get; set; } = null!;

    private string? SearchText
    {
        get => _searchText;
        set
        {
            _searchText = value;
            if (string.IsNullOrWhiteSpace(value))
            {
                FilteredCountries = Countries;
                return;
            }

            FilteredCountries = Countries.Where(x => x.Name.Common.ToLower().Contains(value.ToLower())).ToList();
        }
    }

    [Parameter] public List<Country> Countries { get; set; } = new();

    private List<Country> FilteredCountries { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        FilteredCountries = Countries;
    }

    private void Submit()
    {
        MudDialog.Close(DialogResult.Ok(true));
    }

    private void Cancel()
    {
        MudDialog.Cancel();
    }

    private void SelectCountry(Country item)
    {
        SelectedCountry = item;
        MudDialog.Close(DialogResult.Ok(SelectedCountry));
    }

    private string GetCountryCode(Country? country)
    {
        if (country == null)
        {
            return string.Empty;
        }

        return country.Idd.Suffixes.ToList().Count == 1 ? $"{country.Idd.Root}{country.Idd.Suffixes[0]}" : country.Idd.Root;
    }
}