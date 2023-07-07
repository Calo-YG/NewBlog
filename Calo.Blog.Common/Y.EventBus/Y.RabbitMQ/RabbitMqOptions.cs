namespace Calo.Blog.Common.Y.EventBus.Y.RabbitMQ
{
    public class RabbitMqOptions
    {
        /// <summary>
        /// ip地址
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 交换机路由
        /// </summary>
        public string ExchangeName { get; set; }
    }
}