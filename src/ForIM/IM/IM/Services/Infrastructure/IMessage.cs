namespace ForYou.ForIM.Services.Infrastructure
{
    /// <summary>
    /// 消息格式
    /// </summary>
    public interface IMessage
    {

        /// <summary>
        /// 消息唯一标识
        /// </summary>
        string Id { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        MessageType Type { get; set; }

        /// <summary>
        /// 文本内容
        /// </summary>
        string Content { get; set; }

        /// <summary>
        /// 媒体Id
        /// </summary>
        string MediaId { get; set; }
    }

    /// <summary>
    /// 消息类型
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// 文本
        /// </summary>
        Text = 1,
        /// <summary>
        /// 图文
        /// </summary>
        Image = 2
    }
}