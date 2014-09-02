//-----------------------------------------------------------------------
// <copyright file="GreyscaleEffect.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// <summary>
//  Greyscale effect.
// </summary>
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Ocean.Wpf.Effects {

    /// <summary>
    /// Greyscale effect.
    /// </summary>
    public class GreyscaleEffect : ShaderEffect {

        /// <summary>
        /// Dependency property for Input.
        /// </summary>
        public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty("Input", typeof(GreyscaleEffect), 0 /* assigned to sampler register S0 */);

        /// <summary>
        /// PixelShader for this effect.
        /// </summary>
        static readonly PixelShader _PixelShader;

        /// <summary>
        /// Static constructor - Create a PixelShader for all GreyscaleEffect instances. 
        /// </summary>
        static GreyscaleEffect() {
            _PixelShader = new PixelShader { UriSource = new Uri("pack://application:,,,/Ocean.WPF;component/Effects/Bytecode/Greyscale.ps") };
            _PixelShader.Freeze();
        }

        /// <summary>
        /// Constructor - Assign the PixelShader property and set the shader parameters to default values.
        /// </summary>
        public GreyscaleEffect() {
            this.PixelShader = _PixelShader;
            UpdateShaderValue(InputProperty);
        }

        /// <summary>
        /// Gets or sets Input properties. 
        /// </summary>
        public Brush Input {
            get { return (Brush)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }
    }
}