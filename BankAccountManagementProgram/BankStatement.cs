using System;
using System.Collections.Generic;
using System.Text;

namespace BankAccountManagementProgram
{
    public class BankStatement
    {
        public DateTime Time { get; set; }
        public bool IsDeposit { get; set; }
        public ulong Amount { get; set; }

        public BankStatement(DateTime time, bool isDeposit, ulong amount)
        {
            Time = time;
            IsDeposit = isDeposit;
            Amount = amount;
        }
    }
}
