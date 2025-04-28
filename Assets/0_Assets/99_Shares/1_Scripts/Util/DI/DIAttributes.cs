using System;

namespace Util
{
    /// <summary>
    /// DIPublisher를 주입 받는 역할.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class DISubscriber : Attribute
    {

    }

    /// <summary>
    /// 자신을 DISubsciber에게 뿌려서 주입해주는 역할.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class DIPublisher : Attribute
    {

    }

    /// <summary>
    /// DIPublisher를 여따가 넣으면 된다고 표시하는 역할.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class Inject : Attribute
    {
        /// <summary>
        /// 특정 타입으로 주입받고 싶은 경우 사용.
        /// </summary>
        public Type SpecificType;

        public Inject(Type specificType = null)
        {
            SpecificType = specificType;
        }
    }
}