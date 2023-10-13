using Appstract.Front.Domain.Models;
using Microsoft.AspNetCore.Components;

namespace Appstract.Front.SharedUI.Components.Common.TableFilters;

public partial class CheckboxTableFilter
{
    [Parameter]
    public string Title { get; set; } = string.Empty;
    [Parameter] 
    public List<CheckboxTableFilterModel> Items { get; set; } = new();
    [Parameter] 
    public List<string> SelectedValues { get; set; } = new();
    [Parameter] 
    public EventCallback<List<string>> SelectedValuesChanged { get; set; }
    [Parameter] 
    public bool MultipleSelect { get; set; } = true;
    [Parameter] 
    public Action ReloadTable { get; set; } = null!;
    [Parameter] 
    public string Class { get; set; } = string.Empty;
    
    private List<CheckboxTableFilterModel> SelectedItems => 
        Items.Where(i=>SelectedValues.Contains(i.Value)).ToList();

    private bool _condition = true;
    private List<CheckboxTableFilterModel> _tempSelectedItems = new();
    private bool _isFilterOpen;

    protected override void OnParametersSet()
    {
        _tempSelectedItems = SelectedItems.ToList();
    }

    private void Clear()
    {
        _tempSelectedItems.Clear();
        Ok();
    }

    private void OnCheckedChanged(bool? isChecked, CheckboxTableFilterModel value)
    {
        if (isChecked == null)
        {
            return;
        }

        if (isChecked.Value)
        {
            if (!MultipleSelect)
            {
                _tempSelectedItems.Clear();
            }
            _tempSelectedItems.Add(value);
        }
        else
        {
            _tempSelectedItems.Remove(value);
        }
    }

    private void Ok()
    {
        if (_condition || _tempSelectedItems.Count == 0)
        {
            SelectedValues = _tempSelectedItems.Select(i=>i.Value).ToList();
        }
        else
        {
            SelectedValues = Items.Where(v => !_tempSelectedItems.Contains(v))
                .Select(i=>i.Value).ToList();
        }

        SelectedValuesChanged.InvokeAsync(SelectedValues);
        ReloadTable.Invoke();
        _isFilterOpen = false;
    }
    
    private void Cancel()
    {
        _tempSelectedItems = SelectedItems.ToList();
        _isFilterOpen = false;
    }
}