namespace EduHub.Ch01.Domain;

/// <summary>
/// Sự kiện do một CLB tổ chức.
/// </summary>
public class Event
{
    /// <summary>Khoá chính của sự kiện.</summary>
    public Guid Id { get; private set; }

    /// <summary>Khoá ngoại tới <see cref="Club"/>.</summary>
    public Guid ClubId { get; private set; }

    /// <summary>Tiêu đề sự kiện.</summary>
    public string Title { get; private set; } = string.Empty;

    /// <summary>Ngày giờ diễn ra.</summary>
    public DateTime Date { get; private set; }

    /// <summary>Sức chứa tối đa.</summary>
    public int Capacity { get; private set; }

    /// <summary>Sự kiện trực tuyến hay không.</summary>
    public bool IsOnline { get; private set; }

    private Event() { }

    /// <summary>
    /// Tạo sự kiện mới.
    /// </summary>
    /// <exception cref="ArgumentException">Khi ClubId rỗng, tiêu đề trống hoặc Capacity ≤ 0.</exception>
    public static Event Create(Guid clubId, string title, DateTime date, int capacity, bool isOnline)
    {
        if (clubId == Guid.Empty)
            throw new ArgumentException("ClubId không hợp lệ.", nameof(clubId));
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Tiêu đề sự kiện không được để trống.", nameof(title));
        if (capacity <= 0)
            throw new ArgumentException("Sức chứa phải lớn hơn 0.", nameof(capacity));

        return new Event
        {
            Id = Guid.NewGuid(),
            ClubId = clubId,
            Title = title.Trim(),
            Date = date,
            Capacity = capacity,
            IsOnline = isOnline
        };
    }

    /// <summary>
    /// Kiểm tra sự kiện còn chỗ hay không.
    /// </summary>
    /// <param name="registeredCount">Số lượt đăng ký hiện tại.</param>
    public bool HasCapacity(int registeredCount) => registeredCount < Capacity;
}
