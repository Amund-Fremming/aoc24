namespace puzzle9
{
    internal class Part1
    {
        public static async Task Solve()
        {
            var input = await File.ReadAllTextAsync("PuzzleInput.txt");
            string[] spreadInput = SpreadDiskSpaces(input);
            string[] correctDisk = CorrectDisk(spreadInput);
            var checkSum = CalculateChecksum(correctDisk);

            Console.WriteLine($"Sum : {checkSum}");
        }

        private static long CalculateChecksum(string[] disk)
        {
            long sum = 0;
            for (int i = 0; i < disk.Length; i++)
            {
                if (disk[i] == ".")
                {
                    break;
                }
                sum += int.Parse(disk[i]) * i;
            }

            return sum;
        }

        public static string[] CorrectDisk(string[] disk)
        {
            var left = 0;
            var right = disk.Length - 1;
            while (left < right)
            {
                if (disk[left] == "." && disk[right] != ".")
                {
                    var temp = disk[left];
                    disk[left] = disk[right];
                    disk[right] = temp;
                    left++;
                    right--;
                }
                if (disk[left] != ".")
                {
                    left++;
                }
                if (disk[right] == ".")
                {
                    right--;
                }
            }

            return disk;
        }

        public static string[] SpreadDiskSpaces(string input)
        {
            var absMaxLength = input.Length * 9;
            string[] space = new string[absMaxLength];

            var diskNum = 0;
            var spaceIndex = 0;
            for (int i = 0; i < input.Length; i++)
            {
                var toSpread = ".";
                if (i % 2 == 0)
                {
                    toSpread = diskNum.ToString();
                    diskNum++;
                }

                var num = int.Parse(input.ElementAt(i).ToString());
                for (int j = 0; j < num; j++)
                {
                    space[spaceIndex++] = toSpread;
                }
            }

            return space[0..spaceIndex--];
        }
    }
}