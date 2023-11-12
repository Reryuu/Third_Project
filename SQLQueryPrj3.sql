CREATE DATABASE project3 ;
GO
USE project3;

CREATE TABLE applicant (
  id char(5)  NOT NULL PRIMARY KEY ,
  user_id int IDENTITY(10,1),
  name nvarchar(50) DEFAULT NULL,
  email nvarchar(255) DEFAULT NULL,
  experience nvarchar(255) DEFAULT NULL,
  education nvarchar(100) DEFAULT NULL,
  skills nvarchar(255) DEFAULT NULL,
  target nvarchar(255) DEFAULT NULL,
  status varchar(25) NOT NULL DEFAULT 'Not in process',
  phone varchar(20) NOT NULL,
  image varbinary(Max) NULL,
  createAt date NOT NULL DEFAULT CURRENT_TIMESTAMP
);

GO
INSERT INTO applicant (id,name,email,experience, education, skills,target, status,phone,image, createAt) VALUES
('A0001','name01','name01@gmail.com', N'Đã làm thiết kế website tại FPT', 'Imperial high school', 'Ielts, Java',N'Làm sếp', 'Not in process','2025550171',NULL, '2021-10-08' ),
('A0002','name02','name02@gmail.com', N'Đã làm web tại công ty XYZ', 'National university', 'C#,C++',N'Làm sếp', 'Not in process','2025550149',NULL, '2021-10-08' ),
('A0003','name03','name03@gmail.com', N'Đã làm web tại Aptech', 'RMIT university', 'PHP, SQL, ReactJs',N'Làm sếp', 'Not in process','2025550113',NULL, '2021-10-08'),
('A0004','name04','name04@gmail.com', N'Đã làm giảng viên tại FPT Polytech', 'National university', 'Ielts, Kanji, N5, Python',N'Làm sếp', 'Not in process','1216736833',NULL, '2021-10-08' ),
('A0005','name05','name05@gmail.com', N'Đã thực tập quản lí dữ liệu tại Viettel', 'MIS secondary school', 'Asp.net, Azure',N'Làm sếp', 'Not in process','1214674591',NULL, '2021-10-08'),
('A0006','name06','name06@gmail.com', N'Đã thực tập quản lí tại công ty MNO', 'Harvard university', 'Ruby,Team work, Time Management',N'Làm sếp', 'Not in process','1212783494',NULL, '2021-10-08'),
('A0007','name07','name07@gmail.com', N'Đã làm việc 2 năm về làm game tại công ty XYZ', 'Westpoint university', 'Javascript, Java, C++',N'Làm sếp', 'Not in process','1216433509',NULL, '2021-10-08'),
('A0008','name08','name08@gmail.com', N'Đã làm chủ tịch Apple', 'random university', 'Cheer up colleagues',N'Làm sếp', 'Not in process','121216281',NULL, '2021-10-08'),
('A0009','name09','name09@gmail.com', N'4 MVP, 3 chức vô địch NBA và là cầu thủ được trả lương cao thứ 6 trong lịch sử các giải đấu', 'Innovative Lebron Lab', 'HTML5, CSS, WordPress',N'Vô địch season 21-22', 'Not in process','2025550125',NULL, '2021-10-08' );

GO
DROP TABLE IF EXISTS department;
GO
CREATE TABLE department (
  id char(4) NOT NULL PRIMARY KEY,
  name varchar(100) NOT NULL
) ;

GO
INSERT INTO department (id, name) VALUES
('D001', 'Marketing & Sales'),
('D002', 'It service'),
('D003', 'Human resource'),
('D004', 'Security'),
('D005', 'Research & Development');

GO
DROP TABLE IF EXISTS description;
GO
CREATE TABLE description (
  id int NOT NULL PRIMARY KEY IDENTITY(1,1),
  about nvarchar(1000) DEFAULT NULL,
  required nvarchar(1000) DEFAULT NULL,
  interests nvarchar(1000) DEFAULT NULL
) ;

