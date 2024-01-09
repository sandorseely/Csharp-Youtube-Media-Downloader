# Csharp-Youtube-Media-Downloader
# YouTube Downloader

YouTube Downloader is a simple desktop application for downloading YouTube videos as either MP4 or MP3 files.

## Features

### 1. Add YouTube Links
You can add multiple YouTube links to the download queue. Simply copy and paste the video URLs into the provided text box and click the "Add" button.

### 2. Clear Download Queue
If you want to start fresh, you can clear the download queue with the "Clear" button.

### 3. Select Download Format
Choose between downloading videos as MP4 or extracting audio as MP3 using the provided dropdown menu.

### 4. Download & Convert
Click the "Download" button to start downloading the videos. If the YouTube link contains an ampersand (&), it will be handled to prevent issues.

For MP3 downloads:
- The program first downloads the audio stream in WebM format.
- It then converts the WebM file to MP3 using NAudio and LAME MP3 encoder.

For MP4 downloads:
- The program downloads the video stream in MP4 format with the highest video quality.

### 5. Status Updates
The status updates panel provides real-time information on the download and conversion process. It includes debug information, conversion progress, and success or failure messages.

### 6. Downloads Directory
The application automatically creates a "downloads" directory in the program's working directory if it doesn't exist. All downloaded files are saved there.

## Getting Started

1. Clone the repository or download the source code.
2. Open the solution in Visual Studio.
3. Build and run the application.

## Dependencies

- YoutubeExplode: Used for interacting with the YouTube API.
- NAudio: Used for audio processing, including WebM to MP3 conversion.
- LAME MP3 Encoder: Used in conjunction with NAudio for MP3 encoding.

## Notes

- This application uses the YoutubeExplode library for working with YouTube videos.
- NAudio and LAME MP3 Encoder are employed for audio stream processing and MP3 encoding.

## Contributors

- [Your Name]

Feel free to contribute to the project by submitting pull requests or opening issues!

