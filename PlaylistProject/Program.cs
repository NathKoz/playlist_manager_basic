using System;
using System.Collections.Generic;
using System.Linq;

// Enum for different musical genres
public enum Genre
{
    Rock,
    Pop,
    HipHop,
    // Add more genres as needed
}

// Class representing a single song
public class Song
{
    public string Title { get; set; }
    public string Artist { get; set; }
    public double Duration { get; set; }
    public Genre Genre { get; set; }
    public Album Album { get; set; }

    public override string ToString()
    {
        return $"{Title} - {Artist} ({Duration} mins) Genre: {Genre}";
    }
}

// Abstract class serving as a base class for organizing collections of songs
public abstract class SongList
{
    protected List<Song> Songs;
    public string Title { get; set; }
    public double TotalRuntime => Songs.Sum(song => song.Duration);

    public SongList(string title)
    {
        Title = title;
        Songs = new List<Song>();
    }

    public virtual void AddSong(Song song)
    {
        Songs.Add(song);
    }

    public virtual void RemoveSong(Song song)
    {
        Songs.Remove(song);
    }

    public virtual Song GetSong(int index)
    {
        if (index >= 0 && index < Songs.Count)
        {
            return Songs[index];
        }
        return null;
    }

    public override string ToString()
    {
        return $"{Title} - Total Runtime: {TotalRuntime} mins";
    }
}

// Child class of SongList representing an Album
public class Album : SongList
{
    public string Artist { get; set; }
    public List<string> BandMembers { get; set; }
    public DateTime ReleaseDate { get; set; }

    public Album(string title, string artist, DateTime releaseDate) : base(title)
    {
        Artist = artist;
        BandMembers = new List<string>();
        ReleaseDate = releaseDate;
    }

    public override void AddSong(Song song)
    {
        // Set the album reference for the song
        song.Album = this;
        base.AddSong(song);
    }

    public override string ToString()
    {
        return $"{base.ToString()} - Artist: {Artist}, Release Date: {ReleaseDate.ToShortDateString()}";
    }
}

// Child class of SongList representing a Playlist
public class Playlist : SongList
{
    public Playlist(string title) : base(title)
    {
    }

    public void ShufflePlaylist()
    {
        Random random = new Random();
        Songs = Songs.OrderBy(song => random.Next()).ToList();
    }

    public override string ToString()
    {
        return base.ToString();
    }
}

class Program
{
    static void Main()
    {
        // Sample usage
        Album album = new Album("Album 1", "Artist 1", new DateTime(2022, 1, 1));
        album.AddSong(new Song { Title = "Song 1", Artist = "Artist 1", Duration = 3.5, Genre = Genre.Rock });
        album.AddSong(new Song { Title = "Song 2", Artist = "Artist 1", Duration = 4.2, Genre = Genre.Pop });

        Playlist playlist = new Playlist("My Playlist");
        playlist.AddSong(new Song { Title = "Song 3", Artist = "Artist 2", Duration = 2.8, Genre = Genre.HipHop });

        // Display playlist details
        Console.WriteLine(playlist);

        // Add an album to the playlist
        playlist.AddSong(album.GetSong(0));

        // Display shuffled playlist
        playlist.ShufflePlaylist();
        Console.WriteLine("Shuffled Playlist:");
        Console.WriteLine(playlist);

        // Display details of a specific song in the playlist
        Song specificSong = playlist.GetSong(0);
        if (specificSong != null)
        {
            Console.WriteLine("Details of Specific Song:");
            Console.WriteLine(specificSong);
            Console.WriteLine($"Album: {specificSong.Album}");
        }
    }
}
