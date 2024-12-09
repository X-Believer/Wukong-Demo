-- 插入 ProjectMember 数据
INSERT INTO ProjectMembers (ProjectId, UserId, Role, JoinDate, Status)
VALUES
    -- 项目 1 的成员
    (1, 3, 'Instructor', '2024-01-01', 'Active'),
    (1, 13, 'ProjectLeader', '2024-01-01', 'Active'),
    (1, 14, 'Member', '2024-01-02', 'Active'),
    (1, 15, 'Member', '2024-01-03', 'Active'),
    (1, 16, 'Member', '2024-01-04', 'Active'),

    -- 项目 2 的成员
    (2, 4, 'Instructor', '2024-02-01', 'Active'),
    (2, 14, 'ProjectLeader', '2024-02-01', 'Active'),
    (2, 15, 'Member', '2024-02-02', 'Active'),
    (2, 16, 'Member', '2024-02-03', 'Active'),
    (2, 17, 'Member', '2024-02-04', 'Active'),

    -- 项目 3 的成员
    (3, 5, 'Instructor', '2024-03-01', 'Active'),
    (3, 15, 'ProjectLeader', '2024-03-01', 'Active'),
    (3, 16, 'Member', '2024-03-02', 'Active'),
    (3, 17, 'Member', '2024-03-03', 'Active'),
    (3, 18, 'Member', '2024-03-04', 'Active'),

    -- 项目 4 的成员
    (4, 6, 'Instructor', '2024-04-01', 'Active'),
    (4, 16, 'ProjectLeader', '2024-04-01', 'Active'),
    (4, 17, 'Member', '2024-04-02', 'Active'),
    (4, 18, 'Member', '2024-04-03', 'Active'),
    (4, 19, 'Member', '2024-04-04', 'Active'),

    -- 项目 5 的成员
    (5, 7, 'Instructor', '2024-05-01', 'Active'),
    (5, 17, 'ProjectLeader', '2024-05-01', 'Active'),
    (5, 18, 'Member', '2024-05-02', 'Active'),
    (5, 19, 'Member', '2024-05-03', 'Active'),
    (5, 20, 'Member', '2024-05-04', 'Active'),

    -- 项目 6 的成员
    (6, 8, 'Instructor', '2024-06-01', 'Active'),
    (6, 18, 'ProjectLeader', '2024-06-01', 'Active'),
    (6, 19, 'Member', '2024-06-02', 'Active'),
    (6, 20, 'Member', '2024-06-03', 'Active'),
    (6, 21, 'Member', '2024-06-04', 'Active'),

    -- 项目 7 的成员
    (7, 9, 'Instructor', '2024-07-01', 'Active'),
    (7, 19, 'ProjectLeader', '2024-07-01', 'Active'),
    (7, 20, 'Member', '2024-07-02', 'Active'),
    (7, 21, 'Member', '2024-07-03', 'Active'),
    (7, 22, 'Member', '2024-07-04', 'Active'),

    -- 项目 8 的成员
    (8, 10, 'Instructor', '2024-08-01', 'Active'),
    (8, 20, 'ProjectLeader', '2024-08-01', 'Active'),
    (8, 21, 'Member', '2024-08-02', 'Active'),
    (8, 22, 'Member', '2024-08-03', 'Active'),
    (8, 23, 'Member', '2024-08-04', 'Active'),

    -- 项目 9 的成员
    (9, 11, 'Instructor', '2024-09-01', 'Active'),
    (9, 21, 'ProjectLeader', '2024-09-01', 'Active'),
    (9, 22, 'Member', '2024-09-02', 'Active'),
    (9, 23, 'Member', '2024-09-03', 'Active'),
    (9, 24, 'Member', '2024-09-04', 'Active'),

    -- 项目 10 的成员
    (10, 12, 'Instructor', '2024-10-01', 'Active'),
    (10, 22, 'ProjectLeader', '2024-10-01', 'Active'),
    (10, 23, 'Member', '2024-10-02', 'Active'),
    (10, 24, 'Member', '2024-10-03', 'Active'),
    (10, 25, 'Member', '2024-10-04', 'Active');
