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
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en http://go.microsoft.com/fwlink/?LinkId=234238

namespace Parejas
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Juego8 : Page
    {
        private List<string> numeros = new List<string>()
        {
            "1", "1", "2", "2", "3", "3", "4", "4",
        };

        private List<string> parejas = new List<string>();

        private Button levantado = null;

        private int contParejas = 0;
        private Random random = new Random();

        public Juego8()
        {
            this.InitializeComponent();
            int i;
            for (i = 0; i < 8; i++)
            {

                int randomNumber = random.Next(numeros.Count);
                parejas.Add(numeros[randomNumber]);
                numeros.RemoveAt(randomNumber);

            }
        }

        private async void Voltea_Boton(object sender, RoutedEventArgs e)
        {
            Type tipoSend = sender.GetType();
            if (tipoSend.Equals(typeof(Button)))
            {
                Button b = (Button)sender;

                int pos;

                string pos1 = b.Tag as string;

                pos = Int32.Parse(pos1);

                b.Content = parejas[pos];

                b.IsEnabled = false;

                await System.Threading.Tasks.Task.Delay(500);



                if (this.levantado == null)
                {
                    levantado = b;
                }
                else
                {
                    if (parejas[Int32.Parse(levantado.Tag as string)] == parejas[pos])
                    {
                        contParejas++;
                        levantado = null;
                    }
                    else
                    {

                        levantado.Content = "";
                        levantado.IsEnabled = true;
                        b.Content = "";
                        b.IsEnabled = true;

                        levantado = null;
                    }

                    if (contParejas == 4)
                    {

                        winner.Text = "Ganaste!";
                        volver.Visibility = Windows.UI.Xaml.Visibility.Visible;
                        volver.IsEnabled = true;
                    }

                }




            }

        }
        private void volver_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

    }
}
