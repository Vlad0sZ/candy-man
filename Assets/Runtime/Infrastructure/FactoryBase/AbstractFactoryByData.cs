using UnityEngine;
using Object = UnityEngine.Object;

namespace Runtime.Infrastructure.FactoryBase
{
    public abstract class AbstractFactoryByData<TIn, TOut> : MonoBehaviour where TOut : Object
    {
        public  abstract TOut Instantiate(TIn data, Transform position, bool worldPositionsStay);
    }

    public abstract class AbstractSequenceFactory<TOut> : MonoBehaviour where TOut : Object
    {
        public  abstract TOut InstantiateNext(Transform position, bool worldPositionsStay);
    }
}