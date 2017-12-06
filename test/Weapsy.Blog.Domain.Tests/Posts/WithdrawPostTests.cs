using System;
using System.Linq;
using NUnit.Framework;
using Weapsy.Blog.Domain.Posts;
using Weapsy.Blog.Domain.Posts.Events;

namespace Weapsy.Blog.Domain.Tests.Posts
{
    [TestFixture]
    public class WithdrawPostTests
    {
        private Post _post;
        private PostWithdrew _event;

        [SetUp]
        public void Setup()
        {
            _post = PostFactories.Post();
            _post.Publish();
            _post.Withdraw();
            _event = _post.Events.OfType<PostWithdrew>().Single();
        }

        [Test]
        public void ThrowsApplicationExceptionWhenAlreadyWithdrew()
        {
            Assert.Throws<ApplicationException>(() => _post.Withdraw());
        }

        [Test]
        public void ThrowsApplicationExceptionWhenDeleted()
        {
            _post.Delete();
            Assert.Throws<ApplicationException>(() => _post.Withdraw());
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
