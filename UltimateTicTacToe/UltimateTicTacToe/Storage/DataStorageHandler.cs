using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UltimateTicTacToe.Model;

namespace UltimateTicTacToe.Storage
{
    public class DataStorageHandler
    {
        private IFileHandler _fileHandler;

        public DataStorageHandler(IFileHandler fileHandler)
        {
            _fileHandler = fileHandler;
        }

        public void StoreBoard(bool isPlayerOneTurn, MarkerType winner, MarkerType playerOne, 
            MarkerType playerTwo, SubBoard[,] subboards)
        {
            // TODO
            // SubBoard[3,3] subboards (inkl winner och active i separata)
            JObject game = 
                new JObject(
                    new JProperty("game",
                        new JObject(
                            new JProperty("isPlayerOneTurn", isPlayerOneTurn.ToString()),
                            new JProperty("winner", (int)winner),
                            new JProperty("playerOne", (int)playerOne),
                            new JProperty("playerTwo", (int)playerTwo),
                            new JProperty("subboards", 
                            new))));
        }

    }
}
