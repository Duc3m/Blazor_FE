namespace Blazor_FE.Services.ToastClone
{
    public class ToastCloneService : IToastCloneService
    {
        public event Action<string, ToastLevel>? OnShow;

        public void ShowToast(string message, ToastLevel level = ToastLevel.Success)
        {
            OnShow?.Invoke(message, level);
        }
    }
}
