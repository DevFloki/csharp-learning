namespace csharp_learning.Modules
{
    internal class SmartKitchen
    {
        public static async Task RunAsync()
        {
            Task<string> boilWater = BoilingWaterAsync();
            Task prepearTea = PrepearingTeaAsync();
            await Task.WhenAll(boilWater, prepearTea);

            string boilingWater = boilWater.Result;
            Console.WriteLine(boilingWater);
            Console.WriteLine();

            await MakingTeaAsync();
            DrinkTea();
            Console.WriteLine();
        }

        public static async Task<string> BoilingWaterAsync()
        {
            Console.WriteLine("Boiling water");
            await Task.Delay(2000);
            return "Water is Boiled";
        }

        public static async Task PrepearingTeaAsync()
        {
            Console.WriteLine("Getting cup");
            Console.WriteLine("Geting t-bag");
        }

        public static async Task MakingTeaAsync()
        {
            Console.WriteLine("Pouring water into cup");
            Console.WriteLine("Putting t-bag in cup");
            await Task.Delay(3000);
            Console.WriteLine("Tea is ready to drink");
        }

        public static void DrinkTea()
        {
            Console.WriteLine("Drinking tea");
        }
    }

}