GO
SET IDENTITY_INSERT description ON
GO
INSERT INTO description (id, about, required, interests) VALUES
(1, N'Tham gia thiết kế, phát triển, triển khai và bảo trì các sản phẩm phần mềm trên nền tảng Java của Công ty cho các đối tượng khách hàng khác nhau.\r\nLập trình và tối ưu hóa để đảm bảo hiệu suất, chất lượng và khả năng đáp ứng tốt nhất của các ứng dụng;\r\nXây dựng kế hoạch và quản lý tiến độ lập trình theo kế hoạch\r\nThực hiện các công việc khác do Cán bộ quản lý yêu cầu.\r\n', N'Có kinh nghiệm lập trình tại các dự án thực tế từ khoảng 1 năm trở lên.\r\nCó hiểu biết, kinh nghiệm với Java / JEE, Spring framework (Spring-core, Spring-boot, Spring Security,…), Hibernate, iBatis…;\r\nCó kinh nghiệm về Web Framework (Spring MVC, Struts,v.v..), JavaScript framework (Angular, Jquery, VueJS,…);\r\nƯu tiên:\r\n- Ứng viên có kinh nghiệm xây dựng, phát triển các hệ thống ứng dụng trên các nền tảng công nghệ BPM và ECM\r\n- Có hiểu biết hoặc có kinh nghiệm về microservice (Jersey, SpringBoot, MicroProfile…)\r\n- Ứng viên có khả năng nghiên cứu, nghe, nói, đọc, viết bằng tiếng Anh\r\n- Có tinh thần trách nhiệm, ham học hỏi và chủ động trong công việc', N'Thời gian làm việc từ thứ Hai đến thứ Sáu.\r\nThu nhập cao, tăng lương đều hàng năm tương xứng với năng lực. \r\nThưởng nóng khi thi đỗ chứng chỉ chuyên môn, thưởng giai đoạn dự án, thưởng cuối năm v.v.. và nhiều khoản thưởng hấp dẫn khác theo thành tích công việc và tình hình kinh doanh của Công ty.\r\nMôi trường làm việc trẻ trung, thân thiện, năng động\r\nTeam Building, Tiệc sinh nhật công ty, Tiệc tổng kết năm, tiệc sinh nhật hàng tháng, 08/03, 20/10, Seatech Men’s Day, và nhiều hoạt động ngoại khóa (CLB bóng đá, CLB marathon) v.v... được tổ chức thường xuyên quanh năm\r\nĐược tham gia nhiều khóa đào tạo nâng cao chuyên môn, công ty hỗ trợ kinh phí thi chứng chỉ chuyên ngành phục vụ cho công việc.\r\nCơ hội thăng tiến các vị trí quản lý cho các bạn trẻ có năng lực.\r\nHưởng đầy đủ các chế độ theo luật lao động hiện hành và các chính sách đãi ngộ tốt. Được Công ty tài trợ tham gia gói bảo hiểm sức khỏe với mức bảo lãnh viện phí lên đến 180 triệu/năm.\r\n'),
(2, N'- Tham gia nghiên cứu xây dựng hệ thống và phát triển các dự án sản phẩm của công ty.\r\n- Vận hành và bảo trì các ứng dụng, dịch vụ hiện tại của công ty.   \r\n- Triển khai và phát triển dự án theo đơn đặt hàng của khách hàng.\r\n- Hỗ trợ kỹ thuật, hướng dẫn vận hành cho khách hàng.\r\n- Thực hiện nhiệm vụ khác theo sự phân công trực tiếp của Trưởng phòng, Ban lãnh đạo.\r\n', N'- Tốt nghiệp Cao đẳng – Đại học, hoặc các trường đào tạo IT  \r\n- Có từ 6 tháng kinh nghiệm lập trình bằng ngôn ngữ Python, ưu tiên ứng viên đã làm với Odoo\r\n- Hiểu biết về cơ sở dữ liệu PostgreSQL\r\n- Biết về hệ thống Hosting, server, cài đặt server, Linux là lợi thế\r\n- Tư duy lập trình tốt.   \r\n- Cần cù chịu khó, nhiệt tình trách nhiệm, trung thực chu đáo, tỉ mỉ khó tính về chất lượng và hình thức sản phẩm.\r\n', N'- Lương cơ bản từ 12-20 triệu, thoả thuận theo đúng năng lực, hưởng thu nhập thêm theo dự án.\r\n- Thưởng ngày lễ, thưởng hiệu suất công việc\r\n- Được hưởng đầy đủ các chế độ quyền lợi của công ty như phụ cấp, BHXH, du lịch, thưởng Tết…\r\n- Môi trường làm việc chuyên nghiệp, trẻ trung, không gò bó, tự do thể hiện sáng.\r\n- Được đóng BHXH theo quy định\r\n- Có cơ hội thăng tiến với lộ trình công danh rõ ràng. Định kỳ sẽ xét lên vị trí trưởng nhóm – trưởng phòng đối với những nhân viên có thành tích tốt.\r\n'),
(3, N'- Điều tra hệ thống hiện tại của khách hàng và đề xuất giải pháp phù hợp theo mong muốn của khách hàng. \r\n- Tìm kiếm các giải pháp, phát triển các tính năng mới cho ứng dụng  \r\n- Thực hiện tạo tài liệu ở mức high level design\r\n', N'- Tốt nghiệp ĐH chính quy, chuyên ngành CNTT or Điện tử tự động hóa \r\n- Base IT tốt\r\n - Có kinh nghiệm ít nhất 3 năm đối với ngôn ngữ C/C++\r\n - Thành thạo về con trỏ trong C++, PostMessage, callback, hàm hủy, cơ chế dọn rác, token, cache... \r\n- Có kinh nghiệm vẽ và xử lý responsive UI bằng code behind\r\n- UV có tính ổn định cao, ít nhảy việc \r\n- Có khả năng tự đọc hiểu tài liệu và nắm nghiệp vụ \r\n- Có khả năng làm việc độc lập \r\n- Có khả năng chịu áp lực công việc cao Lợi thế nếu có: \r\n- Có kinh nghiệm về nghiệp vụ tài chính, ngân hàng là 1 lợi thế \r\n- Có thể đọc hiểu tài liệu tiếng Nhật là một lợi thế\r\n', N'● Min 14 tháng lương/năm (chưa tính các khoản thưởng khác trong quá trình làm việc\r\nnhư thưởng KPI, thưởng dự án và thưởng theo doanh thu công ty).\r\n● Làm việc trong một môi trường chuyên nghiệp, năng động, thân thiện.\r\n● Khám sức khỏe định kỳ hàng năm và được đóng BHXH, BHYT, BHTN đầy đủ theo luật định.\r\n● Hưởng chế độ nghỉ mát, teambuilding, các hoạt động thể thao & giải trí phong phú\r\n(các clb bơi, yoga, kiếm đạo…).\r\n● Cơ hội thăng tiến, nâng cao thu nhập cho người có năng lực, tâm huyết và gắn bó.\r\n'),
(4, N'- Daily management of shift quality control team.\r\n- Provide assignments to quality control and make sure that people follow through.\r\n- Assist the Quality control Manager with handling incoming NCR’s.\r\n- Support Production and Purchasing with information and clear problem identification so that they can take steps to improve their operations/those of suppliers.\r\n- Continuous internal quality audits in production to deploy daily/weekly/monthly report.\r\n', N'Graduated from University in furniture, forestry or other engineering fields.\r\n2+ years of experience in furniture field is a must.\r\nAge : 27 - 35\r\nPrefer to have knowledge of Quality Control tools, 6 Sigma, Lean, …\r\nPrefer to have the training skill.\r\nStrong knowledge in woodworking processes, woodworking machinery and wood.\r\nHave a good training skill\r\nGood in written and spoken English\r\nAbility to read and interpret all internal/ external quality documents\r\nAbility to write routine reports and correspondence\r\nAbility to communicate effectively at all level internally within organization\r\n', N'Careful\r\nIntegrity\r\n'),
(5, N'- Receive and discuss requests from customers in Japan\r\n- Collaborate with project PM/PO to plan and monitor project progress\r\n- Report progress periodically to the PM/PO of the project, as well as management departments\r\n- Collaborate with the project team in analyzing the project\"s business documents, verifying requirements, as well as participating in system analysis and design\r\n\r\n- Act as a bridge to convey issues, as well as project proposals between customers and project teams\r\n\r\n- Coordinate with the project comtor to ensure accurate and fast communication of information to the parties\r\n\r\n- Participate in ensuring the quality of the system\"s output products before delivering to customers', N'- At least 3 years of experience in software development, especially PHP applications\r\n- Experience in integrating AWS for system development\r\n- Experience in project team management, or have held similar positions such as Team Leader, Project Leader...\r\n- Have good communication skill\r\n\r\n- Able to analyze and solve project problems\r\n\r\n- Understanding Agile, and how to operate in the project\r\n\r\n- Familiar with project management tools, like Jira \r\n\r\n- Experience in developing Video Conferencing system is a plus', N'- Exciting Projects (MedTech, HRTech, Video Streaming, Drone, FinTech, FoodTech) with Great Team. Working with Agile Scrum model\r\n- Onsite opportunities: short-term and long-term assignments in Japan\r\n- Employee Career Paths\r\n- Premium health insurance\r\n- 12 days off allowance per year\r\n- Flexible working time (40h/week)\r\n- Highly professional equipment (own laptop, adjustable desks, macbook pro…)\r\n- Free annual medical examination (once a year)\r\n- Annual company trips (once a year) & Annual Party\r\n- Monthly team building\r\n- 13th month salary. Review salary 2 times per year\r\n- Full salary insurance\r\n- Allowances: Japanese, lunch, travel, birthday, wedding, funeral, sickness…\r\n- Free in-house entertainment facilities (Golf, Foosball, Dart) and out-house company-funded sport clubs'),
(6, N'Nghiên cứu cải tiến sản phẩm hiện có và phát triển sản phẩm mới.\r\nQuản lý các việc liên quan đến công việc nghiên cứu và phát triển sản phẩm\r\nTriển khai các dự án nghiên cứu thị trường, nhu cầu của khách hàng và đối thủ cạnh tranh\r\nPhối hợp với các bộ phận liên quan để đạt được mục tiêu của công ty\r\nTổ chức, đào tạo chuyên môn, kỹ năng, phát triển đội ngũ R&D\r\nXây dựng và quản lý quy trình làm việc, hệ thông tài liệu', N'Tốt nghiệp đại học, cao đẳng trở lên\r\nKinh nghiệm từ 2 năm trở lên\r\nƯu tiên ứng viên có kinh nghiệm trong mảng giáo dục\r\nCó khả năng xây dựng và phát triển mối quan hệ\r\nCó kỹ năng đào tạo và thuyết trình ', N'- Lương cứng 15 - 20 triệu + Phụ cấp lên đến 1,5 triệu/tháng + thưởng. \r\n- Thu nhập từ 20 - 25 triệu/tháng\r\n- Review lương 3 tháng/lần hoặc theo hiệu quả công việc nếu làm tốt\r\n- Tháng lương thứ 13, thưởng vào các dịp Lễ, Tết, sinh nhật Công ty, sinh nhật Cá nhân..\r\n- Đóng BHXH, BHTN, BHYT sau thời gian thử việc.\r\n- Được Hỗ trợ ăn trưa tại căng tin của công ty.\r\n- Du lịch, teambuiding hàng năm.\r\n- 12 ngày phép/năm\r\n- Được hỗ trợ 100% chi phí các khóa học tại công ty; 85% chi phí khóa học bên ngoài\r\n- Các chế độ khác theo quy định của công ty.'),
(7, N'+ Tốt nghiệp trung cấp trở lên các ngành điện, cơ khí, cơ điện tử, …\r\n+ Am hiểu về nguyên lý hoạt động của máy móc trang thiết bị, sửa chữa, vận hành, quản lý máy móc thiết bị công nghiệp liên quan dến lĩnh vực Sơn, chống thấm và thi công các công trình của Công ty tại các dự án.\r\n+ Có kiến thức về 5S và an toàn lao động.\r\n+ Có thể ra site thi công, hoặc theo các máy móc phân bổ các site nếu có nhiều dự án.', N'- Sức khỏe tốt, tuổi đời từ 25 – 40 tuổi, Nam giới.\r\n- Nhanh nhẹn, hoạt bát, nghiêm túc trong công việc. Ý thức và trách nhiệm trong công việc.\r\n- Gắn bó lâu dài với Công ty, chấp nhận điều động, phân công công tác của Công ty.\r\n- Kinh nghiệm từ 2 năm trở lên.', N'· Mức lương được hưởng: có thể thỏa thuận theo năng lực\r\n· Thưởng Lễ, tết theo quy định…\r\n· Được phụ cấp xăng xe công tác & cơm trưa.\r\n· Chế độ BHXH, BHTN, BHYT theo quy định hiện hành của Nhà nước.\r\n· Làm việc trong môi trường trẻ trung năng động, sáng tạo, cơ hội thăng tiến rõ ràng.\r\n· Được cung cấp đầy đủ trang thiết bị phục vụ cho công việc.'),
(8, N'- Tham gia thiết kế và phát triển ứng dụng trên các nền tảng Android, IOS.\r\n- Thiết kế Icon, Screenshot, Banner, Cover… cho các chiến lược Marketing của Công ty\r\n- Phối hợp với các bộ phận Content, Marketing, Developer để tạo ra sản phẩm chất lượng cao phục vụ người dùng', N'- Có ít nhất 1 năm kinh nghiệm thiết kế mobile app hoặc đã tham gia nhiều dự án với vai trò designer\r\n- Có kiến thức, am hiểu về UX Research, User Story, UI Trend, Prototype\r\n- Nắm vững: Màu sắc, Layout, Typography và Hệ thống lưới\r\n- Hiểu đường nét, mảng, hình khối, ánh sáng trong thiết kế.\r\n- Sử dụng Thành Thạo các công cụ thiết kế PTS, AI, Sketch, Figma\r\n- Có kiến thức cơ bản về CSS, HTML\r\n- Đam mê thiết kế, giàu ý tưởng sáng tạo, thẩm mỹ tốt, có khả năng phát triển những ý tưởng tạo hình độc đáo.', N'- Xét tăng lương 2 lần/ năm.\r\n- Thưởng dự án theo doanh thu, thưởng Tết, cuối năm và các phúc lợi hấp dẫn.\r\n- Làm việc trong môi trường chuyên nghiệp cùng các đồng nghiệp giỏi, có nhiều kinh nghiệm.\r\n- Chế độ bảo hiểm xã hội, bảo hiểm y tế, bảo hiểm thất nghiệp,...theo quy định của nhà nước\r\n- Tham gia các hoạt động vui chơi giải trí, du lịch, team building, ăn uống cùng các thành viên trong công ty.\r\n- Lộ trình phát triển bản thân rõ ràng, tham gia đào tạo chuyên môn để nâng cao năng lực\r\n- Được trang bị thiết bị làm việc: Máy tính, thiết bị văn phòng...'),
(9, N'- Chịu trách nhiệm thiết kế UI/UX cho các sản phẩm của công ty với phong cách đồng nhất nhằm thể hiện giá trị, tinh thần, cá tính và thương hiệu của công ty.\r\n- Thiết kế toàn bộ Layout, Page cho các website về lĩnh vực thương mại điện tử.\r\n- Thiết kế hình ảnh cho các bài viết trên blog, website công ty, các trang mạng xã hội và trang vệ tinh.\r\n- Thiết kế banner cho các chiến dịch marketing.\r\n- Thiết kế portfolio phục vụ cho quảng bá, thu hút khách hàng.\r\n- Xây dựng video giới thiệu và hướng dẫn sử dụng sản phẩm.\r\n- Nghiên cứu và đưa ra các ý tưởng thiết kế mới hỗ trợ cho team Marketing, team Product. Phối hợp với bộ phận kỹ thuật để thiết kế sản phẩm, đưa ra các giao diện landing page tương thích với các màn hình máy tính và di động và các trình duyệt Web.', N'- Có ít nhất 2 năm kinh nghiệm thiết kế UI/UX trên Web và App. Có kinh nghiệm thiết kế với các trang thương mại điện tử là một lợi thế.\r\n- Sử dụng thành thạo Photoshop, Illustrator. Có kỹ năng thiết kế theo các xu hướng thiết kế như: Flat design, Material design, vv.\r\n- Có kinh nghiệm chỉnh sửa Video với ứng dụng After Effect và Adobe Premiere.\r\n- Có khả năng nghiên cứu các tài liệu Tiếng Anh chuyên ngành. Sử dụng thành thạo Tiếng Anh là một lợi thế.\r\n- Có tinh thần cầu tiến, ham học hỏi, chia sẻ, trao dồi kiến thức.\r\n- Có trách nhiệm cao với công việc.\r\n- Có khả năng quản lý tốt công việc, biết ưu tiên thứ tự, mức độ công việc.', N'- Xét tăng lương 1 năm/lần.\r\n- Lương tháng thứ 13.\r\n- Thưởng theo dự án, doanh thu, thưởng Tết và thưởng các ngày lễ.\r\n- Hưởng 12 ngày nghỉ phép hàng năm theo luật lao động.\r\n- Phỏng vấn online mùa dịch\r\n- Hỗ trợ ăn trưa, gửi xe.\r\n- Hỗ trợ máy tính và các trang thiết bị khác phục vụ công việc.\r\n- Làm việc 5 buổi / tuần, nghỉ thứ bảy, chủ nhật và nghỉ tất cả các ngày lễ theo quy định của Nhà nước.\r\n- Đầy đủ các chế độ, quyền lợi như BHXH, BHYT, khám sức khỏe định kỳ theo luật lao động quy định.\r\n- Được làm việc trong môi trường những người trẻ năng động, sáng tạo, ham học hỏi, chia sẻ và hỗ trợ bạn phát huy tối đa khả năng của mình.\r\n- Được tham gia các khóa đào tạo và phát triển cá nhân cả về chuyên môn, kỹ năng tiếng Anh và các kỹ năng mềm.'),
(10, N'● Đánh giá An toàn Thông tin (ATTT) toàn bộ hệ thống : \r\n      ○ Các hệ thống hiện hữu (live): đánh giá định kỳ, rà quét lỗ hổng \r\n      ○ Các hệ thống, tính năng chuẩn bị đưa vào hoạt động: đánh giá source code và rà quét lỗ hổng \r\n      ○ Đánh giá hệ thống mạng, database và các dịch vụ khác (IoT, thiết bị khác,…) \r\n      ○ Đánh giá ứng dụng mobile \r\n● Tham gia tư vấn vào quá trình fix lỗi và phát triển, tích hợp các tính năng, hệ thống mới của VCCorp \r\n● Phối hợp xử lý các lỗ hổng, sự cố ATTT \r\n● Nghiên cứu lỗ hổng, theo dõi các kỹ thuật, lỗ hổng mới có thể ảnh hướng tới VCCorp \r\n● Đào tạo nhân viên về các tiêu chuẩn, quy định ATTT \r\n● Lên kế hoạch đánh giá định kỳ các hệ thống CNTT ', N'● Tốt nghiệp đại học chuyên ngành An toàn thông tin, Công nghệ thông tin hoặc các chuyên ngành có liên quan \r\n● Có kiến thức tốt về Pentest theo chuẩn OWASP \r\n● Có khả năng truy tìm, khai thác và tấn công các lỗ hổng phần mềm \r\n● Có hiểu biết về các loại tấn công vào hạ tầng, dịch vụ \r\n● Có kinh nghiệm sử dụng các công cụ như Metasploit, Kali linux, Burp Suite \r\n● Thành thạo ít nhất 1 ngôn ngữ lập trình như Java/C++/Python', N'● Thưởng đạt, vượt chỉ tiêu KPI/Thưởng năng suất: Xét thưởng áp dụng khi nhân viên đạt chỉ tiêu KPI cá nhân và hoặc tùy thuộc vào tình hình kết quả kinh doanh của công ty. \r\n● Thưởng tháng lương 13 (thưởng Tết Âm Lịch): Xét thưởng định kỳ cuối năm căn cứ theo quy định của công ty và tùy thuộc vào tình hình kết quả kinh doanh của công ty. \r\n● Thưởng thâm niên: Xét thưởng định kỳ cuối năm căn cứ theo thâm niên làm việc của nhân viên theo quy định của công ty và hoặc tùy thuộc vào tình hình kết quả kinh doanh của công ty. \r\n● Thưởng Nóng, Thưởng thành tích vượt trội: Khi có thành tích xuất sắc và hoặc dự án thành công… \r\n● Thưởng vinh danh, tôn vinh: Bình chọn giải cá nhân/bộ phận xuất sắc cấp Công ty định kỳ hàng năm \r\n● Thưởng Tự Khoe cấp Bộ Phận: Khuyến khích CBNV, các bộ phận thi đua hoàn thành tốt các mục tiêu công việc, kích thích đổi mới, sáng tạo trong công việc; ghi nhận, động viên kịp thời các việc hay, sáng kiến hiệu quả của các các nhân, tập thể. Mức thưởng tự khoe, ');

