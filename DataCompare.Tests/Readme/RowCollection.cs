using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DataCompare.Tests.TestData;
using FluentAssertions;
using MoqqerNamespace;
using NUnit.Framework;

namespace DataCompare.Tests.Readme
{
    [TestFixture]
    public class RowCollectionTests
    {
        [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Make README samples easier to read")]
        private List<List<string>> rows;
        [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Make README samples easier to read")]
        private DataComparerConfig config;

        private RowCollection CreateRowCollection()
        {
            return new RowCollection(config, rows);
        }

        [SetUp]
        public void A_TestSetup()
        {
            config = DataComparerConfig.CreateDefault();
            rows = Lists.Default;
        }

        [Test]
        public void Header_Collection()
        {
            rows = Lists.Default;

            var res = CreateRowCollection();

            res.Headers.ShouldBeEquivalentTo(new[] {"Key", "Value"});
        }

        [Test]
        public void Key_Column_Indexes()
        {
            rows = Lists.Default;

            var res = CreateRowCollection();

            res.KeyColumnIndexes.Should().HaveCount(1)
                .And.Contain(0);
        }

        [Test]
        public void Skipped_Column_Indexes()
        {
            rows = Lists.Default
                .AddColumn("Skip");

            var res = CreateRowCollection();

            res.SkippedColumnIndexes.Should().HaveCount(1)
                .And.Contain(2);
        }

        [Test]
        public void Data_Column_Indexes()
        {
            rows = Lists.Default;

            var res = CreateRowCollection();

            res.DataColumnIndexes.Should().HaveCount(1)
                .And.Contain(1);
        }
    }

}