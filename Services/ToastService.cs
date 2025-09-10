using System;
using System.Collections.Generic;
using System.Timers;

public class ToastService
{
  public record Toast(string Message, string? Icon, Guid Id);

  public List<Toast> Items { get; } = new();
  public event Action? Changed;

  public void Success(string message) => Push(message, "bi-check-circle");
  public void Error(string message) => Push(message, "bi-exclamation-octagon");
  public void Info(string message) => Push(message, "bi-info-circle");

  private void Push(string message, string? icon)
  {
    var id = Guid.NewGuid();
    Items.Add(new Toast(message, icon, id));
    Changed?.Invoke();

    var timer = new System.Timers.Timer(2500) { AutoReset = false };
    timer.Elapsed += (_,__) => {
      Items.RemoveAll(t => t.Id == id);
      Changed?.Invoke();
      timer.Dispose();
    };
    timer.Start();
  }
}
