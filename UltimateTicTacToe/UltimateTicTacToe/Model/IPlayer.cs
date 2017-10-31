﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Model
{
    public interface IPlayer
    {
        MarkerType Marker { get; }
        Move GetMove();
    }
}
