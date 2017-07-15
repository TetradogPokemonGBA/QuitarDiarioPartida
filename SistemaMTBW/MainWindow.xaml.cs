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
			MessageBox.Show("Esta aplicación permite inGAME poder borrar una  MO como un ataque más \n\nDesarrollado por Pikachu240 investigado por JPAN", "Sobre la App");
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
						case AbreviacionCanon.BPE:imgDecoración.SetImage(BorrarMos.Imagenes.PokeballEsmeralda);break;
						case AbreviacionCanon.BPR: imgDecoración.SetImage(BorrarMos.Imagenes.PokeballRojoFuego); break;
						case AbreviacionCanon.BPG: imgDecoración.SetImage(BorrarMos.Imagenes.PokeballVerdeHoja); break;
						case AbreviacionCanon.AXV: imgDecoración.SetImage(BorrarMos.Imagenes.PokeballRuby); break;
						case AbreviacionCanon.AXP: imgDecoración.SetImage(BorrarMos.Imagenes.PokeballZafiro); break;
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
			if(PokemonGBAFrameWork.BorrarMos.EstaActivado(rom,edicion,compilacion))
			{
				btnPonerOQuitar.Content = "MOs IMBORRABLES";
			}
			else
			{
				btnPonerOQuitar.Content = "MOs Olvidables";
			}
		}

		private void btnPonerOQuitar_Click(object sender, RoutedEventArgs e)
		{
			if (PokemonGBAFrameWork.BorrarMos.EstaActivado(rom,edicion,compilacion))
			{
				PokemonGBAFrameWork.BorrarMos.Desactivar(rom, edicion,compilacion);
				
			}
			else
			{
				PokemonGBAFrameWork.BorrarMos.Activar(rom, edicion,compilacion);
				
			}
			PonTexto();
			rom.Save();
		}
	}
}
