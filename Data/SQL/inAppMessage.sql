-- ���� InAppMessage ����
INSERT INTO InAppMessages (SenderId, RecipientId, Type, Subject, Content, SentAt, IsRead, RelatedProjectId)
VALUES
    -- 5 �����͸�ͬһ�������� (RecipientId = 1)
    (2, 1, 1, 'ϵͳ֪ͨ', '��ӭ����ƽ̨��', '2024-12-01 10:00:00', 0, NULL),
    (3, 1, 2, '��Ŀ����', '���������Ŀ�ѷ���������', '2024-12-02 11:00:00', 1, 5),
    (4, 1, 3, '�Ŷ���Ϣ', '��鿴�Ŷ����������»��⡣', '2024-12-03 12:00:00', 0, 6),
    (5, 1, 4, '��������', '����һ��������Ҫ��ɣ���ֹ����Ϊ 2024-12-10��', '2024-12-04 13:00:00', 0, 7),
    (6, 1, 5, '��������', '����д���ܵ���Ŀ��չ���档', '2024-12-05 14:00:00', 1, 8),

    -- ���������ߵ�վ����
    (1, 2, 2, 'ϵͳ֪ͨ', '�����˻���Ϣ�Ѹ��¡�', '2024-12-01 15:00:00', 1, NULL),
    (3, 4, 3, '�������', '��ϲ����������µ���Ŀ����', '2024-12-02 16:00:00', 0, 9),
    (5, 6, 4, '����֪ͨ', '��Ŀ�齫��2024-12-07�ٿ����Ȼ��飬��׼ʱ�μӡ�', '2024-12-03 17:00:00', 0, 10),
    (2, 7, 5, '����֪ͨ', '��������������˱��µ��Ŷӽ�����', '2024-12-04 18:00:00', 1, NULL),
    (4, 8, 1, '��Ŀ����', '�������������Ŀ�Ľ��顣', '2024-12-05 19:00:00', 0, NULL);