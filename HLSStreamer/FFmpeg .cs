using System.Diagnostics;

namespace HLSStreamer
{
    public class FFmpeg
    {
        string _inputFilePath;
        string _outputFilePath;
        string _arguments;
        public FFmpeg(string inputFilePath, string outputFilePath)
        {
            // Define the input and output file paths
            _inputFilePath = inputFilePath;
            _outputFilePath = outputFilePath;

            // Set the FFmpeg command arguments || You can change the resolution from 640x360 to anything you wants.
            _arguments = $"-i \"{_inputFilePath}\" -profile:v baseline -level 3.0 -s 640x360 -start_number 0 -hls_time 10 -hls_list_size 0 -f hls \"{_outputFilePath}\"";
        }

        public string ConvertMp4Tom3u8()
        {
            // Start the FFmpeg process
            Process ffmpeg = new();
            ffmpeg.StartInfo.FileName = @"C:\Users\zamir\Downloads\ffmpeg-master-latest-win64-gpl\ffmpeg-master-latest-win64-gpl\bin\ffmpeg.exe"; //download ffmpeg for your OS: https://github.com/BtbN/FFmpeg-Builds/releases and  Set the path to your FFmpeg binary 
            ffmpeg.StartInfo.Arguments = _arguments;
            ffmpeg.StartInfo.UseShellExecute = false;
            ffmpeg.StartInfo.RedirectStandardOutput = true;
            ffmpeg.Start();

            // Wait for the process to finish
            ffmpeg.WaitForExit();

            return _outputFilePath;
        }
    }
}
