namespace Y.SqlsugarRepository.DatabaseConext
{
    public class DbConfigureOptions
    {
        /// <summary>
        /// 是否启用aop打印sql
        /// </summary>
        public bool EnableAopLog { get; set; }
        /// <summary>
        /// 是否启用打印错误信息
        /// </summary>
        public bool EnableAopError { get; set; }
    }
}
