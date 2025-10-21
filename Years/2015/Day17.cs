namespace AdventOfCode.Years._2015
{
    public class Day17
    {
        private const int TargetVolume = 150;

        public void Run(string inputPath)
        {
            var containerSizes = ParseInput(inputPath);

            int totalCombinations = CountAllCombinations(containerSizes);
            int minContainerCombinations = CountMinContainerCombinations(containerSizes);

            Console.WriteLine($"Part One: Total combinations: {totalCombinations}");
            Console.WriteLine($"Part Two: Minimum container combinations: {minContainerCombinations}");
        }

        private List<int> ParseInput(string inputPath)
        {
            return File.ReadAllLines(inputPath).Select(int.Parse).ToList();
        }

        public int CountAllCombinations(List<int> containerSizes)
        {
            return CalculateCombinations(containerSizes, TargetVolume, 0, 0, int.MaxValue, false);
        }

        public int CountMinContainerCombinations(List<int> containerSizes)
        {
            return CalculateCombinations(containerSizes, TargetVolume, 0, 0, int.MaxValue, true);
        }

        private int CalculateCombinations(
            List<int> containerSizes,
            int remainingVolume,
            int containerIndex,
            int containersUsed,
            int minContainers,
            bool trackMinContainers)
        {
            if (remainingVolume == 0)
            {
                if (trackMinContainers)
                {
                    if (containersUsed < minContainers)
                    {
                        minContainers = containersUsed;
                        return 1;
                    }
                    else if (containersUsed == minContainers)
                    {
                        return 1;
                    }
                    return 0;
                }
                return 1;
            }

            if (containerIndex >= containerSizes.Count || remainingVolume < 0)
                return 0;

            int includeCurrent = CalculateCombinations(
                containerSizes,
                remainingVolume - containerSizes[containerIndex],
                containerIndex + 1,
                containersUsed + 1,
                minContainers,
                trackMinContainers);

            int skipCurrent = CalculateCombinations(
                containerSizes,
                remainingVolume,
                containerIndex + 1,
                containersUsed,
                minContainers,
                trackMinContainers);

            return includeCurrent + skipCurrent;
        }
    }
}