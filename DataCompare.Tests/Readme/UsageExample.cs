using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DataCompare.Tests.TestData;
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
        [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Make README samples more readable")]
        private List<List<string>> left;
        [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Make README samples more readable")]
        private List<List<string>> right;

        [SetUp]
        public void A_TestSetup()
        {
            _moq = new Moqqer();
            _subject = _moq.Create<DataComparer>();
            left = Lists.Default;
            right = Lists.Default;
        }

        [Test]
        public void IntroSample()
        {
            left.Add(2, "Diff1")
                .Add(3, "RightOnly");

            right.Add(2, "Diff2")
                .Add(4, "RightOnly");
            
            var res = _subject.Compare(left, right);

            res.Same.Should().BeFalse();

            res.LeftOnly.ShouldBeEquivalentTo(new [] { new [] {"4", "LeftOnly"}});
            res.RightOnly.ShouldBeEquivalentTo(new[] { new[] { "3", "RightOnly" } });

            throw new NotImplementedException("No Differences test");
        }
    }
}