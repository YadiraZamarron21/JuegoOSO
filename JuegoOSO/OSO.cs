

using System;

namespace JuegoOSO
{
    public class OSO
    {
        public char[,] Pizarra { get; private set; }
        public int PuntosJugador { get; set; }
        public int PuntosIA { get; set; }
        public bool Turno { get; set; }

        public OSO()
        {
            ComenzarJuego();
        }
        public void ComenzarJuego()
        {
            Pizarra = new char[3, 3];
            PuntosJugador = 0;
            PuntosIA = 0;
            Turno = true;
        }

        // Verifica si un jugador ha formado la palabra "OSO" y asigna puntos
        public bool Verificar(char jugador)
        {
            if (FormarPalabra(jugador))
            {
                if (jugador == 'O')
                    PuntosJugador += 1;
                else if (jugador == 'S')
                    PuntosIA += 1;
                return true;
            }
            return false;
        }

        // Verifica si el jugador ha formado la palabra "OSO"
        private bool FormarPalabra(char O)
        {
          
            // Horizontal
            for (int i = 0; i < 3; i++)
            {
                if (Pizarra[i, 0] == O && Pizarra[i, 1] == 'S' && Pizarra[i, 2] == O) return true;
            }

            // Vertical
            for (int i = 0; i < 3; i++)
            {
                if (Pizarra[0, i] == O && Pizarra[1, i] == 'S' && Pizarra[2, i] == O) return true;
            }

            // Diagonal
            if (Pizarra[0, 0] == O && Pizarra[1, 1] == 'S' && Pizarra[2, 2] == O) return true;
            if (Pizarra[0, 2] == O && Pizarra[1, 1] == 'S' && Pizarra[2, 0] == O) return true;

            return false;
        }

        // Verifica si el tablero está lleno
        public bool TableroLLeno()
        {
            foreach (var cell in Pizarra)
            {
                if (cell == '\0') return false;
            }
            return true;
        }

        public (int, int) MejorMovimiento()
        {
            int mejorpuntaje = int.MinValue;
            (int, int) mejorMovimiento = (-1, -1);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Pizarra[i, j] == '\0')
                    {
                        Pizarra[i, j] = 'S'; // Probar movimiento de la IA
                        int puntuacion = Minimax(Pizarra, false);
                        Pizarra[i, j] = '\0'; // Deshacer movimiento

                        if (puntuacion > mejorpuntaje)
                        {
                            mejorpuntaje = puntuacion;
                            mejorMovimiento = (i, j);
                        }
                    }
                }
            }

            return mejorMovimiento;
        }

        // Función Minimax que evalúa el puntaje de un movimiento
        private int Minimax(char[,] piz, bool puntos)
        {
            if (FormarPalabra('S')) return 1;
            if (FormarPalabra('O')) return -1;
            if (TableroLLeno()) return 0;

            int mejorpuntaje = puntos ? int.MinValue : int.MaxValue;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (piz[i, j] == '\0')
                    {
                        piz[i, j] = puntos ? 'S' : 'O';
                        int puntuacion = Minimax(piz, !puntos);
                        piz[i, j] = '\0';

                        mejorpuntaje = puntos
                            ? Math.Max(puntuacion, mejorpuntaje)
                            : Math.Min(puntuacion, mejorpuntaje);
                    }
                }
            }
            return mejorpuntaje;
        }
    }
}
