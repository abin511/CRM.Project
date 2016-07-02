namespace CRM.Model
{
    public class Result<T>
    {
        public Result()
        {
            this.Code = ResultEnum.Error;
            this.Msg = null;
        }
        /// <summary>
        /// 返回的错误标识 0错误 1正确
        /// </summary>
        public ResultEnum Code { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 返回的数据
        /// </summary>
        public T Data { get; set; }
    }
}
