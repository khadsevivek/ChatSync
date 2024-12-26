namespace ChatSyncPVT.Models
{
    public class MessageModels
    {
        public class Message
        {
            public int Id { get; set; }
            public int GroupId { get; set; }
            public int SenderId { get; set; }
            public string Content { get; set; }
            public DateTime SentAt { get; set; } = DateTime.UtcNow;

            public Group Group { get; set; }
            public User Sender { get; set; }
        }

        public class Group
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public ICollection<UserGroup> UserGroups { get; set; }
            public ICollection<Message> Messages { get; set; }
        }

        public class User
        {
            public int Id { get; set; }
            public string Username { get; set; }
            public ICollection<UserGroup> UserGroups { get; set; }
            public ICollection<Message> SentMessages { get; set; }
        }

        public class UserGroup
        {
            public int Id { get; set; }
            public int UserId { get; set; }
            public int GroupId { get; set; }

            public User User { get; set; }
            public Group Group { get; set; }
        }
    }
}
