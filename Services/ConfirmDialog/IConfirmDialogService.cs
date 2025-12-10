namespace Blazor_FE.Services.ConfirmDialog
{
    public interface IConfirmDialogService
    {
        event Func<string, Func<Task>, Task> OnConfirmRequested;
        Task<bool> Show(string message);

        public void SetResult(bool result);
    }
}
