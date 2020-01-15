﻿using System.Data.Entity.Migrations;
using System.Linq;
using GigHub.Core.Models;
using GigHub.Persistence;
using NUnit.Framework;
using Microsoft.AspNet.Identity.EntityFramework;


namespace GigHub.Integration.Tests
{
    [SetUpFixture]
    public class GlobalSetUp
    {
        [SetUp]
        public void SetUp()
        {
            MigrateDbToLatestVersion();

            Seed();
        }

        private static void MigrateDbToLatestVersion()
        {
            var configuration = new GigHub.Migrations.Configuration();
            var migrator = new DbMigrator(configuration);
            migrator.Update();
        }

        public void Seed()
        {
            var context = new ApplicationDbContext();

            if (context.Users.Any())
                return;

            context.Users.Add(new ApplicationUser
                {UserName = "user1", Name = "user1", Email = "-", PasswordHash = "-"});

            context.Users.Add(new ApplicationUser
                {UserName = "user2", Name = "user2", Email = "-", PasswordHash = "-"});


            context.SaveChanges();
        }
    }
}
