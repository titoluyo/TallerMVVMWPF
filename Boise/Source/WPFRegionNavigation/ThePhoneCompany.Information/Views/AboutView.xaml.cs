using System;
using System.ComponentModel.Composition;
using System.Timers;
using System.Windows.Controls;

namespace ThePhoneCompany.Information.Views {

    [Export(typeof(AboutView))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class AboutView : UserControl {

        Int32 _counter;
        readonly Timer _timer;

        public AboutView() {
            InitializeComponent();
            _timer = new Timer(1000);

            _timer.Elapsed += (s, e) => this.Dispatcher.BeginInvoke(
                (Action)delegate {
                _counter += 1;
                this.tbElapsedTime.Text = _counter.ToString();
            });
            _timer.Start();
        }
    }
}
