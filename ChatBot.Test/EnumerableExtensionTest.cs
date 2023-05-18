using NuGet.Frameworks;

namespace ChatBot.Test
{
    [TestClass]
    public class EnumerableExtensionTest
    {
        [TestMethod]
        public void ByBatchTest()
        {
            var text = "1. ��������� ������� ���� � ������ ��� �����.\r\n2. �������� ���� ���� � ������ ��� �����.\r\n3. �������� ������� ��������� � ������ ��� �����.\r\n4. ��������� ��������� �������� �����������.";

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