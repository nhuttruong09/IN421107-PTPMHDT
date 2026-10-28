
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

    public static CheckIn Create(int checkInId, Registration registration)
    {
        if (checkInId <= 0)
            throw new ArgumentException("CheckInId phải lớn hơn 0.");

        if (registration == null)
            throw new ArgumentNullException(nameof(registration));

        if (registration.Status != "Confirmed")
            throw new InvalidOperationException(
                "Không thể điểm danh khi phiếu đang ở trạng thái " +
                $"{registration.Status}.");

        return new CheckIn(
            checkInId,
            registration.RegistrationId,
            DateTime.Now);
    }
}
