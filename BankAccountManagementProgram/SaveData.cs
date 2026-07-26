using System;
using System.Collections.Generic;
using System.Text;

namespace BankAccountManagementProgram
{
    public class SaveData
    {
        public ulong Balance { get; set; }
        public List<BankStatement> BankStatements { get; set; }

        public SaveData(ulong balance, List<BankStatement> bankStatements)
        {
            Balance = balance;
            BankStatements = bankStatements;
        }

        public SaveData() { }
    }
}
