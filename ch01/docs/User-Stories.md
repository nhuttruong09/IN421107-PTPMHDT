
# EduHub Ch01 - User Stories

## Vai trò người dùng

1. Sinh viên (Student)
2. Quản lý câu lạc bộ (Club Manager)
3. Quản trị viên (Administrator)

## US01 - Xem thông tin câu lạc bộ

**Là một sinh viên**, tôi muốn xem thông tin câu lạc bộ để lựa chọn câu lạc bộ phù hợp.

**Tiêu chí nghiệm thu:**
- AC1: Hệ thống hiển thị mã câu lạc bộ, tên câu lạc bộ và thông tin cần thiết.
- AC2: Không cho phép tạo câu lạc bộ với mã không hợp lệ hoặc tên bị bỏ trống.

## US02 - Quản lý thành viên

**Là một quản lý câu lạc bộ**, tôi muốn quản lý thành viên để theo dõi danh sách sinh viên tham gia.

**Tiêu chí nghiệm thu:**
- AC1: Mỗi thành viên phải có mã định danh hợp lệ.
- AC2: Không cho phép thêm thành viên có mã không hợp lệ.

## US03 - Quản lý sự kiện

**Là một quản lý câu lạc bộ**, tôi muốn tạo thông tin sự kiện để tổ chức hoạt động cho câu lạc bộ.

**Tiêu chí nghiệm thu:**
- AC1: Sự kiện phải có mã hợp lệ và tên không được để trống.
- AC2: Hệ thống từ chối dữ liệu sự kiện không hợp lệ.

## US04 - Đăng ký tham gia sự kiện

**Là một sinh viên**, tôi muốn đăng ký tham gia sự kiện để có thể tham dự hoạt động.

**Tiêu chí nghiệm thu:**
- AC1: Phiếu đăng ký phải có mã đăng ký và các mã liên quan hợp lệ.
- AC2: Phiếu đăng ký đã bị hủy không được xác nhận lại.

## US05 - Ghi nhận điểm danh

**Là một quản trị viên**, tôi muốn ghi nhận điểm danh để theo dõi việc tham dự sự kiện.

**Tiêu chí nghiệm thu:**
- AC1: Phiếu điểm danh phải có mã điểm danh và mã đăng ký hợp lệ.
- AC2: Thời gian điểm danh được ghi nhận khi tạo phiếu điểm danh thành công.
