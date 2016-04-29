using FluentAssertions;
using MoqqerNamespace;
using NUnit.Framework;

namespace DataCompare.Tests.Readme
{
    [TestFixture]
    public class UsageExample
    {
        private Moqqer _moq;
        private DataComparer _subject;

        [SetUp]
        public void A_TestSetup()
        {
            _moq = new Moqqer();
            _subject = _moq.Create<DataComparer>();
        }

        [Test]
        public void IntroSample()
        {
            var left = new []
            {
                new []{ "Key", "Value"},
                new []{ "1", "Test"},
                new []{ "2", "Diff1"},
                new []{ "3", "RightOnly"},
            };

            var right = new []
            {
                new []{ "Key", "Value"},
                new []{ "1", "Test"},
                new []{ "2", "Diff2"},
                new []{ "4", "Additional"},
            };

            var res = _subject.Compare(left, right);

            res.Same.Should().BeFalse();

            res.LeftOnly.ShouldBeEquivalentTo(new [] { new [] {"4", "Additional"}});
            res.RightOnly.ShouldBeEquivalentTo(new[] { new[] { "3", "RightOnly" } });
        }
    }
}