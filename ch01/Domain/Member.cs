
namespace EduHub.Ch01.Domain;

public class Member
{
    public int MemberId { get; private set; }
    public string FullName { get; private set; }
    public string Email { get; private set; }

    private Member(int memberId, string fullName, string email)
    {
        MemberId = memberId;
        FullName = fullName;
        Email = email;
    }

    public static Member Create(int memberId, string fullName, string email)
    {
        if (memberId <= 0)
            throw new ArgumentException("MemberId phải lớn hơn 0.");

        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Họ tên không được để trống.");

        if (string.IsNullOrWhiteSpace(email) ||
            !email.Contains('@') ||
            email.StartsWith('@') ||
            email.EndsWith('@'))
            throw new ArgumentException("Email không hợp lệ.");

        return new Member(
            memberId,
            fullName.Trim(),
            email.Trim());
    }
}
