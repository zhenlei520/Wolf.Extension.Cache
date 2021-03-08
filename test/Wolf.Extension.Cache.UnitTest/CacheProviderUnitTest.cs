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

        #region 为数字增长1

        [Theory]
        [InlineData("wolf3", 2)]
        [InlineData("test2", 3)]
        public void Increment(string key, long val)
        {
            var ret = this._cacheProvider.Increment(key, val);
        }

        #endregion

        #region 为数字减少1

        /// <summary>
        /// 为数字减少1
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        [Theory]
        [InlineData("wolf3", 2)]
        [InlineData("test2", 3)]
        public void Decrement(string key, long val)
        {
            var ret = this._cacheProvider.Decrement(key, val);
        }

        #endregion

        #region 判断缓存key是否存在

        /// <summary>
        /// 判断缓存key是否存在
        /// </summary>
        /// <param name="key"></param>
        [Theory]
        [InlineData("test2")]
        public void Exist(string key)
        {
            var ret = this._cacheProvider.Exist(key);
        }

        #endregion

        #region 设置过期时间

        /// <summary>
        /// 设置过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="second"></param>
        [Theory]
        [InlineData("test2", 3)]
        public void SetExpire(string key, int second)
        {
            var ret = this._cacheProvider.SetExpire(key, TimeSpan.FromSeconds(second));
        }

        #endregion

        #region 删除指定Key的缓存

        /// <summary>
        /// 删除指定Key的缓存
        /// </summary>
        /// <param name="key"></param>
        [Theory]
        [InlineData("test")]
        public void Remove(string key)
        {
            var ret = this._cacheProvider.Remove(key);
        }

        #endregion

        #region 删除指定的缓存集合

        /// <summary>
        /// 删除指定的缓存集合
        /// </summary>
        /// <param name="keys"></param>
        [Theory]
        [InlineData("test,test3")]
        public void RemoveRange(string keys)
        {
            List<string> keyList = keys.ConvertToList<string>(',');
            var ret = this._cacheProvider.RemoveRange(keyList);
        }

        #endregion

        #region 查找所有符合给定模式( pattern)的 key

        /// <summary>
        /// 查找所有符合给定模式( pattern)的 key
        /// </summary>
        /// <param name="pattern"></param>
        [Theory]
        [InlineData("test")]
        public void Keys(string pattern = "*")
        {
            var ret = this._cacheProvider.Keys(pattern);
        }

        #endregion

        #region MyRegion

        

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
