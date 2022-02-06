using Boox.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Boox.Tests.UnitTests
{
    public class BookComparerTests
    {
        enum ExpectedTestResult
        {
            Negative = -1,
            Zero = 0,
            Positive = 1
        }

        [Theory]
        [InlineData("B50", "B60", ExpectedTestResult.Negative)]
        [InlineData("B5", "B6", ExpectedTestResult.Negative)]
        [InlineData("B99", "B99", ExpectedTestResult.Zero)]
        [InlineData("B9", "B9", ExpectedTestResult.Zero)]
        [InlineData("B99", "B0", ExpectedTestResult.Positive)]
        [InlineData("B9", "B8", ExpectedTestResult.Positive)]
        void BookIdComparer_1_BothInputsOk(string leftVal, string rightVal, ExpectedTestResult etr) {
            BookIdComparer sut = new();

            var res = sut.Compare(leftVal, rightVal);

            switch(etr)
            {
                case ExpectedTestResult.Negative:
                    Assert.True(res < 0);
                    break;
                case ExpectedTestResult.Zero:
                    Assert.True(res == 0);
                    break;
                case ExpectedTestResult.Positive:
                    Assert.True(res > 0);
                    break;
                default:
                    throw new Exception("Result not covered in test");
            }
        }

        [Theory]
        [InlineData("B1", "BB")]
        [InlineData("B22", "B")]
        [InlineData("B98", "")]

        void BookIdComparer_2_LeftOk_RightInvalid_ShouldBePositive(string leftVal, string rightVal)
        {
            BookIdComparer sut = new();

            var res = sut.Compare(leftVal, rightVal);

            Assert.True(res > 0);
        }

        [Theory]
        [InlineData("BB", "B1")]
        [InlineData("B", "B22")]
        [InlineData("", "B98")]
        void BookIdComparer_3_LeftInvalid_RightOk_ShouldBeNegative(string leftVal, string rightVal)
        {
            BookIdComparer sut = new();

            var res = sut.Compare(leftVal, rightVal);

            Assert.True(res < 0);
        }

        [Theory]
        [InlineData("BB", "BB")]
        [InlineData("B", "B")]
        [InlineData("", "")]
        [InlineData(null, "")]
        [InlineData("", null)]
        [InlineData(null, null)]
        void BookIdComparer_4_LeftInvalid_RightInvalid_ShouldBeZero(string leftVal, string rightVal)
        {
            BookIdComparer sut = new();

            var res = sut.Compare(leftVal, rightVal);

            Assert.True(res == 0);
        }
    }
}
