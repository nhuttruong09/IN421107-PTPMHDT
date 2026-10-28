
namespace EduHub.Ch01.Domain;

public class Club
{
    public int ClubId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }

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
}
