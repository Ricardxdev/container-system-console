using System.Diagnostics;
using System.Runtime.InteropServices;
using Terminal.Gui;

namespace containers.Controllers
{
    public class AudioPlayer
    {
        // P/Invoke declarations
        [DllImport("libc.so.6", SetLastError = true)]
        private static extern int kill(int pid, int sig);
        private const int SIGSTOP = 19; // Signal to stop a process
        private const int SIGCONT = 18; // Signal to continue a process
        private DirectoryInfo sourceInfo;
        private FileInfo[] tracks;
        private Process? currentTrack;
        private Random random;

        public AudioPlayer(string sourcePath, string ext, bool recursive)
        {
            random = new Random();
            try
            {
                sourceInfo = new DirectoryInfo(sourcePath);
                tracks = sourceInfo.GetFiles($"*.{ext}", recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void Play()
        {
            Stop();
            int randomIndex = random.Next(0, tracks.Length);
            currentTrack = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "paplay",
                    Arguments = tracks[randomIndex].FullName,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            currentTrack.Start();
        }

        public void Play(string path)
        {
            Stop();
            currentTrack = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "paplay",
                    Arguments = path,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            currentTrack.Start();
        }

        public void Pause()
        {
            if (currentTrack != null && !currentTrack.HasExited)
            {
                kill(currentTrack.Id, SIGSTOP); // Send SIGSTOP signal to pause the currentTrack
            }
        }

        public void Resume()
        {
            if (currentTrack != null && !currentTrack.HasExited)
            {
                kill(currentTrack.Id, SIGCONT); // Send SIGCONT signal to resume the currentTrack
            }
        }

        public void Stop()
        {
            if (currentTrack != null && !currentTrack.HasExited)
            {
                currentTrack.Kill();
                currentTrack.Dispose();
                currentTrack = null;
            }
        }
    }

    public class AudioPlayerComponent : View
    {
        private AudioPlayer player;
        private Button playButton;
        private Button pauseButton;
        private Button stopButton;
        private Button openButton;
        private Button nextButton;

        public AudioPlayerComponent() : base()
        {
            // Initialize buttons
            player = new AudioPlayer("./Media", "mp3", false);
            playButton = new Button("Play")
            {
                X = 1,
                Y = 1
            };
            playButton.Clicked += player.Play;

            pauseButton = new Button("Pause")
            {
                X = 10,
                Y = 1
            };
            pauseButton.Clicked += player.Pause;

            stopButton = new Button("Stop")
            {
                X = 19,
                Y = 1
            };
            stopButton.Clicked += player.Stop;

            openButton = new Button("Open")
            {
                X = 28,
                Y = 1
            };
            openButton.Clicked += OpenFile;

            // Add buttons to the view
            Add(playButton, pauseButton, stopButton, openButton);
        }

        private void OpenFile()
        {
            var dialog = new OpenDialog("Open Audio File", "Select an audio file")
            {
                AllowsMultipleSelection = false
            };

            Application.Run(dialog);

            if (dialog.FilePath.Count() > 0)
            {
                player.Play(dialog.FilePath[0].ToString());
            }
        }
    }

}