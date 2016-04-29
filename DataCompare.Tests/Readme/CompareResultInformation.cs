using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataCompare.Tests.TestData;
using FluentAssertions;
using MoqqerNamespace;
using NUnit.Framework;

namespace DataCompare.Tests.Readme
{
    [TestFixture]
    public class CompareResultInformation
    {
        private Moqqer _moq;
        private DataComparer _comparer;
        [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Make README samples easier to read")]
        private List<List<string>> left;
        [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Make README samples easier to read")]
        private List<List<string>> right;

        [SetUp]
        public void A_TestSetup()
        {
            _moq = new Moqqer();
            _comparer = _moq.Create<DataComparer>();
            left = Lists.Default;
            right = Lists.Default;
        }

        [Test]
        public void Compare_BothDefault_ResSameTrue()
        {
            var diff = Lists.Default.Add(2, "New");

            var res1 = _comparer.Compare(left, right);
            var res2 = _comparer.Compare(left, diff);

            res1.Same.Should().BeTrue();
            res2.Same.Should().BeFalse();
        }

        [Test]
        public void Compare_LeftHasExtraNewRow_ResLeftOnlyHasNewRow()
        {
            var leftExtra = Lists.Default.Add(2, "New");

            var res1 = _comparer.Compare(left, right);
            var res2 = _comparer.Compare(leftExtra, right);

            res1.LeftOnly.Should().BeEmpty();
            res2.LeftOnly.ShouldBeEquivalentTo(leftExtra.Last());
        }

        [Test]
        public void Compare_RightHasExtraNewRow_ResRightOnlyHasNewRow()
        {
            var rightExtra = Lists.Default.Add(2, "New");

            var res1 = _comparer.Compare(left, right);
            var res2 = _comparer.Compare(left, rightExtra);

            res1.RightOnly.Should().BeEmpty();
            res2.RightOnly.ShouldBeEquivalentTo(rightExtra.Last());
        }

        [Test]
        public void Compare_1RowInBothHasDifferentValue_ResDifferencesHasRowResult()
        {
            throw new NotImplementedException("TODO: Compare_1RowInBothHasDifferentValue_ResDifferencesHasRowResult");
        }
    }
}