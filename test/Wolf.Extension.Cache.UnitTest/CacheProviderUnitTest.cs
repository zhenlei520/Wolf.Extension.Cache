using System;
using System.Collections.Generic;
using System.Linq;
using Wolf.Extension.Cache.Abstractions.Configurations;
using Wolf.Extension.Cache.Abstractions.Enum;
using Wolf.Extension.Cache.Abstractions.Request.Base;
using Wolf.Extension.Cache.UnitTest.Base;
using Wolf.Systems.Core;
using Xunit;

namespace Wolf.Extension.Cache.UnitTest
{
    /// <summary>
    ///
    /// </summary>
    public class CacheProviderUnitTest : BaseUnitTest
    {
        #region 设置缓存

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        [Theory]
        [InlineData("test", "1")]
        public void Set(string key, string value)
        {
            base._cacheProvider.Set(key, value, new BasePersistentOps());
        }

        #endregion

        #region 批量设置缓存

        /// <summary>
        /// 批量设置缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        [Theory]
        [InlineData("test,test2", "1,2")]
        public void SetRange(string key, string value)
        {
            List<BaseRequest<string>> list = new List<BaseRequest<string>>();
            for (int i = 0; i < key.ConvertToList<string>(',').Count(); i++)
            {
                list.Add(new BaseRequest<string>()
                {
                    Key = key.Split(',')[i],
                    Value = value.Split(',')[i]
                });
            }

            var ret = base._cacheProvider.Set(list, new PersistentOps());
        }

        #endregion

        #region 设置缓存

        /// <summary>
        /// 设置缓存
        /// </summary>
        [Fact]
        public void SetObject()
        {
            User user = new User()
            {
                Id = 1,
                Name = "张三",
                Sex = true
            };
            var res = base._cacheProvider.Set("test", user);
        }

        #endregion

        #region 获取缓存的值

        /// <summary>
        /// 获取缓存的值
        /// </summary>
        /// <param name="key">缓存键</param>
        [Theory]
        [InlineData("test")]
        public void Get(string key)
        {
            var ret = base._cacheProvider.Get(key);
        }

        #endregion

        #region 获取缓存的值

        /// <summary>
        /// 获取缓存的值
        /// </summary>
        /// <param name="key">缓存键</param>
        [Theory]
        [InlineData("test,test2")]
        public void GetRange(string keys)
        {
            var ret = base._cacheProvider.Get(keys.ConvertToList<string>(','));
        }

        #endregion

    }

    public class User
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public bool Sex { get; set; }
    }
}
