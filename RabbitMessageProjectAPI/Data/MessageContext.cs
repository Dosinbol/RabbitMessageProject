using Microsoft.EntityFrameworkCore;
using RabbitMessageProjectAPI.Models;

namespace RabbitMessageProjectAPI.Data
{
    public class MessageContext : DbContext
    {
        public MessageContext(DbContextOptions<MessageContext> options) : base(options)
        {
            
        }
        public DbSet<Soldier> Soldiers { get; set; }
    }
}
