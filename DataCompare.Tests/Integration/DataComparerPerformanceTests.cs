using System;
using System.Data;
using System.Diagnostics;
using Bogus;
using DataCompare.Enums;
using DataCompare.Tests.TestData;
using FluentAssertions;
using NUnit.Framework;

namespace DataCompare.Tests.Integration
{
    [TestFixture]
    public class DataComparerPerformanceTests
    {
        [SetUp]
        public void A_TestInitialise()
        {
            _comparer = IoC.Container.GetInstance<DataComparer>();
            _left = Tables.Default;
            _right = Tables.Default;
            _watch = new Stopwatch();
            Randomizer.Seed = new Random(1234566789);
        }

        private DataComparer _comparer;
        private DataTable _left;
        private DataTable _right;
        private Stopwatch _watch;

        private CompareResult RunCompare()
        {
            _watch.Restart();

            var res = _comparer.Compare(_left, _right);

            _watch.Stop();

            Console.WriteLine("Total Time: " + _watch.Elapsed);
            Console.WriteLine("Sorting Time: " + _comparer.TimeTakenForSorting);
            Console.WriteLine("Comparing Time: " + _comparer.TimeTakenForComparing);
            return res;
        }

        private void GenerateRows(int count)
        {
            _watch.Restart();
            var values = new[] {"Value1", "Value2", "Value3"};

            int[] id = {2};
            var generator = new Faker<Row>()
                .RuleFor(x => x.Key, x => id[0]++)
                .RuleFor(x => x.Value, x => x.PickRandom(values));

            id[0] = 2;

            foreach (var row in generator.Generate(count))
                _left.Add(row.Key, row.Value);

            id[0] = 2;
            foreach (var row in generator.Generate(count))
                _right.Add(row.Key, row.Value);
            _watch.Stop();
            Console.WriteLine("Generating Time: " + _watch.Elapsed);
        }

        [Test]
        public void Compare_100k_Rows_LessThan1Second()
        {
            GenerateRows(100000);

            var res = RunCompare();

            res.Same.Should().HaveCount(33154);
            res.Different.Should().HaveCount(66848);
            _watch.ElapsedMilliseconds.Should().BeLessOrEqualTo(1000);
        }

        [Test]
        public void Compare_1m_Rows_BothSorted_LessThan1Second()
        {
            GenerateRows(1000000);

            _comparer.Config.IsSorted = DataSource.Both;

            var res = RunCompare();

            res.Same.Should().HaveCount(333478);
            res.Different.Should().HaveCount(666524);

            _watch.ElapsedMilliseconds.Should().BeLessOrEqualTo(1000);
        }
    }
}