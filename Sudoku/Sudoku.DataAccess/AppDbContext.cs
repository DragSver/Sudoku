using Microsoft.EntityFrameworkCore;
using Sudoku.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.DataAccess;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<SudokuUser> SudokuUsers { get; set; }
    public DbSet<SudokuBoard> SudokuBoards { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SudokuUser>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x=>x.Login).IsRequired();
            entity.Property(x => x.Password).IsRequired();
            entity.HasMany(x => x.SudokuBoards)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .HasPrincipalKey(x => x.Id)
                .IsRequired();
        });
        modelBuilder.Entity<SudokuBoard>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.SudokuBoardData).IsRequired();
        });
    }
}

