PRAGMA foreign_keys = OFF;

DELETE FROM InAppMessages;
DELETE FROM ProjectMembers;
DELETE FROM Projects;
DELETE FROM JoinRequests;
DELETE FROM Users;

DELETE FROM sqlite_sequence;

PRAGMA foreign_keys = ON;

INSERT INTO Users (Name, Email, Role)
VALUES
    ('Alice', 'alice@example.com', 'Admin'),
    ('Bob', 'bob@example.com', 'Admin'),
    
    ('Charlie', 'charlie@example.com', 'Teacher'),
    ('David', 'david@example.com', 'Teacher'),
    ('Eve', 'eve@example.com', 'Teacher'),
    ('Frank', 'frank@example.com', 'Teacher'),
    ('Grace', 'grace@example.com', 'Teacher'),
    ('Hannah', 'hannah@example.com', 'Teacher'),
    ('Ivy', 'ivy@example.com', 'Teacher'),
    ('Jack', 'jack@example.com', 'Teacher'),
    ('Kathy', 'kathy@example.com', 'Teacher'),

    ('Louis', 'louis@example.com', 'Student'),
    ('Mona', 'mona@example.com', 'Student'),
    ('Nina', 'nina@example.com', 'Student'),
    ('Oscar', 'oscar@example.com', 'Student'),
    ('Paul', 'paul@example.com', 'Student'),
    ('Quincy', 'quincy@example.com', 'Student'),
    ('Rachel', 'rachel@example.com', 'Student'),
    ('Sam', 'sam@example.com', 'Student'),
    ('Tom', 'tom@example.com', 'Student'),
    ('Uma', 'uma@example.com', 'Student'),
    ('Vera', 'vera@example.com', 'Student'),
    ('Wendy', 'wendy@example.com', 'Student'),
    ('Xander', 'xander@example.com', 'Student'),
    ('Yara', 'yara@example.com', 'Student'),
    ('Zane', 'zane@example.com', 'Student'),
    
    ('Aiden', 'aiden@example.com', 'Student'),
    ('Bella', 'bella@example.com', 'Student'),
    ('Cleo', 'cleo@example.com', 'Student'),
    ('Derek', 'derek@example.com', 'Student'),
    ('Eva', 'eva@example.com', 'Student'),
    ('Felix', 'felix@example.com', 'Student'),
    ('Grace', 'grace@example.com', 'Student'),
    ('Holly', 'holly@example.com', 'Student'),
    ('Ian', 'ian@example.com', 'Student'),
    ('Judy', 'judy@example.com', 'Student'),
    ('Kurt', 'kurt@example.com', 'Student'),
    ('Lilly', 'lilly@example.com', 'Student'),
    ('Mike', 'mike@example.com', 'Student'),
    ('Nadia', 'nadia@example.com', 'Student'),
    
    ('Olivia', 'olivia@example.com', 'Student'),
    ('Perry', 'perry@example.com', 'Student'),
    ('Quinn', 'quinn@example.com', 'Student'),
    ('Rita', 'rita@example.com', 'Student'),
    ('Steve', 'steve@example.com', 'Student'),
    ('Tina', 'tina@example.com', 'Student'),
    ('Ursula', 'ursula@example.com', 'Student'),
    ('Vince', 'vince@example.com', 'Student'),
    ('Walter', 'walter@example.com', 'Student'),
    ('Xena', 'xena@example.com', 'Student'),
    ('Yvonne', 'yvonne@example.com', 'Student'),
    ('Zara', 'zara@example.com', 'Student');

