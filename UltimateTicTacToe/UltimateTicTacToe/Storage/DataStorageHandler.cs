using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using UltimateTicTacToe.Model;

namespace UltimateTicTacToe.Storage
{
    public class DataStorageHandler : IDataStorageHandler
    {
        private readonly IFileHandler _fileHandler;

        public DataStorageHandler(IFileHandler fileHandler)
        {
            _fileHandler = fileHandler;
        }

        public void StoreBoard(BoardDto boardDto)
        {
            // TODO handle possible exceptions
            var subboards = boardDto.Subboards;
            var boards = new List<List<SubBoard>> { new List<SubBoard>(), new List<SubBoard>(), new List<SubBoard>() };
            var subboardsJArray = new JArray();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    boards[i].Add(subboards[i, j]);
                    var aSubboardJArray = new JArray();
                    var aSubboard = subboards[i, j];
                    for (int k = 0; k < 3; k++)
                    {
                        for (int l = 0; l < 3; l++)
                        {
                            aSubboardJArray.Add(aSubboard.GetMarker(new Position(k, l)));
                        }
                    }
                    subboardsJArray.Add(aSubboardJArray);
                }
            }

            var game =
                new JObject(
                    new JProperty("game",
                        new JObject(
                            new JProperty("isPlayerOneTurn", boardDto.IsPlayerOneTurn.ToString()),
                            new JProperty("winner", (int) boardDto.Winner),
                            new JProperty("playerOne", (int) boardDto.PlayerOne),
                            new JProperty("playerTwo", (int) boardDto.PlayerTwo),
                            new JProperty("activeSubboards",
                                new JArray(
                                    from sbs in boards
                                    from sb in sbs
                                    select sb.IsActive)),
                            new JProperty("subboardWinners",
                                new JArray(
                                    from sbs in boards
                                    from sb in sbs
                                    select sb.Winner)),
                            new JProperty("board", subboardsJArray))));
            _fileHandler.Write(game.ToString());
        }

        public bool LoadBoard(IRules rules, out BoardDto boardDto)
        {
            // TODO handle possible exceptions

            boardDto = new BoardDto();
            if (!_fileHandler.FileExists())
            {
                return false;
            }
            try
            {
                var jsonDataString = _fileHandler.Read();
                var jsonObject = JObject.Parse(jsonDataString);
                boardDto.IsPlayerOneTurn = (bool) jsonObject["game"]["isPlayerOneTurn"];
                boardDto.Winner = (MarkerType) (int) jsonObject["game"]["winner"];
                boardDto.PlayerOne = (MarkerType) (int) jsonObject["game"]["playerOne"];
                boardDto.PlayerTwo = (MarkerType) (int) jsonObject["game"]["playerTwo"];

                var activeSubboards =
                    jsonObject["game"]["activeSubboards"].Select(isActive => (bool) isActive).ToList();
                var subboardWinners = jsonObject["game"]["subboardWinners"].Select(winner => (MarkerType) (int) winner)
                    .ToList();
                var subboards = from sb in jsonObject["game"]["board"] select (JArray) sb;
                var subboardsAsMarkerTypeArrays = subboards.Select(b => b.ToObject<List<MarkerType>>()).ToList();

                var subboardArray = new SubBoard[3, 3];
                var tempSubboardList = new List<SubBoard>();
                int counter;
                for (int i = 0; i < 9; i++)
                {
                    counter = 0;
                    var subboardAsArray = subboardsAsMarkerTypeArrays[i];
                    var markerTypeArray = new MarkerType[3, 3];
                    
                    for (int j = 0; j < 3; j++)
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            markerTypeArray[j,k] = subboardAsArray[counter];
                            counter++;
                        }
                    }
                    tempSubboardList.Add(new SubBoard(rules, markerTypeArray, activeSubboards[i], subboardWinners[i]));
                }

                counter = 0;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        subboardArray[i, j] = tempSubboardList[counter];
                        counter++;
                    }
                }
                boardDto.Subboards = subboardArray;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed parsing json, got exception {e}");
                return false;
            }
            return true;
        }
    }
}

