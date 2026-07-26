using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BankAccountManagementProgram
{
    public enum LoadStatus
    {
        NoFile,
        LoadFailed,
        LoadSucceed
    }

    public static class SaveManager
    {
        private static string saveFilePath = default;
        public static LoadStatus LoadStatus = default;

        public static void CheckSaveFolderAndFileAndLoad(ref ulong balance, ref List<BankStatement> statements)
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory;
            string folderName = "Save";

            string folderPath = Path.Combine(filePath, folderName);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string saveFileName = "save.json";
            saveFilePath = Path.Combine(folderPath, saveFileName);

            if (File.Exists(saveFilePath))
            {
                bool isLoadSucceed = TryLoadStatement(ref balance, ref statements);

                if (!isLoadSucceed)
                {
                    Program.ClearAndPrintMessage("파일 로드에 실패했습니다.", 18);
                    statements = new(50);
                    balance = 0;
                }

                LoadStatus = isLoadSucceed ? LoadStatus.LoadSucceed : LoadStatus.LoadFailed;
                return;
            }
            
            Program.ClearAndPrintMessage("저장된 파일이 존재하지 않습니다.", 18);
            LoadStatus = LoadStatus.NoFile;

            statements = new(50);
            balance = 0;
        }

        public static bool isAdministrator()
        {
            try
            {
                using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
                {
                    if (identity == null) return false;

                    WindowsPrincipal principal = new(identity);
                    return principal.IsInRole(WindowsBuiltInRole.Administrator);
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool TrySaveStatement(List<BankStatement> statements, ulong balance, ulong amount, bool isDeposit)
        {
            try
            {
                string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                BankStatement statement = new(currentTime, isDeposit, amount);

                statements.Add(statement);

                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    NumberHandling = JsonNumberHandling.WriteAsString,
                    UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow
                };

                SaveData saveData = new(balance, statements);

                string saveFileString = JsonSerializer.Serialize<SaveData>(saveData, options);
                File.WriteAllText(saveFilePath, saveFileString);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool TryLoadStatement(ref ulong balance, ref List<BankStatement> statements)
        {
            try
            {

                string saveFileString = File.ReadAllText(saveFilePath);

                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    NumberHandling = JsonNumberHandling.AllowReadingFromString,
                    UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow
                };

                SaveData saveData = JsonSerializer.Deserialize<SaveData>(saveFileString, options);

                if (saveData == null) return false;

                statements = saveData.BankStatements;
                balance = saveData.Balance;

                return statements != null;
            }
            catch
            {
                return false;
            }
        }
    }
}
