using System.Text;
using System;
using System.IO;
using System.IO.Pipes;
using System.Threading.Tasks;

class Program
{
  static async Task Main(string[] args)
  {
    Console.WriteLine("=== C# Streams and I/O Comprehensive Demo ===\n");
    // demo different types of streans and I/O operations
    await RunFileStreamDemo();
    await RunMemoryStreamDemo();
    await RunBufferedStreamDemo();

    Console.WriteLine("\n=== All demos completed! ===");
  }

  // FileStream demo - working with files on disk
  static async Task RunFileStreamDemo()
  {
    Console.WriteLine("1. FileStream Demo");
    Console.WriteLine("-----------------");

    string filePath = "demo.txt";
    string content = "Hello from FileStream!, This is our test data.";

    try
    {
      // writing to file using FileStream
      using (FileStream fs = File.Create(filePath))
      {
        byte[] data = Encoding.UTF8.GetBytes(content);
        await fs.WriteAsync(data, 0, data.Length);
        Console.WriteLine($"✓ Written {data.Length} bytes to {filePath}");
      }

      // reading from file using FileStream with proper chunk reading
      using (FileStream fs = File.OpenRead(filePath))
      {
        Console.WriteLine($"File size: {fs.Length} bytes");
        Console.WriteLine($"Can read: {fs.CanRead}, Can write: {fs.CanWrite}, Can seek: {fs.CanSeek}");

        // proper way to read data in chunks
        byte[] buffer = new byte[1000];
        int bytesRead = 0;
        int chunkSize = 1;

        while (bytesRead < buffer.Length && chunkSize > 0)
        {
          chunkSize = await fs.ReadAsync(buffer, bytesRead, buffer.Length - bytesRead);
          bytesRead += chunkSize;
        }

        string result = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        Console.WriteLine($"✓ Read content: {result}");

        // demonstration seeking
        fs.Seek(0, SeekOrigin.Begin);
        int firstByte = fs.ReadByte();
        Console.WriteLine($"✓ First byte after seek: {firstByte} ('{(char)firstByte}')");
      }

      // cleaning up
      File.Delete(filePath);
    }
    catch (Exception ex)
    {
      Console.WriteLine($"❌ Error in FileStream demo: {ex.Message}");
    }
    Console.WriteLine();
  }

  // MemoryStream demo - working with data in memory
  static async Task RunMemoryStreamDemo()
  {
    Console.WriteLine("2. MemoryStream Demo");
    Console.WriteLine("-------------------");

    try
    {
      using (var ms = new MemoryStream())
      {
        string data = "MemoryStream stores data in RAM, not on disk!";
        byte[] bytes = Encoding.UTF8.GetBytes(data);

        // write to memory stream
        await ms.WriteAsync(bytes, 0, bytes.Length);
        Console.WriteLine($"✓ Written {bytes.Length} bytes to MemoryStream");

        // reset position to beginning
        ms.Position = 0;

        // read back from memory stream
        byte[] readBuffer = new byte[ms.Length];
        int bytesRead = await ms.ReadAsync(readBuffer, 0, readBuffer.Length);
        string result = Encoding.UTF8.GetString(readBuffer, 0, bytesRead);

        Console.WriteLine($"✓ Read back: {result}");
        Console.WriteLine($"Stream properties - Position: {ms.Position}, Length: {ms.Length}");

        // demonstrate random access
        ms.Position = 12;   // jump to middle
        int byteAtPosition = ms.ReadByte();
        Console.WriteLine($"✓ Byte at position 12: {byteAtPosition} ('{(char)byteAtPosition}')");
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine($"❌ Error in MemoryStream demo: {ex.Message}");
    }
    Console.WriteLine();
  }

  // BufferedStream demo - adding buffering to improve performance
  static async Task RunBufferedStreamDemo()
  {
    Console.WriteLine("3. BufferedStream Demo");
    Console.WriteLine("---------------------");

    string filePath = "buffered_demo.txt";

    try
    {
      // Create a file with some data
      await File.WriteAllTextAsync(filePath, "This is test data for BufferedStream demonstration.");

      // Use BufferedStream to wrap FileStream
      using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
      using (var bufferedStream = new BufferedStream(fileStream, bufferSize: 1024))
      {
        Console.WriteLine("✓ Created BufferedStream wrapper around FileStream");
        Console.WriteLine($"Buffer size: 1024 bytes");

        // Read using buffered stream - more effiecient for multiple small reads
        byte[] buffer = new byte[10];
        int totalBytesRead = 0;

        while (true)
        {
          int bytesRead = await bufferedStream.ReadAsync(buffer, 0, buffer.Length);
          if (bytesRead == 0) break;

          totalBytesRead += bytesRead;
          string chunk = Encoding.UTF8.GetString(buffer, 0, bytesRead);
          Console.WriteLine($"Chunk read: '{chunk}'");
        }

        Console.WriteLine($"✓ Total bytes read through buffer: {totalBytesRead}");
      }
      File.Delete(filePath);

    }
    catch (Exception ex)
    {
      Console.WriteLine($"❌ Error in BufferedStream demo: {ex.Message}");
    }
    Console.WriteLine();
  }
}