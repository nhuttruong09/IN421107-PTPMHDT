namespace EduHub.Ch01.Domain;

/// <summary>Hình thức điểm danh.</summary>
public enum CheckInMethod
{
    /// <summary>Ban tổ chức điểm danh thủ công.</summary>
    Manual,
    /// <summary>Quét mã QR.</summary>
    QrCode,
    /// <summary>Điểm danh trực tuyến.</summary>
    Online
}

/// <summary>
/// Ghi nhận điểm danh của một <see cref="Registration"/> tại sự kiện.
/// </summary>
public class CheckIn
{
    /// <summary>Khoá chính.</summary>
    public Guid Id { get; private set; }

    /// <summary>Khoá ngoại tới <see cref="Registration"/>.</summary>
    public Guid RegistrationId { get; private set; }

    /// <summary>Thời điểm điểm danh.</summary>
    public DateTime CheckedInAt { get; private set; }

    /// <summary>Hình thức điểm danh.</summary>
    public CheckInMethod Method { get; private set; }

    private CheckIn() { }

    /// <summary>
    /// Tạo bản ghi điểm danh; chỉ cho phép khi phiếu đăng ký đã được xác nhận.
    /// </summary>
    /// <exception cref="ArgumentNullException">Khi registration là null.</exception>
    /// <exception cref="InvalidOperationException">Khi phiếu chưa ở trạng thái Confirmed.</exception>
    public static CheckIn Create(Registration registration, CheckInMethod method)
    {
        ArgumentNullException.ThrowIfNull(registration);
        if (registration.Status != RegistrationStatus.Confirmed)
            throw new InvalidOperationException("Chỉ phiếu đã xác nhận mới được điểm danh.");

        return new CheckIn
        {
            Id = Guid.NewGuid(),
            RegistrationId = registration.Id,
            CheckedInAt = DateTime.Now,
            Method = method
        };
    }

    /// <summary>Chuỗi mô tả bản ghi điểm danh.</summary>
    public string Summary() =>
        $"Check-in {Id.ToString()[..8]} | Phiếu {RegistrationId.ToString()[..8]} | {CheckedInAt:HH:mm:ss dd/MM/yyyy} | {Method}";
}
