namespace EduHub.Ch01.Domain;

/// <summary>
/// Câu lạc bộ sinh viên; quản lý danh sách thành viên.
/// </summary>
public class Club
{
    private readonly List<Member> _members = new();

    /// <summary>Khoá chính của CLB.</summary>
    public Guid Id { get; private set; }

    /// <summary>Tên CLB.</summary>
    public string Name { get; private set; } = string.Empty;

    /// <summary>Mô tả CLB.</summary>
    public string Description { get; private set; } = string.Empty;

    /// <summary>Thời điểm thành lập.</summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>Danh sách thành viên (chỉ đọc từ bên ngoài).</summary>
    public IReadOnlyList<Member> Members => _members;

    private Club() { }

    /// <summary>
    /// Tạo CLB mới.
    /// </summary>
    /// <param name="name">Tên CLB, không được trống.</param>
    /// <param name="description">Mô tả CLB.</param>
    /// <exception cref="ArgumentException">Khi tên trống.</exception>
    public static Club Create(string name, string? description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Tên CLB không được để trống.", nameof(name));

        return new Club
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            Description = description?.Trim() ?? string.Empty,
            CreatedAt = DateTime.Now
        };
    }

    /// <summary>
    /// Thêm thành viên vào CLB.
    /// </summary>
    /// <exception cref="ArgumentNullException">Khi member là null.</exception>
    /// <exception cref="InvalidOperationException">Khi thành viên đã có trong CLB.</exception>
    public void AddMember(Member member)
    {
        ArgumentNullException.ThrowIfNull(member);
        if (_members.Any(m => m.Id == member.Id))
            throw new InvalidOperationException($"Thành viên '{member.Name}' đã có trong CLB.");

        _members.Add(member);
    }

    /// <summary>Số thành viên hiện có.</summary>
    public int MemberCount() => _members.Count;
}
