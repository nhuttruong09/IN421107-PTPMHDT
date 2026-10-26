using EduHub.Ch01.Domain;

Console.OutputEncoding = System.Text.Encoding.UTF8;

const string Separator = "============================================================";
const int EventCapacity = 50;

static void PrintStep(int number, string title)
{
    Console.WriteLine();
    Console.WriteLine(Separator);
    Console.WriteLine($"BƯỚC {number}: {title}");
    Console.WriteLine(Separator);
}

// 1. Tạo CLB
PrintStep(1, "Tạo câu lạc bộ");
var club = Club.Create("Tech Club", "CLB công nghệ thông tin, chia sẻ kiến thức lập trình và AI.");
Console.WriteLine($"Đã tạo CLB '{club.Name}' (Id: {club.Id.ToString()[..8]}) - {club.Description}");

// 2. Thêm 3 thành viên
PrintStep(2, "Thêm thành viên vào CLB");
var members = new[]
{
    Member.Create("Nguyễn Văn An", "an.nv@student.dthu.edu.vn", "SV001"),
    Member.Create("Trần Thị Bình", "binh.tt@student.dthu.edu.vn", "SV002"),
    Member.Create("Lê Minh Cường", "cuong.lm@student.dthu.edu.vn", "SV003")
};
foreach (var m in members)
{
    club.AddMember(m);
    Console.WriteLine($"+ {m.Name} ({m.StudentCode}) - hoạt động: {m.IsActive()}");
}
Console.WriteLine($"Tổng số thành viên: {club.MemberCount()}");

// 3. Tạo sự kiện
PrintStep(3, "Tạo sự kiện");
var workshop = Event.Create(club.Id, "Workshop AI", DateTime.Now.AddDays(7), EventCapacity, isOnline: false);
Console.WriteLine($"Sự kiện '{workshop.Title}' - {workshop.Date:dd/MM/yyyy HH:mm} - sức chứa {workshop.Capacity} - online: {workshop.IsOnline}");

// 4. Đăng ký và xác nhận
PrintStep(4, "Thành viên đăng ký và xác nhận");
var registrations = new List<Registration>();
foreach (var m in members.Take(2))
{
    if (!workshop.HasCapacity(registrations.Count))
    {
        Console.WriteLine("Sự kiện đã hết chỗ.");
        break;
    }

    var reg = Registration.Create(m.Id, workshop.Id);
    registrations.Add(reg);
    Console.WriteLine($"{m.Name} đăng ký -> Status = {reg.Status}");

    reg.Confirm();
    Console.WriteLine($"{m.Name} được xác nhận -> Status = {reg.Status}");
}

// 5. Check-in
PrintStep(5, "Check-in");
var checkIns = new List<CheckIn>();
var firstReg = registrations[0];
var checkIn = CheckIn.Create(firstReg, CheckInMethod.QrCode);
checkIns.Add(checkIn);
Console.WriteLine(checkIn.Summary());

// 6. Báo cáo tham dự
PrintStep(6, "Báo cáo tham dự");
Console.WriteLine($"Sự kiện: {workshop.Title}");
Console.WriteLine($"Số đăng ký: {registrations.Count}/{workshop.Capacity}");
Console.WriteLine($"Số check-in: {checkIns.Count}");
Console.WriteLine("Danh sách đã check-in:");
foreach (var ci in checkIns)
{
    var reg = registrations.First(r => r.Id == ci.RegistrationId);
    var member = members.First(m => m.Id == reg.MemberId);
    Console.WriteLine($"  - {member.Name} ({member.StudentCode})");
}
Console.WriteLine("Danh sách đã đăng ký:");
foreach (var reg in registrations)
{
    var member = members.First(m => m.Id == reg.MemberId);
    Console.WriteLine($"  - {member.Name}: {reg.Status}");
}

// 7. Vi phạm luật nghiệp vụ
PrintStep(7, "Thử thao tác vi phạm luật nghiệp vụ");
var cancelled = registrations[1];
cancelled.Cancel();
Console.WriteLine($"Đã huỷ phiếu của thành viên thứ 2 -> Status = {cancelled.Status}");
try
{
    cancelled.Confirm();
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Bắt được lỗi nghiệp vụ: {ex.Message}");
}

Console.WriteLine();
Console.WriteLine(Separator);
Console.WriteLine("HOÀN TẤT DEMO");
Console.WriteLine(Separator);
