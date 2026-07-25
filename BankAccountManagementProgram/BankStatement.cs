using System;
using System.Collections.Generic;
using System.Text;

namespace BankAccountManagementProgram
{
    public class BankStatement
    {
        public string Time { get; set; }
        public bool IsDeposit { get; set; }
        public ulong Amount { get; set; }
    }
}
