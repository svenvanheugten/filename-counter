Filename Counter
----------------

Usage: `FilenameCounter [path]`

This program counts how many times a filename (minus the extension) appears in the file's contents.

Additional assumptions:

* The file fits in memory, which makes it OK to use `File.ReadAllText`, rather than having to stream the input.
* Given the path `directory/filename.extension`, we want to search for `filename`, not `directory/filename`.
	* This is covered by the integration test called `Main_WithFileWithExtension_WhichContainsTwoOccurrences_FindsTwoOccurrences`.
* For filenames without an extension, we want to search for the filename as a whole.
	* This is covered by the integration test called `Main_WithFileWithoutExtension_WhichContainsTwoOccurrences_FindsTwoOccurrences`.
* For filenames with two "extensions" (i.e. two dots), we want to search for the bit that precedes the final dot. For example, in a file called `test.exe.txt`, we should search for `test.exe`. 
	* This is covered by the integration test called `Main_WithFileWithTwoExtensions_WhichContainsOneOccurrence_FindsOneOccurrence`.

