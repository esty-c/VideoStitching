using System;

namespace VideoStitching
{
    internal class Program
    {
        private static string bashPath;

        private static void Main(string[] args)
        {
            bashPath = Environment.CurrentDirectory.Split(new String[] { "bin" }, StringSplitOptions.None)[0];
            Stitch();
       
        }

        private static void Stitch()
        {
            // string path = bashPath + "/Videos";
            var source_one = bashPath + "Videos/sample3.mp4";
            var source_two = bashPath + "Videos/sample1.mp4";
            var output = bashPath + "Videos/output.mkv";

            System.Diagnostics.Process proc = new System.Diagnostics.Process();

            proc.StartInfo.Arguments = string.Format(" -i {0} -i {1} -filter_complex \"[0:v:0]pad=iw*2:ih[int];[int][1:v:0] overlay=W/2:0[vid]\"  -map [vid]  -c:v libx264  -crf 23 -preset veryfast  {2}", source_one, source_two, output);

            proc.StartInfo.FileName = bashPath + "Encoder/bin/ffmpeg.exe";

            proc.Exited += new EventHandler(closing_event);
            proc.StartInfo.UseShellExecute = false;
            proc.Start();
        }

        private static void closing_event(object sender, EventArgs e)
        {
            Console.WriteLine("complete");
        }
    }
}