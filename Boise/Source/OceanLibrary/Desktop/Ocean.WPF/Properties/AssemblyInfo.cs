using System;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Markup;

[assembly: AssemblyTitle("Ocean.Wpf")]
[assembly: AssemblyDescription("Cross-platform library.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Karl Shifflett")]
[assembly: AssemblyProduct("Ocean - Code Name: Dolphin")]
[assembly: AssemblyCopyright("Copyright (c) 2008, 2009, 2010, 2011 Karl Shifflett.  All rights reserved.")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyVersion("3.0.0.0")]
[assembly: AssemblyFileVersion("3.0.0.0")]
[assembly: CLSCompliant(true)]
[assembly: ComVisible(false)]
[assembly: AssemblyCulture("")]
[assembly: NeutralResourcesLanguage("en-US")]

[assembly: XmlnsDefinition("http://karlshifflett.wordpress.com/ocean", "Ocean.Wpf.Behaviors")]
[assembly: XmlnsDefinition("http://karlshifflett.wordpress.com/ocean", "Ocean.Wpf.CommonDialog")]
[assembly: XmlnsDefinition("http://karlshifflett.wordpress.com/ocean", "Ocean.Wpf.Controls")]
[assembly: XmlnsDefinition("http://karlshifflett.wordpress.com/ocean", "Ocean.Wpf.Converters")]
[assembly: XmlnsDefinition("http://karlshifflett.wordpress.com/ocean", "Ocean.Wpf.Effects")]
[assembly: XmlnsPrefix("http://karlshifflett.wordpress.com/ocean", "ocean:")]

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
    //(used if a resource is not found in the page, 
    // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
    //(used if a resource is not found in the page, 
    // app, or any theme specific resource dictionaries)
)]

