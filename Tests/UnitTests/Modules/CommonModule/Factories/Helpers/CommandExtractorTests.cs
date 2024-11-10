using CommonModule.Factories.Helpers;

namespace UnitTests.Modules.CommonModule.Factories.Helpers
{
    [TestClass]
    public class CommandExtractorTests
    {
        [TestMethod]
        public void ExtractCommand_ShouldReturnCommandNameAndArgs()
        {
            var inputs = new string[] { "Search(H1, 30, SGL)", "Availability(H2, 15, DBL)", "InvalidCommand)@H(" };
            var expectedResults = new (string command, string[] args)[]
            {
                ("Search", new string[] { "H1", "30", "SGL" }),
                ("Availability", new string[] { "H2", "15", "DBL" })
            };

            var actualResults = new (string command, string[] args)[expectedResults.Length];

            for (var i = 0; i < expectedResults.Length; i++)
            {
                actualResults[i] = CommandExtractor.Extract(inputs[i]);
            }

            for (var i = 0; i < expectedResults.Length; i++)
            {
                Assert.AreEqual(expectedResults[i].command, actualResults[i].command);
                CollectionAssert.AreEqual(expectedResults[i].args, actualResults[i].args);
            }

            Assert.ThrowsException<ArgumentException>(() => CommandExtractor.Extract(inputs[2]));

        }
    }
}
