using System.Collections.Generic;
using System.Linq;
using CodeMetricsReportProcessor.Parsing;
using NUnit.Framework;
using Shouldly;

namespace CodeMetricsReportProcessor.Tests.Parsing
{
    [TestFixture]
    public class module
    {
        [Test]
        public void should_find_distinct_list_of_metrics()
        {
            var module = new Module {Metrics = new Dictionary<string, int> {{"m1", 1}, {"m2", 2}}};
            module.Namespaces.Add(new Namespace { Metrics = new Dictionary<string, int> { { "m3", 3 } } });
            module.Namespaces[0].Types.Add(new Type { Metrics = new Dictionary<string, int> { { "m4", 4 } } });

            var result = module.AllPossibleMetrics();

            result.Count().ShouldBe(4);
            result.Last().ShouldBe("m4");
        }


        [Test]
        public void should_flatten_results()
        {
            var module = new Module { Name = "module", Metrics = new Dictionary<string, int> { { "m1", 1 }, { "m2", 2 } } };
            module.Namespaces.Add(new Namespace { Name = "namespace", Metrics = new Dictionary<string, int> { { "m3", 3 } } });
            module.Namespaces[0].Types.Add(new Type { Name = "type", Metrics = new Dictionary<string, int> { { "m4", 4 } } });

            var result = module.Flatten();

            result.Count().ShouldBe(2);
            result.Last().Type.ShouldBe("type");
        }
    }
}
