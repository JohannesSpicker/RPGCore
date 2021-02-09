using System.Collections.Generic;
using Core.Tools;
using NUnit.Framework;

namespace Tests
{
    public class ReflectionTest
    {
        [Test]
        public void ReflectionTestSimplePasses()
        {
            Names<int> names = new Names<int>();

            List<int> list = names.GetAllValues();

            Assert.Contains(names.bed,      list);
            Assert.Contains(names.cupboard, list);
            Assert.Contains(names.window,   list);
            Assert.Contains(names.table,    list);
        }
    }
}