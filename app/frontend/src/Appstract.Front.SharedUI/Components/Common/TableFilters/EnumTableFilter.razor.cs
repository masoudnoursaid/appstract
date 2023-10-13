using Microsoft.AspNetCore.Components;

namespace Appstract.Front.SharedUI.Components.Common.TableFilters;

public partial class EnumTableFilter<TEnum> where TEnum : Enum
{
    [Parameter]
    public string Title { get; set; } = string.Empty;
    [Parameter] 
    public List<TEnum> SelectedValues { get; set; } = new();
    [Parameter] 
    public EventCallback<List<TEnum>> SelectedValuesChanged { get; set; }
    [Parameter] 
    public bool MultipleSelect { get; set; } = true;
    [Parameter] 
    public Action ReloadTable { get; set; } = null!;
    [Parameter] 
    public string Class { get; set; } = string.Empty;

    private bool _condition = true;
    private List<TEnum> _availableValues = Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToList();
    private List<TEnum> _tempSelectedValues = new();
    private bool _isFilterOpen;

    private void Clear()
    {
        _tempSelectedValues.Clear();
        Ok();
    }

    private void OnCheckedChanged(bool? isChecked, Enum value)
    {
        if (isChecked == null)
        {
            return;
        }

        if (isChecked.Value)
        {
            if (!MultipleSelect)
            {
                _tempSelectedValues.Clear();
            }
            _tempSelectedValues.Add((TEnum)value);
        }
        else
        {
            _tempSelectedValues.Remove((TEnum)value);
        }
    }

    private void Ok()
    {
        if (_condition || _tempSelectedValues.Count == 0)
        {
            SelectedValues = _tempSelectedValues.ToList();
        }
        else
        {
            SelectedValues = _availableValues.Where(v => !_tempSelectedValues.Contains(v)).ToList();
        }

        SelectedValuesChanged.InvokeAsync(SelectedValues);
        ReloadTable.Invoke();
        _isFilterOpen = false;
    }
    
    private void Cancel()
    {
        _tempSelectedValues = SelectedValues.ToList();
        _isFilterOpen = false;
    }
}