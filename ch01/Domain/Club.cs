
namespace EduHub.Ch01.Domain;

public class Club
{
    public int ClubId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }

    private readonly List<Member> _members = new();

    public IReadOnlyList<Member> Members => _members.AsReadOnly();

    private Club(int clubId, string name, string description)
    {
        ClubId = clubId;
        Name = name;
        Description = description;
    }

    public static Club Create(int clubId, string name, string description)
    {
        if (clubId <= 0)
            throw new ArgumentException("ClubId phải lớn hơn 0.");

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Tên câu lạc bộ không được để trống.");

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Mô tả không được để trống.");

        return new Club(clubId, name.Trim(), description.Trim());
    }

    public void AddMember(Member member)
    {
        if (member == null)
            throw new ArgumentNullException(nameof(member));

        if (_members.Any(m => m.MemberId == member.MemberId))
            throw new InvalidOperationException("Thành viên đã tồn tại trong câu lạc bộ.");

        _members.Add(member);
    }

    public void RemoveMember(int memberId)
    {
        var member = _members.FirstOrDefault(m => m.MemberId == memberId);

        if (member == null)
            throw new InvalidOperationException("Không tìm thấy thành viên.");

        _members.Remove(member);
    }
}
