namespace WukongDemo.inAppMessage.Models
{
    /// <summary>
    /// 站内信类型
    /// </summary>
    public enum MessageType
    {
        // 系统通知
        SystemNotification = 1,

        // 项目相关通知
        ProjectNotification = 2,

        // 招募通知
        RecruitmentNotification = 3,

        // 申请通知
        ApplicationNotification = 4,

        // 成员变更通知
        MemberChangeNotification = 5,

        // 进度更新通知
        ProgressUpdateNotification = 6,

        // 其他类型
        Other = 99
    }
}