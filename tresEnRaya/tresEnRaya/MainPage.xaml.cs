using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace tresEnRaya
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        const int max = 3;
        int movimientos;
        int turno;
        int[,] valoresBotones = new int[max, max];
        Image imagenTurno, imagenEkis, imagenCero;
       
        public MainPage()
        {
            this.InitializeComponent();
            iniciar();
        }
        private void iniciar()
        {
            movimientos = 0;
            Random random = new Random();
            turno=random.Next(0, 1);
            for (int i = 0; i < max; i++)
                for (int j = 0; j < max; j++)
                    valoresBotones[i, j] = -1;
            imagenCero = new Image();
            imagenEkis = new Image();
            imagenCero.Source = new BitmapImage(new Uri("ms-appx:///Assets//cero.png"));
            imagenEkis.Source = new BitmapImage(new Uri("ms-appx:///Assets//ekis.png"));
            textoCabecera.Text = "Turno: ";
            if (turno == 0)
                imagenCabecera.Source = imagenCero.Source;
            else
                imagenCabecera.Source = imagenEkis.Source;
            foreach (UIElement elemento in panelBotones.Children)
            {

                if (elemento is Button)
                {
                    Button boton = (Button)elemento;
                    boton.IsEnabled = true;
                    boton.Content = "";
               
                }
                    
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button botonPulsado = (Button)sender;
            movimientos++;
            imagenTurno = new Image();
            imagenTurno.Source = imagenCabecera.Source;
            botonPulsado.Content = imagenTurno;

            int fila = Grid.GetRow(botonPulsado);
            int columna = Grid.GetColumn(botonPulsado);
            botonPulsado.IsEnabled = false;
            if (turno==0)
            {
                valoresBotones[fila, columna] = 0;
                turno = 1;
                imagenCabecera.Source = imagenEkis.Source;
            }
            else
            {
                valoresBotones[fila, columna] = 1;
                turno = 0;
                imagenCabecera.Source = imagenCero.Source;
            }

            if (comprobarGanador(valoresBotones))
            {
                mostrarGanador();
            }
            else
                if (movimientos == 9)
                mostrarEmpate();
        }

        private void mostrarGanador()
        {
            textoCabecera.Text = "Ganador: ";
            imagenCabecera.Source = imagenTurno.Source;
            foreach (UIElement elemento in panelBotones.Children)
            {
                if (elemento is Button)
                {
                    Button boton = (Button)elemento;
                    if (boton.IsEnabled == true)
                        boton.IsEnabled = false;
                }
            }

        }
        private void mostrarEmpate()
        {
            textoCabecera.Text = "Empate ";
            imagenCabecera.Source = null;
        }
        private Boolean comprobarGanador(int[,] valor)
        {
            Boolean ganador;
            //filas
            for (int i = 0; i < max; i++)
            {
                ganador = true;

                for (int j = 0; j < max - 1; j++)
                {
                    if ((valor[i, j] != valor[i, j + 1]) | (valor[i, j] == -1))
                    {
                        ganador = false;
                        break;
                    }
                }
                if (ganador == true)
                    return ganador;

            }
            //columnas
            for (int j = 0; j < max; j++)
            {
                ganador = true;
                int i;
                for (i = 0; i < max - 1; i++)
                {
                    if ((valor[i, j] != valor[i + 1, j]) | (valor[i, j] == -1))
                    {
                        ganador = false;
                        break;
                    }
                }
                if (ganador == true)
                    return ganador;
            }

            ganador = true;
            for (int i = 0; i < max - 1; i++)
            {
                if (valor[i, i] != valor[i + 1, i + 1])
                {
                    ganador = false;
                    break;
                }
            }
            if ((ganador == true) && (valor[0, 0] != -1))
                return true;

            ganador = true;
            for (int i = 0, j = max - 1; i < max - 1; i++, j--)
                if (valor[i, j] != valor[i + 1, j - 1])
                {
                    ganador = false;
                    break;
                }

            if ((ganador == true) && (valor[0, max - 1] != -1))
                return true;


            return false;
        }

        private void AppBarButton_Click_Iniciar(object sender, RoutedEventArgs e)
        {
            iniciar();
        }
    }
}
