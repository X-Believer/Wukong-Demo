-- 开启外键约束支持
PRAGMA foreign_keys = OFF;

-- 遍历所有表，生成清空数据的语句
BEGIN TRANSACTION;

-- 删除所有表中的数据
SELECT 'DELETE FROM "' || name || '";' 
FROM sqlite_master 
WHERE type = 'table';

-- 如果需要重置自增主键，可以执行以下命令：
SELECT 'DELETE FROM sqlite_sequence WHERE name = "' || name || '";'
FROM sqlite_master
WHERE type = 'table';

COMMIT;

-- 恢复外键约束支持
PRAGMA foreign_keys = ON;