INSERT INTO Projects (Title, Description, ProjectType, StartDate, EndDate, Status, LeaderId, InstructorId, MaxMembers, CurrentMembers)
VALUES
    ('大创项目 1', '大创项目描述 1', '大创项目', '2024-01-01', '2024-06-30', '进行中', 13, 3, 5, 2),
    ('大创项目 2', '大创项目描述 2', '大创项目', '2024-02-01', '2024-07-31', '已完成', 14, 4, 6, 5),
    ('大创项目 3', '大创项目描述 3', '大创项目', '2024-03-01', '2024-08-30', '已完成', 15, 5, 4, 4),
    ('大创项目 4', '大创项目描述 4', '大创项目', '2024-04-01', '2024-09-30', '进行中', 16, 6, 7, 3),
    ('大创项目 5', '大创项目描述 5', '大创项目', '2024-05-01', '2024-10-31', '已取消', 17, 7, 5, 0),
    ('大创项目 6', '大创项目描述 6', '大创项目', '2024-06-01', '2024-11-30', '进行中', 18, 8, 6, 2),
    ('大创项目 7', '大创项目描述 7', '大创项目', '2024-07-01', '2024-12-31', '进行中', 19, 9, 8, 4),
    ('大创项目 8', '大创项目描述 8', '大创项目', '2024-08-01', '2025-01-31', '进行中', 20, 10, 9, 5),
    ('大创项目 9', '大创项目描述 9', '大创项目', '2024-09-01', '2025-02-28', '进行中', 21, 11, 10, 6),
    ('大创项目 10', '大创项目描述 10', '大创项目', '2024-10-01', '2025-03-31', '已完成', 22, 12, 6, 5),

    ('竞赛项目 1', '竞赛项目描述 1', '竞赛项目', '2024-01-15', '2024-07-15', '进行中', 23, 3, 10, 8),
    ('竞赛项目 2', '竞赛项目描述 2', '竞赛项目', '2024-02-15', '2024-08-15', '进行中', 24, 4, 8, 7),
    ('竞赛项目 3', '竞赛项目描述 3', '竞赛项目', '2024-03-15', '2024-09-15', '已完成', 25, 5, 6, 6),
    ('竞赛项目 4', '竞赛项目描述 4', '竞赛项目', '2024-04-15', '2024-10-15', '进行中', 26, 6, 9, 5),
    ('竞赛项目 5', '竞赛项目描述 5', '竞赛项目', '2024-05-15', '2024-11-15', '已取消', 27, 7, 7, 0),
    ('竞赛项目 6', '竞赛项目描述 6', '竞赛项目', '2024-06-15', '2024-12-15', '进行中', 28, 8, 6, 4),
    ('竞赛项目 7', '竞赛项目描述 7', '竞赛项目', '2024-07-15', '2025-01-15', '进行中', 29, 9, 5, 3),
    ('竞赛项目 8', '竞赛项目描述 8', '竞赛项目', '2024-08-15', '2025-02-15', '已完成', 30, 10, 4, 4),
    ('竞赛项目 9', '竞赛项目描述 9', '竞赛项目', '2024-09-15', '2025-03-15', '进行中', 31, 11, 8, 6),
    ('竞赛项目 10', '竞赛项目描述 10', '竞赛项目', '2024-10-15', '2025-04-15', '进行中', 32, 12, 7, 5),

    ('论文项目 1', '论文项目描述 1', '论文项目', '2024-01-01', '2024-12-31', '进行中', 33, 3, 3, 2),
    ('论文项目 2', '论文项目描述 2', '论文项目', '2024-02-01', '2024-12-31', '进行中', 34, 4, 4, 3),
    ('论文项目 3', '论文项目描述 3', '论文项目', '2024-03-01', '2024-12-31', '已完成', 35, 5, 2, 2),
    ('论文项目 4', '论文项目描述 4', '论文项目', '2024-04-01', '2024-12-31', '已完成', 36, 6, 3, 3),
    ('论文项目 5', '论文项目描述 5', '论文项目', '2024-05-01', '2024-12-31', '进行中', 37, 7, 5, 4),

    ('科研项目 1', '科研项目描述 1', '科研项目', '2024-01-01', '2024-12-31', '进行中', 38, 8, 6, 3),
    ('科研项目 2', '科研项目描述 2', '科研项目', '2024-02-01', '2024-12-31', '进行中', 39, 9, 8, 6),
    ('科研项目 3', '科研项目描述 3', '科研项目', '2024-03-01', '2024-12-31', '进行中', 40, 10, 4, 3),
    ('科研项目 4', '科研项目描述 4', '科研项目', '2024-04-01', '2024-12-31', '已完成', 41, 11, 5, 5),
    ('科研项目 5', '科研项目描述 5', '科研项目', '2024-05-01', '2024-12-31', '进行中', 42, 12, 7, 5);

