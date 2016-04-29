using System.Collections.Generic;
using FluentAssertions;
using MoqqerNamespace;
using NUnit.Framework;

namespace DataCompare.Tests
{
    [TestFixture]
    public class DataCompareTests
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
        public void Compare_SameArrays_SameTrue()
        {
            var array1 = new []
            {
                new []{ "Key", "Value"},
                new []{ "1", "Test"},
            };

            var array2 = new []
            {
                new []{ "Key", "Value"},
                new []{ "1", "Test"},
            };

            var res = _subject.Compare(array1, array2);

            res.Same.Should().BeTrue();
        }
    }
}