﻿using System;
using System.Collections.Generic;
using System.Text;
using Hackathon24.Message;
using System.Linq;

namespace Hackathon24.Message
{
    public class SmogDetails
    {

        //Informacje o dacie i godzinie
        public string date { get; set; }
        public string time { get; set; }

        //Stan smogu i zwracana wiadomość
        public string state { get; set; }
        public string message { get; set; }
        public string image { get; set;  }

        //Wartości liczbowe i procentowe
        private double? _p25Value { get; set; }
        private int _p25Proc { get; set; }
        private double? _p10Value { get; set; }
        private int _p10Proc { get; set; }
        private double? _so2Value { get; set; }
        private int _so2Proc { get; set; }

        //Teksty i kolory
        public string p25Text { get; set; }
        public string p10Text { get; set; }
        public string so2Text { get; set; }
        public string p25Color { get; set; }
        public string p10Color { get; set; }
        public string so2Color { get; set; }

        public SmogDetails()
        {
            date = "09.09.2017";
            time = "14:00";
        }

        private string PickColor(int proc)
        {
            if (proc >= 200)
            {
                return "Pink";
            }
            else if (proc >= 100)
            {
                return "Purple";
            }
            else if (proc >= 50)
            {
                return "Red";
            }
            else if (proc >= 30)
            {
                return "Orange";
            }
            else if (proc >= 15)
            {
                return "Green";
            }
            else
            {
                return "DarkGreen";
            }
        }

        public void Update()
        {
            _p10Value = Connection.GetP10Value();
            _p25Value = Connection.GetP25Value();
            _so2Value = Connection.GetSO2Value();
            _p10Proc = (int)(_p10Value % 50);
            _p25Proc = (int)(_p25Value % 25);
            _so2Proc = (int)(_so2Value % 350);

            int max = (_p25Proc >= _p10Proc ? _p25Proc : _p10Proc);
            max = (max > _so2Proc ? max : _so2Proc);

            if (max >= 200)
            {
                state = "Masakra";
                message = "Dla własnego bezpieczeństwa nie oddychaj.";
                image = "images\\nobreath.png";
            }
            else if (max >= 100)
            {
                state = "Bardzo zły";
                message = "Dla własnego bezpieczeństwa zamknij okno.";
                image = "images\\window.png";
            }
            else if (max >= 50)
            {
                state = "Zły";
                message = "Dla własnego bezpieczeństwa zostań przed kompem.";
                image = "images\\computer.png";
            }
            else if(max >= 30)
            {
                state = "Sredni";
                message = "Lepiej zostać w domu, polecamy kawę i ciastko.";
                image = "images\\cake.png";
            }
            else if (max >= 15)
            {
                state = "Dobry";
                message = "Idź na spacer.";
                image = "images\\stroll.png";
            }
            else
            {
                state = "Bardo dobry";
                message = "Bądź fit, idź biegać.";
                image = "images\\run.png";
            }

            p25Color = PickColor(_p25Proc);
            p10Color = PickColor(_p10Proc);
            so2Color = PickColor(_so2Proc);

            p25Text = "PM 2.5%" + Environment.NewLine + _p25Proc.ToString() + "%";
            p10Text = "PM 10%" + Environment.NewLine + _p10Proc.ToString() + "%";
            so2Text = "SO\u2082" + Environment.NewLine + _so2Proc.ToString() + "%";
        }

        
         
    }
}