-- 插入30条 Project 数据
INSERT INTO Projects (Title, Description, ProjectType, StartDate, EndDate, Status, LeaderId, InstructorId, MaxMembers, CurrentMembers)
VALUES
    -- 大创项目
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

    -- 竞赛项目
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

    -- 论文项目
    ('论文项目 1', '论文项目描述 1', '论文项目', '2024-01-01', '2024-12-31', '进行中', 33, 3, 3, 2),
    ('论文项目 2', '论文项目描述 2', '论文项目', '2024-02-01', '2024-12-31', '进行中', 34, 4, 4, 3),
    ('论文项目 3', '论文项目描述 3', '论文项目', '2024-03-01', '2024-12-31', '已完成', 35, 5, 2, 2),
    ('论文项目 4', '论文项目描述 4', '论文项目', '2024-04-01', '2024-12-31', '已完成', 36, 6, 3, 3),
    ('论文项目 5', '论文项目描述 5', '论文项目', '2024-05-01', '2024-12-31', '进行中', 37, 7, 5, 4),

    -- 科研项目
    ('科研项目 1', '科研项目描述 1', '科研项目', '2024-01-01', '2024-12-31', '进行中', 38, 8, 6, 3),
    ('科研项目 2', '科研项目描述 2', '科研项目', '2024-02-01', '2024-12-31', '进行中', 39, 9, 8, 6),
    ('科研项目 3', '科研项目描述 3', '科研项目', '2024-03-01', '2024-12-31', '进行中', 40, 10, 4, 3),
    ('科研项目 4', '科研项目描述 4', '科研项目', '2024-04-01', '2024-12-31', '已完成', 41, 11, 5, 5),
    ('科研项目 5', '科研项目描述 5', '科研项目', '2024-05-01', '2024-12-31', '进行中', 42, 12, 7, 5);
