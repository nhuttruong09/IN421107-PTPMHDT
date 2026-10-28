
using EduHub.Ch01.Domain;

// Bước 1: Tạo câu lạc bộ
var club = Club.Create(
    1, "CLB Công nghệ", "Câu lạc bộ công nghệ");

// Bước 2: Tạo thành viên
var member = Member.Create(
    1, "Nguyen Van An", "an@example.com");

var member2 = Member.Create(
    2, "Tran Thi Binh", "binh@example.com");

// Bước 3: Thêm thành viên vào câu lạc bộ
club.AddMember(member);
club.AddMember(member2);

Console.WriteLine("=== EDHUB CH01 DEMO ===");
Console.WriteLine($"Câu lạc bộ: {club.Name}");
Console.WriteLine($"Số thành viên: {club.Members.Count}");

foreach (var m in club.Members)
{
    Console.WriteLine(
        $"- {m.MemberId}: {m.FullName} ({m.Email})");
}

// Bước 4: Xóa thành viên
club.RemoveMember(2);

Console.WriteLine(
    $"\nSố thành viên sau khi xóa: {club.Members.Count}");

// Bước 5: Tạo sự kiện
var eventDate = DateTime.Now.AddDays(1);

var ev = EduHub.Ch01.Domain.Event.Create(
    1, club.ClubId, "Workshop C#", eventDate, "Phòng A1");

// Bước 6: Tạo phiếu đăng ký
var registration = Registration.Create(
    1, member.MemberId, ev.EventId);

Console.WriteLine($"\nSự kiện: {ev.Title}");
Console.WriteLine($"Trạng thái đăng ký: {registration.Status}");

// Bước 7: Xác nhận đăng ký
registration.Confirm();

Console.WriteLine(
    $"Sau khi xác nhận: {registration.Status}");

// Bước 8: Điểm danh khi phiếu đã xác nhận
var checkIn = CheckIn.Create(1, registration);

Console.WriteLine($"Mã điểm danh: {checkIn.CheckInId}");
Console.WriteLine(
    $"Thời gian điểm danh: {checkIn.CheckedInAt}");

// Bước 9: Hủy đăng ký
registration.Cancel();

Console.WriteLine($"Sau khi hủy: {registration.Status}");

// Bước 10: Kiểm tra không cho xác nhận lại
Console.WriteLine("\n=== KIỂM TRA QUY TẮC NGHIỆP VỤ ===");

try
{
    registration.Confirm();
}
catch (InvalidOperationException ex)
{
    Console.WriteLine(
        $"Đã chặn thao tác không hợp lệ: {ex.Message}");
}

// Bước 11: Kiểm tra không cho điểm danh sau khi hủy
try
{
    CheckIn.Create(2, registration);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Đã chặn điểm danh: {ex.Message}");
}
