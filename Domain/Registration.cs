
namespace EduHub.Ch01.Domain;

public class Registration
{
    public int RegistrationId { get; private set; }
    public int MemberId { get; private set; }
    public int EventId { get; private set; }
    public string Status { get; private set; }

    private Registration(
        int registrationId,
        int memberId,
        int eventId)
    {
        RegistrationId = registrationId;
        MemberId = memberId;
        EventId = eventId;
        Status = "Pending";
    }

    public static Registration Create(
        int registrationId,
        int memberId,
        int eventId)
    {
        if (registrationId <= 0)
            throw new ArgumentException("RegistrationId phải lớn hơn 0.");

        if (memberId <= 0)
            throw new ArgumentException("MemberId phải lớn hơn 0.");

        if (eventId <= 0)
            throw new ArgumentException("EventId phải lớn hơn 0.");

        return new Registration(registrationId, memberId, eventId);
    }

    public void Confirm()
    {
        if (Status == "Cancelled")
            throw new InvalidOperationException(
                "Không thể xác nhận phiếu đang ở trạng thái Cancelled.");

        if (Status == "Confirmed")
            throw new InvalidOperationException(
                "Phiếu đăng ký đã được xác nhận.");

        Status = "Confirmed";
    }

    public void Cancel()
    {
        if (Status == "Cancelled")
            throw new InvalidOperationException(
                "Phiếu đăng ký đã bị hủy.");

        Status = "Cancelled";
    }
}