GO
DROP TABLE IF EXISTS details_interview;
GO
CREATE TABLE details_interview (
  id int NOT NULL PRIMARY KEY IDENTITY(1,1),
  applicantId char(5) NOT NULL,
  vacancyId char(4) NOT NULL,
  interviewerId char(4) NOT NULL,
  time datetime NOT NULL,
  status char(25) NOT NULL DEFAULT 'Interview Scheduled'
);

GO
DROP TABLE IF EXISTS interviewer;
GO
CREATE TABLE interviewer (
  id char(4) NOT NULL PRIMARY KEY ,
  user_id int NOT NULL UNIQUE,
  department_id char(4) NOT NULL
) ;

GO
INSERT INTO interviewer (id, user_id, department_id) VALUES
('I001', 5, 'D001'),
('I002', 6, 'D002'),
('I003', 7, 'D003'),
('I004', 8, 'D004'),
('I005', 9, 'D005');

GO
DROP TABLE IF EXISTS type_user;
GO
CREATE TABLE type_user (
  id tinyint NOT NULL PRIMARY KEY ,
  type char(15) NOT NULL
) ;

GO
INSERT INTO type_user (id, type) VALUES
(1, 'HR'),
(2, 'Interviewer'),
(3, 'Applicant');

GO
DROP TABLE IF EXISTS [user];
GO
CREATE TABLE [user] (
  id int NOT NULL PRIMARY KEY IDENTITY(1,1),
  username varchar(255) NULL,
  password varchar(255) NULL CHECK(Len(password) >=5 ),
  name nvarchar(255) NOT NULL,
  email varchar(255) NOT NULL,
  type_userId tinyint NOT NULL DEFAULT 3
) ;

