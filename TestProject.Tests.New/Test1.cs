namespace TestProject.Tests.New
{
    [TestClass] // 必须有此特性
    public class Test1
    {
        [TestMethod] // 必须有此特性
        public void TestMethod1()
        {
            Assert.AreEqual(1, 1); // 示例断言
        }
    }
}
