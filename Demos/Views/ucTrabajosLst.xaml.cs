﻿using System;
using System.Collections.Generic;
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

namespace Demos.Views {
    /// <summary>
    /// Lógica de interacción para ucTrabajosLst.xaml
    /// </summary>
    public partial class ucTrabajosLst : UserControl {
        public ucTrabajosLst() {
            InitializeComponent();
        }


        // Using a DependencyProperty as the backing store for Titulo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TituloProperty =
            DependencyProperty.Register("Titulo", typeof(string), typeof(ucTrabajosLst),
                new PropertyMetadata("Sin titulo"));


        public string Titulo {
            get { return (string)GetValue(TituloProperty); }
            set { SetValue(TituloProperty, value); }
        }

        // public string Titulo { get; set; }

        public static readonly RoutedEvent CargadoEvent = EventManager.RegisterRoutedEvent(
            "Cargado", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ucTrabajosLst));

        public event RoutedEventHandler Cargado {
            add { AddHandler(CargadoEvent, value); }
            remove { RemoveHandler(CargadoEvent, value); }
        }

    }
}
