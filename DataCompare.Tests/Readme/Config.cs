using System;
using FluentAssertions;
using MoqqerNamespace;
using NUnit.Framework;

namespace DataCompare.Tests.Readme
{
    /// <summary>
    /// If you change anything in this class, make sure you update the README.md Config seciton!
    /// </summary>
    [TestFixture]
    public class Config
    {
        private DataComparerConfig _config;

        [SetUp]
        public void A_TestSetup()
        {
            _config = DataComparerConfig.Default;
        }

        [Test]
        public void Keys()
        {
            _config.Keys.ShouldBeEquivalentTo(new []{ "Key"});
        }

        [Test]
        public void Skip()
        {
            _config.Skip.ShouldBeEquivalentTo(new []{ "ID", "Skip"});
        }
    }
}