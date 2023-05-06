using Microsoft.EntityFrameworkCore;
using PropertyService.Models;

namespace PropertyService.Data
{
    public interface IAppDbContext
    {
        DbSet<User> Users { get; set; }
    }
}