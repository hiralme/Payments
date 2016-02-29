using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class Mod10Check
    {
        public static bool IsCardNumberValid(string cccardNumber)
        {
            cccardNumber = cccardNumber.Replace(" ", "");

            //1st STEP: Double each digit starting from the right
            int[] doubledDigits = new int[cccardNumber.Length / 2];
            int k = 0;
            for (int i = cccardNumber.Length - 2; i >= 0; i -= 2)
            {
                int digit = int.Parse(cccardNumber[i].ToString());
                doubledDigits[k] = digit * 2;
                k++;
            }

            //2nd STEP: Add up digits
            int total = 0;
            foreach (int i in doubledDigits)
            {
                string number = i.ToString();
                for (int j = 0; j < number.Length; j++)
                {
                    total += int.Parse(number[j].ToString());
                }
            }

            //3rd STEP: Add up other digits
            int total2 = 0;
            for (int i = cccardNumber.Length - 1; i >= 0; i -= 2)
            {
                int digit = int.Parse(cccardNumber[i].ToString());
                total2 += digit;
            }

            //4th STEP: Total
            int final = total + total2;

            //At last mod 10
            return final % 10 == 0; 
        }
    }
}
