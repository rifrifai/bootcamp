// file handling comprehensive demo
// this project demonstrates all file handling concepts using System.IO namespace

using System.Text;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("=== C# File Handling Comprehensive Demo ===\n");

    try
    {
      // core file handling concepts
      RunFileStreamDemo();
    }
    catch (Exception ex)
    {
      Console.WriteLine($"\n❌ An unexpected error occurred: {ex.Message}");
      Console.WriteLine($"Stack trace: {ex.StackTrace}");
    }
  }

  // FileStream demo - Low level file operations with complete control
  static void RunFileStreamDemo()
  {
    Console.WriteLine("1. FileStream Operations Demo");
    Console.WriteLine("============================");

    // file paths for demonstration
    string filePath = "filestream_demo.txt";
    string content = "FileStream provides complete control over file operations!";

    DemoFileCreation(filePath);
    DemoFileWriting(filePath, content);
    DemoFileReading(filePath);
    DemoFileModesAndAccess();

    // clean up
    if (File.Exists(filePath))
      File.Delete(filePath);

    Console.WriteLine();
  }

  // demo file creation using FileSteam
  static void DemoFileCreation(string filePath)
  {
    Console.WriteLine("File Creation with FileStream: ");

    try
    {
      // create file using FileStream constructor
      using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
      {
        Console.WriteLine($"✓ File '{filePath}' created successfully");
        Console.WriteLine($"  Can Read: {fileStream.CanRead}");
        Console.WriteLine($"  Can Write: {fileStream.CanWrite}");
        Console.WriteLine($"  Can Seek: {fileStream.CanSeek}");

        // important: always close or use 'using' to release resources
        // fileStream.Close(); // not need with 'using' statement
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine($"❌ Error creating file: {ex.Message}");
    }
  }

  // demo writing to file using FileStream
  static void DemoFileWriting(string filePath, string content)
  {
    Console.WriteLine("\nFile Writing with FileStream:");

    try
    {
      // open file for appending data
      using (FileStream fileStream = new FileStream(filePath, FileMode.Append))
      {
        // convert string to byte array for writing
        byte[] data = Encoding.UTF8.GetBytes(content);

        // write data to file
        fileStream.Write(data, 0, data.Length);

        // ensure all data is written to disk (even if buffer isn't full)
        fileStream.Flush();

        Console.WriteLine($"✓ Written {data.Length} bytes to file");
        Console.WriteLine($"  Content: {content}");
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine($"❌ Error writing to file: {ex.Message}");
    }
  }

  // demo reading from file using FileStream
  static void DemoFileReading(string filePath)
  {
    Console.WriteLine("\nFile Reading with FileStream:");

    try
    {
      string readContent;

      // open file for reading only
      using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
      {
        // using StreamReader for easier text reading
        using (StreamReader reader = new StreamReader(fileStream))
        {
          readContent = reader.ReadToEnd();
        }
      }

      Console.WriteLine("✓ Read content: {readContent}");
    }
    catch (FileNotFoundException)
    {
      Console.WriteLine("❌ File not found for reading");
    }
    catch (Exception ex)
    {
      Console.WriteLine($"❌ Error reading file: {ex.Message}");
    }
  }

  // demo different FileMode, FileAccess, and FileShare options
  static void DemoFileModesAndAccess()
  {
    Console.WriteLine("\nFileMode, FileAccess & FileShare Demo:");

    string testFile = "modes_dmeo.txt";

    try
    {
      // FileMode.CreateNew - fails if file exists
      Console.WriteLine("Testing FileMode options: ");

      using (var fs1 = new FileStream(testFile, FileMode.CreateNew))
      {
        byte[] data = Encoding.UTF8.GetBytes("CreateNew mode test");
        fs1.Write(data, 0, data.Length);
        Console.WriteLine("✓ FileMode.CreateNew - Created new file");
      }

      // FileMode.Open - opens existing file
      using (var fs2 = new FileStream(testFile, FileMode.Open, FileAccess.Read))
      {
        Console.WriteLine("✓ FileMode.Open - Opened existing file for reading");
      }

      // FileMode.Append - opens for writing at end
      using (var fs3 = new FileStream(testFile, FileMode.Append))
      {
        byte[] appendData = Encoding.UTF8.GetBytes(" + appended text");
        fs3.Write(appendData, 0, appendData.Length);
        Console.WriteLine("✓ FileMode.Append - Appended data to file");
      }

      // FileMode.Truncate - opens existing file and truncates to 0 bytes
      using (var fs4 = new FileStream(testFile, FileMode.Truncate))
      {
        Console.WriteLine("✓ FileMode.Truncate - Truncated file to 0 bytes");
      }

      // clean up
      File.Delete(testFile);
    }
    catch (Exception ex)
    {
      Console.WriteLine($"❌ Error in FileMode demo: {ex.Message}");
    }
  }

}