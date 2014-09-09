using System;
using System.Collections.ObjectModel;
using MicroMvvm;
using WpfEncuestas.Common;
using WpfEncuestas.DataFake;

namespace WpfEncuestas.ViewModels
{
    public class PlantillaViewModel :  Item1ViewModelBase<PlantillaViewModel> //ObservableObject
    {
        #region ctor

        public PlantillaViewModel()
        {
            SeccionList = new SeccionListViewModel();
            SeccionPrueba = new SeccionViewModel();
            SeccionPrueba.Container = SeccionList;
        }

        #endregion

        #region Instance
        private static PlantillaViewModel _instance = null;

        public static PlantillaViewModel Instance()
        {
            return _instance ?? (_instance = PlantillaFake.CreateInstance());
        }
        #endregion

        #region Propiedades Modelo

        public ObservableCollection<VariableViewModel> Variables { get; set; }
        //public ObservableCollection<SeccionViewModel> Secciones { get; set; }

        private string _titulo;
        private string _mensaje;
        private string _idEvaluacion;
        private string _periodo;
        private Boolean _todosPeriodos;
        private SeccionListViewModel _seccionList;
        
        
        public string Titulo
        {
            get { return _titulo; }
            set
            {
                if(_titulo==value)return;
                _titulo = value;
                RaisePropertyChanged("Titulo");
            }
        }
        
        public string Mensaje
        {
            get { return _mensaje; }
            set
            {
                if(_titulo==value)return;
                _mensaje = value;
                RaisePropertyChanged("Mensaje");
            }
        }

        public string IdEvaluacion
        {
            get { return _idEvaluacion; }
            set
            {
                if(_idEvaluacion==value)return;
                _idEvaluacion = value;
                RaisePropertyChanged("IdEvaluacion");
            }
        }

        public string Periodo
        {
            get { return _periodo; }
            set
            {
                if(_periodo==value)return;
                _periodo = value;
                RaisePropertyChanged("Periodo");
            }
        }

        public Boolean TodosPeriodos
        {
            get { return _todosPeriodos; }
            set
            {
                if(_todosPeriodos==value)return;
                _todosPeriodos = value;
                RaisePropertyChanged("TodosPeriodos");
            }
        }

        public SeccionListViewModel SeccionList
        {
            get { return _seccionList; }
            set
            {
                if(_seccionList == value)return;
                _seccionList = value;
                RaisePropertyChanged("SeccionList");
            }
        }

        #endregion

        #region selected options
        private SeccionViewModel _selectedSeccionPregunta;

        public SeccionViewModel SelectedSeccionPregunta
        {
            get { return _selectedSeccionPregunta; }
            set
            {
                if(_selectedSeccionPregunta == value) return;
                _selectedSeccionPregunta = value;
                RaisePropertyChanged("SelectedSeccionPregunta");
            }
        }

        private SeccionViewModel _selectedSeccionOpcion;

        public SeccionViewModel SelectedSeccionOpcion
        {
            get { return _selectedSeccionOpcion; }
            set
            {
                if(_selectedSeccionOpcion == value) return;
                _selectedSeccionOpcion = value;
                RaisePropertyChanged("SelectedSeccionOpcion");
            }
        }

        private PreguntaViewModel _selectedPreguntaOpcion;

        public PreguntaViewModel SelectedPreguntaOpcion
        {
            get { return _selectedPreguntaOpcion; }
            set
            {
                if (_selectedPreguntaOpcion == value) return;
                _selectedPreguntaOpcion = value;
                RaisePropertyChanged("SelectedPreguntaOpcion");
            }
        }
        #endregion

        #region Commands

        protected override void Grabar()
        {
            Console.WriteLine("Grabando en BD");
            base.Grabar();
        }

        protected override void Cancelar()
        {
            Console.WriteLine("Cancelando....");
            base.Cancelar();
        }

        #endregion

        private SeccionViewModel _seccionPrueba;

        public SeccionViewModel SeccionPrueba
        {
            get { return _seccionPrueba; }
            set
            {
                if(_seccionPrueba == value) return;
                _seccionPrueba = value;
                RaisePropertyChanged(() => SeccionPrueba);
            }
        }

    }
}
