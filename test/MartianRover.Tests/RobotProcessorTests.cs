using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace RedBadgerTest.Tests;

public class RobotProcessorTests
{
    [Theory]
    [MemberData(nameof(SampleInputs))]
    public void Process_ReturnsExpectedOutput_ForSampleInputs(string[] inputLines, string[] expectedOutput)
    {
        var processor = new RobotProcessor();
        var result = processor.Process(inputLines);
        result.Should().BeEquivalentTo(expectedOutput, options => options.WithStrictOrdering());
    }

    [Fact]
    public void Process_Handles_Single_Robot()
    {
        var input = new[]
        {
            "2 2",
            "0 0 N",
            "FFF"
        };
        var processor = new RobotProcessor();
        var result = processor.Process(input);
        result.Should().ContainSingle().Which.Should().Be("0 2 N LOST");
    }

    [Fact]
    public void Process_NoRobots_ReturnsEmpty()
    {
        var input = new[] { "5 5" };
        var processor = new RobotProcessor();
        var result = processor.Process(input);
        result.Should().BeEmpty();
    }

    [Fact]
    public void Process_RobotWithEmptyInstructions_DoesNotMove()
    {
        var input = new[]
        {
            "5 5",
            "2 2 N",
            ""
        };
        var processor = new RobotProcessor();
        var result = processor.Process(input);
        result.Should().ContainSingle().Which.Should().Be("2 2 N");
    }

    [Fact]
    public void Process_RobotWithWhitespaceInstructions_DoesNotMove()
    {
        var input = new[]
        {
            "5 5",
            "2 2 N",
            "   "
        };
        var processor = new RobotProcessor();
        var result = processor.Process(input);
        result.Should().ContainSingle().Which.Should().Be("2 2 N");
    }

    [Fact]
    public void Process_MultipleRobots_SomeWithEmptyInstructions()
    {
        var input = new[]
        {
            "5 5",
            "1 1 E",
            "RFRFRFRF",
            "2 2 N",
            "",
            "3 3 S",
            "   "
        };
        var processor = new RobotProcessor();
        var result = processor.Process(input);
        result.Should().Contain("1 1 E");
        result.Should().Contain("2 2 N");
        result.Should().Contain("3 3 S");
        result.Count.Should().Be(3);
    }

    [Fact]
    public void Process_RobotAtGridEdge_MovesWithinBounds()
    {
        var input = new[]
        {
            "2 2",  
            "2 2 S",
            "F"
        };
        var processor = new RobotProcessor();
        var result = processor.Process(input);
        result.Should().ContainSingle().Which.Should().Be("2 1 S");
    }

    [Fact]
    public void Process_RobotGetsLost_SecondRobotIgnoresScent()
    {
        var input = new[]
        {
            "1 1",
            "1 1 N",
            "F",
            "1 1 N",
            "F"
        };
        var processor = new RobotProcessor();
        var result = processor.Process(input);
        result.Count.Should().Be(2);
        result[0].Should().Be("1 1 N LOST");
        result[1].Should().Be("1 1 N");
    }

    [Fact]
    public void Process_InvalidOrientation_Throws()
    {
        var input = new[]
        {
            "5 5",
            "1 1 X",
            "F"
        };
        var processor = new RobotProcessor();
        Action act = () => processor.Process(input);
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Process_InvalidPosition_Throws()
    {
        var input = new[]
        {
            "5 5",
            "a b N",
            "F"
        };
        var processor = new RobotProcessor();
        Action act = () => processor.Process(input);
        act.Should().Throw<FormatException>();
    }

    public static IEnumerable<object[]> SampleInputs()
    {
        yield return new object[]
        {
            new[]
            {
                "5 3",
                "1 1 E",
                "RFRFRFRF",
                "3 2 N",
                "FRRFLLFFRRFLL",
                "0 3 W",
                "LLFFFLFLFL"
            },
            new[]
            {
                "1 1 E",
                "3 3 N LOST",
                "2 3 S"
            }
        };
        yield return new object[]
        {
            new[]
            {
                "2 2",
                "0 0 N",
                "FFFF",
                "2 2 S",
                "LFRF"
            },
            new[]
            {
                "0 2 N LOST",
                "2 2 E LOST"
            }
        };
        yield return new object[]
        {
            new[]
            {
                "1 1",
                "1 1 N",
                "F",
                "0 0 S",
                "F"
            },
            new[]
            {
                "1 1 N LOST",
                "0 0 S LOST"
            }
        };
    }
}