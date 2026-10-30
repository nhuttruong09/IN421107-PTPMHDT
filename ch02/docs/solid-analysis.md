# Phân tích vi phạm SOLID trong ClubManager

## 1. SRP: CreateClub làm 4 việc (dòng 35-64)
- Vi phạm: kiểm tra tên, lưu CLB, ghi log file, gửi thông báo trong một method.
- Hậu quả: sửa luật tên có thể làm hỏng phần gửi thông báo; không test riêng được từng việc.
- Cách sửa: Club.Create kiểm tra tên; ClubService chỉ điều phối logger và notifier.

## 2. DIP: ghi file trực tiếp (dòng 48, 79, 94, 111)
- Vi phạm: logic nghiệp vụ gọi thẳng File.AppendAllText.
- Hậu quả: test nào cũng ghi ra đĩa; đổi nơi ghi log phải sửa 4 chỗ.
- Cách sửa: IAppLogger nhận qua constructor.

## 3. OCP: if/else theo tên miền email (dòng 52-63)
- Vi phạm: chuỗi if/else theo @gmail.com, @yahoo.com.
- Hậu quả: thêm SMS hay Slack phải mở method ra sửa.
- Cách sửa: INotificationService cộng với Factory.

## 4. OCP và SRP: CreateEvent tự gửi thông báo (dòng 96-97)
- Vi phạm: CreateEvent tự lặp qua thành viên để gửi email.
- Hậu quả: muốn thêm hành động khi có sự kiện mới phải sửa CreateEvent.
- Cách sửa: Observer với IEventObserver.