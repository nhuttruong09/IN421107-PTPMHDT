
namespace EduHub.Ch01.Domain;

public class Event
{
    public int EventId { get; private set; }
    public int ClubId { get; private set; }
    public string Title { get; private set; }
    public DateTime StartTime { get; private set; }
    public string Location { get; private set; }

    private Event(
        int eventId,
        int clubId,
        string title,
        DateTime startTime,
        string location)
    {
        EventId = eventId;
        ClubId = clubId;
        Title = title;
        StartTime = startTime;
        Location = location;
    }

    public static Event Create(
        int eventId,
        int clubId,
        string title,
        DateTime startTime,
        string location)
    {
        if (eventId <= 0)
            throw new ArgumentException("EventId phải lớn hơn 0.");

        if (clubId <= 0)
            throw new ArgumentException("ClubId phải lớn hơn 0.");

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Tên sự kiện không được để trống.");

        if (startTime < DateTime.Now)
            throw new ArgumentException("Thời gian sự kiện không hợp lệ.");

        if (string.IsNullOrWhiteSpace(location))
            throw new ArgumentException("Địa điểm không được để trống.");

        return new Event(
            eventId,
            clubId,
            title.Trim(),
            startTime,
            location.Trim());
    }
}
