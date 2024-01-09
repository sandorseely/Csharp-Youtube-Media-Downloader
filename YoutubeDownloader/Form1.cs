using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using System.Diagnostics;
using NAudio;
using NAudio.Wave;
using NAudio.Lame;

namespace YoutubeDownloader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private static string downloadsDirectory = Path.Combine(Environment.CurrentDirectory, "downloads");
        private static string extension;
        private void btnAdd_Click(object sender, EventArgs e)
        {
            listDownload.Items.Add(txtLink.Text);

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            listDownload.Items.Clear();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            string extension = (string)comboBoxExtension.SelectedItem;
            listStatus.Items.Add("[DEBUG] Selected extension is: " + extension);
            foreach (string s in listDownload.Items)
            {
                int ampersandIndex = s.IndexOf('&');
                if (ampersandIndex != -1)
                {
                    download(s.Substring(0, ampersandIndex), extension);
                }
            }          

        }

        private async void download(string link, string extension)
        {
            var youtube = new YoutubeClient();
            var video = await youtube.Videos.GetAsync(link);

            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(link);

            // Get the audio-only stream with the highest bitrate
            var audioStreamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();

            string outputPath = Path.Combine(downloadsDirectory, $"{video.Title}.{extension}");

            listStatus.Items.Add("Attempting download: " + outputPath);

            if (extension == "mp3" && audioStreamInfo != null)
            {
                listStatus.Items.Add("[DEBUG] Downloading Webm.");
                listStatus.Items.Add( "[DEBUG] Starting conversion to Mp3...");

                // Download the WebM audio stream to a temporary file
                string tempWebmPath = Path.Combine(downloadsDirectory, $"{video.Title}.temp.webm");
                await youtube.Videos.Streams.DownloadAsync(audioStreamInfo, tempWebmPath);

                // Convert the WebM to MP3 using NAudio
                using (var reader = new MediaFoundationReader(tempWebmPath))
                {
                    string outputPathMp3 = Path.Combine(downloadsDirectory, $"{video.Title}.mp3");
                    using (var writer = new LameMP3FileWriter(outputPathMp3, reader.WaveFormat, LAMEPreset.VBR_90))
                    {
                        reader.CopyTo(writer);
                    }
                }


                // Delete the temporary WebM file
                File.Delete(tempWebmPath);

                listStatus.Items.Add("MP3 conversion successful: " + outputPath);
            }
            else if (extension == "mp4")
            {
                // Get the muxed video stream with the highest video quality
                var videoStreamInfo = streamManifest.GetMuxedStreams().GetWithHighestVideoQuality();

                listStatus.Items.Add("[DEBUG] Downloading mp4.");

                await youtube.Videos.Streams.DownloadAsync(videoStreamInfo, outputPath);
            }

            if (File.Exists(outputPath))
            {
                listStatus.Items.Add("DOWNLOAD SUCCESS: " + outputPath);
            }
            else
            {
                listStatus.Items.Add("DOWNLOAD FAIL: " + outputPath);
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(downloadsDirectory))
            {
                listStatus.Items.Add("Creating downloads directory");
                Directory.CreateDirectory(downloadsDirectory);

            }
            else
            {
                listStatus.Items.Add("Downloads directory found");
            }




        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/sandorseely/Csharp-Youtube-Media-Downloader");

        }
    }
}
