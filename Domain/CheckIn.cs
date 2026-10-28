
namespace EduHub.Ch01.Domain;

public class CheckIn
{
    public int CheckInId { get; private set; }
    public int RegistrationId { get; private set; }
    public DateTime CheckedInAt { get; private set; }

    private CheckIn(int checkInId, int registrationId, DateTime checkedInAt)
    {
        CheckInId = checkInId;
        RegistrationId = registrationId;
        CheckedInAt = checkedInAt;
    }

    public static CheckIn Create(int checkInId, int registrationId)
    {
        if (checkInId <= 0)
            throw new ArgumentException("CheckInId phải lớn hơn 0.");

        if (registrationId <= 0)
            throw new ArgumentException("RegistrationId phải lớn hơn 0.");

        return new CheckIn(checkInId, registrationId, DateTime.Now);
    }
}
