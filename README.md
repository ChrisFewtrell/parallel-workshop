# parallel-workshop

Some exercises in writing performant parallel code in .Net, with an emphasis
on: -
* Avoiding locks where possible
* Choosing appropriate locks where they are needed

The exercises are in the project ParallelWorkshop, with one exercise per
folder. The ParallelWorkshopTests project contains an equivalent folder for
each exercise, containing a few tests.

Typically, the code in the exercise is part-written and the test(s) show, by
failing, one or more deficiencies of the code written so far. The exercise
is to: -
* Make the test(s) green (without changing test code)
* Get experience of using different classes and techniques for parallel code

Among the .Net framework classes that should be considered are: -

Class | Purpose
----- | -------
Interlocked | Perform simple operations thread-safely, without locks
Volatile | Perform reads and writes thread-safely, without locks
Monitor | The classic .Net lock, used by the `lock` keyword. Allows only a single thread at a time to own the lock. Also allows basic signalling between threads. Supports re-entrance.
Lazy<T> | Initialise a value only when needed, and then only once. Choice of thread-safety levels.
LazyInitializer<T> | Initialise a value only when needed, and then only once. Unlike Lazy<T>, does not involve instantiating an additional object.
ReaderWriterLock and ReaderWriterLockSlim | Lock allowing multiple simultaneous reads, but only one simultaneous write (which cannot coincide with any read). Slower to enter and exit than Monitor, so use only when there is a real benefit to multiple simultaneous reads. Can support re-entrance, at additional cost.
ManualResetEvent | A way to signal events between threads. Has some advantages over Monitor.
ConcurrentDictionary<T>, and other concurrent collections | Thread-safe collections, with special operations targetting parallel usage.
BlockingCollection<T> | Excellent class for producer-consumer pattern.

If you there are any classes in the above list that you have never
encountered before, it would be worth quickly reading their online
documentation.

Each exercise has at least one pre-made possible solution, in a folder
called PossibleSolution. These solutions certainly make the tests go green
but they might not always be the best possible solution. See if you can
do better, in terms of: -
* Performance
* Correctness
* Clarity
