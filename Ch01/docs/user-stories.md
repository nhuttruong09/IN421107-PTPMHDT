# User Stories – EduHub

## Actor: Sinh viên

### US-01 – Đăng ký tham gia CLB
**As a** student, **I want** to register to join a club, **so that** I can take part in its activities.

Acceptance criteria:
- **Given** I am not a member of the club, **When** I submit my name, email and student code, **Then** I am added to the club's member list.
- **Given** I am already a member, **When** I try to register again, **Then** the system rejects it with a clear message.
- **Given** my email has no `@`, **When** I submit, **Then** the system shows a validation error.

### US-02 – Đăng ký sự kiện
**As a** student, **I want** to register for a club event, **so that** I can reserve a seat.

Acceptance criteria:
- **Given** the event still has capacity, **When** I register, **Then** a registration is created with status `Pending`.
- **Given** the event is full, **When** I register, **Then** the system refuses and tells me the event is full.
- **Given** I cancelled my registration, **When** I view it, **Then** its status shows `Cancelled`.

### US-03 – Check-in tại sự kiện
**As a** student, **I want** to check in at an event, **so that** my attendance is recorded.

Acceptance criteria:
- **Given** my registration is `Confirmed`, **When** I check in, **Then** a check-in record is created with the time and method.
- **Given** my registration is `Pending` or `Cancelled`, **When** I check in, **Then** the system rejects the check-in.

## Actor: Ban CLB

### US-04 – Tạo sự kiện
**As a** club manager, **I want** to create an event, **so that** members can sign up for it.

Acceptance criteria:
- **Given** I provide a title, date and capacity > 0, **When** I save, **Then** the event is created and linked to my club.
- **Given** the capacity is ≤ 0 or the title is empty, **When** I save, **Then** the system shows a validation error.

### US-05 – Xem danh sách thành viên
**As a** club manager, **I want** to view the member list, **so that** I can manage my club.

Acceptance criteria:
- **Given** my club has members, **When** I open the list, **Then** I see each member's name, student code and join date.
- **Given** the club has no members, **When** I open the list, **Then** I see an empty-state message.

### US-06 – Xuất báo cáo tham dự
**As a** club manager, **I want** to export an attendance report, **so that** I can evaluate the event.

Acceptance criteria:
- **Given** an event has ended, **When** I request the report, **Then** it shows the number of registrations, number of check-ins and attendee names.
- **Given** nobody checked in, **When** I request the report, **Then** the check-in count is 0 and the list is empty.

## Actor: Admin

### US-07 – Quản lý CLB
**As an** admin, **I want** to create and edit clubs, **so that** the list of clubs stays accurate.

Acceptance criteria:
- **Given** I enter a unique club name, **When** I save, **Then** the club is created.
- **Given** the name is empty, **When** I save, **Then** the system rejects it.

### US-08 – Phê duyệt thành viên
**As an** admin, **I want** to approve or deactivate members, **so that** only valid students participate.

Acceptance criteria:
- **Given** a pending member, **When** I approve, **Then** the member becomes active.
- **Given** an active member, **When** I deactivate, **Then** `IsActive()` returns false.
