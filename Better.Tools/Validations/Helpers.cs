using System.Runtime.CompilerServices;

namespace Better.Tools.Validations
{
    public static class Helpers
    {
        public static int GetLineNumber([CallerLineNumber] int lineNumber = -1)
        {
            return lineNumber;
        }

        public static List<List<object>> GetListOfMatches()
        {
            var games = new List<List<object>>
            {
                new List<object>() { "1001", new DateTime(2022, 11, 20, 13, 0, 0), "0001", "0002" },
                new List<object>() { "1002", new DateTime(2022, 11, 21, 13, 0, 0), "0004", "0003" },
                new List<object>() { "1003", new DateTime(2022, 11, 25, 10, 0, 0), "0001", "0004" },
                new List<object>() { "1004", new DateTime(2022, 11, 25, 13, 0, 0), "0003", "0002" },
                new List<object>() { "1005", new DateTime(2022, 11, 29, 12, 0, 0), "0003", "0001" },
                new List<object>() { "1006", new DateTime(2022, 11, 29, 12, 0, 0), "0002", "0004" },

                new List<object>() { "1007", new DateTime(2022, 11, 21, 10, 0, 0), "0006", "0007" },
                new List<object>() { "1008", new DateTime(2022, 11, 21, 16, 0, 0), "0005", "0008" },
                new List<object>() { "1009", new DateTime(2022, 11, 25, 7, 0, 0), "0008", "0007" },
                new List<object>() { "1010", new DateTime(2022, 11, 25, 16, 0, 0), "0006", "0005" },
                new List<object>() { "1011", new DateTime(2022, 11, 29, 16, 0, 0), "0007", "0005" },
                new List<object>() { "1012", new DateTime(2022, 11, 29, 16, 0, 0), "0008", "0006" },

                new List<object>() { "1013", new DateTime(2022, 11, 22, 7, 0, 0), "0009", "0010" },
                new List<object>() { "1014", new DateTime(2022, 11, 22, 13, 0, 0), "0011", "0012" },
                new List<object>() { "1015", new DateTime(2022, 11, 26, 10, 0, 0), "0012", "0010" },
                new List<object>() { "1016", new DateTime(2022, 11, 26, 16, 0, 0), "0009", "0011" },
                new List<object>() { "1017", new DateTime(2022, 11, 30, 16, 0, 0), "0012", "0009" },
                new List<object>() { "1018", new DateTime(2022, 11, 30, 16, 0, 0), "0010", "0011" },

                new List<object>() { "1019", new DateTime(2022, 11, 22, 10, 0, 0), "0014", "0016" },
                new List<object>() { "1020", new DateTime(2022, 11, 22, 16, 0, 0), "0015", "0013" },
                new List<object>() { "1021", new DateTime(2022, 11, 26, 7, 0, 0), "0016", "0013" },
                new List<object>() { "1022", new DateTime(2022, 11, 26, 13, 0, 0), "0015", "0014" },
                new List<object>() { "1023", new DateTime(2022, 11, 30, 12, 0, 0), "0016", "0015" },
                new List<object>() { "1024", new DateTime(2022, 11, 30, 12, 0, 0), "0013", "0014" },

                new List<object>() { "1025", new DateTime(2022, 11, 23, 10, 0, 0), "0017", "0020" },
                new List<object>() { "1026", new DateTime(2022, 11, 23, 13, 0, 0), "0019", "0018" },
                new List<object>() { "1027", new DateTime(2022, 11, 27, 7, 0, 0), "0020", "0018" },
                new List<object>() { "1028", new DateTime(2022, 11, 27, 16, 0, 0), "0019", "0017" },
                new List<object>() { "1029", new DateTime(2022, 12, 1, 16, 0, 0), "0020", "0019" },
                new List<object>() { "1030", new DateTime(2022, 12, 1, 16, 0, 0), "0018", "0017" },

                new List<object>() { "1031", new DateTime(2022, 11, 23, 7, 0, 0), "0024", "0023" },
                new List<object>() { "1032", new DateTime(2022, 11, 23, 16, 0, 0), "0021", "0022" },
                new List<object>() { "1033", new DateTime(2022, 11, 27, 10, 0, 0), "0021", "0024" },
                new List<object>() { "1034", new DateTime(2022, 11, 27, 13, 0, 0), "0023", "0022" },
                new List<object>() { "1035", new DateTime(2022, 12, 1, 12, 0, 0), "0023", "0021" },
                new List<object>() { "1036", new DateTime(2022, 12, 1, 12, 0, 0), "0022", "0024" },

                new List<object>() { "1037", new DateTime(2022, 11, 24, 7, 0, 0), "0027", "0026" },
                new List<object>() { "1038", new DateTime(2022, 11, 24, 16, 0, 0), "0025", "0028" },
                new List<object>() { "1039", new DateTime(2022, 11, 28, 7, 0, 0), "0026", "0028" },
                new List<object>() { "1040", new DateTime(2022, 11, 28, 13, 0, 0), "0025", "0027" },
                new List<object>() { "1041", new DateTime(2022, 12, 2, 16, 0, 0), "0026", "0025" },
                new List<object>() { "1042", new DateTime(2022, 12, 2, 16, 0, 0), "0028", "0027" },

                new List<object>() { "1043", new DateTime(2022, 11, 24, 10, 0, 0), "0032", "0029" },
                new List<object>() { "1044", new DateTime(2022, 11, 24, 13, 0, 0), "0031", "0030" },
                new List<object>() { "1045", new DateTime(2022, 11, 28, 10, 0, 0), "0029", "0030" },
                new List<object>() { "1046", new DateTime(2022, 11, 28, 16, 0, 0), "0031", "0032" },
                new List<object>() { "1047", new DateTime(2022, 12, 2, 12, 0, 0), "0029", "0031" },
                new List<object>() { "1048", new DateTime(2022, 12, 2, 12, 0, 0), "0030", "0032" },
            };

            return games;
        }

