using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskBoard.Data.Models;

namespace TaskBoard.Data.Configure
{
    public class BoardConfiguration : IEntityTypeConfiguration<Board>
    {
        public void Configure(EntityTypeBuilder<Board> builder)
        {
            builder
                .HasData(new Board[]
                {
                    ConfigurationHelper.OpenBoard,
                    ConfigurationHelper.InProgressBoard,
                    ConfigurationHelper.DoneBoard
                });
        }
    }
}
