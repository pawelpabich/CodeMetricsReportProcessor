using System.Linq;
using CodeMetricsReportProcessor.Parsing;
using NUnit.Framework;
using Shouldly;

namespace CodeMetricsReportProcessor.Tests.Parsing
{
    [TestFixture]
    public class code_metrics_parser
    {
        [Test]
        public void parses_code_metrics_raw_data()
        {
            const string content =
                @"<?xml version='1.0' encoding='utf-8'?>
                    <CodeMetricsReport Version='10.0'>
                      <Targets>
                        <Target Name='C:\Test\SomeProject.dll'>
                          <Modules>
                            <Module Name='SomeProject.dll' AssemblyVersion='1.0.0.0' FileVersion='1.0.0.0'>
                              <Metrics>
                                <Metric Name='MaintainabilityIndex' Value='87' />
                                <Metric Name='CyclomaticComplexity' Value='2,202' />
                                <Metric Name='ClassCoupling' Value='546' />
                                <Metric Name='DepthOfInheritance' Value='8' />
                                <Metric Name='LinesOfCode' Value='3,574' />
                              </Metrics>
                              <Namespaces>
                                <Namespace Name='ProjectNamespace'>
                                  <Metrics>
                                    <Metric Name='MaintainabilityIndex' Value='90' />
                                    <Metric Name='CyclomaticComplexity' Value='55' />
                                    <Metric Name='ClassCoupling' Value='24' />
                                    <Metric Name='DepthOfInheritance' Value='2' />
                                    <Metric Name='LinesOfCode' Value='72' />
                                  </Metrics>
                                  <Types>
                                    <Type Name='ProjectClass'>
                                      <Metrics>
                                        <Metric Name='MaintainabilityIndex' Value='94' />
                                        <Metric Name='CyclomaticComplexity' Value='5' />
                                        <Metric Name='ClassCoupling' Value='0' />
                                        <Metric Name='DepthOfInheritance' Value='1' />
                                        <Metric Name='LinesOfCode' Value='5' />
                                      </Metrics>
                                      <Members>
                                        <Member Name='ProjectProperty.get() : string'>
                                          <Metrics>
                                            <Metric Name='MaintainabilityIndex' Value='98' />
                                            <Metric Name='CyclomaticComplexity' Value='1' />
                                            <Metric Name='ClassCoupling' Value='0' />
                                            <Metric Name='LinesOfCode' Value='1' />
                                          </Metrics>
                                        </Member>
                                      </Members>
                                    </Type>
                                  </Types>
                                </Namespace>            
                              </Namespaces>
                            </Module>
                          </Modules>
                        </Target>
                      </Targets>
                    </CodeMetricsReport>";

            var parser = new CodeMetricsParser();
            var report = parser.Parse(content);

            report.Targets.Count.ShouldBe(1);
            report.Targets.Single()
                  .Modules.Single()
                  .Namespaces.Single()
                  .Types.Single()
                  .Members.Single().Metrics["MaintainabilityIndex"].ShouldBe(98);
        }
    }
}
