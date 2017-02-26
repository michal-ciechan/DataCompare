using System;
using System.Data;
using System.Linq;
using Bogus;
using DataCompare.Tests.TestData;
using FluentAssertions;
using NUnit.Framework;

namespace DataCompare.Tests.Integration
{
    [TestFixture]
    class DataComparerTests
    {
        private DataComparer _comparer;
        private DataTable _left;
        private DataTable _right;

        [SetUp]
        public void A_TestInitialise()
        {
            _comparer = IoC.Container.GetInstance<DataComparer>();
            _left = Tables.Default;
            _right = Tables.Default;
            Randomizer.Seed = new Random(1234566789);
        }

        [Test]
        public void Compare_Default_Result_SameCollection_Has2()
        {
            var res = _comparer.Compare(_left, _right);

            res.Same.Should().HaveCount(2);
        }

        [Test]
        public void Compare_SecondRowDifferent_Result_SameCollection_Has1_DiffCollection_Has1()
        {
            _left.Rows[0]["Value"] = "Different";

            var res = _comparer.Compare(_left, _right);

            res.Same.Should().HaveCount(1);
            res.Different.Should().HaveCount(1);

            var diffRow = res.Different.Single();

            diffRow.Left.Key().Should().Be(0);
            diffRow.Left.GetValue().Should().Be("Different");
        }

        [Test]
        public void Compare_LeftAdditionalRow_Result_LeftOnly_Has1()
        {
            _left.Add(10, "Test");

            var res = _comparer.Compare(_left, _right);

            res.LeftOnly.Should().HaveCount(1);
            res.LeftOnly.Single().Key().Should().Be(10);
        }

        [Test]
        public void Compare_RightAdditionalRow_Result_RightOnly_Has1()
        {
            _right.Add(11, "RightTest");

            var res = _comparer.Compare(_left, _right);

            res.RightOnly.Should().HaveCount(1);
            res.RightOnly.Single().Key().Should().Be(11);
        }

        [Test]
        public void Compare_RightTableSameButReverseOrder_Result_Same_Has2()
        {
            _right.Clear();
            _right.Add(1, "Second");
            _right.Add(0, "First");

            var res = _comparer.Compare(_left, _right);

            res.Same.Should().HaveCount(2);
        }



    }

    public class Row
    {
        public int Key { get; set; }
        public string Value { get; set; }
    }
}
