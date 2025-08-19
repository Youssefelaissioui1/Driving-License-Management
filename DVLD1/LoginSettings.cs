using System;
using System.IO;
using System.Text;

namespace DVLD1
{
    public static class LoginSettings
    {
        // Chemin complet du fichier de login
        private static readonly string _filePath = Path.Combine(
            System.Windows.Forms.Application.StartupPath, "application", "pdf", "login.txt"
        );

        public static void SaveCredentials(string username, string password)
        {
            // S’assurer que le dossier existe
            string directory = Path.GetDirectoryName(_filePath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            File.WriteAllText(_filePath, $"{username}|{password}", Encoding.UTF8);
        }

        public static (string username, string password)? LoadCredentials()
        {
            if (!File.Exists(_filePath))
                return null;

            string content = File.ReadAllText(_filePath, Encoding.UTF8);
            var parts = content.Split('|');
            if (parts.Length == 2)
                return (parts[0], parts[1]);

            return null;
        }

        public static void ClearCredentials()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);
        }
    }
}