GO
SET IDENTITY_INSERT description OFF
GO
SET IDENTITY_INSERT [user] ON
GO
INSERT INTO [user] (id, username, password, name, email, type_userId) VALUES
(1, 'admin', '21232f297a57a5a743894a0e4a801fc3', 'Admin', 'admin@gmail.com', 1),
(2, 'thanos', '2c6c2b29fa7a56495478e7198ce1d1ef', 'Ngọc', 'ngoc.bt.a1908g08@aptechlearning.edu.vn', 1),
(3, 'user13', 'e10adc3949ba59abbe56e057f20f883e', 'Huy', 'huy.nd.a1908g09@aptechlearning.edu.vn', 1),
(4, 'user1405', '4bd9c19f5149ddd010f04a3808ecbf92', 'Tùng', 'tung.th.a1908g26@aptechlearning.edu.vn', 1),
(5, 'tusngu', 'af88a0ae641589b908fa8b31f0fcf6e1', 'Tú', 'staff01@gmail.com', 2),
(6, 'tenz', '51d25b4ae8ce20ad29b25cf4f2e23203', 'TenZ', 'staff02@gmail.com', 2),
(7, 'conganh', '690a8d66c5fb67b23f45e286ddb57ead', 'Công Minh', 'staff03@gmail.com', 2),
(8, 'congminh', '58c6031a76a5d8af719ee005546d972f', 'Công Anh', 'staff04@gmail.com', 2),
(9, 'projectkhoqua', 'd27812fb71ef1469e08e5b19fed2e977', 'Bomman', 'staff05@gmail.com', 2),
(10, 'user01', '28a4565a4953cb4e7e23317ba0504f4c', 'man01', 'anhquannguyen2004@gmail.com', 3),
(11, 'user02', '89cdd3dd30e3a9445922222b60a0c1f6', 'man02', 'man02@gmail.com', 3),
(12, 'user03', '4775a717f7d414378e42638b53906f9b', 'man03', 'man03@gmail.com', 3),
(13, 'user04', 'f716f0e34a23a69592a491012ba73a19', 'man04', 'man04@gmail.com', 3),
(14, 'user05', 'e8c0d8856e088182604708842b653007', 'man05', 'man05@gmail.com', 3),
(15, 'user06', 'e20ad3c26ed64f0b9fab03693d3d4b77', 'man06', 'man06@gmail.com', 3),
(16, 'user07', 'bef619a537a0345100deddff98e7e779', 'man07', 'man07@gmail.com', 3),
(17, 'user08', '6de5aa7c0ecc37c324d2f0073bfc47d5', 'man08', 'man08@gmail.com', 3),
(18, 'leborn', '70f822a97769cc0a39ea5b28e28e7e41', 'King', 'lebron@gmail.com', 3);

