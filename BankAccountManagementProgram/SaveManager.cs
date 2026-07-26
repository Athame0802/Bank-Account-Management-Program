using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace BankAccountManagementProgram
{
    public enum LoadStatus
    {
        NoFile,
        LoadFailed,
        LoadSucceed,
        FileModified
    }

    public static class SaveManager
    {
        private static string saveFilePath = default;
        public static LoadStatus LoadStatus = default;

        public static void CheckSaveFolderAndFileAndLoad(ref ulong balance, ref List<BankStatement> statements)
        {
            try
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
                        statements = new(50);
                        balance = 0;
                    }
                    else
                    {
                        foreach (BankStatement statement in statements)
                        {
                            bool isNotModified = Regex.IsMatch(statement.Time, "\\d{2}-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[01]) ([0-1][0-9]|2[0-3]):([0-5][0-9]):([0-5][0-9])");
                            if (!isNotModified)
                            {
                                LoadStatus = LoadStatus.FileModified;

                                statements = new(50);
                                balance = 0;
                                return;
                            }
                        }
                    }

                    LoadStatus = isLoadSucceed ? LoadStatus.LoadSucceed : LoadStatus.LoadFailed;
                    return;
                }
            
                LoadStatus = LoadStatus.NoFile;

                statements = new(50);
                balance = 0;
            }
            catch
            {
                LoadStatus = LoadStatus.LoadFailed;

                statements = new(50);
                balance = 0;
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
