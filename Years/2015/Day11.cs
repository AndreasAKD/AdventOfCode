namespace AdventOfCode.Years._2015
{
    public class Day11
    {
        public void Run(string inputPath)
        {
            var line = File.ReadAllText(inputPath).Trim();

            string santasNextPassword = SantasNextPassword(line);

            Console.WriteLine($"Part One: Santa's Next Password: {santasNextPassword}");

            string santasNextPassword2 = SantasNextPassword(santasNextPassword);
            Console.WriteLine($"Part Two: Santa's Next Next Password: {santasNextPassword2}");

        }

        public string SantasNextPassword(string currentPassword)
        {
            char[] password = currentPassword.ToCharArray();
            do
            {
                IncrementPassword(password);
            } while (!IsValidPassword(password));
            return new string(password);
        }

        private void IncrementPassword(char[] password)
        {
            int i = password.Length - 1;
            while (i >= 0)
            {
                if (password[i] == 'z')
                {
                    password[i] = 'a';
                    i--;
                }
                else
                {
                    password[i]++;
                    break;
                }
            }
        }

        private bool IsValidPassword(char[] password)
        {
            // Rule 2: No i, o, l
            foreach (char c in password)
            {
                if (c == 'i' || c == 'o' || c == 'l')
                    return false;
            }

            // Rule 1: At least one increasing straight of three letters
            bool hasStraight = false;
            for (int i = 0; i < password.Length - 2; i++)
            {
                if (password[i + 1] == password[i] + 1 &&
                    password[i + 2] == password[i] + 2)
                {
                    hasStraight = true;
                    break;
                }
            }
            if (!hasStraight)
                return false;

            // Rule 3: At least two different, non-overlapping pairs
            int pairs = 0;
            int iPair = 0;
            while (iPair < password.Length - 1)
            {
                if (password[iPair] == password[iPair + 1])
                {
                    pairs++;
                    iPair += 2; // skip next to avoid overlap
                }
                else
                {
                    iPair++;
                }
            }
            return pairs >= 2;
        }
    }
}