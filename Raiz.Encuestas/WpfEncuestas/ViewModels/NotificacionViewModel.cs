using System;
using System.Windows.Input;
using MicroMvvm;
using WpfEncuestas.Util;

namespace WpfEncuestas.ViewModels
{
    public class NotificacionViewModel : ObservableObject
    
    
    {


        string _codigoNotificacion;
        string _descripNotificacion;
        string _numerodiaNotificacion;
        Boolean _fechaNotificacion;// Antes de la fecha de fin de la notificación // Antes de la fecha de Inicio de la Notificación
        string _prioridadNotificacion;
        
        public ProcesoViewModel Proceso { get; set; }


      //  private ObservableCollection<NotificacionViewModel> _NotifiList;

        public NotificacionViewModel()
        {
            
          
        }



        public string CodigoNotificacion
        {
            get { return _codigoNotificacion; }
            set
            {
                if (_codigoNotificacion != value)
                {

                    _codigoNotificacion = value;
                    RaisePropertyChanged("CodigoNotificacion");
                }

            }
        }

        public string DescripcionNotificacion
        {
            get { return _descripNotificacion; }
            set
            {
                if (_descripNotificacion != value)
                {
                    _descripNotificacion = value;
                    RaisePropertyChanged("DescripcionNotificacion");

                }

            }
        }

        public string NumerodiaNotificacion
        {
            get { return _numerodiaNotificacion; }
            set
            {

                if (_numerodiaNotificacion != value)
                {
                    _numerodiaNotificacion = value;
                    RaisePropertyChanged("NumerodiaNotificacion");
                }

            }
        }

        public Boolean FechaNotificacion   
        {
            get { return _fechaNotificacion; }
            set
            {
                if (_fechaNotificacion != value)
                {
                    _fechaNotificacion = value;
                    RaisePropertyChanged("FechaNotificacion");
                }

            }
        }

        public string FechaNotificacionDescripcion
        {
            get { return FechaNotificacion ? "Antes del Inicio" : "Antes del Fin"; }
        }


        public string PrioridadNotificacion
        {
            get { return _prioridadNotificacion; }
            set
            {
                if (_prioridadNotificacion != value)
                {
                    _prioridadNotificacion = value;
                    RaisePropertyChanged("PrioridadNotificacion");
                }

            }
        }



        // Metodo Agregar 

        void AddContact_Execute()
        {

            #region example
            //Contacts.Add(new Contact() { Name = "Test1", Address = "X", Email = "X", PhoneNumber   = "X", Twitter = "X" });
            Proceso.ListaNotificaciones.Add(this);
            #endregion

       //     _NotifiList.Add(new NotificacionViewModel() { CodigoNotificacion = "CodigoNotificacion", DescripcionNotificacion = "DescripcionNotificacion", NumerodiaNotificacion = "NumerodiaNotificacion", FechaNotificacion = true, PrioridadNotificacion = "PrioridadNotificacion" });

        }


        bool AddContact_CanExecute()
        {
            return true;
        }


        public ICommand AddContact { get { return new RelayCommand(AddContact_Execute, AddContact_CanExecute); } }




        //Implementacion para enlazar ventana para agregar 

        public Mode Mode
        {
            get;
            set;
        }



    }



}
