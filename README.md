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
* Interlocked
* Lazy
* LazyInitializer
* ReaderWriterLock and ReaderWriterLockSlim
* ConcurrentDictionary, and other concurrent collections
* BlockingCollection

If you there are any classes in the above list that you have never
encountered before, it would be worth quickly reading their online
documentation.