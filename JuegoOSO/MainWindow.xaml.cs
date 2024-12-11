using System;
using System.Windows;
using System.Windows.Controls;

namespace JuegoOSO
{
    public partial class MainWindow : Window
    {
        private OSO juego;

        public MainWindow()
        {
            InitializeComponent();
            juego = new OSO(); 
            BorrarPizarra();
            ActualizarPuntajes();
        }

     
        private void BorrarPizarra()
        {
            Pizzarra.Children.Clear();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    var boton = new Button
                    {
                        FontSize = 24,
                        Tag = new Tuple<int, int>(i, j),
                        Content = juego.Pizarra[i, j] == '\0' ? string.Empty : juego.Pizarra[i, j].ToString(),
                        IsEnabled = juego.Pizarra[i, j] == '\0' // Solo habilitar botones en espacios vacíos
                    };
                    boton.Click += Button_Click;
                    Pizzarra.Children.Add(boton);
                }
            }
        }

        private void ActualizarPuntajes()
        {
            PuntajeJugadorLbl.Content = $"Jugador (O): {juego.PuntosJugador}";
            PuntajeIALbl.Content = $"IA (S): {juego.PuntosIA}";
        }

     
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!juego.Turno) return;

            var boton = (Button)sender;
            var posicion = (Tuple<int, int>)boton.Tag;

            if (juego.Pizarra[posicion.Item1, posicion.Item2] != '\0') return;

            
            juego.Pizarra[posicion.Item1, posicion.Item2] = 'O';
            juego.Turno = false; // Cambia turno a la IA
            BorrarPizarra();

           
            if (juego.Verificar('O')) // Verifica si el jugador 'O' formó "OSO"
            {
                ActualizarPuntajes();
            }

           
            if (!juego.TableroLLeno())
            {
                var mejorMovimiento = juego.MejorMovimiento();
                juego.Pizarra[mejorMovimiento.Item1, mejorMovimiento.Item2] = 'S';
                juego.Turno = true; // Cambia turno al jugador
                BorrarPizarra();

              
                if (juego.Verificar('S')) // Verifica si la IA 'S' formó "OSO"
                {
                    ActualizarPuntajes();
                }
            }

            
            if (juego.TableroLLeno())
            {
                MessageBox.Show($"Juego terminado. Puntos finales:\n Jugador (O): {juego.PuntosJugador}\n IA (S): {juego.PuntosIA}");

                RestartGame();
            }
        }
        private void RestartGame_Click(object sender, RoutedEventArgs e)
        {
            RestartGame();
        }

        private void RestartGame()
        {
            juego.ComenzarJuego();
            juego.PuntosJugador = 0;
            juego.PuntosIA = 0;
            BorrarPizarra();
            ActualizarPuntajes();
        }
    }
}

