using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise {
    class Program {
        static void Main(string[] args) {
            var songs = new Song[] {
                new Song("A","A",256),
                new Song("B","B",365),
                new Song("C","C",589)
            };

            PrintSongs(songs);
        }

        private static void PrintSongs(Song[] songs) {
            foreach (var song in songs) {
                Console.WriteLine("{0}, {1}, {2:m\\:ss}",
                    song.Title, song.ArtistName, TimeSpan.FromSeconds(song.Length));
            }
        }
    }
}
