using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UltimateTicTacToe.Model;
using UltimateTicTacToe.Model.CustomExceptions;

namespace UltimateTicTacToe.Storage
{
    public class DataStorageHandler : IDataStorageHandler
    {
        private readonly IFileHandler _fileHandler;

        public DataStorageHandler(IFileHandler fileHandler)
        {
            _fileHandler = fileHandler;
        }

        /// <summary>
        /// Stores the board described in the <paramref name="boardDto"/> object to storage.
        /// </summary>
        /// <param name="boardDto">Object with the needed information to store.</param>
        public void StoreBoard(BoardDto boardDto)
        {
            try
            {
                SubBoard[,] subboards;
                try
                {
                    subboards = boardDto.Subboards;
                }
                catch (Exception e)
                {
                    throw new ArgumentException("Could not access SubBoard from boardDto.", e);
                }
                var boards =
                    new List<List<SubBoard>> { new List<SubBoard>(), new List<SubBoard>(), new List<SubBoard>() };
                var subboardsJArray = new JArray();
                for (var i = 0; i < 3; i++)
                {
                    for (var j = 0; j < 3; j++)
                    {
                        boards[i].Add(subboards[i, j]);
                        var aSubboardJArray = new JArray();
                        var aSubboard = subboards[i, j];
                        for (var k = 0; k < 3; k++)
                        {
                            for (var l = 0; l < 3; l++)
                            {
                                aSubboardJArray.Add(aSubboard.GetMarker(new Position(k, l)));
                            }
                        }
                        subboardsJArray.Add(aSubboardJArray);
                    }
                }
                try
                {
                    var game =
                        new JObject(
                            new JProperty("game",
                                new JObject(
                                    new JProperty("isPlayerOneTurn", boardDto.IsPlayerOneTurn.ToString()),
                                    new JProperty("winner", (int)boardDto.Winner),
                                    new JProperty("playerOne", (int)boardDto.PlayerOne),
                                    new JProperty("playerTwo", (int)boardDto.PlayerTwo),
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
                catch (Exception e)
                {
                    throw new SerializationException(
                        "Failed to serialize and write to file, see inner exception for details.", e);
                }
            }
            catch (Exception e)
            {
                throw new SaveStateFailException("Failed to StoreBoard, see inner exception for details.", e);
            }
        }

        /// <summary>
        /// Loads a board from a persisted state into <paramref name="boardDto"/>.
        /// </summary>
        /// <param name="rules">The rules to use with the loaded board.</param>
        /// <param name="boardDto">The object to store the state in.</param>
        /// <returns></returns>
        public bool LoadBoard(IRules rules, out BoardDto boardDto)
        {
            try
            {
                boardDto = new BoardDto();
                if (!_fileHandler.FileExists())
                {
                    return false;
                }
                List<bool> activeSubboards;
                List<MarkerType> subboardWinners;
                List<List<MarkerType>> subboardsAsMarkerTypeArrays;
                try
                {
                    var jsonDataString = _fileHandler.Read();
                    var jsonObject = JObject.Parse(jsonDataString);

                    boardDto.IsPlayerOneTurn = (bool) jsonObject["game"]["isPlayerOneTurn"];
                    boardDto.Winner = (MarkerType) (int) jsonObject["game"]["winner"];
                    boardDto.PlayerOne = (MarkerType) (int) jsonObject["game"]["playerOne"];
                    boardDto.PlayerTwo = (MarkerType) (int) jsonObject["game"]["playerTwo"];

                    activeSubboards = jsonObject["game"]["activeSubboards"].Select(isActive => (bool) isActive)
                        .ToList();
                    subboardWinners = jsonObject["game"]["subboardWinners"].Select(winner => (MarkerType) (int) winner)
                        .ToList();
                    var subboards = from sb in jsonObject["game"]["board"] select (JArray) sb;
                    subboardsAsMarkerTypeArrays = subboards.Select(b => b.ToObject<List<MarkerType>>()).ToList();
                }
                catch (Exception e)
                {
                    throw new SerializationException("Failed to deserialize from file, see inner exception for details",
                        e);
                }

                var subboardArray = new SubBoard[3, 3];
                var tempSubboardList = new List<SubBoard>();
                int counter;
                for (var i = 0; i < 9; i++)
                {
                    counter = 0;
                    var subboardAsArray = subboardsAsMarkerTypeArrays[i];
                    var markerTypeArray = new MarkerType[3, 3];

                    for (var j = 0; j < 3; j++)
                    {
                        for (var k = 0; k < 3; k++)
                        {
                            markerTypeArray[j, k] = subboardAsArray[counter];
                            counter++;
                        }
                    }
                    tempSubboardList.Add(new SubBoard(rules, markerTypeArray, activeSubboards[i], subboardWinners[i]));
                }

                counter = 0;
                for (var i = 0; i < 3; i++)
                {
                    for (var j = 0; j < 3; j++)
                    {
                        subboardArray[i, j] = tempSubboardList[counter];
                        counter++;
                    }
                }
                boardDto.Subboards = subboardArray;
                return true;
            }
            catch (Exception e)
            {
                throw new LoadStateFailException("Failed to LoadBoard, see inner exception for details", e);
            }
        }
    }
}

