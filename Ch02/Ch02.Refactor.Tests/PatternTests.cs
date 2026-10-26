using EduHub.Ch02.Domain;
using EduHub.Ch02.Logging;
using EduHub.Ch02.Notifications;
using EduHub.Ch02.Patterns.Observers;
using EduHub.Ch02.Patterns.Strategies;
using EduHub.Ch02.Repositories;
using EduHub.Ch02.Services;

namespace Ch02.Refactor.Tests;

/// <summary>Test cho 4 pattern và service. Dùng class giả (fake) viết tay, không cần thư viện mock.</summary>
public class PatternTests
{
    private sealed class FakeLogger : IAppLogger
    {
        public List<string> Lines { get; } = new();
        public void Info(string message) => Lines.Add(message);
    }

    private sealed class FakeNotifier : INotificationService
    {
        public List<(string To, string Message)> Sent { get; } = new();
        public void Send(string to, string message) => Sent.Add((to, message));
    }

    private sealed class FakeObserver : IEventObserver
    {
        public List<string> Titles { get; } = new();
        public void OnEventCreated(Event ev, Club club) => Titles.Add(ev.Title);
    }

    // ───────────── Factory ─────────────

    [Theory]
    [InlineData(NotificationChannel.Email, typeof(EmailNotificationService))]
    [InlineData(NotificationChannel.Sms, typeof(SmsNotificationService))]
    [InlineData(NotificationChannel.Slack, typeof(SlackNotificationService))]
    public void Factory_Create_ReturnsServiceMatchingChannel(NotificationChannel channel, Type expected)
    {
        // Arrange
        INotificationServiceFactory factory = new NotificationServiceFactory();

        // Act
        var service = factory.Create(channel);

        // Assert
        Assert.IsType(expected, service);
    }

    [Fact]
    public void Factory_Create_UnknownChannel_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        INotificationServiceFactory factory = new NotificationServiceFactory();

        // Act
        var act = () => factory.Create((NotificationChannel)999);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(act);
    }

    // ───────────── Decorator ─────────────

    [Fact]
    public void LoggingClubRepository_Add_LogsAndDelegatesToInner()
    {
        // Arrange
        var logger = new FakeLogger();
        var inner = new InMemoryClubRepository();
        IClubRepository repo = new LoggingClubRepository(inner, logger);
        var club = Club.Create("Tech Club", "mô tả");

        // Act
        repo.Add(club);

        // Assert
        Assert.Single(logger.Lines);
        Assert.Same(club, inner.GetById(club.Id));
    }

    // ───────────── Strategy ─────────────

    [Fact]
    public void ReportService_Generate_UsesGivenStrategy()
    {
        // Arrange
        var clubs = new InMemoryClubRepository();
        clubs.Add(Club.Create("Tech Club", "mô tả"));
        var report = new ReportService(clubs, new InMemoryMemberRepository(), new InMemoryEventRepository());

        // Act
        var csv = report.Generate(new CsvReportStrategy());
        var console = report.Generate(new ConsoleReportStrategy());

        // Assert
        Assert.StartsWith("Club,Members,Events,Registered,CheckedIn", csv);
        Assert.StartsWith("Club: Tech Club", console);
    }

    // ───────────── Observer ─────────────

    [Fact]
    public void EventService_CreateEvent_NotifiesAllSubscribedObservers()
    {
        // Arrange
        var clubs = new InMemoryClubRepository();
        var club = Club.Create("Tech Club", "mô tả");
        clubs.Add(club);
        var service = new EventService(clubs, new InMemoryMemberRepository(), new InMemoryEventRepository());
        var first = new FakeObserver();
        var second = new FakeObserver();
        service.Subscribe(first);
        service.Subscribe(second);

        // Act
        service.CreateEvent(club.Id, "Workshop SOLID", new DateTime(2026, 11, 20));

        // Assert
        Assert.Equal(new[] { "Workshop SOLID" }, first.Titles);
        Assert.Equal(new[] { "Workshop SOLID" }, second.Titles);
    }

    [Fact]
    public void EventService_Unsubscribe_StopsNotifyingObserver()
    {
        // Arrange
        var clubs = new InMemoryClubRepository();
        var club = Club.Create("Tech Club", "mô tả");
        clubs.Add(club);
        var service = new EventService(clubs, new InMemoryMemberRepository(), new InMemoryEventRepository());
        var observer = new FakeObserver();
        service.Subscribe(observer);
        service.Unsubscribe(observer);

        // Act
        service.CreateEvent(club.Id, "Workshop SOLID", DateTime.Today);

        // Assert
        Assert.Empty(observer.Titles);
    }

    // ───────────── ClubService (DIP: nhận phụ thuộc qua interface) ─────────────

    [Fact]
    public void ClubService_CreateClub_SavesClubAndNotifiesPresident()
    {
        // Arrange
        var clubs = new InMemoryClubRepository();
        var notifier = new FakeNotifier();
        var service = new ClubService(clubs, new InMemoryMemberRepository(), notifier, new FakeLogger());

        // Act
        var club = service.CreateClub("Tech Club", "mô tả", "president@gmail.com");

        // Assert
        Assert.Same(club, clubs.GetById(club.Id));
        Assert.Equal("president@gmail.com", Assert.Single(notifier.Sent).To);
    }

    [Fact]
    public void ClubService_AddMember_WhenClubIsFull_ThrowsInvalidOperationException()
    {
        // Arrange
        var clubs = new InMemoryClubRepository();
        var members = new InMemoryMemberRepository();
        var service = new ClubService(clubs, members, new FakeNotifier(), new FakeLogger());
        var club = service.CreateClub("Tech Club", "mô tả", "p@gmail.com");
        for (var i = 0; i < Club.MaxMembers; i++)
            service.AddMember(club.Id, $"Member {i}", $"m{i}@student.dthu.edu.vn");

        // Act
        var act = () => service.AddMember(club.Id, "Người thứ 51", "x@student.dthu.edu.vn");

        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }
}
