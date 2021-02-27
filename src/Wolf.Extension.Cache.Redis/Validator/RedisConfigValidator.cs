// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using FluentValidation;

namespace Wolf.Extension.Cache.Redis.Validator
{
    /// <summary>
    /// Redis配置校验
    /// </summary>
    public class RedisConfigValidator : AbstractValidator<RedisConfig>, IFluentlValidator<RedisConfig>
    {
        /// <summary>
        ///
        /// </summary>
        public RedisConfigValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Ip).NotNull()
                .WithMessage("Redis主机信息异常");
            RuleFor(x => x.Port).NotEqual(0)
                .WithMessage("Redis端口信息异常");
            RuleFor(x => x.Timer).Must(x => x > 0)
                .WithMessage("Redis时间信息异常");
        }
    }
}
