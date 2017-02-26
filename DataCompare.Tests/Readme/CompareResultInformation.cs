using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataCompare.Tests.TestData;
using FluentAssertions;
using NUnit.Framework;

namespace DataCompare.Tests.Readme
{
    [TestFixture]
    public class CompareResultInformation
    {
        private DataComparer _comparer;
        [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Make README samples easier to read")]
        private DataTable left;
        [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Make README samples easier to read")]
        private DataTable right;

        [SetUp]
        public void A_TestSetup()
        {
            _comparer = IoC.Container.GetInstance<DataComparer>();
            left = Tables.Default;
            right = Tables.Default;
        }

        [Test]
        public void Compare_BothDefault_ResSameTrue()
        {
            var diff = Tables.Default;

            var res1 = _comparer.Compare(left, right);
            var res2 = _comparer.Compare(left, diff);

            res1.Same.Should().HaveCount(2);
            res2.Same.Should().HaveCount(2);
        }

        [Test]
        public void Compare_LeftHasExtraNewRow_ResLeftOnlyHasNewRow()
        {
            var key = 2;

            var leftExtra = Tables.Default.Add(key, "New");

            var res1 = _comparer.Compare(left, right);
            var res2 = _comparer.Compare(leftExtra, right);

            res1.LeftOnly.Should().BeEmpty();
            res2.LeftOnly.Single().Key().Should().Be(key);
        }

        [Test]
        public void Compare_RightHasExtraNewRow_ResRightOnlyHasNewRow()
        {
            var key = 2;

            var rightExtra = Tables.Default.Add(key, "New");

            var res1 = _comparer.Compare(left, right);
            var res2 = _comparer.Compare(left, rightExtra);

            res1.RightOnly.Should().BeEmpty();
            res2.RightOnly.Single().Key().Should().Be(key);
        }

        [Test]
        public void Compare_1RowInBothHasDifferentValue_ResDifferencesHasRowResult()
        {
            var rightDiff = Tables.Default;
            rightDiff.Rows[0]["Value"] = "Random";

            var res1 = _comparer.Compare(left, right);
            var res2 = _comparer.Compare(left, rightDiff);

            res1.Different.Should().BeEmpty();
            res2.Different.Should().HaveCount(1);

            res2.Different
                .Select(x => $"{x.Left.GetValue()} - {x.Right.GetValue()}").Single()
                .Should().Be("First - Random");
        }
    }
}