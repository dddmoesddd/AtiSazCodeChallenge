using System.Globalization;
using System.Resources;

namespace CodeChallenge.Utility.Resources
{
    public class CodeChallengeMessages
    {
        private static readonly ResourceManager Rm = new("CodeChallenge.Utility.Resources.CodeChallengeResource",
            typeof(CodeChallengeMessages).Assembly);

        public static string? ExceptionInCallingHttpBin =
            string.IsNullOrEmpty(Rm.GetString("ExceptionInCallingHttpBin", CultureInfo.CurrentCulture))
                ? string.Empty
                : Rm.GetString("ExceptionInCallingHttpBin", CultureInfo.CurrentCulture);
        
        public static string? AddDataException =
         string.IsNullOrEmpty(Rm.GetString("AddDataException", CultureInfo.CurrentCulture))
             ? string.Empty
             : Rm.GetString("AddDataException", CultureInfo.CurrentCulture);
        public static string? AddDataSuccess =
  string.IsNullOrEmpty(Rm.GetString("AddDataSuccess", CultureInfo.CurrentCulture))
      ? string.Empty
      : Rm.GetString("AddDataSuccess", CultureInfo.CurrentCulture);
    }
}
