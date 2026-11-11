namespace Samples.UnitTests;

using System;
using System.Collections.Generic;
using Samples.Plugins.Hooks.TelegramNotifier;
using Sdk.Hooks;
using Sdk.Shared;
using Xunit;

public class TelegramNotifierHookTests
{
    [Fact]
    public void RegisterHooks_ThrowsOnNullRegistry()
    {
        var hook = new TelegramNotifierHook();
        Assert.Throws<ArgumentNullException>(() => hook.RegisterHooks(null!));
    }

    [Fact]
    public void RegisterHooks_And_ProcessEvents_EnqueuesFormattedAlerts()
    {
        TelegramNotifierHook.ClearOutbox();
        Assert.Equal(0, TelegramNotifierHook.PendingCount);

        var hook = new TelegramNotifierHook();
        var fakeRegistry = new FakeHookRegistry();
        hook.RegisterHooks(fakeRegistry);

        // 1. Trigger Position Opened
        var posOpen = new Position
        {
            Ticket = 1001,
            Symbol = "BTCUSDT",
            Type = OrderType.Buy,
            Volume = 0.5,
            OpenPrice = 65000.0,
        };
        fakeRegistry.FakeLive.TriggerPositionOpened(posOpen);

        Assert.Equal(1, TelegramNotifierHook.PendingCount);
        Assert.True(TelegramNotifierHook.TryGetNextMessage(out var msg1));
        Assert.Contains("Position Opened", msg1);
        Assert.Contains("BTCUSDT", msg1);
        Assert.Contains("65000", msg1);

        // 2. Trigger Order Executed
        var execReport = new ExecutionReport
        {
            Ticket = 1002,
            Symbol = "ETHUSDT",
            Type = OrderType.Sell,
            ExecutedPrice = 3500.0,
            ExecutedVolume = 2.0,
        };
        fakeRegistry.FakeLive.TriggerOrderExecuted(execReport);

        Assert.Equal(1, TelegramNotifierHook.PendingCount);
        Assert.True(TelegramNotifierHook.TryGetNextMessage(out var msg2));
        Assert.Contains("Order Executed", msg2);
        Assert.Contains("ETHUSDT", msg2);
        Assert.Contains("3500", msg2);

        // 3. Trigger Position Closed
        var posClose = new Position
        {
            Ticket = 1001,
            Symbol = "BTCUSDT",
            Type = OrderType.Buy,
            ClosePrice = 66000.0,
            Profit = 500.0,
        };
        fakeRegistry.FakeLive.TriggerPositionClosed(posClose);

        Assert.Equal(1, TelegramNotifierHook.PendingCount);
        Assert.True(TelegramNotifierHook.TryGetNextMessage(out var msg3));
        Assert.Contains("Position Closed", msg3);
        Assert.Contains("Profit: 500", msg3);

        TelegramNotifierHook.ClearOutbox();
        Assert.Equal(0, TelegramNotifierHook.PendingCount);
    }

    private sealed class FakeHookRegistry : IHookRegistry
    {
        public FakeLiveHooks FakeLive { get; } = new();

        public IBacktestHooks Backtest => throw new NotImplementedException();
        public ILiveHooks Live => FakeLive;
        public IOptimizationHooks Optimization => throw new NotImplementedException();
        public IReportHooks Report => throw new NotImplementedException();

        public void ClearAll() { }
    }

    private sealed class FakeLiveHooks : ILiveHooks
    {
        private readonly FakeActionRegistration<Position> _onPositionOpened = new();
        private readonly FakeActionRegistration<Position> _onPositionClosed = new();
        private readonly FakeActionRegistration<ExecutionReport> _onOrderExecuted = new();

        public IActionRegistration OnStart => throw new NotImplementedException();
        public IFilterRegistration<Tick> OnTickReceived => throw new NotImplementedException();
        public IActionRegistration<Tick> OnTickProcessed => throw new NotImplementedException();
        public IFilterRegistration<AdapterOrderRequest> OnOrderValidation => throw new NotImplementedException();
        public IFilterRegistration<AdapterOrderRequest> OnOrderBeforeSend => throw new NotImplementedException();
        public IActionRegistration<ExecutionReport> OnOrderExecuted => _onOrderExecuted;
        public IActionRegistration<(AdapterOrderRequest Request, string Reason)> OnOrderRejected => throw new NotImplementedException();
        public IActionRegistration<Position> OnPositionOpened => _onPositionOpened;
        public IActionRegistration<Position> OnPositionClosed => _onPositionClosed;
        public IActionRegistration<Position> OnPositionStopout => throw new NotImplementedException();
        public IActionRegistration OnSyncBefore => throw new NotImplementedException();
        public IActionRegistration OnSyncAfter => throw new NotImplementedException();
        public IActionRegistration<int> OnReconnectAttempt => throw new NotImplementedException();
        public IActionRegistration<int> OnReconnectSuccess => throw new NotImplementedException();
        public IActionRegistration OnStop => throw new NotImplementedException();
        public IActionRegistration<EquitySnapshot> OnEquityChanged => throw new NotImplementedException();

        public void TriggerPositionOpened(Position p) => _onPositionOpened.Invoke(p);
        public void TriggerPositionClosed(Position p) => _onPositionClosed.Invoke(p);
        public void TriggerOrderExecuted(ExecutionReport r) => _onOrderExecuted.Invoke(r);
    }

    private sealed class FakeActionRegistration<T> : IActionRegistration<T>
    {
        private readonly List<Action<T, IHookContext>> _callbacks = new();

        public void Register(Action<T, IHookContext> callback, int priority = 100)
        {
            _callbacks.Add(callback);
        }

        public void Invoke(T data)
        {
            foreach (var cb in _callbacks)
            {
                cb(data, new FakeHookContext());
            }
        }
    }

    private sealed class FakeHookContext : IHookContext
    {
        public string HookName => "TestHook";
        public DateTime UtcNow => DateTime.UtcNow;
        public CancellationToken CancellationToken => CancellationToken.None;
    }
}
