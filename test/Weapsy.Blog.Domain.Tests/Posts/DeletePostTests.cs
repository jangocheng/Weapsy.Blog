using System;
using System.Linq;
using NUnit.Framework;
using Weapsy.Blog.Domain.Posts;
using Weapsy.Blog.Domain.Posts.Events;

namespace Weapsy.Blog.Domain.Tests.Posts
{
    [TestFixture]
    public class DeletePostTests
    {
        private Post _post;
        private PostDeleted _event;

        [SetUp]
        public void Setup()
        {
            _post = PostFactories.Post();
            _post.Delete();
            _event = _post.Events.OfType<PostDeleted>().Single();
        }

        [Test]
        public void ThrowsApplicationExceptionWhenAlreadyDeleted()
        {
            Assert.Throws<ApplicationException>(() => _post.Delete());
        }

        [Test]
        public void SetsStatus()
        {
            Assert.AreEqual(PostStatus.Deleted, _post.Status);
        }

        [Test]
        public void SetsStatusTimeStamp()
        {
            Assert.AreEqual(_event.TimeStamp, _post.StatusTimeStamp);
        }
    }
}
