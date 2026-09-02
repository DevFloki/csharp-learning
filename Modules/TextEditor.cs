namespace csharp_learning.Modules
{
    internal class TextEditor
    {
        public static void Run()
        {

            string dataDirectory = Path.Combine("Data");
            string filePath = Path.Combine(dataDirectory, "recipes.txt");

            Directory.CreateDirectory(dataDirectory);
        }
    }
}
