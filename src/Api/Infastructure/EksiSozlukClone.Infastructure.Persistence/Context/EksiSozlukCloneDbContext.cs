﻿using EksiSozlukClone.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace EksiSozlukClone.Infastructure.Persistence.Context;

public class EksiSozlukCloneDbContext : DbContext
{
    public const string DEFAULT_SCHEMA = "dbo";

    public EksiSozlukCloneDbContext()
    {

    }
    
    public EksiSozlukCloneDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Entry> Entries { get; set; }
    public DbSet<EmailConfirmation> EmailConfirmations { get; set; }

    public DbSet<EntryFavorite> EntryFavorites { get; set; }
    public DbSet<EntryVote> EntryVotes { get; set; }

    public DbSet<EntryComment> EntryComments { get; set; }
    public DbSet<EntryCommentFavorite> EntryCommentsFavorites { get; set; }
    public DbSet<EntryCommentVote> EntryCommentsVotes { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
       if (!optionsBuilder.IsConfigured)
        {
            //var connStr = "Server=localhost; Database=eksisozlukclone; User=root; Password=*sB2xa";
            var connStr = "Host=localhost; Database=eksisozlukclone; Persist Security Info=True; User Id=postgres; Password=*sB2xa";
            
            optionsBuilder.UseNpgsql(connStr, opt =>
            {
                opt.EnableRetryOnFailure();
            });
            //optionsBuilder.UseSqlServer(connStr, opt =>
            //{
            //    opt.EnableRetryOnFailure();
            //});
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        //modelBuilder.HasDefaultSchema(DEFAULT_SCHEMA);
        //modelBuilder.Entity<EmailConfirmation>()
        //        .Property(e => e.Id)
        //        .HasColumnType("uuid");

    }
    public override int SaveChanges()
    {
        OnBeforeSave();
        return base.SaveChanges();
    }
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnBeforeSave();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        OnBeforeSave();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        OnBeforeSave();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void OnBeforeSave()
    {
        var addedEntities = ChangeTracker.Entries()
            .Where(i => i.State == EntityState.Added)
            .Select(i => (BaseEntity)i.Entity);

        PrepareAddedEntities(addedEntities);
    }
    private void PrepareAddedEntities(IEnumerable<BaseEntity> entities)
    {
        foreach (var entity in entities)
        {
            if(entity.CreateDate == DateTime.MinValue)
                entity.CreateDate = DateTime.Now;
        }
    }
}
