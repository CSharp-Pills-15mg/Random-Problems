# The Bad Twin

## Problem Description

When generating a `Random` instances without providing a seed, it ends up using the timestamp of the system as seed.

Now, imagine that two instances of the `Random` class are created in the same time. They will use the same timestamp of the system as seed and both will generate the same sequence of "random" numbers.

How do we fix this problem?

## Solution 1

One solution would be to ensure that, in our applications, we never generate two `Random` instances at the same time. Correct?

We can a delay between each `Random` instance creation. Probably a factory class to handle the delay action.

### Evaluation

- **Pros**: Each instance is guaranteed to have random values different from other instances.

- **Cons**: ???

## Solution 2

Another solution would be to use only one instance of the `Random` class. We can even make it static. Singleton Pattern.

```csharp
internal class SomeClass
{
	private static readonly Random Random = new Random();
    
    public void SomeMethod()
    {
        int value = Rasdom.Next();
    }
}
```

 If there is only one instance, there cannot be any problems, isn't it? Wrong.

**Problem**:

- What if the instance is accessed from multiple threads? Unfortunately, the `Random` class is not thread safe.

**Solution**:

- Use a synchronization mechanism like a "lock" block.

```csharp
internal class SomeClass
{
	private static readonly Random Random = new Random();
    
    public void SomeMethod()
    {
        int value;
        
        lock(Random)
        {
            value = Rasdom.Next();
        }
    }
}
```

Problem solved... or... is it?

**Problem**:

- Now the Random instance become a bottleneck. It is slow to be accessed.

**Solution**:

- Unfortunately, none... Actually, there is another way, but let's consider for now this solution. It solves the problem after all even if it is not the most performant one. Let's make a short evaluation of it.

### Evaluation

- **Pros**: Each thread is guaranteed to have random values different from other threads.

- **Cons**: It is slow. Each thread must wait after all the other threads in order to generate the next random number.

## Solution 2'

A small twist:

- Let's use the static single instance from the previous solution, to generate a new seed each time we need a new `Random` instance. In this way we can generate a new, different `Random`, on each thread we work.

### Evaluation

- **Pros**: It is faster than the previous solution.
- **Cons** There still exists a very low probability for two identical seeds to be generated. The probability is sufficiently low though and it is acceptable.
  - Probability of collisions: 1 in `Int32.MaxValue`. That is 1 in 2.147.483.647