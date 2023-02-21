# Solution description:

I wrote two WordCounter that are sharing the common ground logic: a prompt asks for the file, read it line by line, and split each line into words. Then remove any non-alphabetic chars and lowercase each word before counting their occurrences. 
Finally, both are printing the sorted word counts as well as execution time and memory usage.

## How to use:

In a terminal, navigate to the correct directory and run:
- For C#: .\WordCounter.exe
- For Scala: scala WordCounter.scala

## Key assumptions made:

The file is processed line by line, so its size is not a concern for memory.
The regex expression matches non-Latin characters, and words are case-insensitive.
For testing convenience, I chose to reorder words alphabetically.

## Test files description:

- SCTest.txt: string sent together with the assignment
- alphanumeric.txt: mix of words, numbers and a set of special characters
- nonLatin.txt: few sentences using non-Latin chars.
- performanceBenchmark.txt: a large file of 11.6 MB used to roughly compare performances between the Scala and C# implementations.

A note on regex discrepancy, I tried different regex expression in Scala to find one that can effectively match nonLatin chars, as it's happening for C#, and succefully pass nonLatin.txt test. The words are correctly split but only nonLatin chars are changed into ã or â. 

## Time and Space complexity:
Given n as the total number of words:
- Time Complexity: O(nlog(n)). The main time-consuming operation is the sorting of the word counts in alphabetical order.
- Space Complexity: O(n). This is because the program stores each word and its count. 
