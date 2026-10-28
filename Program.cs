
using EduHub.Ch01.Domain;

// Bước 1: Tạo câu lạc bộ
var club = Club.Create(1, "CLB Công nghệ", "Câu lạc bộ công nghệ");

// Bước 2: Tạo thành viên
var member = Member.Create(1, "Nguyen Van An", "an@example.com");

// Bước 3: Tạo sự kiện vào ngày mai
var eventDate = DateTime.Now.AddDays(1);
var ev = EduHub.Ch01.Domain.Event.Create(
    1, club.ClubId, "Workshop C#", eventDate, "Phòng A1");

// Bước 4: Tạo phiếu đăng ký
var registration = Registration.Create(
    1, member.MemberId, ev.EventId);

// Bước 5: In thông tin ban đầu
Console.WriteLine("=== EDHUB CH01 DEMO ===");
Console.WriteLine($"Câu lạc bộ: {club.Name}");
Console.WriteLine($"Thành viên: {member.FullName}");
Console.WriteLine($"Sự kiện: {ev.Title}");
Console.WriteLine($"Trạng thái đăng ký: {registration.Status}");

// Bước 6: Xác nhận đăng ký
registration.Confirm();
Console.WriteLine($"Sau khi xác nhận: {registration.Status}");

// Bước 7: Hủy đăng ký
registration.Cancel();
Console.WriteLine($"Sau khi hủy: {registration.Status}");

// Bước 8: Thử xác nhận lại phiếu đã hủy
Console.WriteLine("\n=== KIỂM TRA QUY TẮC NGHIỆP VỤ ===");

try
{
    registration.Confirm();
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Đã chặn thao tác không hợp lệ: {ex.Message}");
}

// Bước 9: Tạo bản ghi điểm danh
var checkIn = CheckIn.Create(1, registration.RegistrationId);
Console.WriteLine($"Mã điểm danh: {checkIn.CheckInId}");
Console.WriteLine($"Thời gian điểm danh: {checkIn.CheckedInAt}");
