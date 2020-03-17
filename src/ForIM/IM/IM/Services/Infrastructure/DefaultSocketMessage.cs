namespace ForYou.ForIM.Services.Infrastructure
{
    /// <inheritdoc />
    public class DefaultSocketMessage : IMessage
    {
        /// <inheritdoc />
        public string Id { get; set; }

        /// <inheritdoc />
        public MessageType Type { get; set; }

        /// <inheritdoc />
        public string Content { get; set; }

        /// <inheritdoc />
        public string MediaId { get; set; }
    }
}