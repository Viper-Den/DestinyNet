﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AutoMapper;

namespace DestinyNet
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Data _data;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _data = Load();
            MainWindow app = new MainWindow() { DataContext = new ManagerViewModel(_data) };
            app.Show();
        }
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            Save(_data);
        }

        public void Save(Data data)
        {
            var path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "//Configuration.json";
            var mapper = DestinyNetMapper.GetMapper();
            var s = JsonConvert.SerializeObject(mapper.Map<DataDTO>(data));
            File.WriteAllText(path, s);
        }
        public Data Load()
        {
            var path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "//Configuration.json";
            if (!File.Exists(path))
            {
                var d = new Data();
                File.WriteAllText(path, JsonConvert.SerializeObject(d, Formatting.Indented));
                return d;
            }
            else
            {
                var dataDTO = JsonConvert.DeserializeObject<DataDTO>(File.ReadAllText(path));
                var mapper = DestinyNetMapper.GetMapper();
                return mapper.Map<Data>(dataDTO);
            }
        }
    }
}
