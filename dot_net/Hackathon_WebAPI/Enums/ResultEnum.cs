using System.ComponentModel;

namespace Hackathon_WebAPI.Enums
{
    public enum ResultEnum
    {
        ///<summar>成功</summar>
        [Description("成功")]
        Success = 0,

        /// <summar>Catch錯誤</summar>
        [Description("Catch錯誤")]
        CatchError = 9999
    }
}
