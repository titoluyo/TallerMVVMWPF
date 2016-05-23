using System.Collections.ObjectModel;
using System.Windows.Input;
using MicroMvvm;
using WpfEncuestas.DataFake;
using WpfEncuestas.Interface;
using WpfEncuestas.Sevice;
using WpfEncuestas.Util;

namespace WpfEncuestas.ViewModels
{
     public class ProcesoViewModel : ObservableObject
    {


    #region Propiedades del Proceso
        string _nombreProceso;
        string _descripcionProceso;
        string _fechainiProceso;
        string _fechafinProceso;
        int _periodicidadProceso;


        public string NombreProceso
        {
            get { return _nombreProceso; }
            set
            {
                if (_nombreProceso != value)
                {
                    _nombreProceso = value;
                    RaisePropertyChanged("NombreProceso");
                }

            }
        }
        public string DescripcionProceso
        {
            get { return _descripcionProceso; }
            set
            {
                if (_descripcionProceso != value)
                {
                    _descripcionProceso = value;
                    RaisePropertyChanged("DescripcionProceso");
                }
            }
        }
        public string FechaIniProceso
        {
            get { return _fechainiProceso; }
            set
            {
                if (_fechainiProceso != value)
                {
                    _fechainiProceso = value;
                    RaisePropertyChanged("FechaIniProceso");
                }
            }
        }
        public string FechaFinProceso
        {
            get { return _fechafinProceso; }
            set
            {
                if (_fechafinProceso != value)
                {

                    _fechafinProceso = value;
                    RaisePropertyChanged("FechaFinProceso");
                }
            }
        }
        public int PeriodicidadProceso
        {
            get { return _periodicidadProceso; }
            set
            {
                if (_periodicidadProceso != value)
                {
                    _periodicidadProceso = value;
                    RaisePropertyChanged("PeriodicidadProceso");
                }
            }
        }

        #endregion

    #region Propiedades del Periodo
        
        string _fechainiPeriodo;
        string _horainiPeriodo;
        int _duraciondiasPeriodo;
        int _duracionhorasPeriodo;
        string _fechafinPeriodo;
        string _periodoActual;
        int _numeroNotificacionesxPerioodo;

        public string FechaIniPeriodo
        {
            get { return _fechainiPeriodo; }
            set
            {
                if (_fechainiPeriodo != value)
                {
                    _fechainiPeriodo = value;
                    RaisePropertyChanged("FechaIniPeriodo");
                }

            }
        }

        public string HoraIniPeriodo
        {
            get { return _horainiPeriodo; }
            set
            {
                if (_horainiPeriodo != value)
                {
                    _horainiPeriodo = value;
                    RaisePropertyChanged("HoraIniPeriodo");
                }

            }
        }

        public int DuraciondiasPeriodo
        {
            get { return _duraciondiasPeriodo; }
            set
            {
                if (_duraciondiasPeriodo != value)
                {
                    _duraciondiasPeriodo = value;
                    RaisePropertyChanged("DuraciondiasPeriodo");
                }

            }
        }

        public int DuracionhorasPeriodo
        {
            get { return _duracionhorasPeriodo; }
            set
            {
                if (_duracionhorasPeriodo != value)
                {

                    _duracionhorasPeriodo = value;
                    RaisePropertyChanged("DuracionhorasPeriodo");

                }

            }
        }

        public string FechaFinPeriodo
        {
            get { return _fechafinPeriodo; }
            set
            {
                if (_fechafinPeriodo != value)
                {
                    _fechafinPeriodo = value;
                    RaisePropertyChanged("FechaFinPeriodo");
                }


            }
        }

        public string PeriodoActual
        {
            get { return _periodoActual; }
            set
            {
                if (_periodoActual != value)
                {
                    _periodoActual = value;
                    RaisePropertyChanged("PeriodoActual");
                }

            }
        }



