# `Random` Problems 

## Description

There are rare occasions when we need to generate random numbers. C# offers a class for generating pseudo-random numbers. I will not explain in this pill what pseudo-random numbers are and how are they generated by C#. Instead, I want to highlight a problem that we may encounter when using the `Random` class: this class is not thread-safe.

This aspect will raise a couple of questions:

## Questions

- What actually happens when we use a Random instance from multiple threads?
- Can we do something to make it thread-safe?
