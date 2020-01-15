using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using GigHub.Controllers;
using GigHub.Core.Models;
using GigHub.Core.ViewModels;
using GigHub.Integration.Tests.Extensions;
using GigHub.Persistence;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace GigHub.Integration.Tests.Controllers.Api
{
    [TestFixture]
    public class GigsControllerTests
    {

        private GigHub.Controllers.Api.GigsController _controller;
        private ApplicationDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext();
            _controller = new GigHub.Controllers.Api.GigsController(new UnitOfWork(_context));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }



        [Test, Isolated]
        public void Cancel_WhenCalled_ShouldRemoveTheGivenGig()
        {
            // Arrange
            var user = _context.Users.First();
            _controller.MockCurrentUser(user.Id, user.Name);

            var genre = _context.Genres.Single(g => g.Id == 1);
            var gig = new Gig { Artist = user, DateTime = DateTime.Now.AddDays(1), Genre = genre, Venue = "-" };

            _context.Gigs.Add(gig);
            //_context.SaveChanges();

            var gig2 = new Gig { Artist = user, DateTime = DateTime.Now.AddDays(1), Genre = genre, Venue = "-" };
            _context.Gigs.Add(gig2);

            _context.SaveChanges();

            //Act
            _controller.Cancel(gig.Id);

            // Assert
            _context.Gigs.Find(gig2.Id).Should().NotBe(null);
            _context.Gigs.SingleOrDefault(g => g.Id == 1).Should().BeNull();
        }
    }
}
