using System;
using System.Data;
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
        private DataTable _left;
        private DataTable _right;

        [SetUp]
        public void A_TestSetup()
        {
            _moq = new Moqqer();
            _comparer = IoC.Container.GetInstance<DataComparer>();
            _left = Tables.Default;
            _right = Tables.Default;
        }

        [Test]
        public void Compare_EitherNull_ThrowsException()
        {
            Action act1 = () => _comparer.Compare(null, null);
            Action act2 = () => _comparer.Compare(_left, null);
            Action act3 = () => _comparer.Compare(null, _right);

            act1.ShouldThrow<ArgumentNullException>().Which.ParamName.Should().Be("left");
            act2.ShouldThrow<ArgumentNullException>().Which.ParamName.Should().Be("right");
            act3.ShouldThrow<ArgumentNullException>().Which.ParamName.Should().Be("left");
        }

        [Test]
        public void Compare_BothDefault_SameTrue()
        {
            var left = Tables.Default;
            var right = Tables.Default;

            _comparer.Compare(left, right)
                .Same.Should().HaveCount(2);
        }

        [Test]
        public void Compare_LeftHasExtraNewRow_LeftOnlyHasNewRow()
        {
            var left = Tables.Default.Add(2, "NewRow");
            var right = Tables.Default;

            var res = _comparer.Compare(left, right);

            res.Same.Should().HaveCount(2);
            res.LeftOnly.Should().HaveCount(1);
        }
    }
}