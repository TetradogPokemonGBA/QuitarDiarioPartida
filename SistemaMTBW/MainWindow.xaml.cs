using Gabriel.Cat;
using Gabriel.Cat.Extension;
using Microsoft.Win32;
using PokemonGBAFrameWork;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MOSinMedallas
{
	/// <summary>
	/// Lógica de interacción para MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		RomGba rom;
		EdicionPokemon edicion;
		Compilacion compilacion;
		public MainWindow()
		{
			InitializeComponent();
		}

		private void MenuItem_Click(object sender, RoutedEventArgs e)
		{
			if(MessageBox.Show("Esta aplicación permite quitar el diario de la partida \n\nDesarrollado por Pikachu240 investigado por Darthatron\nQuieres ver el codigo fuente?", "Sobre la App",MessageBoxButton.YesNo,MessageBoxImage.Information)==MessageBoxResult.Yes)
				System.Diagnostics.Process.Start("https://github.com/TetradogPokemonGBA/QuitarDiarioPartida");
		}

		private void MenuItem_Click_1(object sender, RoutedEventArgs e)
		{
			OpenFileDialog opnRom = new OpenFileDialog();
			opnRom.Filter = "Rom Pokemon GBA|*.gba";
			
			if (opnRom.ShowDialog().GetValueOrDefault())
			{
				rom = new RomGba(opnRom.FileName);
				edicion=EdicionPokemon.GetEdicionPokemon(rom);
				compilacion=Compilacion.GetCompilacion(rom,edicion);
				PonTexto();
				btnPonerOQuitar.IsEnabled = true;
				switch(edicion.AbreviacionRom)
				{

						case AbreviacionCanon.BPR: imgDecoración.SetImage(BorrarMos.Imagenes.PokeballRojoFuego); break;
						case AbreviacionCanon.BPG: imgDecoración.SetImage(BorrarMos.Imagenes.PokeballVerdeHoja); break;
						default:MessageBox.Show("No hay diario en esta edición..."); btnPonerOQuitar.IsEnabled=false;break;
				}
			}
			else if(rom!=null)
			{
				MessageBox.Show("No se ha cambiado la rom","Atención",MessageBoxButton.OK,MessageBoxImage.Exclamation);
			}else
			{
				MessageBox.Show("No se ha cargado nada...");
			}
			
		}

		private void PonTexto()
		{
			if(PokemonGBAFrameWork.QuitarDiarioPartida.EstaActivado(rom,edicion,compilacion))
			{
				btnPonerOQuitar.Content = "Poner diario partida";
			}
			else
			{
				btnPonerOQuitar.Content = "Quitar diario partida";
			}
		}

		private void btnPonerOQuitar_Click(object sender, RoutedEventArgs e)
		{
			if (PokemonGBAFrameWork.QuitarDiarioPartida.EstaActivado(rom,edicion,compilacion))
			{
				PokemonGBAFrameWork.QuitarDiarioPartida.Desactivar(rom, edicion,compilacion);
				
			}
			else
			{
				PokemonGBAFrameWork.QuitarDiarioPartida.Activar(rom, edicion,compilacion);
				
			}
			PonTexto();
			rom.Save();
		}
	}
}
