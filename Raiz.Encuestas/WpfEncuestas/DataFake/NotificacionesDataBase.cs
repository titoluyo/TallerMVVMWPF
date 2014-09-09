using System;
using System.Collections.Generic;
using WpfEncuestas.ViewModels;

namespace WpfEncuestas.DataFake
{
    public class NotificacionesDataBase
    {

        Random _random = new Random();
        string[] _notificacion1 = {"Notificacion01", "Para evento mensual", "4", "x", "a", "urgente"};

        private IList<NotificacionViewModel> _NotifiList;


        public NotificacionesDataBase()
        {

            _NotifiList = new List<NotificacionViewModel>
            {

                new NotificacionViewModel
                {
                    CodigoNotificacion = "Notificacion01",
                    DescripcionNotificacion = "Eensual",
                    NumerodiaNotificacion = "4",
                   // FechaIniNotificacion = "x",
                   // FechaFinNotificacion = " ",
                    PrioridadNotificacion = "Urgente"
                }


            };


        }

        public IList<NotificacionViewModel> Notificaciones
        {

            get { return _NotifiList; }
            set { _NotifiList = value; }
        }





        public string GetNotificaciones
        {


            get
            {return _notificacion1[0];}
           
        }





       
        }

       


    }

