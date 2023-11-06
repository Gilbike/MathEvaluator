using ConsoleApp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    internal class EvaluatorTests
    {
        [Test]
        public void InvalidOperatorShouldBeError()
        {
            Assert.Throws<ArgumentException>(() => Evaluator.Evaluate("40l50"));
        }

        [Test]
        [TestCase(new object[] {"5+5", 10})]
        [TestCase(new object[] {"50+30+10", 90})]
        [TestCase(new object[] {"1+1+1+1+1", 5})]
        [TestCase(new object[] {"1234+4321", 5555})]
        public void AdditionIsCorrect(object[] args)
        {
            string input = (string)args[0];
            int expected = (int)args[1];

            double result = Evaluator.Evaluate(input);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase("10-5")]
        [TestCase("12-7")]
        [TestCase("8-3")]
        public void SubstractionIsCorrect(string input)
        {
            double result = Evaluator.Evaluate(input);
            Assert.That(result, Is.EqualTo(5));
        }

        [Test]
        [TestCase("3*4")]
        [TestCase("6*2")]
        [TestCase("1*12")]
        public void MultiplicationIsCorrect(string input)
        {
            double result = Evaluator.Evaluate(input);
            Assert.That(result, Is.EqualTo(12));
        }

        [Test]
        [TestCase("3/0")]
        [TestCase("30%10/0")]
        [TestCase("76-43/0")]
        public void DivisionByZeroShouldBeError(string input)
        {
            Assert.Throws<DivideByZeroException>(() => Evaluator.Evaluate(input));
        }

        [Test]
        [TestCase(new object[] { "4/2", 2.0 })]
        [TestCase(new object[] { "53245/5", 10649.0 })]
        [TestCase(new object[] { "234/12", 19.5 })]
        public void DivisionIsCorrect(object[] args)
        {
            string input = (string)args[0];
            double expected = (double)args[1];

            double result = Evaluator.Evaluate(input);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(new object[] { "236+54-2/5", 289.6 })]
        [TestCase(new object[] { "1/1%1*1", 0.0 })]
        [TestCase(new object[] { "20*43%10-2+31", 29.0 })]
        public void ComplexExpressionsAreCorrect(object[] args)
        {
            string input = (string)args[0];
            double expected = (double)args[1];

            double result = Evaluator.Evaluate(input);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void SpaceShouldNotCauseError()
        {
            double result = Evaluator.Evaluate("2 + 2");
            Assert.That(result, Is.EqualTo(4));
        }

        [Test]
        public void InvalidTokenShouldCauseException()
        {
            Assert.Throws<ArgumentException>(() => Evaluator.ExtractTokens("3*123+asd"));
        }

        [Test]
        public void IsExtractionCorrect()
        {
            List<string> output = Evaluator.ExtractTokens("3*25+21234");
            bool same = Enumerable.SequenceEqual(output, new List<string>() { "3", "*", "25", "+", "21234" });
            Assert.That(same, Is.True);
        }
    }
}
