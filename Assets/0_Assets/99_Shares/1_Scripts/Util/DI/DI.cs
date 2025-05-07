using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Util
{
    public static class DI
    {
        private static List<Type> _publishTypes;
        private static List<Type> _subscribeTypes;
        private static Dictionary<Type, Dictionary<Type, MemberInfo>> _subscribingTypes;    // Key: Publisher, Value: Publisher를 구독하는 Subscriber들
        private static Dictionary<Type, Dictionary<Type, MemberInfo>> _subscribedMembers;   // Key: Subscriber, Value: Subscriber에서 구독 중인 Publisher에 대한 MemberInfo

        private static Dictionary<Type, object> _publishers;    // 반드시 하나의 인스턴스만 존재해야 함
        private static Dictionary<Type, object> _subscribers;   // 반드시 하나의 인스턴스만 존재해야 함

        public static void Init()
        {
            _publishers = new Dictionary<Type, object>();
            _subscribers = new Dictionary<Type, object>();

            // 현재 어셈블리에서 모든 타입을 가져옴
            Type[] typesInAssembly = Assembly.GetExecutingAssembly().GetTypes();

            // DIPublisher 또는 DISubscriber 어트리뷰트가 달린 클래스들을 찾음
            _publishTypes = typesInAssembly.Where(t => t.IsClass && t.GetCustomAttribute<DIPublisher>() != null).ToList();
            _subscribeTypes = typesInAssembly.Where(t => t.IsClass && t.GetCustomAttribute<DISubscriber>() != null).ToList();

            // DIPublisher를 주입 받기를 원하는 타입을 찾음 + DISubscriber가 주입 받기를 원하는 멤버 변수 정보를 찾음
            _subscribingTypes = new Dictionary<Type, Dictionary<Type, MemberInfo>>();
            _subscribedMembers = new Dictionary<Type, Dictionary<Type, MemberInfo>>();

            foreach (Type publishType in _publishTypes)
            {
                _subscribingTypes[publishType] = new Dictionary<Type, MemberInfo>();
            }
            foreach (Type subscribeType in _subscribeTypes)
            {
                _subscribedMembers.Add(subscribeType, new Dictionary<Type, MemberInfo>());

                // 필드 중 Inject 어트리뷰트가 달린 필드 찾기
                FieldInfo[] fields = subscribeType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (FieldInfo field in fields)
                {
                    Inject injectInfo = field.GetCustomAttribute<Inject>();
                    if (injectInfo != null)
                    {
                        if (injectInfo.SpecificType != null)
                        {
                            _subscribedMembers[subscribeType].Add(injectInfo.SpecificType, field);
                            _subscribingTypes[injectInfo.SpecificType].Add(subscribeType, field);
                        }
                        else
                        {
                            _subscribedMembers[subscribeType].Add(field.FieldType, field);
                            _subscribingTypes[field.FieldType].Add(subscribeType, field);
                        }
                    }
                }

                // 프로퍼티 중 Inject 어트리뷰트가 달린 프로퍼티 찾기
                PropertyInfo[] properties = subscribeType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (PropertyInfo property in properties)
                {
                    Inject injectInfo = property.GetCustomAttribute<Inject>();
                    if (injectInfo != null)
                    {
                        if (injectInfo.SpecificType != null)
                        {
                            _subscribedMembers[subscribeType].Add(injectInfo.SpecificType, property);
                            _subscribingTypes[injectInfo.SpecificType].Add(subscribeType, property);
                        }
                        else
                        {
                            _subscribedMembers[subscribeType].Add(property.PropertyType, property);
                            _subscribingTypes[property.PropertyType].Add(subscribeType, property);
                        }
                    }
                }
            }
        }

        public static void Register(object target)
        {
            Type type = target.GetType();

            if (_publishTypes.Contains(type)) Publish(target, type);
            if (_subscribeTypes.Contains(type)) Subscribe(target, type);
        }

        private static void Publish(object target, Type type)
        {
            // Publisher로 등록
            _publishers[type] = target;

            // Subscriber로 등록된 것 중에서 현재 주입해줄 수 있는 것이 있는지 확인 후 주입
            foreach (var subscriberInfo in _subscribingTypes[type])
            {
                Type subscribeType = subscriberInfo.Key;

                if (_subscribers.ContainsKey(subscribeType))
                {
                    object subscriber = _subscribers[subscribeType];
                    MemberInfo subscribedMember = subscriberInfo.Value;

                    SetMemberValue(subscriber, subscribedMember, target);
                }
            }
        }

        private static void Subscribe(object target, Type type)
        {
            // Subscriber로 등록
            _subscribers[type] = target;

            // Publisher로 등록된 것 중에서 주입받을 수 있는 것이 있는지 확인 후 주입
            foreach (var subscribedInfo in _subscribedMembers[type])
            {
                Type publishType = subscribedInfo.Key;

                if (_publishers.ContainsKey(publishType))
                {
                    object publisher = _publishers[publishType];
                    MemberInfo subscribedMember = subscribedInfo.Value;

                    SetMemberValue(target, subscribedMember, publisher);
                }
            }
        }

        private static Type GetMemberType(MemberInfo member)
        {
            if (member is FieldInfo field)
            {
                // 필드일 경우 FieldType 반환
                return field.FieldType;
            }
            else if (member is PropertyInfo property)
            {
                // 프로퍼티일 경우 PropertyType 반환
                return property.PropertyType;
            }
            else
            {
                throw new ArgumentException("Member must be of type FieldInfo or PropertyInfo");
            }
        }

        private static void SetMemberValue(object target, MemberInfo member, object value)
        {
            if (member is FieldInfo field)
            {
                // 필드인 경우
                field.SetValue(target, value);
            }
            else if (member is PropertyInfo property)
            {
                // 프로퍼티인 경우
                property.SetValue(target, value);
            }
            else
            {
                throw new ArgumentException("Member must be of type FieldInfo or PropertyInfo");
            }
        }

        public static bool DEBUG_Subscribed(Type type)
        {
            return _subscribers.ContainsKey(type);
        }
        public static bool DEBUG_Published(Type type)
        {
            return _publishers.ContainsKey(type);
        }


    }
}