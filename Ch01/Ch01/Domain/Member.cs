namespace EduHub.Ch01.Domain;

/// <summary>
/// Thành viên sinh viên của một hoặc nhiều câu lạc bộ.
/// </summary>
public class Member
{
    /// <summary>Khoá chính của thành viên.</summary>
    public Guid Id { get; private set; }

    /// <summary>Họ tên đầy đủ.</summary>
    public string Name { get; private set; } = string.Empty;

    /// <summary>Địa chỉ email liên hệ.</summary>
    public string Email { get; private set; } = string.Empty;

    /// <summary>Mã số sinh viên.</summary>
    public string StudentCode { get; private set; } = string.Empty;

    /// <summary>Thời điểm tham gia.</summary>
    public DateTime JoinedAt { get; private set; }

    private bool _isDeactivated;

    private Member() { }

    /// <summary>
    /// Tạo thành viên mới sau khi kiểm tra dữ liệu đầu vào.
    /// </summary>
    /// <param name="name">Họ tên, không được trống.</param>
    /// <param name="email">Email hợp lệ (có ký tự @).</param>
    /// <param name="studentCode">Mã số sinh viên, không được trống.</param>
    /// <param name="joinedAt">Thời điểm tham gia; mặc định là hiện tại.</param>
    /// <exception cref="ArgumentException">Khi dữ liệu không hợp lệ.</exception>
    public static Member Create(string name, string email, string studentCode, DateTime? joinedAt = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Tên thành viên không được để trống.", nameof(name));
        if (!IsValidEmail(email))
            throw new ArgumentException("Email không hợp lệ (thiếu @).", nameof(email));
        if (string.IsNullOrWhiteSpace(studentCode))
            throw new ArgumentException("Mã số sinh viên không được để trống.", nameof(studentCode));

        return new Member
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            Email = email.Trim(),
            StudentCode = studentCode.Trim(),
            JoinedAt = joinedAt ?? DateTime.Now
        };
    }

    /// <summary>
    /// Thành viên đang hoạt động khi đã tham gia (JoinedAt không ở tương lai) và chưa bị vô hiệu hoá.
    /// </summary>
    public bool IsActive() => !_isDeactivated && JoinedAt <= DateTime.Now;

    /// <summary>Vô hiệu hoá thành viên.</summary>
    public void Deactivate() => _isDeactivated = true;

    private static bool IsValidEmail(string? email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;
        int at = email.IndexOf('@');
        return at > 0 && at < email.Length - 1;
    }
}
