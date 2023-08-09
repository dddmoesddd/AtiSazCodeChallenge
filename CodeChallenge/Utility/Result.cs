using System.Collections.Generic;

namespace CodeChallenge.Utility
{
    public class Result
    {
        public bool IsSucess { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public List<string> InnerException { get; set; }
        public dynamic ReturnValue { get; set; }
        public long Code { get; set; }
        public long StausCode { get; set; }
    }
}
