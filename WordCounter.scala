import scala.collection.immutable.SortedMap
import scala.io.Source
import scala.io.StdIn.readLine
import scala.util.matching.Regex
import scala.concurrent.duration._

object WordCounter {
  def main(args: Array[String]): Unit = {
    val filePath = readLine("Enter file path: ")
    
    val (wordCounts, time, memory) = try {
      measureTimeAndMemory(countWords(Source.fromFile(filePath).getLines()))
    } finally {
      println("File closed.")
    }
    
    printWordCounts(wordCounts)
    
    println(s"Execution time: ${time.toMillis} ms")
    println(s"Memory usage: ${memory / 1000000} MB")
  }

  def countWords(lines: Iterator[String]): SortedMap[String, Int] = {
    val delimiters = Array(' ','\n', '\t')
    val regex = new Regex("\\W+")

    lines.flatMap(line => line.split(delimiters))
         .map(word => regex.replaceAllIn(word.toLowerCase(), ""))
         .filter(word => word.nonEmpty)
         .foldLeft(SortedMap.empty[String, Int])((counts, word) =>
        counts.updated(word, counts.getOrElse(word, 0) + 1))
  }

  def printWordCounts(wordCounts: SortedMap[String, Int]): Unit = {
    for ((word, count) <- wordCounts) {
      println(s"$word: $count")
    }
  }

  def measureTimeAndMemory[T](block: => T): (T, FiniteDuration, Long) = {
    val start = System.nanoTime()
    val result = block
    val time = (System.nanoTime() - start).nanos
    val memory = Runtime.getRuntime.totalMemory() - Runtime.getRuntime.freeMemory()
    (result, time, memory)
  }
}
