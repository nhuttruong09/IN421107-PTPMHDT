# Bài tập Ch02 — Refactoring EduHub với SOLID và Design Patterns

Bài làm Ch02: tách God class `ClubManager` thành các class nhỏ, phụ thuộc qua interface, áp dụng 4 pattern (Factory, Decorator, Strategy, Observer) và viết unit test xUnit theo mẫu Arrange-Act-Assert.

## Chạy thử

```bash
dotnet build Ch02.Refactor.sln
dotnet test Ch02.Refactor.sln
dotnet run --project Ch02.Refactor
```

## Cấu trúc

```
Ch02/
├── Ch02.Refactor/
│   ├── Domain/          Club, Event, Member (quy tắc nghiệp vụ nằm ở đây)
│   ├── Logging/         IAppLogger, ConsoleLogger, FileLogger
│   ├── Notifications/   INotificationService, 3 kênh, Factory
│   ├── Repositories/    IClubRepository, IMemberRepository, IEventRepository,
│   │                    bản InMemory và LoggingClubRepository (Decorator)
│   ├── Patterns/
│   │   ├── Observers/   IEventObserver, EmailNotifier, LogNotifier
│   │   └── Strategies/  IReportStrategy, Console, CSV
│   ├── Services/        ClubService, EventService, ReportService (+ interface)
│   └── Program.cs       Composition root: nơi duy nhất dùng new class cụ thể
└── Ch02.Refactor.Tests/
    ├── DomainTests.cs   8 test AAA cho Club và Event
    └── PatternTests.cs  Test cho Factory, Decorator, Strategy, Observer, ClubService
```

## Nhiệm vụ 1: Vi phạm SOLID trong `ClubManager` (bản gốc)

| # | Dòng | Nguyên tắc | Vi phạm | Hậu quả | Cách sửa |
|---|------|-----------|---------|---------|----------|
| 1 | 35-63, 67-84, 88-99 | SRP | Mỗi method vừa validate, lưu dữ liệu, ghi log, vừa gửi thông báo. Cả class quản lý CLB, thành viên, sự kiện, báo cáo | Đổi một việc phải sửa cả class, rất khó test riêng từng phần | Tách `ClubService`, `EventService`, `ReportService` |
| 2 | 52-63 | OCP | Chuỗi `if/else` theo đuôi email (Gmail, Yahoo, khác) | Thêm kênh SMS hay Slack phải sửa method đang chạy ổn | `INotificationService` + `NotificationServiceFactory` |
| 3 | 48, 79, 94, 111 | DIP | Gọi thẳng `File.AppendAllText` và `Console.WriteLine` | Không thay được nơi ghi log, test sẽ ghi ra file thật | `IAppLogger` nhận qua constructor |
| 4 | 30-31, 116 | DIP / đóng gói | Tự giữ `List<Club>`, `List<Member>` và `GetAllClubs()` trả về chính list đó | Bên ngoài sửa được dữ liệu bên trong, không đổi được nơi lưu trữ | `IClubRepository`, `IMemberRepository`, `IEventRepository` |
| 5 | 72 | SRP | Luật "tối đa 50 thành viên" và luật tên CLB nằm trong service thay vì domain | Quy tắc nghiệp vụ bị phân tán, dễ lệch nhau | Chuyển vào `Club.Create` và `Club.HasRoom` |
| 6 | 103-113 | OCP | `PrintClubReport` cố định định dạng in ra console | Muốn xuất CSV phải sửa method | `IReportStrategy` (Console, CSV) |
| 7 | 88-99 | Lỗi logic | `CreateEvent` tạo `Event` nhưng không lưu đâu cả | Sự kiện mất ngay sau khi trả về | `IEventRepository.Add` trong `EventService` |

## Nhiệm vụ 2 và 3: Class, interface và pattern

| Pattern | Giải quyết vấn đề | Nơi áp dụng |
|---------|-------------------|-------------|
| Factory | Thay chuỗi `if/else` chọn kênh thông báo | `INotificationServiceFactory`, `NotificationServiceFactory` |
| Decorator | Ghi log truy cập repository mà không sửa repository gốc | `LoggingClubRepository` bọc `IClubRepository` |
| Strategy | Đổi định dạng báo cáo mà không sửa `ReportService` | `IReportStrategy`, `ConsoleReportStrategy`, `CsvReportStrategy` |
| Observer | Tạo sự kiện xong tự báo cho nhiều bên, không hard-code | `IEventObserver`, `EmailNotifier`, `LogNotifier`, `EventService.Subscribe` |

Mọi phụ thuộc đi qua interface và được truyền vào bằng constructor (DIP). Mỗi interface chỉ có vài method (ISP). Thêm kênh thông báo hay định dạng báo cáo mới chỉ cần thêm class (OCP).

## Nhiệm vụ 4: Unit test

- `DomainTests.cs`: 8 test (10 ca chạy) cho `Club.Create`, `Club.HasRoom`, `Event.Register`, `Event.CheckIn`, gồm cả trường hợp lỗi và biên 49/50 thành viên.
- `PatternTests.cs`: test cho Factory, Decorator, Strategy, Observer và `ClubService`, dùng class giả viết tay nên không cần thư viện mock.
