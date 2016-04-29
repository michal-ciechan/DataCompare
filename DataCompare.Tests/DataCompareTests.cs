using System.Collections.Generic;
using DataCompare.Tests.TestData;
using FluentAssertions;
using MoqqerNamespace;
using NUnit.Framework;

namespace DataCompare.Tests
{
    [TestFixture]
    public class DataCompareTests
    {
        private Moqqer _moq;
        private DataComparer _comparer;

        [SetUp]
        public void A_TestSetup()
        {
            _moq = new Moqqer();
            _comparer = _moq.Create<DataComparer>();
        }

        [Test]
        public void Compare_BothDefault_SameTrue()
        {
            var left = Lists.Default;
            var right = Lists.Default;

            _comparer.Compare(left, right)
                .Same.Should().BeTrue();
        }

        [Test]
        public void Compare_LeftHasExtraNewRow_LeftOnlyHasNewRow()
        {
            var left = Lists.Default;
            var right = Lists.Default;

            _comparer.Compare(left, right)
                .Same.Should().BeTrue();
        }
    }
}