        public static List<List<string>> GetListOfTeams()
        {
            var teams = new List<List<string>>
            {
                new List<string>() { "0001", "Catar", "CAT", "Qatar" },
                new List<string>() { "0002", "Equador", "EQU", "Ecuador" },
                new List<string>() { "0003", "Holanda", "HOL", "Netherlands" },
                new List<string>() { "0004", "Senegal", "SEN", "Senegal" },
                new List<string>() { "0005", "Estados Unidos", "USA", "United States" },
                new List<string>() { "0006", "Inglaterra", "ING", "England" },
                new List<string>() { "0007", "Ira", "IRA", "Iran" },
                new List<string>() { "0008", "Pais de Gales", "PDG", "Wales" },
                new List<string>() { "0009", "Argentina", "ARG", "Argentina" },
                new List<string>() { "0010", "Arabia Saudita", "ARA", "Saudi Arabia" },
                new List<string>() { "0011", "Mexico", "MEX", "Mexico" },
                new List<string>() { "0012", "Polonia", "POL", "Poland" },
                new List<string>() { "0013", "Australia", "AUS", "Australia" },
                new List<string>() { "0014", "Dinamarca", "DIN", "Denmark" },
                new List<string>() { "0015", "Franca", "FRA", "France" },
                new List<string>() { "0016", "Tunisia", "TUN", "Tunisia" },
                new List<string>() { "0017", "Alemanha", "ALE", "Germany" },
                new List<string>() { "0018", "Costa Rica", "COS", "Costa Rica" },
                new List<string>() { "0019", "Espanha", "ESP", "Spain" },
                new List<string>() { "0020", "Japao", "JAP", "Japan" },
                new List<string>() { "0021", "Belgica", "BEL", "Belgium" },
                new List<string>() { "0022", "Canada", "CAN", "Canada" },
                new List<string>() { "0023", "Croacia", "CRO", "Croatia" },
                new List<string>() { "0024", "Marrocos", "MAR", "Morocco" },
                new List<string>() { "0025", "Brasil", "BRA", "Brazil" },
                new List<string>() { "0026", "Camaroes", "CAM", "Cameroon" },
                new List<string>() { "0027", "Suica", "SUI", "Switzerland" },
                new List<string>() { "0028", "Servia", "SER", "Serbia" },
                new List<string>() { "0029", "Coreia do Sul", "COR", "South Korea" },
                new List<string>() { "0030", "Gana", "GAN", "Ghana" },
                new List<string>() { "0031", "Portugal", "POR", "Portugal" },
                new List<string>() { "0032", "Uruguai", "URU", "Uruguay" }
            };

            return teams;
        }

        public static string GetMethodName([CallerMemberName] string methodName = null)
        {
            return methodName;
        }

        public static bool GuidIsValid(Guid guid)
        {
            return guid != Guid.Empty && guid != default;
        }

        public static string GuidValue(string lastFour)
        {
            return $"00000000-0000-0000-0000-00000000{lastFour}";
        }

        public static string LogThis(string text, [CallerMemberName] string methodName = null, [CallerLineNumber] int lineNumber = -1)
        {
            return $"[{methodName} at L{lineNumber}]|{text}";
        }
    }
}
