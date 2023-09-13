using System.Collections.Generic;

namespace Model
{
    public class ErrorInfo
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string ErrorMsg { get; set; }
        public string Type { get; set; }
        public List<ErrorDetailInfo> ErrorList { get; set; } = new List<ErrorDetailInfo>();

    }

    public class ErrorDetailInfo
    {
        public string Type { get; set; }
    }
}