INSERT INTO InAppMessages (SenderId, RecipientId, Type, Subject, Content, SentAt, IsRead, RelatedProjectId)
VALUES
    (2, 1, 1, '系统通知', '欢迎加入平台！', '2024-12-01 10:00:00', 0, NULL),
    (3, 1, 2, '项目更新', '您负责的项目已发布新任务。', '2024-12-02 11:00:00', 1, 5),
    (4, 1, 3, '团队消息', '请查看团队讨论区的新话题。', '2024-12-03 12:00:00', 0, 6),
    (5, 1, 4, '任务提醒', '您有一个任务需要完成，截止日期为 2024-12-10。', '2024-12-04 13:00:00', 0, 7),
    (6, 1, 5, '反馈请求', '请填写本周的项目进展报告。', '2024-12-05 14:00:00', 1, 8),
    (7, 1, 1, '系统通知', '欢迎加入平台！', '2024-12-01 10:00:00', 0, NULL),
    (8, 1, 2, '项目更新', '您负责的项目已发布新任务。', '2024-12-02 11:00:00', 1, 5),
    (9, 1, 3, '团队消息', '请查看团队讨论区的新话题。', '2024-12-03 12:00:00', 0, 6),
    (10, 1, 4, '任务提醒', '您有一个任务需要完成，截止日期为 2024-12-10。', '2024-12-04 13:00:00', 0, 7),
    (11, 1, 5, '反馈请求', '请填写本周的项目进展报告。', '2024-12-05 14:00:00', 1, 8),
    (12, 1, 1, '系统通知', '欢迎加入平台！', '2024-12-01 10:00:00', 0, NULL),
    (13, 1, 2, '项目更新', '您负责的项目已发布新任务。', '2024-12-02 11:00:00', 1, 5),
    (14, 1, 3, '团队消息', '请查看团队讨论区的新话题。', '2024-12-03 12:00:00', 0, 6),
    (15, 1, 4, '任务提醒', '您有一个任务需要完成，截止日期为 2024-12-10。', '2024-12-04 13:00:00', 0, 7),
    (16, 1, 5, '反馈请求', '请填写本周的项目进展报告。', '2024-12-05 14:00:00', 1, 8),

    (1, 2, 2, '系统通知', '您的账户信息已更新。', '2024-12-01 15:00:00', 1, NULL),
    (3, 4, 3, '任务完成', '恭喜您完成了最新的项目任务。', '2024-12-02 16:00:00', 0, 9),
    (5, 6, 4, '会议通知', '项目组将在2024-12-07召开进度会议，请准时参加。', '2024-12-03 17:00:00', 0, 10),
    (2, 7, 5, '奖励通知', '您因表现优秀获得了本月的团队奖励。', '2024-12-04 18:00:00', 1, NULL),
    (4, 8, 1, '项目建议', '请提出关于新项目的建议。', '2024-12-05 19:00:00', 0, NULL);

