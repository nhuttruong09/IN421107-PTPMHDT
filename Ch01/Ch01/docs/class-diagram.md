# Class Diagram – EduHub (sơ bộ)

```mermaid
classDiagram
    class Member {
        +Guid Id  [PK]
        +string Name
        +string Email
        +string StudentCode
        +DateTime JoinedAt
        +Create(name, email, studentCode)$ Member
        +IsActive() bool
        +Deactivate() void
    }

    class Club {
        +Guid Id  [PK]
        +string Name
        +string Description
        +DateTime CreatedAt
        +List~Member~ Members
        +Create(name, description)$ Club
        +AddMember(member) void
        +MemberCount() int
    }

    class Event {
        +Guid Id  [PK]
        +Guid ClubId  [FK to Club]
        +string Title
        +DateTime Date
        +int Capacity
        +bool IsOnline
        +Create(clubId, title, date, capacity, isOnline)$ Event
        +HasCapacity(registeredCount) bool
    }

    class Registration {
        +Guid Id  [PK]
        +Guid MemberId  [FK to Member]
        +Guid EventId  [FK to Event]
        +DateTime RegisteredAt
        +RegistrationStatus Status
        +Create(memberId, eventId)$ Registration
        +Confirm() void
        +Cancel() void
    }

    class CheckIn {
        +Guid Id  [PK]
        +Guid RegistrationId  [FK to Registration]
        +DateTime CheckedInAt
        +CheckInMethod Method
        +Create(registration, method)$ CheckIn
        +Summary() string
    }

    Club "*" -- "*" Member : has members
    Club "1" --> "*" Event : organizes
    Member "1" --> "*" Registration : makes
    Event "1" --> "*" Registration : receives
    Registration "1" --> "0..1" CheckIn : attended via
```

**Chú thích:** `Id` là khoá chính (PK); các thuộc tính `ClubId`, `MemberId`, `EventId`, `RegistrationId` là khoá ngoại (FK).
Quan hệ Member ↔ Event là nhiều–nhiều, được hiện thực qua bảng nối `Registration`.
