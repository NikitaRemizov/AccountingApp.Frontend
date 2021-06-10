using Microsoft.AspNetCore.Components;

namespace AccountingApp.Frontend.Utils.Extensions
{
    public static class NavigationManagerExtensions
    {
        public static void NavigateToAuthentification(this NavigationManager navigationManager)
        {
            navigationManager.NavigateTo("/");
        }
    }
}
