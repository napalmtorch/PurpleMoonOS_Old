// system libraries
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using FS = Cosmos.System.FileSystem;

// os libraries
using PurpleMoon.Core;
using PurpleMoon.Math;
using PurpleMoon.Types;
using PurpleMoon.Processes;

namespace PurpleMoon.Hardware
{
    public static class PMFAT
    {
        // device
        public static FS.CosmosVFS device;

        // init
        public static bool Initialize()
        {
            // init
            try
            {
                device = new FS.CosmosVFS();
                FS.VFS.VFSManager.RegisterVFS(device);
                return true;
            }
            catch { return false; }
        }

        public static string[] GetFiles(string path)
        {
            string[] files = new string[Directory.GetFiles(path).Length];
            if (files.Length > 0)
            { files = Directory.GetFiles(path); }
            else { files[0] = "No files found."; }
            return files;
        }
        public static string[] GetFolders(string path)
        {
            string[] folders = new string[Directory.GetDirectories(path).Length];
            if (folders.Length > 0)
            { folders = Directory.GetDirectories(path); }
            else { folders[0] = "No folders found."; }
            return folders;
        }

        // exists
        public static bool FileExists(string file) { return File.Exists(file); }
        public static bool FolderExists(string path) { return Directory.Exists(path); }

        // reads
        public static string[] ReadLines(string path)
        {
            string[] data;
            data = File.ReadAllLines(path);
            return data;
        }
        public static string ReadText(string path)
        {
            try
            {
                string data;
                data = File.ReadAllText(path);
                return data;
            }
            catch (Exception ex) { return ex.Message; }
        }
        public static byte[] ReadBytes(string path)
        {
            byte[] data;
            data = File.ReadAllBytes(path);
            return data;
        }

        public static string ReadTextFile(string path)
        {
            try
            {
                FS.Listing.DirectoryEntry file = device.GetFile(path);
                Stream stream = file.GetFileStream();

                if (stream.CanRead)
                {
                    byte[] txt = new byte[stream.Length];
                    stream.Read(txt, 0, (int)stream.Length);
                    return Encoding.Default.GetString(txt);
                }
                else { return "File not reading for reading!"; }
            }
            catch (Exception ex) { return ex.Message; }
        }

        // writes
        public static void WriteAllText(string path, string text)
        {
            File.WriteAllText(path, text);
        }
        public static void WriteAllLines(string path, string[] lines)
        {
            File.WriteAllLines(path, lines);
        }
        public static void WriteAllBytes(string path, byte[] data)
        {
            File.WriteAllBytes(path, data);
        }

        // creates
        public static bool CreateFolder(string name)
        {
            bool value = false;
            if (FolderExists(name)) { value = false; }
            else { Directory.CreateDirectory(name); value = true; }
            return value;
        }

        // edit
        public static bool RenameFolder(string input, string newName)
        {
            bool value = false;
            if (Directory.Exists(input))
            { Directory.Move(input, newName); value = true; }
            else { value = false; }
            return value;
        }
        public static bool RenameFile(string input, string newName)
        {
            bool value = false;
            if (FileExists(input))
            { File.Move(input, newName); value = true; }
            else { value = false; }
            return value;
        }
        public static void DeleteFolder(string path)
        {
            if (FolderExists(path)) { Directory.Delete(path); }
        }
    }
}
