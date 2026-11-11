namespace Samples.Plugins.Hooks.TelegramNotifier;

using System;
using System.Collections.Concurrent;
using Sdk.Hooks;
using Sdk.Shared;

/// <summary>
/// Reference production Hook plugin implementing <see cref="IHookManifest"/>.
/// Observes order executions, position entries, and position closures, formatting
/// trade alerts for Telegram broadcast via a thread-safe dispatch outbox.
/// </summary>
public sealed class TelegramNotifierHook : IHookManifest
{
    private static readonly ConcurrentQueue<string> Outbox = new();

    /// <summary>Gets the pending outbox count for verification and testing.</summary>
    public static int PendingCount => Outbox.Count;

    /// <summary>Pops a notification message from the outbox.</summary>
    /// <param name="message">The dequeued notification message.</param>
    /// <returns>True if a message was dequeued; otherwise false.</returns>
    public static bool TryGetNextMessage(out string message) => Outbox.TryDequeue(out message!);

    /// <summary>Clears the outbox messages.</summary>
    public static void ClearOutbox() => Outbox.Clear();

    /// <inheritdoc/>
    public void RegisterHooks(IHookRegistry registry)
    {
        ArgumentNullException.ThrowIfNull(registry);

        // 1. Observe position openings
        registry.Live.OnPositionOpened.Register(OnPositionOpened, priority: 50);

        // 2. Observe position closures
        registry.Live.OnPositionClosed.Register(OnPositionClosed, priority: 50);

        // 3. Observe execution reports (order fills)
        registry.Live.OnOrderExecuted.Register(OnOrderExecuted, priority: 50);
    }

    private static void OnPositionOpened(Position position, IHookContext context)
    {
        var msg = $"[LIVE] Position Opened: {position.Symbol} | Type: {position.Type} | Volume: {position.Volume} | OpenPrice: {position.OpenPrice}";
        Outbox.Enqueue(msg);
    }

    private static void OnPositionClosed(Position position, IHookContext context)
    {
        var msg = $"[LIVE] Position Closed: {position.Symbol} | Type: {position.Type} | ClosePrice: {position.ClosePrice} | Profit: {position.Profit}";
        Outbox.Enqueue(msg);
    }

    private static void OnOrderExecuted(ExecutionReport report, IHookContext context)
    {
        var msg = $"[LIVE] Order Executed: {report.Symbol} | Ticket: {report.Ticket} | Price: {report.ExecutedPrice} | Volume: {report.ExecutedVolume}";
        Outbox.Enqueue(msg);
    }
}
