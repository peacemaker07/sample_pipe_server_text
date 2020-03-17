using System;
using System.IO;
using System.IO.Pipes;
using System.Text;

class PipeServerText
{
    static void Main()
    {
        using (NamedPipeServerStream pipe = new NamedPipeServerStream("/tmp/app.testpipe"))
        {
            Console.WriteLine("NamedPipeServerStream object created.");

            // Wait for a client to connect
            Console.Write("Waiting for client connection...");
            pipe.WaitForConnection();

            Console.WriteLine("Client connected.");
            try
            {
                Console.Write("Enter text: ");
                var str = Console.ReadLine();
                Console.WriteLine(str);

                var writer = new StreamWriter(pipe);

                writer.Write("{ \"type\": \"app.message\", \"data\": \"" + str + "\" }" + "\f");
                writer.Flush();
                // pipe.WaitForPipeDrain();

            }
            // Catch the IOException that is raised if the pipe is broken
            // or disconnected.
            catch (IOException e)
            {
                Console.WriteLine("ERROR: {0}", e.Message);
            }
        }
    }
}
