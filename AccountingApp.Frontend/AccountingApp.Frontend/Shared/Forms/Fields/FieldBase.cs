using Microsoft.AspNetCore.Components;

namespace AccountingApp.Frontend.Shared.Forms.Fields
{
    public abstract class FieldBase : ComponentBase
    {
        [Parameter]
        public string Class { get; set; }
    }
}
