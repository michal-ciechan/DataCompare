using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataCompare.Tests.TestData;
using FluentAssertions;
using MoqqerNamespace;
using NUnit.Framework;

namespace DataCompare.Tests.Readme
{
    [TestFixture]
    public class UsageExample
    {
        [SetUp]
        public void A_TestSetup()
        {
            _subject = IoC.Container.GetInstance<DataComparer>();
            left = Tables.Default;
            right = Tables.Default;
        }

        private DataComparer _subject;
        [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Make README samples more readable")] private DataTable left;
        [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Make README samples more readable")] private DataTable right;

        [Test]
        public void IntroSample()
        {
            left.Add(2, "Diff1")
                .Add(3, "LeftOnly");

            right.Add(2, "Diff2")
                .Add(4, "RightOnly");

            var res = _subject.Compare(left, right);

            res.Same.Should().HaveCount(2);
            res.Different.Values().Single().ShouldBeEquivalentTo(new[] {"Diff1", "Diff2"});

            res.LeftOnly.Items().ShouldBeEquivalentTo(new[] {new[] {"3", "LeftOnly"}});
            res.RightOnly.Items().ShouldBeEquivalentTo(new[] {new[] {"4", "RightOnly"}});
        }
    }
}