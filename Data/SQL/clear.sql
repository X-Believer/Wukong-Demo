-- �������Լ��֧��
PRAGMA foreign_keys = OFF;

-- �������б�����������ݵ����
BEGIN TRANSACTION;

-- ɾ�����б��е�����
SELECT 'DELETE FROM "' || name || '";' 
FROM sqlite_master 
WHERE type = 'table';

-- �����Ҫ������������������ִ���������
SELECT 'DELETE FROM sqlite_sequence WHERE name = "' || name || '";'
FROM sqlite_master
WHERE type = 'table';

COMMIT;

-- �ָ����Լ��֧��
PRAGMA foreign_keys = ON;
