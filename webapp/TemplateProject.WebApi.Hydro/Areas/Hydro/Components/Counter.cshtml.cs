using Hydro;

namespace TemplateProject.WebApi.Hydro.Areas.Hydro.Components;

public class Counter : HydroComponent
{
    public int Count { get; set; }
    
    public void Add()
    {
        Count++;
    }
}