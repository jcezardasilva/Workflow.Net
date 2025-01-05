using AutoFixture;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using WorkflowNet.Collections.Example;
using WorkflowNet.Core.Models;

namespace WorkflowNet.Tests
{
    public class WorkflowTests
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
        };
        private readonly Fixture _fixture = new Fixture();
        [Test]
        public async Task Should_Serialize_Package()
        {
            var package = _fixture.Create<Package>();

            var jsonPackage = JsonSerializer.Serialize(package);
            Assert.That(jsonPackage, Is.Not.Null);
            Assert.That(jsonPackage, Is.Not.Empty);
        }
        [Test]
        public async Task Should_Deserialize_Package()
        {
            var fs = new FileStream("empty-package.json", FileMode.Open, FileAccess.Read);
            string strFlow;
            using (var reader = new StreamReader(fs))
            {
                strFlow = reader.ReadToEnd();
            }
            var package = JsonSerializer.Deserialize<Package>(strFlow, _jsonSerializerOptions);
            Assert.That(package, Is.Not.Null);
            Assert.That(package.Pages, Is.Empty);
        }
        [Test]
        public async Task Should_Run_A_Flow()
        {
            var fs = new FileStream("example-package.json", FileMode.Open, FileAccess.Read);
            string strFlow;
            using (var reader = new StreamReader(fs))
            {
                strFlow = reader.ReadToEnd();
            }
            var package = JsonSerializer.Deserialize<Package>(strFlow, _jsonSerializerOptions);

            Assert.That(package, Is.Not.Null);

            var actions = new ServiceCollection()
                .AddLogging()
                .AddExampleCollection()
                .BuildServiceProvider();

            var output = await new Workflow(actions).ProcessAsync(package);
            Assert.That(output, Is.Not.Null);
        }
    }
}