using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EksiSozlukClone.Api.Domain.Models;
using EksiSozlukClone.Common.Infastructure;
using Bogus;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using EksiSozlukClone.Infastructure.Persistence.Migrations;

namespace EksiSozlukClone.Infastructure.Persistence.Context;

internal class SeedData
{
    private static List<User> GetUsers()
    {
        var result = new Faker<User>("tr")
            .RuleFor(i => i.Id, i => Guid.NewGuid())
            .RuleFor(i => i.CreateDate, i => i.Date.Between(DateTime.UtcNow.AddDays(-100), DateTime.UtcNow))
            .RuleFor(i => i.FirstName, i => i.Person.FirstName)
            .RuleFor(i => i.LastName, i => i.Person.LastName)
            .RuleFor(i => i.EmailAddress, i => i.Internet.Email())
            .RuleFor(i => i.UserName, i => i.Internet.UserName())
            .RuleFor(i => i.Password, i => PasswordEncryptor.Encrpt( i.Internet.Password()))
            .RuleFor(i => i.EmailConfirmed, i => i.PickRandom(true, false))
            .Generate(500);

        return result;
    }

    public async Task SeedAsync(IConfiguration configuration)
    {
        var dbContextBuilder = new DbContextOptionsBuilder();
        dbContextBuilder.UseNpgsql(configuration["EksiSozlukCloneDbConnectionString"]);

        var context = new EksiSozlukCloneDbContext(dbContextBuilder.Options);

        if (context.Users.Any())
        {
            await Task.CompletedTask;
            return;
        }
        var users = GetUsers();
        var userIds = users.Select(i => i.Id);

        await context.Users.AddRangeAsync(users);

        var guids = Enumerable.Range(0, 150).Select(i => Guid.NewGuid()).ToList();
        var counter = 0;

        var entries = new Faker<Entry>("tr")
            .RuleFor(i => i.Id, i => guids[counter++])
            .RuleFor(i => i.CreateDate, i => i.Date.Between(DateTime.UtcNow.AddDays(-100), DateTime.UtcNow))
            .RuleFor(i => i.Subject, i => i.Lorem.Sentence(5, 5))
            .RuleFor(i => i.Content, i => i.Lorem.Paragraph(2))
            .RuleFor(i => i.CreatedById, i => i.PickRandom(userIds))
            .Generate(150);

        await context.Entries.AddRangeAsync(entries);

        var comments = new Faker<EntryComment>("tr")
            .RuleFor(i => i.Id, i => Guid.NewGuid())
            .RuleFor(i => i.CreateDate, i => i.Date.Between(DateTime.UtcNow.AddDays(-100), DateTime.UtcNow))
            .RuleFor(i => i.Content, i => i.Lorem.Paragraph(2))
            .RuleFor(i => i.CreatedById, i => i.PickRandom(userIds))
            .RuleFor(i => i.EntryId, i => i.PickRandom(guids))
            .Generate(1000);

        await context.EntryComments.AddRangeAsync(comments);

        await context.SaveChangesAsync();
    }
}
