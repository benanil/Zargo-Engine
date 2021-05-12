#pragma warning disable CA1416 // Validate platform compatibility

using System;
using System.Linq;
using Microsoft.Win32;
using ZargoEngine.Core;

namespace ZargoEngine.SaveLoad
{
    public static class PlayerPrefs
    {
        public const string MiddleGamesFolder = @"SOFTWARE\MiddleGames";

        static RegistryKey projectKey;

        public static void EnsureProjectRegistryFolder(string projectFolder)
        {
            CheckOS();
            if (OperatingSystem.IsWindows())
            {
                var middleGamesKey = Registry.CurrentUser.OpenSubKey(MiddleGamesFolder);
                if (middleGamesKey == null){
                    middleGamesKey = Registry.CurrentUser.CreateSubKey(MiddleGamesFolder);
                }

                if (projectKey == null) 
                    projectKey = middleGamesKey.OpenSubKey(projectFolder, true);
                    if (projectKey == null)
                        projectKey = Registry.CurrentUser.CreateSubKey(MiddleGamesFolder + '\\' + projectFolder,true);
            }
        }

        public static void ClearAllPlayerPrefs(){
            EnsureProjectRegistryFolder(ProjectSettings.ProjectName);

            string[] keys = projectKey.GetValueNames();

            for (int k = 0; k < keys.Length; k++){
                projectKey.DeleteValue(keys[k]);
            }
            Debug.Log("Player Prefs cleared");
        }

        #region getters
        public static int GetInt(string name){
            TryGetInt(name, out int value);
            return value;
        }

        public static float GetFloat(string name){
            TryGetFloat(name, out float value);
            return value;
        }

        public static bool GetBool(string name){
            TryGetInt(name, out int value);
            return value == 1;
        }

        public static string GetString(string name){
            TryGetString(name, out string value);
            return value;
        }

        public static bool TryGetInt(string name,out int value){
            return TryGetNumericValue(name, out value);
        }

        public static bool TryGetFloat(string name,out float value){
            TryGetStringValue(name, out string strValue);
            value = float.Parse(strValue);
            return projectKey.GetSubKeyNames().Contains(name);
        }

        public static bool TryGetBool(string name,out bool value){
            TryGetNumericValue(name, out int intValue);
            value = intValue == 1;
            return projectKey.GetSubKeyNames().Contains(name);
        }

        public static bool TryGetString(string name,out string value){
            value = (string)projectKey.GetValue(name);
            return projectKey.GetValueNames().Contains(name);
        }

        public static bool TryGetStringValue(string name, out string value){
            bool canConvert = true;
            value = (string)projectKey.GetValue(name);
            return canConvert;
        }

        public static bool TryGetNumericValue(string name, out int value){
            bool canConvert = true;
            value = (int)projectKey.GetValue(name);
            return canConvert;
        }

        private static void CheckOS()
        {
            if (!OperatingSystem.IsWindows()) throw new PlatformNotSupportedException();
        }
        #endregion

        #region setters
        public static void SetInt(string name, int value){
            SetNumericValue(name, value);
        }

        public static void SetFloat(string name, float value){
            SetStringValue(name, value.ToString());
        }

        public static void SetBool(string name, bool value){
            projectKey.SetValue(name, value ? 1 : 0, RegistryValueKind.DWord);
        }

        public static void SetString(string name, string value){
            SetStringValue(name, value);
        }

        private static void SetStringValue(string name, string value){ 
            EnsureProjectRegistryFolder(ProjectSettings.ProjectName);
            projectKey.SetValue(name, value, RegistryValueKind.String);
        }

        private static void SetNumericValue(string name, int value){
            EnsureProjectRegistryFolder(ProjectSettings.ProjectName);
            projectKey.SetValue(name, value,RegistryValueKind.DWord);
        }
        #endregion
    }
}
