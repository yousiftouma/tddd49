using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UltimateTicTacToe.Model;

namespace UltimateTicTacToe.Storage
{
    public class DataStorageHandler : IDataStorageHandler
    {
        private IFileHandler _fileHandler;

        public DataStorageHandler(IFileHandler fileHandler)
        {
            _fileHandler = fileHandler;
        }

        public void StoreBoard(bool isPlayerOneTurn, MarkerType winner, MarkerType playerOne,
            MarkerType playerTwo, SubBoard[,] subboards)
        {
            var boards = new List<List<SubBoard>> {new List<SubBoard>(), new List<SubBoard>(), new List<SubBoard>()};
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    boards[i].Add(subboards[i, j]);
                }
            }

            JArray one = new JArray();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    JArray aSubboard = new JArray();
                    SubBoard lol = subboards[i, j];
                    for (int k = 0; k < 3; k++)
                    {
                        for (int l = 0; l < 3; l++)
                        {

                            aSubboard.Add(lol.GetMarker(new Position(k, l)));
                        }
                    }
                    one.Add(aSubboard);
                }
            }

            //TODO
                //SubBoard[3, 3] subboards(inkl winner och active i separata)
                JObject game =
                    new JObject(
                        new JProperty("game",
                            new JObject(
                                new JProperty("isPlayerOneTurn", isPlayerOneTurn.ToString()),
                                new JProperty("winner", (int) winner),
                                new JProperty("playerOne", (int) playerOne),
                                new JProperty("playerTwo", (int) playerTwo),
                                new JProperty("activeSubboards",
                                    new JArray(
                                        from sbs in boards
                                        from sb in sbs
                                        select sb.IsActive)),
                                new JProperty("winnerSubboards",
                                    new JArray(
                                        from sbs in boards
                                        from sb in sbs
                                        select sb.Winner)),
                                new JProperty("board", one))));

            }

        }
    }

