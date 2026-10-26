using EduHub.Ch02.Domain;

namespace Ch02.Refactor.Tests;

// Mẫu AAA: Arrange (chuẩn bị) - Act (thực hiện) - Assert (kiểm tra).
// Mỗi test chỉ kiểm tra MỘT hành vi. Đặt tên: TênMethod_TìnhHuống_KếtQuảMongĐợi.
public class DomainTests
{
    // ───────────── Club.Create ─────────────

    [Fact]
    public void Club_Create_WithValidName_ReturnsClub()
    {
        // Arrange
        const string name = "Tech Club";

        // Act
        var club = Club.Create(name, "Câu lạc bộ công nghệ");

        // Assert
        Assert.Equal(name, club.Name);
        Assert.NotEqual(Guid.Empty, club.Id);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Club_Create_WithEmptyName_ThrowsArgumentException(string name)
    {
        // Arrange (tên rỗng hoặc toàn khoảng trắng)

        // Act
        var act = () => Club.Create(name, "mô tả");

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void Club_Create_WithNameLongerThan100_ThrowsArgumentException()
    {
        // Arrange
        var tooLong = new string('a', Club.MaxNameLength + 1);

        // Act
        var act = () => Club.Create(tooLong, "mô tả");

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    // ───────────── Club.HasRoom ─────────────

    [Fact]
    public void Club_HasRoom_With49Members_ReturnsTrue()
    {
        // Arrange
        var club = Club.Create("Tech Club", "mô tả");

        // Act
        var hasRoom = club.HasRoom(Club.MaxMembers - 1);

        // Assert
        Assert.True(hasRoom);
    }

    [Fact]
    public void Club_HasRoom_AtMaxMembers_ReturnsFalse()
    {
        // Arrange
        var club = Club.Create("Tech Club", "mô tả");

        // Act
        var hasRoom = club.HasRoom(Club.MaxMembers);

        // Assert
        Assert.False(hasRoom);
    }

    // ───────────── Event.Register ─────────────

    [Fact]
    public void Event_Register_NewMember_ReturnsTrue()
    {
        // Arrange
        var ev = new Event { Title = "Workshop" };
        var memberId = Guid.NewGuid();

        // Act
        var registered = ev.Register(memberId);

        // Assert
        Assert.True(registered);
        Assert.Contains(memberId, ev.RegisteredMemberIds);
    }

    [Fact]
    public void Event_Register_SameMemberTwice_ReturnsFalse()
    {
        // Arrange
        var ev = new Event { Title = "Workshop" };
        var memberId = Guid.NewGuid();
        ev.Register(memberId);

        // Act
        var registeredAgain = ev.Register(memberId);

        // Assert
        Assert.False(registeredAgain);
        Assert.Single(ev.RegisteredMemberIds);
    }

    // ───────────── Event.CheckIn ─────────────

    [Fact]
    public void Event_CheckIn_RegisteredMember_MarksCheckedIn()
    {
        // Arrange
        var ev = new Event { Title = "Workshop" };
        var memberId = Guid.NewGuid();
        ev.Register(memberId);

        // Act
        ev.CheckIn(memberId);

        // Assert
        Assert.Contains(memberId, ev.CheckedInMemberIds);
    }

    [Fact]
    public void Event_CheckIn_UnregisteredMember_ThrowsInvalidOperationException()
    {
        // Arrange
        var ev = new Event { Title = "Workshop" };

        // Act
        var act = () => ev.CheckIn(Guid.NewGuid());

        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }
}
