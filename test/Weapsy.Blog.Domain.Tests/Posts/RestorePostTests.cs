using System;
using System.Linq;
using NUnit.Framework;
using Weapsy.Blog.Domain.Posts;
using Weapsy.Blog.Domain.Posts.Events;

namespace Weapsy.Blog.Domain.Tests.Posts
{
    [TestFixture]
    public class RestorePostTests
    {
        private Post _post;
        private PostRestored _event;

        [SetUp]
        public void Setup()
        {
            _post = PostFactories.Post();
            _post.Delete();
            _post.Restore();
            _event = _post.Events.OfType<PostRestored>().Single();
        }

        [Test]
        public void ThrowsApplicationExceptionWhenNotDeleted()
        {
            Assert.Throws<ApplicationException>(() => _post.Restore());
        }

        [Test]
        public void SetsStatus()
        {
            Assert.AreEqual(PostStatus.Draft, _post.Status);
        }

        [Test]
        public void SetsStatusTimeStamp()
        {
            Assert.AreEqual(_event.TimeStamp, _post.StatusTimeStamp);
        }
    }
}