        public int NumeroNotificacionesPeriodo
        {
            get { return _numeroNotificacionesxPerioodo; }
            set
            {
                if (_numeroNotificacionesxPerioodo != value)
                {
                    _numeroNotificacionesxPerioodo = value;
                    RaisePropertyChanged("NumeroNotificacionesPeriodo");
                }

            }
        }

        #endregion


    
          public ProcesoViewModel()
          {

              PeriodosList = new PeriodoListaViewModel();
              FlujoList = new FlujoEvaluacionListViewModel();
              ZonaList = new ZonaListViewModel();

          }

         //=============================================
         //Get the Items of PeriodosList
         private PeriodoListaViewModel _periodosList;

         public PeriodoListaViewModel PeriodosList
         {
             get { return _periodosList; }
             set
             {
                 if (_periodosList == value) return;
                 _periodosList = value;
                 RaisePropertyChanged("PeriodosList");
             }
         }

         //=============================================




         //=============================================
         //Get the Items of RolesList

         private FlujoEvaluacionListViewModel _flujoList;

         public FlujoEvaluacionListViewModel FlujoList
         {
             get { return _flujoList; }
             set
             {
                 if (_flujoList == value) return;
                 _flujoList = value;
                 RaisePropertyChanged("FlujoList");
             }
         }

         //=============================================


         //=============================================
         //Get the Items of ZonaList

         private ZonaListViewModel _zonaList;

         public ZonaListViewModel ZonaList
         {
             get { return _zonaList; }
             set
             {
                 if(_zonaList == value)return;
                 _zonaList = value;
                 RaisePropertyChanged("ZonaList");
             }
         }


         //=============================================






         private static ProcesoViewModel _instance = null;

         public static ProcesoViewModel Instance()
         {
             return _instance ?? (_instance = ProcesoFake.CreateInstance());
         }




       //  ObservableCollection<NotificacionViewModel> _notificaciones = new ObservableCollection<NotificacionViewModel>();

         public ObservableCollection<NotificacionViewModel> ListaNotificaciones { get; set; }

         private ICommand showAddCommand;



         #region selected options

         private PeriodosViewModel _selectedPeriodoProceso;

         public PeriodosViewModel SelectedPeriodoProceso
         {
             get { return _selectedPeriodoProceso; }
             set
             {
                 if(_selectedPeriodoProceso == value) return;
                 _selectedPeriodoProceso = value;
                 RaisePropertyChanged("SelectedPeriodoProceso");
             }
         }



         #endregion




         public ICommand ShowAddCommand
         {
             get
             {
                 if (showAddCommand == null)
                 {
                     showAddCommand = new CommandBase(i => this.ShowAddDialog(), null);
                 }

                 return showAddCommand;

             }
         }

        

         private void ShowAddDialog()
         {
             NotificacionViewModel notificacion = new NotificacionViewModel();
             notificacion.Mode = Mode.Add;
             notificacion.Proceso = this;

             IModalDialog dialog = ServiceProvider.Instance.Get<IModalDialog>();
             dialog.BindViewModel(notificacion);
             dialog.ShowDialog();
         }


        






        #region CodeTest
        
        //public Persona2 Model;

    //    public string Nombre
    //    {

    //        get { return Model.Nombre; }
    //        set { Model.Nombre = value; NotifyChange("Nombre" , "NombreCompleto"); }
    //    }


    //    public string Apellidp
    //    {
            
    //        get { return Model.Apellido; }
    //        set { Model.Apellido = value; NotifyChange("Apellido", "NombreCompleto"); }
    //    }

    //    public string NombreCompleto
    //    {
    //        get { return Model.Nombre + " " + Model.Apellido; } 
    //    }



    // void NotifyChange(params string[] ids)
    //{
    //    if (PropertyChanged !=null)
    //        foreach (var id in ids)
    //            PropertyChanged(this, new PropertyChangedEventArgs(id));

    //}

    // #region INotifyPropertyChanged Members


    // public event PropertyChangedEventHandler PropertyChanged;

        // #endregion


        #endregion
    }
}
