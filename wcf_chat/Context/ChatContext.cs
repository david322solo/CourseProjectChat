using System.Data.Entity;
namespace wcf_chat.Context
{
    public class ChatContext:DbContext
    {
        public ChatContext() : base("Chat") { }
        public DbSet<Users> users { get; set; }
        public DbSet<Contacts> contacts { get; set; }
        public DbSet<Conversation> conversations { get; set; }
        public DbSet<ConversationReply> conversationReplies { get; set; }
    }
}
