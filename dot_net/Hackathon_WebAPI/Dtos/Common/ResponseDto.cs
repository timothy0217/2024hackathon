using Hackathon_WebAPI.Enums;
using Hackathon_WebAPI.Extenstions;

namespace Hackathon_WebAPI.Dtos.Common
{
    /// <summary>預設成功與空訊息</summary>
    public class ResponseDto
    {
        public ResponseDto()
        {
            RtnCode = ResultEnum.Success;

            RtnMsg = "";

            IsSuccess = true;
        }

        /// <summary>回傳碼與回傳碼預設訊息</summary>
        /// <param name="result">回傳碼</param>
        public ResponseDto(ResultEnum result)
        {
            RtnCode = result;

            RtnMsg = result.ToDescription();
        }

        public ResponseDto(ResultEnum result, bool Issuccess)
        {
            RtnCode = result;

            RtnMsg = result.ToDescription();

            IsSuccess = Issuccess;
        }

        /// <summary>回傳碼與自訂訊息</summary>
        /// <param name="result">回傳碼</param>
        /// <param name="message">自訂訊息</param>
        public ResponseDto(ResultEnum result, string message)
        {
            RtnCode = result;

            RtnMsg = message;
        }
        public ResponseDto(ResultEnum result, string message, bool isSuccess)
        {
            RtnCode = result;

            RtnMsg = message;

            IsSuccess = isSuccess;
        }

        /// <summary>回傳碼</summary>
        public ResultEnum RtnCode { get; set; }

        /// <summary>回傳訊息</summary>
        public string RtnMsg { get; set; }

        /// <summary>成功與否(bool)</summary>
        public bool IsSuccess { get; set; }
    }

    /// <summary>須用到Model</summary>
    public class ResponseDto<T> : ResponseDto
    {
        /// <summary>回傳碼與回傳碼預設訊息</summary>
        public ResponseDto() : base() { }

        /// <summary>訊息預設</summary>
        /// <param name="result"></param>
        public ResponseDto(ResultEnum result) : base(result)
        {

        }

        /// <summary>回傳資料，回傳碼與回傳碼預設訊息</summary>
        /// <param name="info"></param>
        /// <param name="result"></param>
        public ResponseDto(T info, ResultEnum result) : base(result)
        {
            Info = info;
        }

        /// <summary></summary>
        /// <param name="result"></param>
        /// <param name="message"></param>
        public ResponseDto(ResultEnum result, string message) : base(result, message)
        {

        }

        /// <summary>回傳資料，回傳碼與自訂訊息</summary>
        /// <param name="info"></param>
        /// <param name="result"></param>
        public ResponseDto(T info, ResultEnum result, string message) : base(result, message)
        {
            Info = info;
        }
        public ResponseDto(T info, ResultEnum result, string message, bool isSuccess) : base(result, message, isSuccess)
        {
            Info = info;
        }

        public ResponseDto(T info, ResultEnum result, bool isSuccess) : base(result, isSuccess)
        {
            Info = info;
        }

        /// <summary>回傳資料</summary>
        public T Info { get; set; }
    }
}
