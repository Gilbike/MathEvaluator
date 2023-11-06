using ConsoleApp;

namespace UnitTests
{
    public class IOTests
    {
        const string defaultPath = @"C:\Users\dimar\Documents\Programming\School\UnitTestingLearn\Output\file.txt";
        const string relativePath = @".\Output\file.txt";
        BusinessLogic? logic;

        private void DeleteDefaultFile()
        {
            File.Delete(defaultPath);
        }

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            DeleteDefaultFile();
            logic = new BusinessLogic();
        }

        [TearDown]
        public void TearDown()
        {
            DeleteDefaultFile();
        }

        [Test]
        [TestCase(@"C:\Users\dimar\Documents\Programming\School\UnitTestingLearn\Output\file.txt")]
        [TestCase(defaultPath)]
        public void NormalUseCase(string path)
        {
            logic!.MakeFile(path);
            Assert.IsTrue(File.Exists(path));
        }

        [Test]
        public void ThrowsArgumentExceptionWhenPathIsRelative()
        {
            Assert.Throws(typeof(ArgumentException), () => logic!.MakeFile(relativePath));
        }

        [Test]
        public void ThrowsIOExceptionWhenFileExists()
        {
            File.WriteAllText(defaultPath, "");
            Assert.Throws(typeof(IOException), () => logic!.MakeFile(defaultPath));
        }
    }
}