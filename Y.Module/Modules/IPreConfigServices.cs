namespace Y.Module.Modules
{
    public interface IPreConfigServices
    {
        /// <summary>
        /// 预处理初始化程序
        /// </summary>
        /// <param name="context"></param>
        void PreConfigerService(ConfigerServiceContext context);
    }
}
