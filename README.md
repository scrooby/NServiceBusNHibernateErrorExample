Sample solution to replicate the NHibernate error being thrown.

There should just be two things needed to get the solution running:

1. Update database connection strings in both projects
2. Enter a valid NServiceBus licence key in both projects

The solution has two tests:

1. Scenario: Start saga
2. Scenario: Start saga from failure event

Running `Start saga` should pass.

Running `Start saga from failure event` should fail and put a message on the error queue:

```
NHibernate.StaleObjectStateException: Row was updated or deleted by another transaction (or unsaved-value mapping was incorrect): [Saga.Host.SagaData.TestSagaData#1702fd3a-469f-473f-a86b-e40ce3b49c09]
   at NHibernate.Persister.Entity.AbstractEntityPersister.Update(Object id, Object[] fields, Object[] oldFields, Object rowId, Boolean[] includeProperty, Int32 j, Object oldVersion, Object obj, SqlCommandInfo sql, ISessionImplementor session)
   at NHibernate.Persister.Entity.AbstractEntityPersister.UpdateOrInsert(Object id, Object[] fields, Object[] oldFields, Object rowId, Boolean[] includeProperty, Int32 j, Object oldVersion, Object obj, SqlCommandInfo sql, ISessionImplementor session)
   at NHibernate.Persister.Entity.AbstractEntityPersister.Update(Object id, Object[] fields, Int32[] dirtyFields, Boolean hasDirtyCollection, Object[] oldFields, Object oldVersion, Object obj, Object rowId, ISessionImplementor session)
   at NHibernate.Action.EntityUpdateAction.Execute()
   at NHibernate.Engine.ActionQueue.Execute(IExecutable executable)
   at NHibernate.Engine.ActionQueue.ExecuteActions(IList list)
   at NHibernate.Engine.ActionQueue.ExecuteActions()
   at NHibernate.Event.Default.AbstractFlushingEventListener.PerformExecutions(IEventSource session)
   at NHibernate.Event.Default.DefaultFlushEventListener.OnFlush(FlushEvent event)
   at NHibernate.Impl.SessionImpl.Flush()
   at NServiceBus.Persistence.NHibernate.NHibernateLazyAmbientTransactionSynchronizedStorageSession.Dispose() in C:\BuildAgent\work\5135de308b2f3016\src\NServiceBus.NHibernate\SynchronizedStorage\NHibernateAmbientTransactionSynchronizedStorageSession.cs:line 30
   at NServiceBus.LoadHandlersConnector.&lt;Invoke&gt;d__1.MoveNext() in C:\BuildAgent\work\3206e2123f54fce4\src\NServiceBus.Core\Pipeline\Incoming\LoadHandlersConnector.cs:line 50
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at NServiceBus.InvokeSagaNotFoundBehavior.&lt;Invoke&gt;d__0.MoveNext() in C:\BuildAgent\work\3206e2123f54fce4\src\NServiceBus.Core\Sagas\InvokeSagaNotFoundBehavior.cs:line 16
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at NServiceBus.DeserializeLogicalMessagesConnector.&lt;Invoke&gt;d__1.MoveNext() in C:\BuildAgent\work\3206e2123f54fce4\src\NServiceBus.Core\Pipeline\Incoming\DeserializeLogicalMessagesConnector.cs:line 31
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at NServiceBus.SubscriptionReceiverBehavior.&lt;Invoke&gt;d__1.MoveNext() in C:\BuildAgent\work\3206e2123f54fce4\src\NServiceBus.Core\Routing\MessageDrivenSubscriptions\SubscriptionReceiverBehavior.cs:line 29
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at NServiceBus.ProcessingStatisticsBehavior.&lt;Invoke&gt;d__0.MoveNext() in C:\BuildAgent\work\3206e2123f54fce4\src\NServiceBus.Core\Performance\Statistics\ProcessingStatisticsBehavior.cs:line 27
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at NServiceBus.ReceivePerformanceDiagnosticsBehavior.&lt;Invoke&gt;d__2.MoveNext() in C:\BuildAgent\work\3206e2123f54fce4\src\NServiceBus.Core\Performance\Statistics\ReceivePerformanceDiagnosticsBehavior.cs:line 40
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at NServiceBus.TransportReceiveToPhysicalMessageProcessingConnector.&lt;Invoke&gt;d__1.MoveNext() in C:\BuildAgent\work\3206e2123f54fce4\src\NServiceBus.Core\Pipeline\Incoming\TransportReceiveToPhysicalMessageProcessingConnector.cs:line 37
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at NServiceBus.MainPipelineExecutor.&lt;Invoke&gt;d__1.MoveNext() in C:\BuildAgent\work\3206e2123f54fce4\src\NServiceBus.Core\Pipeline\MainPipelineExecutor.cs:line 32
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at NServiceBus.ReceiveStrategy.&lt;TryProcessMessage&gt;d__7.MoveNext() in C:\BuildAgent\work\3206e2123f54fce4\src\NServiceBus.Core\Transports\Msmq\ReceiveStrategy.cs:line 109
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at NServiceBus.TransactionScopeStrategy.&lt;ProcessMessage&gt;d__2.MoveNext() in C:\BuildAgent\work\3206e2123f54fce4\src\NServiceBus.Core\Transports\Msmq\TransactionScopeStrategy.cs:line 86
```
