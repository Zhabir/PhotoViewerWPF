using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ВПФ_Фоторедактор.ImageList;
using System;

namespace ВПФ_Фоторедактор
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod_exceptionWidthHeight()
        {
            var exception = Assert.ThrowsException<System.Exception>(() => ImageList.exceptionWidth(-1));
        }
        [TestMethod()]
        public void DeleteCarriageTest()
        {
            var exception = Assert.ThrowsException<System.Exception>(() => ImageList.exceptionHeight(-1));

        }
    }
}
