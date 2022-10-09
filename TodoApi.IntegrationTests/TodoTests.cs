using System.Text.Json.Serialization;

[assembly: Parallelize(Workers = 0, Scope = ExecutionScope.MethodLevel)]
namespace TodoApi.IntegrationTests
{
#nullable disable warnings
    public class TodoItemDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("isComplete")]
        public bool IsComplete { get; set; }
    }
#nullable enable warnings

    [TestClass]
    public class TodoTests : TodoTestsBase
    {
        [ClassInitialize]
        public static void Setup(TestContext tc) => InitializeApi();

        private async Task ExecuteTest()
        {
            // Arrange
            var id = await PostTodoItem();

            // Act
            var result = await GetTodoItem(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
        }

        [TestMethod]
        public async Task TestMethod0()
        {
            await ExecuteTest();
        }

        [TestMethod]
        public async Task TestMethod1()
        {
            await ExecuteTest();
        }

        [TestMethod]
        public async Task TestMethod2()
        {
            await ExecuteTest();
        }

        //[TestMethod]
        //public async Task TestMethod3()
        //{
        //    await ExecuteTest();
        //}
        //
        // ... and so on

        //[TestMethod]
        //public void WriteTestsToFile()
        //{
        //    var lines = new List<string>();

        //    for (int i = 0; i < 10_000; i++)
        //    {
        //        lines.AddRange(new string[]{
        //            "[TestMethod]", $"public async Task TestMethod{i}()", "{", "await ExecuteTest();", "}", ""
        //            });
        //    }

        //    File.WriteAllLinesAsync("WriteLines.txt", lines).Wait();
        //}
    }
}