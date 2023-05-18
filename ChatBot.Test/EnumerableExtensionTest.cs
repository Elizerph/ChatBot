using NuGet.Frameworks;

namespace ChatBot.Test
{
    [TestClass]
    public class EnumerableExtensionTest
    {
        [TestMethod]
        public void ByBatchTest()
        {
            var text = "1. Поместите бутылку воды в стойку для зелий.\r\n2. Добавьте глаз края в стойку для зелий.\r\n3. Добавьте золотой картофель в стойку для зелий.\r\n4. Дождитесь окончания процесса заваривания.";

            var words = text.Split(' ');

            var partitions = words.ByBatch(20, e => e.Length).ToArray();

            foreach (var partition in partitions)
            {
                Console.WriteLine("---");
                Console.WriteLine(string.Join(' ', partition));
            }
        }
    }
}