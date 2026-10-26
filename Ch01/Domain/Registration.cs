namespace EduHub.Ch01.Domain;

/// <summary>Trạng thái của phiếu đăng ký.</summary>
public enum RegistrationStatus
{
    /// <summary>Chờ xác nhận.</summary>
    Pending,
    /// <summary>Đã xác nhận.</summary>
    Confirmed,
    /// <summary>Đã huỷ.</summary>
    Cancelled
}

/// <summary>
/// Phiếu đăng ký của một <see cref="Member"/> tham dự một <see cref="Event"/> (bảng nối nhiều–nhiều).
/// </summary>
public class Registration
{
    /// <summary>Khoá chính.</summary>
    public Guid Id { get; private set; }

    /// <summary>Khoá ngoại tới <see cref="Member"/>.</summary>
    public Guid MemberId { get; private set; }

    /// <summary>Khoá ngoại tới <see cref="Event"/>.</summary>
    public Guid EventId { get; private set; }

    /// <summary>Thời điểm đăng ký.</summary>
    public DateTime RegisteredAt { get; private set; }

    /// <summary>Trạng thái hiện tại.</summary>
    public RegistrationStatus Status { get; private set; }

    private Registration() { }

    /// <summary>
    /// Tạo phiếu đăng ký mới với trạng thái <see cref="RegistrationStatus.Pending"/>.
    /// </summary>
    /// <exception cref="ArgumentException">Khi MemberId hoặc EventId rỗng.</exception>
    public static Registration Create(Guid memberId, Guid eventId)
    {
        if (memberId == Guid.Empty)
            throw new ArgumentException("MemberId không hợp lệ.", nameof(memberId));
        if (eventId == Guid.Empty)
            throw new ArgumentException("EventId không hợp lệ.", nameof(eventId));

        return new Registration
        {
            Id = Guid.NewGuid(),
            MemberId = memberId,
            EventId = eventId,
            RegisteredAt = DateTime.Now,
            Status = RegistrationStatus.Pending
        };
    }

    /// <summary>
    /// Xác nhận phiếu đăng ký.
    /// </summary>
    /// <exception cref="InvalidOperationException">Khi phiếu đã bị huỷ.</exception>
    public void Confirm()
    {
        if (Status == RegistrationStatus.Cancelled)
            throw new InvalidOperationException("Không thể xác nhận phiếu đã huỷ.");

        Status = RegistrationStatus.Confirmed;
    }

    /// <summary>
    /// Huỷ phiếu đăng ký.
    /// </summary>
    /// <exception cref="InvalidOperationException">Khi phiếu đã được huỷ trước đó.</exception>
    public void Cancel()
    {
        if (Status == RegistrationStatus.Cancelled)
            throw new InvalidOperationException("Phiếu đã được huỷ trước đó.");

        Status = RegistrationStatus.Cancelled;
    }
}
