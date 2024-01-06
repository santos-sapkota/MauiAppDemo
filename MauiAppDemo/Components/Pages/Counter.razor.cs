using Microsoft.AspNetCore.Components;

namespace MauiAppDemo.Components.Pages
{
    public partial class Counter
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private int currentCount = 0;

        private void IncrementCount()
        {
            currentCount++;
            NavigationManager.NavigateTo("/weather");
        }
    }
}