GO
DROP TABLE IF EXISTS vacancy;
GO
CREATE TABLE vacancy (
  id char(4) NOT NULL PRIMARY KEY ,
  title nvarchar(100) NOT NULL,
  endAt DATETIME NOT NULL ,
  status varchar(25) NOT NULL DEFAULT 'Open',
  salary varchar(50) NOT NULL,
  required int NOT NULL,
  descriptionId int NOT NULL UNIQUE,
  departmentId char(4) NOT NULL ,
  applicants_Id varchar(250) DEFAULT NULL
) ;

GO
INSERT INTO vacancy (id, title, endAt, status, salary, required, descriptionId, departmentId, applicants_Id) VALUES
('V001', 'Java developer', '2021-11-30 05:00:00', 'Open', '10,000,000-50,000,000 VND', 5, 1, 'D002', NULL),
('V002', 'Python developer', '2021-11-30 05:00:00', 'Open', '12,000,000-20,000,000 VND', 5, 2, 'D002', NULL),
('V003', 'C++ developer', '2021-11-15 05:00:00', 'Open', '15,000,000-20,000,000 VND', 4, 3, 'D005', NULL),
('V004', 'Supervisor', '2021-11-14 05:00:00', 'Open', '9,000,000-15,000,000 VND', 1, 4, 'D003', NULL),
('V005', 'Project leader', '2021-11-13 05:00:00', 'Open', '25,000,000-40,000,000 VND', 3, 5, 'D005', NULL),
('V006', 'Product developer', '2021-11-09 11:30:00', 'Open', '12,000,000-22,000,000 VND', 5, 6, 'D005', NULL),
('V007', 'equipment maintenance staff', '2021-11-12 05:00:00', 'Open', '10,000,000-15,000,000  VND', 4, 7, 'D002', NULL),
('V008', 'UX/UI Designer', '2021-11-09 11:30:00', 'Open', '12,000,000-30,000,000 VND', 5, 8, 'D001', NULL),
('V009', 'Web Designer', '2021-11-09 11:30:00', 'Open', '16,000,000-23,000,000 VND', 7, 9, 'D001', NULL),
('V010', 'Security specialist', '2021-11-09 11:30:00', 'Suspended', '20,000,000-40,000,000 VND', 5, 10, 'D004', NULL);

