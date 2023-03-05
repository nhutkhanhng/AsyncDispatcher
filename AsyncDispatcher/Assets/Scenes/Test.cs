

using Cysharp.Threading.Tasks;
using MessagePipe;
using UnityEngine;

public class Test : MonoBehaviour
{
    MessagePipeOptions options = new MessagePipeOptions();
    // MessagePipeDiagnosticsInfo diagnosticsInfo;
    FilterAttachedAsyncMessageHandlerFactory asyncHandlerFactory;
    public (IAsyncPublisher<T>, IAsyncSubscriber<T>) CreateAsyncEvent<T>()
    {
        var core = new AsyncMessageBrokerCore<T>(options);
        var publisher = new DisposableAsyncPublisher<T>(core);
        var subscriber = new AsyncMessageBroker<T>(core, asyncHandlerFactory);
        return (publisher, subscriber);
    }

    private void Start()
    {
        asyncHandlerFactory = new FilterAttachedAsyncMessageHandlerFactory(options);

        var Pub_n_Sub = CreateAsyncEvent<int>();

        var pub = Pub_n_Sub.Item1;
        var sub = Pub_n_Sub.Item2;


        sub.Subscribe(async (x, c) => { await UniTask.Yield(); Debug.LogError(x); });

        pub.PublishAsync(10);


    }
}