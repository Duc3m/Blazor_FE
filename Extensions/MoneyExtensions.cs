using System.Globalization;

namespace Blazor_FE.Extensions;

public static class MoneyExtensions
{
    private static readonly CultureInfo VnCulture = new CultureInfo("vi-VN");

    public static string ToVnd(this decimal amount)
    {
        return amount.ToString("N0", VnCulture) + " đ";
    }

    public static string ToVnd(this decimal? amount)
    {
        return (amount ?? 0).ToString("N0", VnCulture) + " đ";
    }
}