GO
ALTER TABLE applicant
ADD CONSTRAINT FK_UserApplicant 
FOREIGN KEY ([user_id]) REFERENCES [user](id);

GO
ALTER TABLE details_interview
ADD CONSTRAINT FK_DetailsApplicant 
FOREIGN KEY (applicantId) REFERENCES applicant(id);

GO
ALTER TABLE details_interview
ADD CONSTRAINT FK_DetailsVacancy 
FOREIGN KEY (vacancyId) REFERENCES vacancy(id);

GO
ALTER TABLE details_interview
ADD CONSTRAINT FK_DetailsInterviewer
FOREIGN KEY (interviewerId) REFERENCES interviewer(id);

GO
ALTER TABLE interviewer
ADD CONSTRAINT FK_interviewer_user
FOREIGN KEY (user_id)REFERENCES [user](id);

GO
ALTER TABLE interviewer
ADD CONSTRAINT FK_DepartmentInterviewer
FOREIGN KEY (department_id) REFERENCES department(id);

GO
ALTER TABLE [user]
ADD CONSTRAINT FK_Usertype 
FOREIGN KEY (type_userId) REFERENCES type_user(id);

GO
ALTER TABLE vacancy
ADD CONSTRAINT FK_DepartmentVacancy 
FOREIGN KEY (departmentId) REFERENCES department(id);

GO
ALTER TABLE vacancy
ADD CONSTRAINT FK_DescribeVacancy 
FOREIGN KEY (descriptionId) REFERENCES description(id);