# `Random` Problems

- Create a console application.

- The problem

  - Create a static `Random` instance.

    ```csharp
    private static readonly Random Random = new();
    ```

  - Create 100.000 tasks that uses the single `Random` instance to generate random numbers.

    ```csharp
    Task[] tasks = Enumerable.Range(0, 100_000)
      .Select(x => Task.Run(() =>
      {
          int number = Random.Next();
          Numbers.Add(number);
      }))
      .ToArray();

    Task.WaitAll(tasks);
    ```
  
  - Save the numbers in a file and open the file to see the problem.

- Solution using `ThreadStatic` attribute.

  - Make the `Random` instance thread-static.

  - Initialize it on each thread, if needed.

    ```csharp
    if (Random == null)
    {
        int threadId = Thread.CurrentThread.ManagedThreadId;
        Console.WriteLine($"Creating new random instance for thread {threadId}...");
    
        Random = new Random();
    }
    ```

- Solution using the `ThreadLocal<T>` class.

  - Replace `Random` instance with `ThreadLocal<Random>`
  - Extract the `Random` instance initialization in a separate method and use it in the `ThreadLocal<T>` initialization.