INSERT INTO JoinRequests (ProjectId, ApplicantId, Type, Reason, SelfIntroduction, Status, SubmittedAt, ReviewedBy, ReviewedAt)
VALUES
    (1, 2, 'Leader', '希望带领团队完成挑战。', '您好，我有丰富的项目经验，非常适合担任Leader角色。', 'Pending', '2024-12-01 10:00:00', NULL, NULL),
    (1, 3, 'Member', '希望参与此项目学习新知识。', '我熟悉相关领域技术，期待加入团队。', 'Approved', '2024-12-01 11:00:00', 5, '2024-12-02 09:00:00'),
    (1, 4, 'Member', '对项目主题非常感兴趣。', '我是一名热爱学习的学生，期待为团队贡献力量。', 'Pending', '2024-12-02 12:00:00', NULL, NULL),
    (1, 5, 'Member', '有相关技能，希望贡献力量。', '擅长软件开发，期待为项目提供支持。', 'Rejected', '2024-12-03 13:00:00', 6, '2024-12-04 14:00:00'),
    (1, 6, 'Leader', '愿意承担领导职责。', '我曾多次担任项目负责人，有信心带领团队。', 'Pending', '2024-12-04 15:00:00', NULL, NULL),
    (1, 22, 'Leader', '希望带领团队完成挑战。', '您好，我有丰富的项目经验，非常适合担任Leader角色。', 'Pending', '2024-12-01 10:00:00', NULL, NULL),
    (1, 23, 'Member', '希望参与此项目学习新知识。', '我熟悉相关领域技术，期待加入团队。', 'Approved', '2024-12-01 11:00:00', 5, '2024-12-02 09:00:00'),
    (1, 24, 'Member', '对项目主题非常感兴趣。', '我是一名热爱学习的学生，期待为团队贡献力量。', 'Pending', '2024-12-02 12:00:00', NULL, NULL),
    (1, 25, 'Member', '有相关技能，希望贡献力量。', '擅长软件开发，期待为项目提供支持。', 'Rejected', '2024-12-03 13:00:00', 6, '2024-12-04 14:00:00'),
    (1, 26, 'Leader', '愿意承担领导职责。', '我曾多次担任项目负责人，有信心带领团队。', 'Pending', '2024-12-04 15:00:00', NULL, NULL),
    (1, 12, 'Leader', '希望带领团队完成挑战。', '您好，我有丰富的项目经验，非常适合担任Leader角色。', 'Pending', '2024-12-01 10:00:00', NULL, NULL),
    (1, 13, 'Member', '希望参与此项目学习新知识。', '我熟悉相关领域技术，期待加入团队。', 'Approved', '2024-12-01 11:00:00', 5, '2024-12-02 09:00:00'),
    (1, 14, 'Member', '对项目主题非常感兴趣。', '我是一名热爱学习的学生，期待为团队贡献力量。', 'Pending', '2024-12-02 12:00:00', NULL, NULL),
    (1, 15, 'Member', '有相关技能，希望贡献力量。', '擅长软件开发，期待为项目提供支持。', 'Rejected', '2024-12-03 13:00:00', 6, '2024-12-04 14:00:00'),
    (1, 16, 'Leader', '愿意承担领导职责。', '我曾多次担任项目负责人，有信心带领团队。', 'Pending', '2024-12-04 15:00:00', NULL, NULL),

    (2, 7, 'Member', '项目方向与我的研究一致。', '热爱创新，期待加入团队并贡献自己的力量。', 'Approved', '2024-12-01 16:00:00', 8, '2024-12-02 10:00:00'),
    (3, 8, 'Leader', '擅长团队管理，愿意担任Leader。', '我有相关领域的深入研究经验。', 'Pending', '2024-12-03 17:00:00', NULL, NULL),
    (4, 9, 'Member', '对项目主题非常感兴趣。', '熟悉相关领域的知识和工具，希望加入团队。', 'Pending', '2024-12-04 18:00:00', NULL, NULL),
    (5, 10, 'Member', '希望与优秀的团队一起合作。', '我擅长数据分析，能够为项目提供支持。', 'Approved', '2024-12-05 19:00:00', 7, '2024-12-06 09:00:00'),
    (6, 11, 'Leader', '想带领团队完成研究目标。', '丰富的科研经历，使我有能力胜任Leader职位。', 'Rejected', '2024-12-06 20:00:00', 8, '2024-12-07 10:00:00');

