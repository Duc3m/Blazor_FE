namespace Blazor_FE.Services.ConfirmDialog
{

    public class ConfirmDialogService : IConfirmDialogService
    {

        public event Func<string, Func<Task>, Task>? OnConfirmRequested;

        // TaskCompletionSource dùng để tạo một Task có thể được hoàn thành từ bên ngoài.
        // Giúp phương thức Show có thể await kết quả từ người dùng (xác nhận/hủy).
        private TaskCompletionSource<bool> _tcs = new();

        /// Hiển thị popup và trả về một Task sẽ hoàn thành khi người dùng lựa chọn.
        public Task<bool> Show(string message)
        {
            // Khởi tạo lại TaskCompletionSource cho mỗi yêu cầu mới.
            _tcs = new TaskCompletionSource<bool>();

            // Đây là hàm callback mà component UI sẽ gọi khi người dùng xác nhận.
            Func<Task> callback = () =>
            {
                _tcs.SetResult(true); // Người dùng đã xác nhận.
                return Task.CompletedTask;
            };

            // Kích hoạt sự kiện để hiển thị UI, truyền vào message và hàm callback.
            OnConfirmRequested?.Invoke(message, callback);

            // Trả về Task, nó sẽ hoàn thành khi người dùng đưa ra lựa chọn.
            return _tcs.Task;
        }

        /// Phương thức này được component UI gọi để báo hiệu kết quả (thường là hủy).
        public void SetResult(bool result)
        {
            // Hoàn thành Task với kết quả được cung cấp.
            if (_tcs.Task.IsCompleted == false)
            {
                _tcs.SetResult(result);
            }
        }
    }
}
