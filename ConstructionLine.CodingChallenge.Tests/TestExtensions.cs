using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    public static class TestExtensions
    {
        public static TestCaseData WithName(this TestCaseData testcase, string name)
        {
            testcase.SetName(name);
            return testcase;
        }
    }
}
