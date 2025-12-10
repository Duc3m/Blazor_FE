namespace Blazor_FE.Services.ToastClone
{
        public enum ToastLevel
        {
            Info,
            Success,
            Warning,
            Error
        }

        public interface IToastCloneService
        {
            event Action<string, ToastLevel> OnShow;
            void ShowToast(string message, ToastLevel level = ToastLevel.Success);
        }
}
