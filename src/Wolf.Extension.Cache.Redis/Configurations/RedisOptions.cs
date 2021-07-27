// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Wolf.Extension.Cache.Redis.Configurations
{
    /// <summary>
    /// Redis缓存配置
    /// </summary>
    public class RedisOptions
    {
        /// <summary>
        /// 初始化Redis配置（推荐）
        /// </summary>
        /// <param name="ip">Ip地址</param>
        /// <param name="port">端口（默认端口3306）</param>
        /// <param name="password">密码（默认密码为空）</param>
        public RedisOptions(string ip, int port = 3306, string password = "") : this(ip, port, password, "", 0, 50,
            false)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ip">Ip地址</param>
        /// <param name="port">端口</param>
        /// <param name="password">密码</param>
        /// <param name="prefix">前缀</param>
        /// <param name="dataBase">储存的数据库索引</param>
        /// <param name="poolSize">Redis连接池连接数</param>
        /// <param name="ssl">Enable encrypted transmission</param>
        public RedisOptions(string ip, int port, string password, string prefix, int dataBase, int poolSize,
            bool ssl = false)
        {
            this.Prefix = prefix;
            this.Ip = ip;
            this.Port = port;
            this.Password = password;
            this.DataBase = dataBase;
            this.PoolSize = poolSize;
            this.Ssl = ssl;
        }

        /// <summary>
        /// 前缀
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// Ip地址
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 储存的数据库索引
        /// </summary>
        public int DataBase { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Redis连接池连接数
        /// </summary>
        public int PoolSize { get; set; }

        /// <summary>
        /// Enable encrypted transmission（默认false）
        /// </summary>
        public bool Ssl { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return
                $"{this.Ip}:{this.Port},password={this.Password},defaultDatabase={this.DataBase},prefix={this.Prefix},ssl={this.Ssl}";
        }

        #region methods

        #region 设置Redis前缀

        /// <summary>
        /// 设置Redis前缀
        /// </summary>
        /// <param name="prefix">前缀</param>
        /// <returns></returns>
        public RedisOptions SetPrefix(string prefix)
        {
            this.Prefix = prefix;
            return this;
        }

        #endregion

        #region 设置数据库

        /// <summary>
        /// 设置数据库
        /// </summary>
        /// <param name="dataBase">数据库下标</param>
        /// <returns></returns>
        public RedisOptions SetDataBase(int dataBase)
        {
            this.DataBase = dataBase;
            return this;
        }

        #endregion

        #region 设置线程池大小

        /// <summary>
        /// 设置线程池大小
        /// </summary>
        /// <param name="poolSize">线程池大小</param>
        /// <returns></returns>
        public RedisOptions SetPoolSize(int poolSize)
        {
            this.PoolSize = poolSize;
            return this;
        }

        #endregion

        #region 启用加密传输

        /// <summary>
        /// 启用加密传输
        /// </summary>
        /// <returns></returns>
        public RedisOptions EnableSsl()
        {
            this.Ssl = true;
            return this;
        }

        #endregion

        #endregion
    }
}
