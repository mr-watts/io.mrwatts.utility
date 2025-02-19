# 2025-02-19 | 2.0.1
 - Removed items that were split up to dedicated packages:
   - `io.mrwatts.statekeepers`
   - `io.mrwatts.extensions`
 - Support `ValueTask`s in `QueuedTaskRunner`.

# 2024-11-17 | 2.0.0
 - Add `QueuedTaskRunner` to run tasks sequentially.
 - Drop `WorkQueueHandler` since it was replaced by the `MainThreadDispatcher`.
 - `AsyncVoidMethodFactory` can now also wrap `UnityAction` and `System.Action`.

# 2024-07-17 | 1.0.0
 - `DataStateKeeper` and friends no longer force nullability.
 - `DataStateKeeper` now has a constructor to allow setting the initial value.
 - Refactored method names in `MainThreadDispatcher` to reduce confusion.

# 2024-06-24 | 0.5.0
 - Add proper license file.
 - Fix SelectAsync having non-deterministic or incorrect sorting.
 - Add data state keeper classes to maintain and share global application state through service container.

 # 2023-08-25 | 0.4.3
 - Flexible grid layout now has the options the give the cells a fixed size.

# 2023-08-09 | 0.4.2
 - Extended AsyncVoidMethodFactory with a generic eventargs application

# 2023-06-27 | 0.4.1
 - Corrected access modifier of `SelectAsync` method to be public

# 2023-06-14 | 0.4.0
 - Add `IEnumerable` `ForEach`, `ForEachAsync`, and `SelectAsync`

 # 2023-04-11 | 0.3.2
 - Fixed missing meta files

# 2023-04-11 | 0.3.1
 - Forgot to update Package version and didn't want to delete existing tag

# 2023-03-21 | 0.3.0
 - Added FlexibleGridLayoutGroup

# 2023-03-03 | 0.2.2
 - Fix incorrect namespaces for new platform checking code

 # 2023-03-03 | 0.2.1
 - Extended PlatformChecker

# 2023-02-24 | 0.2.0
 - Added PlatformChecker
 - Sorted scripts in nicely named folders

# 2023-01-25 | 0.1.1
 - Fixed some GUIDS being the same as in Onion, causing conflicts when both packages are used at the same time.

# 2023-01-19 | 0.1.0
 - Removed unused packages
 - Made TransformExtensions public instead of internal class

# 2022-11-21 | 0.0.1
 - First version.