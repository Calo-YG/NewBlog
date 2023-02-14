using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Y.Module.Modules
{
    public interface IPreInitApplication
    {
        /// <summary>
        /// 预处理初始化程序
        /// </summary>
        /// <param name="context"></param>
        void PreInitApplication(ConfigerServiceContext context);
    }
}
