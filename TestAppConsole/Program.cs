using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAppConsole
{
    class Program
    {
        static List<Mines> _mines= new List<Mines>();
        static List<ConnectedMines> _connectedMines = new List<ConnectedMines>();
        static void Main(string[] args)
        {
            sortArray();
            for (int i = 0; i < 10; i++)
            {
                Mines mine = new Mines {
                    Id = i + 1,
                    Name="M"+(i+1),
                    Weight=(i+1)*2
                };
                _mines.Add(mine);
            }
            ConnectedMines connected = new ConnectedMines();
            connected.Connection = new List<Mines>();
            connected.Connection.Add(new Mines { Id = 1});
            connected.Connection.Add(new Mines { Id = 2 });
            connected.Connection.Add(new Mines { Id = 5});
            _connectedMines.Add(connected);
            ExploreMine();
            //string[] words = { "a", "b", "ba", "bca", "bda", "bdca" };
            //int output = longestChain(words.ToList());

            Console.Read();
        }
        static void sortArray()
        {
            int[] arr = new int[] { 1, 3,2,4,7,9,8 };

            int temp;

            // traverse 0 to array length 
            for (int i = 0; i < arr.Length - 1; i++)

                // traverse i+1 to array length 
                for (int j = i + 1; j < arr.Length; j++)

                    // compare array element with  
                    // all next element 
                    if (arr[i] < arr[j])
                    {

                        temp = arr[i];
                        arr[i] = arr[j];
                        arr[j] = temp;
                    }
        }
        static void ExploreMine()
        {
            while (_connectedMines.Count>0)
            {
                ExploreConnectedMines();
            }
            foreach (var item in _mines)
            {
                Console.WriteLine(item.Name + " using weight " + item.Weight);
            }   
            
        }
        static void ExploreConnectedMines()
        {
            foreach (var item in _connectedMines)
            {
                var conn = _mines.Join(
                    item.Connection,
                    x => x.Id,
                    y => y.Id,
                    (x, y) => new { x, y }
                    ).Select(x => x.x).OrderBy(ord => ord.Weight).ToList();
                Console.WriteLine(string.Join(",", conn.Select(x => x.Name)) + " using weight " + conn.Min(x => x.Weight));
                _mines.RemoveAll(x => conn.Contains(x));
                _connectedMines.Remove(item);
                if (_connectedMines.Count == 0)
                    break;
            }
        }
        static int longestChain(List<string> wordList)
        {
          
            
            foreach (string word in wordList)
            {
                int counter = 0;
                var temp = word;
                if (word.Length>1)
                {
                    int endIndex = 1;
                    foreach (var item in word.ToCharArray())
                    {
                       
                            int startIndex = word.IndexOf(item);
                        
                            temp=temp.Remove(startIndex, endIndex);
                            if (wordList.Contains(temp))
                            {
                                counter++;
                                
                            }
                       
                        
                    }
                }
               
            }

            return 0;
        }

        static string GetExcelColumnName(long columnNumber)
        {
            //max number of column per row
            const long maxColPerRow = 702;
            //find row number
            long rowNum = (columnNumber / maxColPerRow);
            //find tierable columns in the row.
            long dividend = columnNumber - (maxColPerRow * rowNum);

            string columnName = String.Empty;
            
            long modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return rowNum+1+ columnName;
        }
    }
}
