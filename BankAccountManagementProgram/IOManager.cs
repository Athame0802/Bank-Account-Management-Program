using System;
using System.Collections.Generic;
using System.Text;

namespace BankAccountManagementProgram
{
    public static class IOManager
    { 
        public static bool TryInputNumberAndQuit(out ulong number, out bool isQuit)
        {
            string? input = Console.ReadLine();

            if (input == "q" || input == "Q")
            {
                number = default;
                isQuit = true;
                return true;
            }

            isQuit = false;

            if (string.IsNullOrWhiteSpace(input))
            {
                number = default;
                return false;
            }


            bool isParsingSucceed = ulong.TryParse(input, out number);
            return isParsingSucceed;
        }

        public static bool TryInputNumber(out ulong number)
        {
            // 텍스트 입력 받아 TryParse로 false 반환 시 false 반환

            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                number = default;
                return false;
            }

            bool isParsingSucceed = ulong.TryParse(input, out number);
            return isParsingSucceed;
        }

        public static bool TryInputNumber(out int number)
        {
            // 텍스트 입력 받아 TryParse로 false 반환 시 false 반환

            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                number = default;
                return false;
            }

            bool isParsingSucceed = int.TryParse(input, out number);
            return isParsingSucceed;
        }

        public static void PrintNumberWithComma(ulong number)
        {
            // ToString("N0") 사용

            Console.Write(number.ToString("N0"));
        }
    }
}
