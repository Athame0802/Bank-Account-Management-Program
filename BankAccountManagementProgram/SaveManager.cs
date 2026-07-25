using System;
using System.Collections.Generic;
using System.Text;

namespace BankAccountManagementProgram
{
    public static class SaveManager
    {
        public static bool TrySaveStatement(ulong amount, bool isDeposit)
        {
            throw new NotImplementedException("SaveManager의 TrySaveStatement가 구현되지 않았습니다.");
        }

        public static bool TryLoadStatement(out BankStatement statement)
        {
            throw new NotImplementedException("SaveManager의 TryLoadStatement가 구현되지 않았습니다.");
        }
    }
}
