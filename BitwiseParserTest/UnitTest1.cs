using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace BitwiseParserTest;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    //Tests basic logic gates using example data
    public void TestSimpleCircuit()
    {
        
        var instructions = new Dictionary<string, string>
        {
            { "x", "123" },
            { "y", "456" },
            { "d", "x AND y" },
            { "e", "x OR y" },
            { "f", "x LSHIFT 2" },
            { "g", "y RSHIFT 2" },
            { "h", "NOT x" },
            { "i", "NOT y" }
        };
        
        var parser = new Parser(instructions);

        Assert.AreEqual(123, parser.GetSignal("x"));
        Assert.AreEqual(456, parser.GetSignal("y"));
        Assert.AreEqual(72, parser.GetSignal("d"));
        Assert.AreEqual(507, parser.GetSignal("e"));
        Assert.AreEqual(492, parser.GetSignal("f"));
        Assert.AreEqual(114, parser.GetSignal("g"));
        Assert.AreEqual(65412, parser.GetSignal("h"));
        Assert.AreEqual(65079, parser.GetSignal("i"));
    }

    [TestMethod]
    //Tests a direct assignment to wire a
    public void TestDirectAssignment()
    {
        var instructions = new Dictionary<string, string>
        {
            { "lx", "1234" },
            { "a", "lx" }
        };

        var parser = new Parser(instructions);

        Assert.AreEqual(1234, parser.GetSignal("a"));
    }
}