INSERT INTO ProjectMembers (ProjectId, UserId, Role, JoinDate, Status)
VALUES
    (1, 3, 'Instructor', '2024-01-01', 'Active'),
    (1, 13, 'ProjectLeader', '2024-01-01', 'Active'),
    (1, 14, 'Member', '2024-01-02', 'Active'),
    (1, 15, 'Member', '2024-01-03', 'Active'),
    (1, 16, 'Member', '2024-01-04', 'Active'),

    (2, 4, 'Instructor', '2024-02-01', 'Active'),
    (2, 14, 'ProjectLeader', '2024-02-01', 'Active'),
    (2, 15, 'Member', '2024-02-02', 'Active'),
    (2, 16, 'Member', '2024-02-03', 'Active'),
    (2, 17, 'Member', '2024-02-04', 'Active'),

    (3, 5, 'Instructor', '2024-03-01', 'Active'),
    (3, 15, 'ProjectLeader', '2024-03-01', 'Active'),
    (3, 16, 'Member', '2024-03-02', 'Active'),
    (3, 17, 'Member', '2024-03-03', 'Active'),
    (3, 18, 'Member', '2024-03-04', 'Active'),

    (4, 6, 'Instructor', '2024-04-01', 'Active'),
    (4, 16, 'ProjectLeader', '2024-04-01', 'Active'),
    (4, 17, 'Member', '2024-04-02', 'Active'),
    (4, 18, 'Member', '2024-04-03', 'Active'),
    (4, 19, 'Member', '2024-04-04', 'Active'),

    (5, 7, 'Instructor', '2024-05-01', 'Active'),
    (5, 17, 'ProjectLeader', '2024-05-01', 'Active'),
    (5, 18, 'Member', '2024-05-02', 'Active'),
    (5, 19, 'Member', '2024-05-03', 'Active'),
    (5, 20, 'Member', '2024-05-04', 'Active'),

    (6, 8, 'Instructor', '2024-06-01', 'Active'),
    (6, 18, 'ProjectLeader', '2024-06-01', 'Active'),
    (6, 19, 'Member', '2024-06-02', 'Active'),
    (6, 20, 'Member', '2024-06-03', 'Active'),
    (6, 21, 'Member', '2024-06-04', 'Active'),

    (7, 9, 'Instructor', '2024-07-01', 'Active'),
    (7, 19, 'ProjectLeader', '2024-07-01', 'Active'),
    (7, 20, 'Member', '2024-07-02', 'Active'),
    (7, 21, 'Member', '2024-07-03', 'Active'),
    (7, 22, 'Member', '2024-07-04', 'Active'),

    (8, 10, 'Instructor', '2024-08-01', 'Active'),
    (8, 20, 'ProjectLeader', '2024-08-01', 'Active'),
    (8, 21, 'Member', '2024-08-02', 'Active'),
    (8, 22, 'Member', '2024-08-03', 'Active'),
    (8, 23, 'Member', '2024-08-04', 'Active'),

    (9, 11, 'Instructor', '2024-09-01', 'Active'),
    (9, 21, 'ProjectLeader', '2024-09-01', 'Active'),
    (9, 22, 'Member', '2024-09-02', 'Active'),
    (9, 23, 'Member', '2024-09-03', 'Active'),
    (9, 24, 'Member', '2024-09-04', 'Active'),

    (10, 12, 'Instructor', '2024-10-01', 'Active'),
    (10, 22, 'ProjectLeader', '2024-10-01', 'Active'),
    (10, 23, 'Member', '2024-10-02', 'Active'),
    (10, 24, 'Member', '2024-10-03', 'Active'),
    (10, 25, 'Member', '2024-10-04', 'Active');