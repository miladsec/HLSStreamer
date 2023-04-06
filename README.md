HLS Server with C# and Js
===========================

This project is a basic implementation of a server that provides HLS (HTTP Live Streaming) capabilities. It uses C# on the server side and HTML/JS on the client side. The server has 3 API routes: one for converting an MP4 file to an M3U8 playlist, one for returning the playlist, and one for returning the TS files.

![How HLS works](http://www.streamingmedia.com/Images/ArticleImages/ArticleImage.11612.jpg)

Requirements
------------

*   [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
*   [FFmpeg](https://github.com/BtbN/FFmpeg-Builds/releases)

Getting Started
---------------

1.  Clone the repository: `git clone https://github.com/Miiilc/HLSStreamer.git`
2.  Navigate to the project directory: `cd hls-server-csharp`
3.  Build the project: `dotnet build`
4.  Run the project: `dotnet run`

The server will start running on `http://localhost:5000`.

API Routes
----------

### Convert an MP4 file to an M3U8 playlist

`GET /api/Video`

*   `inputFilePath`: Path/To/File/Name.mp4
*   `outPutFilePath`: Path/To/File/Name.m3u8

Response Body:

string: `outPutFilePath`

### Get an M3U8 playlist

`GET /api/Video/{playListName}.m3u8`

*   `playListName`: The name of the playlist to retrieve.

Response Body:
`<binary data>`

### Get a TS file

`GET /api/Video/{fileName}.ts`

*   `fileName`: The number of the segment to retrieve.

Response Body:
`<binary data>`

Client Side
-----------

The client side is implemented in HTML and JavaScript. You can access the HLS stream using a video tag and the MediaSource API.

License
-------

This project is licensed under the MIT License - see the [LICENSE](LICENSE)
